using UnityEngine;
using System.Collections;
using FSM;

namespace BillianaireChessRPG {
	public class FSMGameLoadingState : FSMBaseState
	{
		private CGameManager m_Manager;

		public FSMGameLoadingState(IContext context) : base (context)
		{
			m_Manager = context as CGameManager;
		}

		public override void StartState()
		{
			base.StartState ();
			m_Manager.OnLoadingGame ();
			m_Manager.GameState = CEnum.EGameState.StartGame;
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