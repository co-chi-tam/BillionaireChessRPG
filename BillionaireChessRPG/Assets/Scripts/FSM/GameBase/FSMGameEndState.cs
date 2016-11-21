using UnityEngine;
using System.Collections;
using FSM;

namespace BillianaireChessRPG {
	public class FSMGameEndState : FSMBaseState
	{
		private CGameManager m_Manager;

		public FSMGameEndState(IContext context) : base (context)
		{
			m_Manager = context as CGameManager;
		}

		public override void StartState()
		{
			base.StartState ();
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