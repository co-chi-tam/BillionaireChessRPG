using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using FSM;

namespace BillianaireChessRPG {
	public class CChestController : CMonsterController {
		
		protected override void Init ()
		{
			base.Init ();
		}

		protected override void Awake ()
		{
			base.Awake ();
		}

		protected override void Start ()
		{
			base.Start ();
			m_Data = TinyJSON.JSON.Load (m_DataText.text).Make<CCharacterData> ();
			m_FSMManager.LoadFSM (m_FSMText.text);
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
