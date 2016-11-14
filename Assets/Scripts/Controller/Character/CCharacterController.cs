using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using FSM;

namespace BillianaireChessRPG {

	public partial class CCharacterController : CObjectController {

		#region Properties

		[SerializeField]	protected AnimatorCustom m_AnimatorController;
		[SerializeField]	protected CObjectController m_TargetAttack;
		[SerializeField]	protected CObjectController m_TargetAlly;
		[SerializeField]	protected string m_DataPath = "Data/Character/CharacterData";

		protected CEnum.EAnimation m_CurrentAnimation = CEnum.EAnimation.Idle;

		protected CMovableComponent m_MovableComponent;
		protected CBattlableComponent m_BattleComponent;
		protected CGameManager m_GameManager;
		protected bool m_DidAttack = false;

		protected CCharacterData m_Data;

		#endregion

		#region Implementation Monobehaviour

		protected override void Awake ()
		{
			base.Awake ();
		}

		protected override void Start ()
		{
			base.Start ();
			m_GameManager = CGameManager.GetInstance ();
		}

		protected override void OnRegisterFSM() {
			base.OnRegisterFSM ();
			var idleState 	= new FSMCharacterIdleState (this);
			var moveState 	= new FSMCharacterMoveState (this);
			var attackState = new FSMCharacterAttackState (this);
			var autoAttackState = new FSMCharacterAutoAttackState (this);
			var deathState 	= new FSMCharacterDeathState (this);
			var findTarget = new FSMFindTargetState (this);

			m_FSMManager.RegisterState ("CharacterIdleState", 	idleState);
			m_FSMManager.RegisterState ("CharacterMoveState", 	moveState);
			m_FSMManager.RegisterState ("CharacterAttackState", attackState);
			m_FSMManager.RegisterState ("CharacterAutoAttackState", autoAttackState);
			m_FSMManager.RegisterState ("CharacterDeathState", 	deathState);
			m_FSMManager.RegisterState ("FindTargetState", 	findTarget);

			m_FSMManager.RegisterCondition ("HaveTargetAttack",		HaveTargetAttack);
			m_FSMManager.RegisterCondition ("HaveTargetAlly",		HaveTargetAlly);
			m_FSMManager.RegisterCondition ("DidMoveToBlock", 		DidMoveToBlock);
			m_FSMManager.RegisterCondition ("DidAttack", 			this.GetDidAttack);
		}

		protected override void OnRegisterComponent() {
			base.OnRegisterComponent ();
			m_MovableComponent = new CMovableComponent (this);
			m_MovableComponent.currentTransform = m_Transform;
			m_BattleComponent = new CBattlableComponent (this);
		}

		protected override void OnRegisterAnimation() {
			base.OnRegisterComponent ();
			m_AnimatorController.RegisterAnimation ("Attack", AttackToTarget);
			m_AnimatorController.RegisterAnimation ("AttackPureDamage", AttackPureDamage);
			m_AnimatorController.RegisterAnimation ("AttackPhysicDamage", AttackPhysicDamage);
			m_AnimatorController.RegisterAnimation ("AttackMagicDamage", AttackMagicDamage);

			m_AnimatorController.RegisterAnimation ("BuffHealth", BuffHealth);
			m_AnimatorController.RegisterAnimation ("BuffAllyHealth", BuffAllyHealth);

			m_AnimatorController.RegisterAnimation ("Death", DeactiveObject);
		}

		#endregion

		#region Main methods

		public override void ResetBaseStartTurn ()
		{
			base.ResetBaseStartTurn ();
			SetAnimation (CEnum.EAnimation.Idle);
			m_DidAttack = false;
		}

		public override void ResetBaseEndTurn ()
		{
			base.ResetBaseEndTurn ();
		}

		public override void ApplyDamage(int damage, CEnum.EAttackType damageType) {
			base.ApplyDamage (damage, damageType);
			m_BattleComponent.ApplyDamage (damage, damageType);
			var health = 0;
			if (m_BattleComponent.CalculateHealth (this.GetCurrentHealth (), out health)) {
				SetCurrentHealth (health);
			}
		}

		public override void ApplyBuff (int buff, CEnum.EStatusType statusType)
		{
			base.ApplyBuff (buff, statusType);
			m_BattleComponent.ApplyBuff (buff, statusType);
			var health = 0;
			if (m_BattleComponent.CalculateHealth (this.GetCurrentHealth (), out health)) {
				SetCurrentHealth (health);
			}
		}

		public override void MoveToTarget(Vector3 target, float dt) {
			base.MoveToTarget (target, dt);
			m_MovableComponent.targetPosition = target;
			m_MovableComponent.MoveForwardToTarget (dt);
		}

		public override void LookAtTarget(Vector3 target) {
			base.LookAtTarget (target);
			m_MovableComponent.targetPosition = target;
			m_MovableComponent.LookForwardToTarget (target);
		} 

		public virtual void DeactiveObject(string animationName) {
			this.gameObject.SetActive (false);
		}

		#endregion

		#region FSM

		internal virtual bool HaveTargetAttack() {
			if (m_TargetAttack == null)
				return false;
			return m_TargetAttack != null 
				&& m_TargetAttack.GetActive() 
				&& m_TargetAttack.GetTeam() != this.GetTeam();
		}

		internal virtual bool HaveTargetAlly() {
			if (m_CurrentBlock == null)
				return false;
			if (m_TargetAlly == null)
				return false;
			return m_TargetAlly != null 
				&& m_TargetAlly.GetActive() 
				&& m_TargetAlly.GetTeam() == this.GetTeam()
				&& m_TargetAlly.GetCurrentBlock() == this.GetCurrentBlock();
		}

		internal virtual bool DidMoveToBlock() {
			if (m_TargetBlock == null)
				return false;
			var position = m_TargetBlock.GetPosition();
			return m_MovableComponent.DidMoveToTarget(position);
		}

		internal override bool IsDeath ()
		{
			return GetCurrentHealth () <= 0;
		}

		internal override bool CanRollDice ()
		{
			return DidMoveToBlock() && !HaveTargetAttack() && !IsFinishBlock();
		}

		#endregion

		#region Getter && Setter

		public override void SetData (CObjectData value)
		{
			base.SetData (value);
			m_Data = value as CCharacterData;
		}

		public override CObjectData GetData ()
		{
			base.GetData ();
			return m_Data;
		}

		public override string GetFSMStateName ()
		{
			base.GetFSMStateName ();
			return string.Empty;
		}

		public override string GetFSMPath ()
		{
			base.GetFSMPath ();
			return m_Data.FSMPath;
		}

		public override void SetCurrentHealth (int value)
		{
			base.SetCurrentHealth (value);
			m_Data.currentHealth = value;
		}

		public override int GetCurrentHealth ()
		{
			base.GetCurrentHealth ();
			return m_Data.currentHealth;
		}

		public override int GetMaxHealth ()
		{
			base.GetMaxHealth ();
			return m_Data.maxHealth;
		}

		public override void SetCurrentMana (int value)
		{
			base.SetCurrentMana (value);
			m_Data.currentMana = value;
		}

		public override int GetCurrentMana ()
		{
			base.GetCurrentMana ();
			return m_Data.currentMana;
		}

		public override int GetMaxMana ()
		{
			base.GetMaxMana ();
			return m_Data.maxMana;
		}

		public override int GetPhysicDefend() {
			base.GetPhysicDefend ();
			return m_Data.physicDefend;
		}

		public override int GetMagicDefend() {
			base.GetMagicDefend ();
			return m_Data.magicDefend;
		}

		public override void SetLuckyPoint (int value)
		{
			base.SetLuckyPoint (value);
			m_Data.luckyPoint = Mathf.Clamp (value, 0, 50);
		}

		public override int GetLuckyPoint() {
			base.GetLuckyPoint ();
			return m_Data.luckyPoint;
		}

		public override void SetChallengePoint (int value)
		{
			base.SetChallengePoint (value);
			m_Data.challengePoint = Mathf.Clamp (value, 0, 50);
		}

		public override int GetChallengePoint() {
			base.GetChallengePoint ();
			return m_Data.challengePoint;
		}

		public override int GetPureDamage() {
			base.GetPureDamage ();
			return m_Data.pureDamage;
		}

		public override int GetPhysicDamage() {
			base.GetPhysicDamage ();
			return m_Data.physicDamage;
		}

		public override int GetMagicDamage() {
			base.GetMagicDamage ();
			return m_Data.magicDamage;
		}

		public override void SetActiveSkill (int index)
		{
			base.SetActiveSkill (index);
			SetAnimation ((CEnum.EAnimation)index);
		}

		public override void SetAnimation(CEnum.EAnimation anim) {
			base.SetAnimation (anim);
			m_AnimatorController.SetInteger ("AnimParam", (int)anim);
		}

		public virtual CEnum.EAnimation GetAnimation() {
			return m_CurrentAnimation;
		}

		public override float GetMoveSpeed ()
		{
			base.GetMoveSpeed ();
			return 5f;
		}

		public override float GetAttackRange ()
		{
			return base.GetAttackRange ();
		}

		public override float GetDistanceToTarget ()
		{
			base.GetDistanceToTarget ();
			if (m_TargetAttack == null)
				return base.GetDistanceToTarget ();
//			return this.GetAttackRange () + m_TargetAttack.GetSize();
			return 0.5f;
		}

		public override void SetTargetAttack(CObjectController value) {
			base.SetTargetAttack (value);
			if (value != null) {
				switch (value.GetObjectType ()) {
				case CEnum.EObjectType.Monster:
				case CEnum.EObjectType.Hero:
					m_TargetAttack = value;
					break;
				}
			} else {
				m_TargetAttack = value;
			}
		}

		public override CObjectController GetTargetAttack() {
			base.GetTargetAttack ();
			return m_TargetAttack;
		}

		public override void SetTargetAlly (CObjectController value)
		{
			base.SetTargetAlly (value);
			if (value != null) {
				switch (value.GetObjectType ()) {
				case CEnum.EObjectType.Hero:
					m_TargetAlly = value;
					break;
				}
			} else {
				m_TargetAlly = value;
			}
		}

		public override CObjectController GetTargetAlly ()
		{
			base.GetTargetAlly ();
			return m_TargetAlly;
		}

		public override void SetDidAttack(bool value) {
			base.SetDidAttack (value);
			m_DidAttack = value;
		}

		public override bool GetDidAttack() {
			base.GetDidAttack ();
			return m_DidAttack;
		}

		public override CEnum.EObjectType GetObjectType ()
		{
			return m_Data.objectType;
		}

		public override void SetObjectType (CEnum.EObjectType objectType)
		{
			base.SetObjectType (objectType);
			m_Data.objectType = objectType;
		}

		#endregion

	}
}
