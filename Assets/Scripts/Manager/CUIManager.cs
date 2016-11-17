using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BillianaireChessRPG {
	public class CUIManager : CMonoSingleton<CUIManager> {

		[SerializeField]	private Animator m_Animator;
		[SerializeField]	public CObjectController target;

		public Action OnRollDiceComplete;

		public void SetAnimation(int anim) {
			m_Animator.SetInteger ("AnimParam", anim);
		}

		public void SetAnimation(CEnum.EUIState anim) {
			m_Animator.SetInteger ("AnimParam", (int)anim);
		}

		public void ActionRollDiceComplete() {
			if (OnRollDiceComplete != null) {
				OnRollDiceComplete ();
				OnRollDiceComplete = null;
			}
		}

	}
}
