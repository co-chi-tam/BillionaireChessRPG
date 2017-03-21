using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BillianaireChessRPG {
	[Serializable]
	public class CCharacterData : CObjectData {

		public string modelPath;
		public string avatarPath;

		public int currentHealth;
		public int maxHealth;

		public int currentMana;
		public int maxMana;

		public int luckyPoint;

		public int pureDamage;

		public int physicDamage;
		public int physicDefend;

		public int magicDamage;
		public int magicDefend;

		public int goldReward;

		public CCharacterData () : base ()
		{
			this.modelPath 		= string.Empty;
			this.avatarPath 	= string.Empty;

			this.currentHealth 	= 0;
			this.maxHealth 		= 0;

			this.currentMana 	= 0;
			this.maxMana 		= 0;

			this.luckyPoint 	= 0;

			this.pureDamage 	= 0;

			this.physicDamage 	= 0;
			this.physicDefend 	= 0;

			this.magicDamage 	= 0;
			this.magicDefend 	= 0;

			this.goldReward 	= 0;
		}

		public override void LoadFromDictionary (Dictionary<string, object> value)
		{
			base.LoadFromDictionary (value);

			this.modelPath 		= value["modelPath"].ToString();
			this.avatarPath 	= value["avatarPath"].ToString();

			this.currentHealth 	= int.Parse (value["currentHealth"].ToString());
			this.maxHealth 		= int.Parse (value["maxHealth"].ToString());

			this.currentMana 	= int.Parse (value["currentMana"].ToString());
			this.maxMana 		= int.Parse (value["maxMana"].ToString());

			this.luckyPoint 	= int.Parse (value["luckyPoint"].ToString());

			this.pureDamage 	= int.Parse (value["pureDamage"].ToString());

			this.physicDamage 	= int.Parse (value["physicDamage"].ToString());
			this.physicDefend 	= int.Parse (value["physicDefend"].ToString());

			this.magicDamage 	= int.Parse (value["magicDamage"].ToString());
			this.magicDefend 	= int.Parse (value["magicDefend"].ToString());

			this.goldReward 	= int.Parse (value["goldReward"].ToString());
		}

	}
}
