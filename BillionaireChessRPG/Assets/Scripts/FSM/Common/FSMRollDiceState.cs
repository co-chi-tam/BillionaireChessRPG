using UnityEngine;
using System.Collections;
using FSM;

namespace BillianaireChessRPG {
	public class FSMRollDiceState : FSMBaseControllerState
	{
		public FSMRollDiceState(IContext context) : base (context)
		{

		}

		public override void StartState()
		{
			base.StartState ();
			CGameManager.Instance.OnObjectSelectRollDice ();
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