using UnityEngine;
using System.Collections;
using FSM;

namespace BillianaireChessRPG {
	public class FSMCharacterMoveState : FSMBaseControllerState
	{
		private CMapManager m_MapManager;

		public FSMCharacterMoveState(IContext context) : base (context)
		{
			
		}

		public override void StartState()
		{
			base.StartState ();
			m_Controller.SetTargetAttack (null);
			m_Controller.SetTargetAlly (null);
			m_Controller.SetAnimation (CEnum.EAnimation.Move);
			m_MapManager = CMapManager.GetInstance ();
		}

		public override void UpdateState(float dt)
		{
			base.UpdateState (dt);
			m_Controller.MoveToTargetBlock (dt);
		}

		public override void ExitState()
		{
			base.ExitState ();
			m_Controller.SetCurrentBlock (m_Controller.GetTargetBlock ());
		}
	}
}