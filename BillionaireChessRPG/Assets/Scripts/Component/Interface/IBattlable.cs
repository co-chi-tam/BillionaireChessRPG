using UnityEngine;
using System;
using System.Collections;

namespace BillianaireChessRPG {
	public interface IBattlable {

		string GetID();
		void ApplyDamage(int damage, CEnum.EAttackType damageType);
		void ApplyBuff(int buff, CEnum.EStatusType statusType);

		bool GetActive();
		void SetActive(bool value);

		CEnum.EObjectType GetObjectType();
		void SetObjectType(CEnum.EObjectType objectType);

	}
}
