using UnityEngine;
using System.Collections;
using FSM;

namespace BillianaireChessRPG {
	public class FSMMapIdleState : FSMBaseState
	{
		private CMapManager m_Manager;

		public FSMMapIdleState(IContext context) : base (context)
		{
			m_Manager = context as CMapManager;
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