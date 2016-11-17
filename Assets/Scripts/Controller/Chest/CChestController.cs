using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using FSM;

namespace BillianaireChessRPG {
	public class CChestController : CMonsterController {
		
		private string m_StateName;

		protected override void Init ()
		{
			base.Init ();
		}

		protected override void Awake ()
		{
			base.Awake ();
			// TEST
			m_DataPath = "Data/Chest/ChestData";
		}

		protected override void Start ()
		{
			base.Start ();
			var dataJSON = Resources.Load<TextAsset>(m_DataPath);
			m_Data = TinyJSON.JSON.Load (dataJSON.text).Make<CCharacterData> ();
			var jsonText 	= Resources.Load<TextAsset>(this.GetFSMPath());
			m_FSMManager.LoadFSM (jsonText.text);
			SetCurrentBlock (m_CurrentBlock);
			SetActive (true);
		}

		protected override void UpdateBaseTime (float dt)
		{
			base.UpdateBaseTime (dt);
			if (m_GameManager.GameState != CEnum.EGameState.EndGame && GetActive()) {
				m_FSMManager.UpdateState (dt);
				m_StateName = m_FSMManager.currentStateName;
			}
		}

		protected override void OnRegisterAnimation ()
		{
			// TODO
		}

		public override void SetAnimation (CEnum.EAnimation anim)
		{
			// TODO
		}

		public override void SetActive (bool value)
		{
			base.SetActive (value);
			this.gameObject.SetActive (value);
		}

		public override string GetFSMStateName ()
		{
			base.GetFSMStateName ();
			return m_StateName;
		}

	}
}
