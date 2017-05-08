using UnityEngine;
using System.Collections;

namespace Dice {
	// [RequireComponent (typeof (Rigidbody))]
	public class CDiceController : MonoBehaviour {

		public enum EDiceValue: int {
			None 	= 0,
			One 	= 1,
			Two 	= 2,
			Three 	= 3,
			Four 	= 4,
			Five 	= 5,
			Six 	= 6
		}

		[SerializeField]	private EDiceValue m_DiceValue;
		[SerializeField]	private bool m_FixDiceValue;

		private Transform m_DiceTransform;
		private Rigidbody m_DiceRigidbody;
		private Vector3 m_LatePosition;
		private bool m_Rolling;

		private Vector3 oneValue 	= new Vector3 (270f, 0f, 0f);
		private Vector3 twoValue 	= new Vector3 (0f, 0f, 180f);
		private Vector3 threeValue 	= new Vector3 (0f, 0f, 270f);
		private Vector3 fourValue 	= new Vector3 (0f, 0f, 90f);
		private Vector3 fiveValue 	= new Vector3 (0f, 0f, 0f);
		private Vector3 sixValue 	= new Vector3 (90f, 0f, 0f);

		private void Awake () {
			// Parameter
			m_DiceTransform = this.GetComponent<Transform> ();
			m_DiceRigidbody = this.GetComponent<Rigidbody> ();
			m_Rolling = false;
			// Prevert position
			m_LatePosition = m_DiceTransform.position;
		}

		private void Start() {
			// Scroll dice
			this.RollDice ();
		}

		private void LateUpdate() {
			if (m_DiceRigidbody.velocity == Vector3.zero) {
				m_DiceValue = CheckDiceValue ();
				m_Rolling = false;
			}
			if (Input.GetKeyDown (KeyCode.A)) {
				RollDice ();
			}
		}
		
		public void RollDice() {
			// Return position
			m_DiceRigidbody.transform.position = m_LatePosition;		
			// Free Rigidbody
			m_DiceRigidbody.useGravity = true;
			m_DiceRigidbody.isKinematic = false;
			m_DiceRigidbody.constraints = RigidbodyConstraints.None;
			// Random rotation
			m_DiceTransform.rotation = Quaternion.Euler (Random.insideUnitSphere * 45f);
			// Rolling
			m_Rolling = true;
		}

		public EDiceValue CheckDiceValue() {
			if (m_Rolling == true)
				return EDiceValue.None;
			var rotationDice = this.m_DiceTransform.rotation.eulerAngles;
			rotationDice.y = 0f;
			// Check dice value
			if (((rotationDice - oneValue).sqrMagnitude < 0.1f)) {
				return EDiceValue.One;
			} else if (((rotationDice - twoValue).sqrMagnitude < 0.1f)) {
				return EDiceValue.Two;
			} else if (((rotationDice - threeValue).sqrMagnitude < 0.1f)) {
				return EDiceValue.Three;
			} else if (((rotationDice - fourValue).sqrMagnitude < 0.1f)) {
				return EDiceValue.Four;
			} else if (((rotationDice - fiveValue).sqrMagnitude < 0.1f)) {
				return EDiceValue.Five;
			} else if (((rotationDice - sixValue).sqrMagnitude < 0.1f)) {
				return EDiceValue.Six;
			} 
			return EDiceValue.None;
		}

	}
}
