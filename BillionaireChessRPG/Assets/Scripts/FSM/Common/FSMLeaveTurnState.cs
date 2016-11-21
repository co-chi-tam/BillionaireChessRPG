using UnityEngine;
using System.Collections;
using FSM;

namespace BillianaireChessRPG {
	public class FSMLeaveTurnState : FSMBaseControllerState
	{
		public FSMLeaveTurnState(IContext context) : base (context)
		{

		}

		public override void StartState()
		{
			base.StartState ();
			if (m_Controller.OnEndTurn != null) {
				m_Controller.OnEndTurn ();
			}
			CGameManager.Instance.UnRegisterObject (m_Controller);
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