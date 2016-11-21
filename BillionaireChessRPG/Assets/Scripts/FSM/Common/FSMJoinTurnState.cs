using UnityEngine;
using System.Collections;
using FSM;

namespace BillianaireChessRPG {
	public class FSMJoinTurnState : FSMBaseControllerState
	{
		public FSMJoinTurnState(IContext context) : base (context)
		{

		}

		public override void StartState()
		{
			base.StartState ();
			if (m_Controller.OnStartTurn != null) {
				m_Controller.OnStartTurn ();
			}
			CGameManager.Instance.RegisterObject (m_Controller);
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