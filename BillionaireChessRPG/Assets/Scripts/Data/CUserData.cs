using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BillianaireChessRPG {
	[Serializable]
	public class CUserData : CObjectData {

		public int gold;
		public int diamond;

		public CUserData () : base ()
		{
			this.gold 		= 0;
			this.diamond 	= 0;
		}

		public override void LoadFromDictionary (Dictionary<string, object> value)
		{
			base.LoadFromDictionary (value);
			this.gold 		= int.Parse (value ["gold"].ToString ());
			this.diamond 	= int.Parse (value["diamond"].ToString ());
		}

	}
}
