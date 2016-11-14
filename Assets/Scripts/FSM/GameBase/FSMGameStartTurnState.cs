using UnityEngine;
using System.Collections;
using FSM;

namespace BillianaireChessRPG {
	public class FSMGameStartTurnState : FSMBaseState
	{
		private CGameManager m_Manager;

		public FSMGameStartTurnState(IContext context) : base (context)
		{
			m_Manager = context as CGameManager;
		}

		public override void StartState()
		{
			base.StartState ();
			m_Manager.GameState = CEnum.EGameState.UpdateTurn;
			m_Manager.OnObjectStartTurn ();
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