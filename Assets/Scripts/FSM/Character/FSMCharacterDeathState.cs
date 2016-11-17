using UnityEngine;
using System.Collections;
using FSM;

namespace BillianaireChessRPG {
	public class FSMCharacterDeathState : FSMBaseControllerState
	{
		private CGameManager m_GameManager;

		public FSMCharacterDeathState(IContext context) : base (context)
		{

		}

		public override void StartState()
		{
			base.StartState ();
			m_GameManager = CGameManager.GetInstance ();
			m_Controller.SetAnimation (CEnum.EAnimation.Death);
			m_Controller.SetActive (false);
			if (m_Controller.OnEndTurn != null) {
				m_Controller.OnEndTurn ();
			}
			m_Controller.SetCurrentBlock (null);
			m_GameManager.UnRegisterObject (m_Controller);
			m_GameManager.OnUserRewardGold (m_Controller.GetGoldReward ());
		}

		public override void UpdateState(float dt)
		{
			base.UpdateState (dt);
		}

		public override void ExitState()
		{
			base.ExitState ();
		}
	}
}