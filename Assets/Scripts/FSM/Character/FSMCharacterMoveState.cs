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
			var currentBlock = m_Controller.GetCurrentBlock () as CBlockController;
			var targetBlock = m_Controller.GetTargetBlock () as CBlockController;
			var nextBlock = m_MapManager.GetBlockPath (0, currentBlock, targetBlock);
			var distance = (nextBlock.GetPosition () - m_Controller.GetPosition ()).sqrMagnitude;
			if (distance >= m_Controller.GetDistanceToTarget() * m_Controller.GetDistanceToTarget()){
				m_Controller.MoveToTarget (nextBlock.GetPosition (), dt);
			} else {
				m_Controller.SetCurrentBlock (nextBlock);
			}
		}

		public override void ExitState()
		{
			base.ExitState ();
			m_Controller.SetCurrentBlock (m_Controller.GetTargetBlock ());
		}
	}
}