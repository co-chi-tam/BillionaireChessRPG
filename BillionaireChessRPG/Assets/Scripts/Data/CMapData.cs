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
		public string mapFSMPath;
		public int limitStep;

		public CMapData () : base ()
		{
			this.mapName = string.Empty;
			this.mapModels = null;
			this.gameFSMPath = string.Empty;
			this.mapFSMPath = string.Empty;
			this.limitStep = 0;
		}

	}
}
