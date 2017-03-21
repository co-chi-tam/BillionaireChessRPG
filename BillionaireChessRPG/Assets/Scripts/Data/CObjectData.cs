using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BillianaireChessRPG {
	[Serializable]
	public class CObjectData : CBaseData {

		public string name;
		public CEnum.EObjectType objectType;

		public CObjectData () : base ()
		{
			this.name 		= string.Empty;
			this.objectType = CEnum.EObjectType.None;
		}

		public override void LoadFromDictionary (Dictionary<string, object> value)
		{
			base.LoadFromDictionary (value);
			this.name 		= value["name"].ToString();
			this.objectType = (CEnum.EObjectType) Enum.Parse (typeof (CEnum.EObjectType), value["objectType"].ToString());
		}

	}
}
