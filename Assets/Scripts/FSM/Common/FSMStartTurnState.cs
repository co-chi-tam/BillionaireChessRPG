using UnityEngine;
using System.Collections;
using FSM;

namespace BillianaireChessRPG {
	public class FSMStartTurnState : FSMBaseControllerState
	{
		public FSMStartTurnState(IContext context) : base (context)
		{

		}

		public override void StartState()
		{
			base.StartState ();
			m_Controller.ResetBaseStartTurn ();
			if (m_Controller.OnStartTurn != null) {
				m_Controller.OnStartTurn ();
			}
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