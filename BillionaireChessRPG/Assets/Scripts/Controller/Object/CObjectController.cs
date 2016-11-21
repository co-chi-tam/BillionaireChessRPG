using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using FSM;

namespace BillianaireChessRPG {
	[RequireComponent(typeof(CapsuleCollider))]
	public class CObjectController : CBaseController, IContext, IMovable, IBattlable, IStatus {

		#region Properties

		public Action OnStartTurn;
		public Action<float> OnUpdateTurn;
		public Action OnEndTurn;

		[SerializeField]	protected CapsuleCollider m_CapsuleCollider;
		[SerializeField]	protected CObjectController m_CurrentBlock;
		[SerializeField]	protected CObjectController m_TargetBlock;

		[SerializeField]	protected CEnum.ETeam m_CurrentTeam = CEnum.ETeam.None;
		protected CEnum.ETurnState m_CurrentStateTurn = CEnum.ETurnState.None;

		protected CGameManager m_GameManager;
		protected CMapManager m_MapManager;
		protected FSMManager m_FSMManager;
		protected bool m_DidRollDice = false;

		#endregion

		#region Implementation Monobehaviour

		protected override void Init ()
		{
			base.Init ();
		}

		protected override void Awake ()
		{
			base.Awake ();
			m_FSMManager 		= new FSMManager ();
		}

		protected override void Start ()
		{
			base.Start ();
			m_GameManager = CGameManager.GetInstance ();
			m_MapManager = CMapManager.GetInstance ();
			OnRegisterComponent ();
			OnRegisterFSM ();
			OnRegisterAnimation ();
		}

		protected virtual void OnRegisterComponent() {
			
		}

		protected virtual void OnRegisterFSM() {
			var startTurnState 	= new FSMStartTurnState (this);
			var endTurnState 	= new FSMEndTurnState (this);
			var leaveTurnState 	= new FSMLeaveTurnState (this);
			var joinTurnState 	= new FSMJoinTurnState (this);
			var rollDiceState 	= new FSMRollDiceState (this);
			var enterBlockState = new FSMEnterBlockState (this);
			var waitingState 	= new FSMWaitingState (this);

			m_FSMManager.RegisterState ("StartTurnState", 	startTurnState);
			m_FSMManager.RegisterState ("EndTurnState", 	endTurnState);
			m_FSMManager.RegisterState ("LeaveTurnState", 	leaveTurnState);
			m_FSMManager.RegisterState ("JoinTurnState", 	joinTurnState);
			m_FSMManager.RegisterState ("RollDiceState", 	rollDiceState);
			m_FSMManager.RegisterState ("EnterBlockState", 	enterBlockState);
			m_FSMManager.RegisterState ("WaitingState", 	waitingState);

			m_FSMManager.RegisterCondition ("IsStartTurn", 	IsStartTurn);
			m_FSMManager.RegisterCondition ("IsEndTurn", 	IsEndTurn);
			m_FSMManager.RegisterCondition ("IsDeath", 		IsDeath);
			m_FSMManager.RegisterCondition ("DidMoveToTargetBlock", DidMoveToTargetBlock);
			m_FSMManager.RegisterCondition ("IsActive",		GetActive);
			m_FSMManager.RegisterCondition ("DidRollDice", 	GetRollDice);
			m_FSMManager.RegisterCondition ("CanRollDice", 	CanRollDice);
			m_FSMManager.RegisterCondition ("Did5Turn", m_GameManager.Did5Turn);
			m_FSMManager.RegisterCondition ("Did10Turn", m_GameManager.Did10Turn);
			m_FSMManager.RegisterCondition ("Did20Turn", m_GameManager.Did20Turn);
			m_FSMManager.RegisterCondition ("Did30Turn", m_GameManager.Did30Turn);
			m_FSMManager.RegisterCondition ("Did40Turn", m_GameManager.Did40Turn);
			m_FSMManager.RegisterCondition ("Did50Turn", m_GameManager.Did50Turn);
		}

		protected virtual void OnRegisterAnimation() {
			
		}

		#endregion

		#region Main methods

		public virtual void ResetBaseStartTurn() {
			m_DidRollDice = false;
		}

		public virtual void ResetBaseEndTurn() {
			
		}

		public virtual void ResetBaseGame() {
			this.OnEndTurn = null;
			this.OnStartTurn = null;
			this.OnUpdateTurn = null;
		}

		public virtual void ApplyDamage(int damage, CEnum.EAttackType damageType) {

		}

		public virtual void ApplyBuff(int buff, CEnum.EStatusType statusType) {

		}

		public virtual void MoveToTargetBlock(float dt) {
			
		}

		public virtual void MoveToTarget(Vector3 target, float dt) {
			
		}

		public virtual void LookAtTarget(Vector3 target) {

		}

		#endregion

		#region FSM

		internal virtual bool CanRollDice() {
			return false;
		}

		internal virtual bool IsStartTurn() {
			return m_CurrentStateTurn == CEnum.ETurnState.StartTurn;
		}

		internal virtual bool IsEndTurn() {
			return m_CurrentStateTurn == CEnum.ETurnState.EndTurn;
		}

		internal virtual bool IsDeath() {
			return false;
		}

		internal virtual bool DidMoveToTargetBlock() {
			return m_CurrentBlock == m_MapManager.GetTargetBlock(0);
		}

		#endregion

		#region Getter && Setter

		public virtual void SetData(CObjectData value) {
			
		}

		public virtual CObjectData GetData() {
			return null;
		}

		public virtual string GetFSMStateName() {
			return string.Empty;
		}

		public virtual string GetFSMName() {
			return string.Empty;
		}

		public virtual void SetTeam(CEnum.ETeam value) {
			m_CurrentTeam = value;
		}

		public virtual CEnum.ETeam GetTeam() {
			return m_CurrentTeam;
		}

		public virtual void SetRollDice(bool value) {
			m_DidRollDice = value;
		}

		public virtual bool GetRollDice() {
			return m_DidRollDice;
		}

		public override void SetActive (bool value)
		{
			base.SetActive (value);
		}

		public override bool GetActive ()
		{
			return base.GetActive ();
		}

		public virtual void SetTurnState(CEnum.ETurnState value) {
			m_CurrentStateTurn = value;
		}

		public virtual CEnum.ETurnState GetTurnState() {
			return m_CurrentStateTurn;
		}

		public virtual void SetActiveSkill(int index) {
			
		}

		public virtual void SetAnimation(CEnum.EAnimation value) {
			
		}

		public virtual CEnum.EAnimation GetAnimation() {
			return CEnum.EAnimation.Idle;
		}

		public virtual void SetTargetAttack(CObjectController value) {
			
		}

		public virtual CObjectController GetTargetAttack() {
			return null;
		}

		public virtual void SetTargetAlly(CObjectController value) {
		
		}

		public virtual CObjectController GetTargetAlly() {
			return null;
		}

		public virtual CEnum.EObjectType GetObjectType() {
			return CEnum.EObjectType.None;
		}

		public virtual void SetObjectType(CEnum.EObjectType objectType) {
			
		}

		public virtual void SetCurrentBlock(CObjectController value) {
			if (m_CurrentBlock != null && Application.isPlaying) {
				CGameManager.Instance.OnObjectLeaveBlock (this, m_CurrentBlock);
			}
			m_CurrentBlock = value;
			if (value != null && Application.isPlaying) {
				CGameManager.Instance.OnObjectEnterBlock (this, value);
			}
		}

		public virtual CObjectController GetCurrentBlock() {
			return m_CurrentBlock;
		}

		public virtual void SetTargetBlock(CObjectController value) {
			m_TargetBlock = value;
		}

		public virtual CObjectController GetTargetBlock() {
			return m_TargetBlock;
		}

		public virtual CObjectController GetOwner() {
			return null;
		}

		public virtual void SetOwner(CObjectController value) {
			
		}

		public virtual void SetDidAttack(bool value) {
			
		}

		public virtual bool GetDidAttack() {
			return false;
		}

		public virtual string GetID() {
			return this.gameObject.GetInstanceID () + "";
		}

		public virtual float GetMoveSpeed() {
			return 0f;
		}

		public virtual float GetDistanceToTarget() {
			return GetSize();
		}

		public virtual void SetIsObstacle(bool value) {
			
		}

		public virtual bool GetIsObstacle() {
			return false;
		}

		public virtual int GetPhysicDefend() {
			return 0;
		}

		public virtual int GetMagicDefend() {
			return 0;
		}

		public virtual int GetCurrentHealth() {
			return 0;
		}

		public virtual int GetMaxHealth() {
			return 0;
		}

		public virtual void SetCurrentHealth(int value) {

		}

		public virtual int GetCurrentMana() {
			return 0;
		}

		public virtual int GetMaxMana() {
			return 0;
		}

		public virtual void SetCurrentMana(int value) {

		}

		public virtual void SetLuckyPoint(int value) {
		
		}

		public virtual int GetLuckyPoint() {
			return 0;
		}

		public virtual int GetPureDamage() {
			return 0;
		}

		public virtual int GetPhysicDamage() {
			return 0;
		}

		public virtual int GetMagicDamage() {
			return 0;
		}

		public virtual float GetAttackRange() {
			return 0f;
		}

		public virtual float GetSize() {
			return m_CapsuleCollider.radius;
		}

		public virtual float GetHeight() {
			return m_CapsuleCollider.height / 2f;
		}

		public virtual int GetMapIndex() {
			return 0;
		}

		public virtual int GetGoldReward() {
			return 0;
		}

		#endregion

	}
}
