using UnityEngine;
using System.Collections;
using FSM;

namespace BillianaireChessRPG {
	public class FSMFindTargetState : FSMBaseControllerState
	{
		public FSMFindTargetState(IContext context) : base (context)
		{

		}

		public override void StartState()
		{
			base.StartState ();
			var currentBlock = m_Controller.GetCurrentBlock () as CBlockController;
			var attackTargets = currentBlock.GetGuestBaseCondition ((x) => {
				return x.GetTeam() != m_Controller.GetTeam () && x.GetActive();
			});
			var allyTargets = currentBlock.GetGuestBaseCondition ((x) => {
				return x.GetTeam() == m_Controller.GetTeam () && x.GetActive() && x != m_Controller;
			});
			if (attackTargets.Length > 0) {
				var random = (int)(Mathf.PerlinNoise(Time.time, Time.time) * attackTargets.Length);	
				var target = attackTargets [random];
				m_Controller.SetTargetAttack (target);
				CGameManager.Instance.RegisterObject (target);
			}
			if (allyTargets.Length > 0) {
				var random = (int)(Mathf.PerlinNoise(Time.time, Time.time) * allyTargets.Length);	
				var target = allyTargets [random];
				m_Controller.SetTargetAlly (target);
			}
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