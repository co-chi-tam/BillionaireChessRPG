using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BillianaireChessRPG {
	public class CBaseMonoBehaviour : MonoBehaviour {

		protected Transform m_Transform;

		protected virtual void Init() {

		}

		protected virtual void Awake() {
			m_Transform = this.transform;
		}

		protected virtual void Start () {
		
		}
		
		protected virtual void Update () {
			this.UpdateBaseTime (Time.deltaTime);
		}

		protected virtual void UpdateBaseTime(float dt) {
			
		}

		public virtual void SetActive(bool value) {
			
		}

		public virtual bool GetActive() {
			return this.gameObject.activeInHierarchy;
		}

		public virtual void SetPosition(Vector3 value) {
			m_Transform.position = value;
		}

		public virtual Vector3 GetPosition() {
			return m_Transform.position;
		}

	}
}