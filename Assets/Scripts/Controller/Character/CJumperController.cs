using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BillianaireChessRPG {
	[RequireComponent(typeof(Parabola))]
	public class CJumperController : CCharacterController {
		
		protected Parabola m_Parabola;
		protected Queue<Vector3> m_StepQueue;
		protected bool m_Jumping = false;
		protected int m_JumpStep = -1;

		protected override void Awake ()
		{
			base.Awake ();
			m_Parabola = this.GetComponent<Parabola> ();
			m_Jumping = false;
			m_JumpStep = -1;
			m_StepQueue = new Queue<Vector3> ();
		}

		public virtual void OnClientJumpComplete(Vector3 position) {

		}

		public virtual void OnClientAllStepComplete() {

		}

		public virtual void JumpToPosition(Vector3 position) {
			JumpToPosition (position, () => { 
				m_Jumping = false;
				OnClientAllStepComplete(); 
				OnClientJumpComplete(position); 
			});
		}

		public virtual void JumpToPosition(Vector3 position, Action complete) {
			m_Jumping = true;	
			CHandleEvent.Instance.AddEvent (m_Parabola.HandleJumpToPosition (position, (jump) => {
				// TODO
			}, () => {
				m_Jumping = false;
				if (complete != null) {
					complete ();
				}
			}));
		}

		public virtual void JumpToPositions(string positions) {
			if (m_Jumping == false) {
				m_Jumping = true;	
				var v3Array = positions.Split (';'); // (x,y,z);(x,y,z);....
				var v3Positions = new Vector3[v3Array.Length];
				for (int i = 0; i < v3Positions.Length; i++) {
					v3Positions [i] = CUtil.V3Parser (v3Array [i]);
				}
				JumpToPositions (OnClientJumpComplete, () => {
					m_Jumping = false;	
					OnClientAllStepComplete();
				}, v3Positions);
			}
		}

		public virtual void JumpToPositions(Vector3[] positions) {
			if (m_Jumping == false) {
				m_Jumping = true;	
				JumpToPositions (OnClientJumpComplete, () => {
					m_Jumping = false;	
					OnClientAllStepComplete();
				}, positions);
			}
		}

		protected virtual void JumpToPositions(Action<Vector3> jumpComplete = null, Action allComplete = null, params Vector3[] positions) {
			if (m_StepQueue.Count > 0)
				return;
			for (int i = 0; i < positions.Length; i++) {
				m_StepQueue.Enqueue (positions [i]);
			}
			CHandleEvent.Instance.AddEvent (HandleJumpToPositions (jumpComplete), allComplete);
		}

		protected virtual IEnumerator HandleJumpToPositions(Action<Vector3> complete = null) {
			while (m_StepQueue.Count > 0) {
				if (GetJumping () == false) {
					var position = m_StepQueue.Peek ();
					AddStep ();
					JumpToPosition (position, () => {
						if (complete != null) {
							complete(position);
						}
						m_StepQueue.Dequeue();
					});
				}
				yield return null;
			}
			yield return null;
		}

		protected virtual void AddStep() {
			m_JumpStep++;
		}

		public virtual void SetPosition(Vector3 position) {
			m_Transform.position = position;
		}

		public virtual Vector3 GetPosition() {
			return m_Transform.position;
		}

		public virtual bool GetJumping() {
			return m_Jumping;
		}

		public virtual int GetJumpStep() {
			return m_JumpStep;
		}

	}
}
