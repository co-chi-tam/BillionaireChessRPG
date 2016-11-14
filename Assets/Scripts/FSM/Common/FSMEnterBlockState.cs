using UnityEngine;
using System.Collections;
using FSM;

namespace BillianaireChessRPG {
	public class FSMEnterBlockState : FSMBaseControllerState
	{
		public FSMEnterBlockState(IContext context) : base (context)
		{

		}

		public override void StartState()
		{
			base.StartState ();
			CGameManager.Instance.OnObjectEnterBlock (m_Controller, m_Controller.GetCurrentBlock());
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