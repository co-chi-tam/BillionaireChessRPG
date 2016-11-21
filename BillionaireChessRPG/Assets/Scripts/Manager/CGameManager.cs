using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using FSM;

namespace BillianaireChessRPG {
	public class CGameManager : CMonoSingleton<CGameManager>, IContext {

		#region Properties

		[SerializeField]	private string m_StateName;
		[SerializeField]	public CEnum.EGameState GameState = CEnum.EGameState.None;
		[SerializeField]	private CMapManager m_MapManager;
		[SerializeField]	private CUIManager m_UIManager;
		[SerializeField]	private CUserManager m_UserManager;
		[SerializeField]	private CameraController m_CameraController;
		[SerializeField]	private int m_TurnCount = 0;

		// Object base turn
		[SerializeField]	public CObjectController objectSelected;

		// Register List
		[SerializeField]	public List<CObjectController> registerObjects;

		protected FSMManager m_FSMManager;
		private SequenceList<CObjectController> m_SequenceList;
		private float m_WaitingTime = 0f;
		private float m_WaitingTimeInterval = 1f;

		#endregion

		#region Implementation Monobehaviour

		protected override void Awake ()
		{
			base.Awake ();
			m_SequenceList = new SequenceList<CObjectController> ();
			registerObjects = new List<CObjectController> ();
		}

		protected override void Start ()
		{
			base.Start ();
			m_MapManager = CMapManager.GetInstance ();
			m_UIManager = CUIManager.GetInstance ();
			m_UserManager = CUserManager.GetInstance ();
			m_CameraController = CameraController.GetInstance ();

			OnLoadMap ();
			OnRegisterFSM ();
		}

		protected override void UpdateBaseTime (float dt)
		{
			base.UpdateBaseTime (dt);
			m_FSMManager.UpdateState (dt);
			m_StateName = m_FSMManager.currentStateName;
		}

		protected virtual void OnRegisterFSM() {
			var jsonText 		= Resources.Load<TextAsset>(m_MapManager.mapData.gameFSMPath);
			var gameIdle		= new FSMGameIdleState (this);
			var gameLoading 	= new FSMGameLoadingState (this);
			var gameStart 		= new FSMGameStartState (this);
			var gameProcessing 	= new FSMGameProcessingState (this);
			var startTurn 		= new FSMGameStartTurnState (this);
			var updateTurn 		= new FSMGameUpdateTurnState (this);
			var endTurn			= new FSMGameEndTurnState (this);
			var gameEnd 		= new FSMGameEndState (this);
			var winGame 		= new FSMWinGameState (this);
			var closeGame 		= new FSMCloseGameState (this);

			m_FSMManager = new FSMManager ();
			m_FSMManager.RegisterState ("GameIdleState", gameIdle);
			m_FSMManager.RegisterState ("GameLoadingState", gameLoading);
			m_FSMManager.RegisterState ("GameStartState", gameStart);
			m_FSMManager.RegisterState ("GameProcessingState", gameProcessing);
			m_FSMManager.RegisterState ("StartTurnState", startTurn);
			m_FSMManager.RegisterState ("UpdateTurnState", updateTurn);
			m_FSMManager.RegisterState ("EndTurnState", endTurn);
			m_FSMManager.RegisterState ("GameEndState", gameEnd);
			m_FSMManager.RegisterState ("WinGameState", winGame);
			m_FSMManager.RegisterState ("CloseGameState", closeGame);

			m_FSMManager.RegisterCondition ("IsLoading", IsLoading);
			m_FSMManager.RegisterCondition ("IsLoadingComplete", IsLoadingComplete);
			m_FSMManager.RegisterCondition ("IsProcessing", IsProcessing);
			m_FSMManager.RegisterCondition ("IsStartTurn", IsStartTurn);
			m_FSMManager.RegisterCondition ("IsUpdateTurn", IsUpdateTurn);
			m_FSMManager.RegisterCondition ("IsEndTurn", IsEndTurn);
			m_FSMManager.RegisterCondition ("IsEndGame", IsEndGame);
			m_FSMManager.RegisterCondition ("IsAllCompleteBlock", IsAllCompleteBlock);
			m_FSMManager.RegisterCondition ("IsOnceCompleteBlock", IsOnceCompleteBlock);
			m_FSMManager.RegisterCondition ("IsAllDeath", IsAllDeath);
			m_FSMManager.RegisterCondition ("IsOnceDeath", IsOnceDeath);
			m_FSMManager.RegisterCondition ("DidDeactiveTargetObject", DidDeactiveTargetObject);
			m_FSMManager.RegisterCondition ("Did5Turn", Did5Turn);
			m_FSMManager.RegisterCondition ("Did10Turn", Did10Turn);
			m_FSMManager.RegisterCondition ("Did20Turn", Did20Turn);
			m_FSMManager.RegisterCondition ("Did30Turn", Did30Turn);
			m_FSMManager.RegisterCondition ("Did40Turn", Did40Turn);
			m_FSMManager.RegisterCondition ("Did50Turn", Did50Turn);

			m_FSMManager.LoadFSM (jsonText.text);
		}

		protected virtual void OnLoadMap() {
			m_MapManager.mapData = TinyJSON.JSON.Load (m_UserManager.mapSelectedText.text).Make<CMapData> ();
			m_MapManager.LoadMapObject ();
			m_MapManager.LoadMapData ();
		}

		protected virtual void OnLoadHero() {
			m_UserManager.OnLoadHero += (hero) => {
				var firstBlock = m_MapManager.GetBlock(0, 0);
				hero.SetCurrentBlock (firstBlock);
				hero.SetTargetBlock (firstBlock);
				hero.SetActive (true);
				registerObjects.Add(hero);
			};
			m_UserManager.LoadHeroData ();
		}

		#endregion

		#region Main methods

		public virtual void OnObjectSelectRollDice() {
			var currentBlock = objectSelected.GetCurrentBlock () as CBlockController;
			var randomStep = UnityEngine.Random.Range(1, m_MapManager.mapData.limitStep + 1);
			var nextBlock = m_MapManager.GetBlockStep (0, currentBlock, randomStep);
			m_UIManager.SetAnimation (CEnum.EUIState.RollDice);
			m_UIManager.OnRollDiceComplete += () => {
				objectSelected.SetTargetBlock (nextBlock);
				objectSelected.SetRollDice (true);
			};
		}

		public virtual void OnObjectSelectInAttack() {
			if (registerObjects.Contains (objectSelected)) {
				m_UIManager.SetAnimation (CEnum.EUIState.Battle);
			}
		}

		public virtual void OnObjectSelectAttackSkill(int index) {
			objectSelected.SetActiveSkill (index);
		}

		public virtual void OnObjectEnterBlock(CObjectController guest, CObjectController block) {
			var currentBlock = block as CBlockController;
			currentBlock.AddGuest (guest);
		}

		public virtual void OnObjectLeaveBlock(CObjectController guest, CObjectController block) {
			var currentBlock = block as CBlockController;
			currentBlock.RemoveGuest (guest);
		}

		public virtual void OnObjectStartTurn() {
			var turn = m_TurnCount + 1;
			m_TurnCount = turn;
			OnTurnChange (m_TurnCount);
			objectSelected = m_SequenceList.Peek ();
			if (objectSelected == null)
				return;
			objectSelected.OnEndTurn -= OnObjectEndTurn;
			objectSelected.OnEndTurn += OnObjectEndTurn;
			objectSelected.SetTurnState (CEnum.ETurnState.StartTurn);
			m_UIManager.SetAnimation (CEnum.EUIState.Idle);
			m_CameraController.target = objectSelected.transform;
			m_UIManager.target = objectSelected;
		}

		public virtual void OnObjectEndTurn() {
			objectSelected.OnEndTurn -= OnObjectEndTurn;
			objectSelected.SetTurnState (CEnum.ETurnState.EndTurn);
			m_WaitingTime = m_WaitingTimeInterval;
			m_SequenceList.Dequeue ();
			GameState = CEnum.EGameState.EndTurn;
		}

		public virtual void OnTurnChange(int turn) {
			
		}

		public virtual void OnLoadingGame() {
			OnLoadHero ();
		}
			
		public virtual void OnStartGame() {
			m_UIManager.SetAnimation (CEnum.EUIState.Idle);
			GameState = CEnum.EGameState.StartGame;
			for (int i = 0; i < registerObjects.Count; i++) {
				var player = registerObjects [i];
				player.SetActive (true);
				RegisterObject (player);
			}
		}

		public virtual void OnWinGame() {
			m_UIManager.SetAnimation (CEnum.EUIState.EndGame);
			GameState = CEnum.EGameState.EndGame;
		}

		public virtual void OnCloseGame() {
			m_UIManager.SetAnimation (CEnum.EUIState.CloseGame);
			GameState = CEnum.EGameState.EndGame;
		}

		public virtual void OnReloadGame() {
			SceneManager.LoadScene ("MainScene");
		}

		public virtual void OnUserRewardGold(int value) {
			m_UserManager.user.gold += value;
		}

		public void RegisterObject(CObjectController value) {
			if (value == null || value.GetActive() == false)
				return;
			if (m_SequenceList.Contain (value))
				return;
			m_SequenceList.Enqueue (value);
			m_UIManager.RegisterUIAvatar (value);
		}

		public void UnRegisterObject(CObjectController value) {
			if (value == null)
				return;
			if (m_SequenceList.Contain (value) == false)
				return;
			m_SequenceList.Remove (value);
			value.ResetBaseGame ();
			m_UIManager.UnRegisterUIAvatar (value);
		}

		#endregion

		#region FSM

		internal virtual bool IsLoading() {
			return GameState == CEnum.EGameState.Loading;
		}

		internal virtual bool IsLoadingComplete() {
			return GameState == CEnum.EGameState.StartGame
				&& m_MapManager.mapData != null
				&& m_MapManager.mapBlockControllers.Count > 0
				&& m_UserManager.user != null;
		}

		internal virtual bool IsProcessing() {
			return GameState == CEnum.EGameState.Processing;
		}

		internal virtual bool IsStartTurn() {
			m_WaitingTime -= Time.deltaTime;
			return GameState == CEnum.EGameState.StartTurn && m_WaitingTime <= 0f;
		}

		internal virtual bool IsUpdateTurn() {
			return GameState == CEnum.EGameState.UpdateTurn;
		}

		internal virtual bool IsEndTurn() {
			return GameState == CEnum.EGameState.EndTurn;
		}

		internal virtual bool IsEndGame() {
			return GameState == CEnum.EGameState.EndGame;
		}

		internal virtual bool IsAllCompleteBlock() {
			if (m_SequenceList.Count == 0)
				return false;
			var winGame = true;
			var oneAlive = false;
			for (int i = 0; i < registerObjects.Count; i++) {
				var player = registerObjects [i]; 
				var targetBlock = m_MapManager.GetTargetBlock (player.GetMapIndex());
				var currentBlock = player.GetCurrentBlock ();
				if (currentBlock != null && targetBlock != null && player.GetActive()) {
					if (currentBlock != targetBlock) {
						winGame = false;
					} else {
						winGame &= true;
					}
					oneAlive = true;
				}
			}
			return winGame && oneAlive;
		}

		internal virtual bool IsOnceCompleteBlock() {
			if (m_SequenceList.Count == 0)
				return false;
			var winGame = false;
			var oneAlive = false;
			for (int i = 0; i < registerObjects.Count; i++) {
				var player = registerObjects [i]; 
				var targetBlock = m_MapManager.GetTargetBlock (player.GetMapIndex());
				var currentBlock = player.GetCurrentBlock ();
				if (currentBlock != null && targetBlock != null && player.GetActive()) {
					if (currentBlock == targetBlock) {
						winGame = true;
					}
					oneAlive = true;
				}
			}
			return winGame && oneAlive;
		}

		internal virtual bool IsAllDeath() {
			if (m_SequenceList.Count == 0)
				return true;
			for (int i = 0; i < registerObjects.Count; i++) {
				var player = registerObjects [i]; 
				if (player.GetActive()) {
					return false;
				}
			}
			return true;
		}

		internal virtual bool IsOnceDeath() {
			if (m_SequenceList.Count == 0)
				return true;
			for (int i = 0; i < registerObjects.Count; i++) {
				var player = registerObjects [i]; 
				if (player.GetActive() == false) {
					return true;
				}
			}
			return false;
		}

		internal virtual bool DidDeactiveTargetObject() {
			var allSkill = true;
			var mapBlockController = m_MapManager.mapBlockControllers [0];
			for (int i = 0; i < mapBlockController.targetObjects.Count; i++) {
				var targetObject = mapBlockController.targetObjects [i];
				if (targetObject != null && targetObject.GetActive () == false) {
					allSkill &= true; 
				} else {
					allSkill = false;
				}
			}
			return allSkill && mapBlockController.targetObjects.Count > 0;
		}

		internal virtual bool Did5Turn() {
			return m_TurnCount == 5;
		}

		internal virtual bool Did10Turn() {
			return m_TurnCount == 10;
		}

		internal virtual bool Did20Turn() {
			return m_TurnCount == 20;
		}

		internal virtual bool Did30Turn() {
			return m_TurnCount == 20;
		}

		internal virtual bool Did40Turn() {
			return m_TurnCount == 20;
		}

		internal virtual bool Did50Turn() {
			return m_TurnCount == 20;
		}

		#endregion

	}
}
