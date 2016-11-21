using UnityEngine;
using System.Collections;
using FSM;

namespace BillianaireChessRPG {
	public class FSMEndTurnState : FSMBaseControllerState
	{
		public FSMEndTurnState(IContext context) : base (context)
		{

		}

		public override void StartState()
		{
			base.StartState ();
			m_Controller.ResetBaseEndTurn ();
			if (m_Controller.OnEndTurn != null) {
				m_Controller.OnEndTurn ();
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