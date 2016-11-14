using UnityEngine;
using System.Collections;
using FSM;

namespace BillianaireChessRPG {
	public class FSMCharacterDeathState : FSMBaseControllerState
	{
		public FSMCharacterDeathState(IContext context) : base (context)
		{

		}

		public override void StartState()
		{
			base.StartState ();
			m_Controller.SetAnimation (CEnum.EAnimation.Death);
			m_Controller.SetActive (false);
			if (m_Controller.OnEndTurn != null) {
				m_Controller.OnEndTurn ();
			}
			m_Controller.SetCurrentBlock (null);
			CGameManager.Instance.UnRegisterObject (m_Controller);
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