using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BillianaireChessRPG {
	[Serializable]
	public class CBaseData {

		public string id;

		public CBaseData ()
		{
			this.id = string.Empty;
		}

		public virtual void LoadFromDictionary(Dictionary<string, object> value) {
			this.id = value ["id"].ToString ();
		}

	}
}
