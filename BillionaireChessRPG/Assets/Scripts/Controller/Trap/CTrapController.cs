using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BillianaireChessRPG {
	public class CTrapController : CCharacterController {

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

		public override string GetFSMStateName ()
		{
			base.GetFSMStateName ();
			return m_StateName;
		}

		public override void SetActiveSkill (int index)
		{
			// Alwy Basic Skill
			base.SetActiveSkill (index);
			SetAnimation (CEnum.EAnimation.Attack_1);
		}

	}
}
