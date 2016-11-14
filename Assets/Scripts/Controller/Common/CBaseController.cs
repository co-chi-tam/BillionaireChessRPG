using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BillianaireChessRPG {
	public class CBaseController : CBaseMonoBehaviour {

		protected bool m_Active;

		protected override void Awake ()
		{
			base.Awake ();
			m_Active = false;
		}

		public override void SetActive (bool value)
		{
			base.SetActive (value);
			m_Active = value;
		}

		public override bool GetActive() {
			base.GetActive ();
			return m_Active;
		}

	}
}
