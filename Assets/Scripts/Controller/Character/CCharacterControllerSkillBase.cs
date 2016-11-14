using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using FSM;

namespace BillianaireChessRPG {
	public partial class CCharacterController {

		#region Attack

		protected virtual void AttackToTarget(string animationName) {
			m_DidAttack = true;
			if (m_TargetAttack != null) {
				m_TargetAttack.ApplyDamage (GetPureDamage(), CEnum.EAttackType.Pure);
			}
		}

		protected virtual void AttackPureDamage(string animationName) {
			m_DidAttack = true;
			if (m_TargetAttack != null) {
				m_TargetAttack.ApplyDamage (GetPureDamage(), CEnum.EAttackType.Pure);
			}
		}

		protected virtual void AttackPhysicDamage(string animationName) {
			m_DidAttack = true;
			if (m_TargetAttack != null) {
				m_TargetAttack.ApplyDamage (GetPhysicDamage(), CEnum.EAttackType.Physic);
			}
		}

		protected virtual void AttackMagicDamage(string animationName) {
			m_DidAttack = true;
			if (m_TargetAttack != null) {
				m_TargetAttack.ApplyDamage (GetMagicDamage(), CEnum.EAttackType.Magic);
			}
		}

		#endregion

		#region Buff

		protected virtual void BuffHealth(string animationName) {
			m_DidAttack = true;
			if (m_TargetAlly != null) {
				m_TargetAlly.ApplyBuff (50, CEnum.EStatusType.Health);
			} else {
				this.ApplyBuff (50, CEnum.EStatusType.Health);
			}
		}

		protected virtual void BuffAllyHealth(string animationName) {
			m_DidAttack = true;
			if (m_TargetAlly != null) {
				m_TargetAlly.ApplyBuff (100, CEnum.EStatusType.Health);
			}
		}

		#endregion

	}
}
