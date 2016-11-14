using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BillianaireChessRPG {
	[Serializable]
	public class CMapData : CBaseData {

		public string mapName;
		public string[] mapModels;
		public string gameFSMPath;

		public CMapData () : base ()
		{
			this.mapName = string.Empty;
			this.mapModels = null;
			this.gameFSMPath = string.Empty;
		}

	}
}
