using UnityEngine;
using System.Collections;

namespace BillianaireChessRPG {
	public interface IStatus {

		void SetData (CObjectData value);
		CObjectData GetData();

		void SetTeam (CEnum.ETeam value);
		CEnum.ETeam GetTeam();

		void SetAnimation (CEnum.EAnimation value);
		CEnum.EAnimation GetAnimation();

		void SetTurnState (CEnum.ETurnState value);
		CEnum.ETurnState GetTurnState();

		void SetObjectType (CEnum.EObjectType value);
		CEnum.EObjectType GetObjectType ();

		bool GetActive();
		void SetActive(bool value);

		string GetFSMStateName();
		string GetFSMName();

		int GetCurrentHealth();
		int GetMaxHealth();
		void SetCurrentHealth(int value);

		int GetCurrentMana();
		int GetMaxMana();
		void SetCurrentMana(int value);

		void SetLuckyPoint(int value);
		int GetLuckyPoint();			// Point to avoid trap and object can dangerous, or obtain chest

		int GetPureDamage();

		int GetPhysicDefend ();
		int GetPhysicDamage ();

		int GetMagicDefend ();
		int GetMagicDamage ();

		int GetGoldReward();
	}
}
