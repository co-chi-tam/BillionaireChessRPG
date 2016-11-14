using UnityEngine;
using System.Collections;
using FSM;

namespace BillianaireChessRPG {
	public class FSMCharacterAutoAttackState : FSMBaseControllerState
	{
		public FSMCharacterAutoAttackState(IContext context) : base (context)
		{

		}

		public override void StartState()
		{
			base.StartState ();
			var random = (int)(Mathf.PerlinNoise(Time.time, Time.time) * 4);
			m_Controller.SetActiveSkill((int)CEnum.EAnimation.Attack_1 + random);
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