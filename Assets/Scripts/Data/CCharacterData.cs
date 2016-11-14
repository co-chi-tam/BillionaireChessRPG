using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BillianaireChessRPG {
	[Serializable]
	public class CCharacterData : CObjectData {

		public string FSMPath;
		public string avatarPath;

		public int currentHealth;
		public int maxHealth;

		public int currentMana;
		public int maxMana;

		public int luckyPoint;
		public int challengePoint;

		public int pureDamage;

		public int physicDamage;
		public int physicDefend;

		public int magicDamage;
		public int magicDefend;

		public CCharacterData () : base ()
		{
			this.FSMPath = string.Empty;
			this.avatarPath = string.Empty;

			this.currentHealth = 0;
			this.maxHealth = 0;

			this.currentMana = 0;
			this.maxMana = 0;

			this.luckyPoint = 0;
			this.challengePoint = 0;

			this.pureDamage = 0;

			this.physicDamage = 0;
			this.physicDefend = 0;

			this.magicDamage = 0;
			this.magicDefend = 0;
		}

	}
}
