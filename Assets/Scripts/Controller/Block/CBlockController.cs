using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BillianaireChessRPG {
	public class CBlockController : CObjectController {

		[SerializeField]	private List<CObjectController> m_Guests;

		protected override void Awake ()
		{
			base.Awake ();
			m_Guests = new List<CObjectController> ();
		}

		public virtual void AddGuest(CObjectController value) {
			if (m_Guests.Contains (value) || value == this)
				return;
			m_Guests.Add (value);
		}

		public virtual void RemoveGuest(CObjectController value) {
			if (m_Guests.Contains (value) == false)
				return;
			m_Guests.Remove (value);
		}

		public virtual List<CObjectController> GetGuests() {
			if (m_Guests == null)
				return null;
			return m_Guests;
		}

		public virtual CObjectController[] GetGuestBaseTeam(CEnum.ETeam team) {
			var result = m_Guests.Where ((x) => {
				return x.GetTeam() == team;
			});
			return result.ToArray ();
		}

		public virtual CObjectController[] GetGuestBaseCondition(Func<CObjectController, bool> predicate) {
			var result = m_Guests.Where (predicate);
			return result.ToArray ();
		}

	}
}
