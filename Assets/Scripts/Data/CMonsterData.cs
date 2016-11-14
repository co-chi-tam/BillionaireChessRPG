using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BillianaireChessRPG {
	[Serializable]
	public class CMonsterData : CCharacterData {
		
		public int goldReward;

		public CMonsterData () : base ()
		{
			this.goldReward = 0;
		}

	}
}
