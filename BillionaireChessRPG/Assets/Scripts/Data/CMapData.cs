using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BillianaireChessRPG {
	[Serializable]
	public class CMapData : CBaseData {

		public string mapName;
		public string[] mapModels;
		public string gameFSMPath;
		public string mapFSMPath;
		public int limitStep;

		public CMapData () : base ()
		{
			this.mapName 		= string.Empty;
			this.mapModels 		= null;
			this.gameFSMPath 	= string.Empty;
			this.mapFSMPath 	= string.Empty;
			this.limitStep 		= 0;
		}

		public override void LoadFromDictionary (Dictionary<string, object> value)
		{
			base.LoadFromDictionary (value);
			this.mapName 		= value["mapName"].ToString();
			var listMapModels 	= value ["mapModels"] as List<object>;
			this.mapModels 		= new string[listMapModels.Count];
			for (int i = 0; i < listMapModels.Count; i++) {
				var model = listMapModels [i].ToString();
				this.mapModels [i] = model;
			}
			this.gameFSMPath 	= value["gameFSMPath"].ToString();
			this.mapFSMPath 	= value["mapFSMPath"].ToString();
			this.limitStep 		= int.Parse (value["limitStep"].ToString());
		}

	}
}
