using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BillianaireChessRPG {
	public class CUIManager : CMonoSingleton<CUIManager> {

		[SerializeField]	private AnimatorCustom m_Animator;
		[SerializeField]	public CObjectController target;
		[SerializeField]	private List<GameObject> m_UIObjects;

		public Action OnRollDiceComplete;

		private SequenceList<GameObject> m_RegistedUIObjects;

		protected override void Awake ()
		{
			base.Awake ();
			m_RegistedUIObjects = new SequenceList<GameObject> ();
			for (int i = 0; i < m_UIObjects.Count; i++) {
				var uiAvatar = m_UIObjects [i];
				uiAvatar.SetActive (false);
				m_RegistedUIObjects.Enqueue (uiAvatar);
			}
		}

		public void RegisterUIAvatar(CObjectController value) {
			
		}

		public void UnRegisterUIAvatar(CObjectController value) {
			
		}

		private void UIAvatarAnimation() {
		
		}
			
		public void ActionRollDiceComplete() {
			if (OnRollDiceComplete != null) {
				OnRollDiceComplete ();
				OnRollDiceComplete = null;
			}
		}

		public void SetAnimation(int anim) {
			m_Animator.SetInteger ("AnimParam", anim);
		}

		public void SetAnimation(CEnum.EUIState anim) {
			m_Animator.SetInteger ("AnimParam", (int)anim);
		}

	}
}
