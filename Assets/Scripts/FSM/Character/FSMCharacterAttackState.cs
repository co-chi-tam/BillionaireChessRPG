using UnityEngine;
using System.Collections;
using FSM;

namespace BillianaireChessRPG {
	public class FSMCharacterAttackState : FSMBaseControllerState
	{
		public FSMCharacterAttackState(IContext context) : base (context)
		{

		}

		public override void StartState()
		{
			base.StartState ();
			CGameManager.Instance.OnObjectSelectInAttack ();
		}

		public override void UpdateState(float dt)
		{
			base.UpdateState (dt);
			var target = m_Controller.GetTargetAttack ();
			m_Controller.LookAtTarget (target.GetPosition ());
		}

		public override void ExitState()
		{
			base.ExitState ();
		}
	}
}