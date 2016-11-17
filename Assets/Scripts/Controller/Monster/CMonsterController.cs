using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BillianaireChessRPG {
	public class CMonsterController : CCharacterController {

		[SerializeField]	private string m_StateName;

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
			var dataJSON = Resources.Load<TextAsset>(m_DataPath);
			m_Data = TinyJSON.JSON.Load (dataJSON.text).Make<CCharacterData> ();
			var jsonText = Resources.Load<TextAsset>(this.GetFSMPath());
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

		public override string GetFSMStateName ()
		{
			base.GetFSMStateName ();
			return m_StateName;
		}

	}
}
