using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

namespace BillianaireChessRPG {
	[CanEditMultipleObjects]
	[CustomEditor (typeof(CMapBlocksController))]
	public class CMapBlocksControllerInspector : Editor {

		public override void OnInspectorGUI ()
		{
			base.OnInspectorGUI ();

			var controller = target as CMapBlocksController;

			GUILayout.Space (20f);
			if (GUILayout.Button ("Add All Child To List")) {
				AddAllChildToList (controller, controller.transform.FindChild("Blocks").gameObject);
			}
			if (GUILayout.Button ("Update Monster Block")) {
				UpdateMonsterCurrentBlock (controller, controller.transform.FindChild("Monsters").gameObject);
			}
			if (GUILayout.Button ("Update Trap Block")) {
				UpdateMonsterCurrentBlock (controller, controller.transform.FindChild("Traps").gameObject);
			}
			if (GUILayout.Button ("Update Chest Block")) {
				UpdateMonsterCurrentBlock (controller, controller.transform.FindChild("Chests").gameObject);
			}

		}

		private void AddAllChildToList(CMapBlocksController target, GameObject parent) {
			var childCount = parent.transform.childCount;
			target.blocks = new System.Collections.Generic.List<CBlockController> ();
			for (int i = 0; i < childCount; i++) {
				var child = parent.transform.GetChild (i);
				var block = child.GetComponent<CBlockController> ();
				if (block != null) {
					target.blocks.Add (block);
				}
			}
		}

		private void UpdateMonsterCurrentBlock(CMapBlocksController target, GameObject parent) {
			if (target.blocks.Count == 0)
				return;
			var childCount = parent.transform.childCount;
			for (int i = 0; i < childCount; i++) {
				var child = parent.transform.GetChild (i);
				var objControl = child.GetComponent<CObjectController> ();
				var objPosition = child.transform.position;
				for (int x = 0; x < target.blocks.Count; x++) {
					var block = target.blocks [x];
					var blockPosition = block.transform.position;
					if (objPosition.x == blockPosition.x
					    && objPosition.y == blockPosition.y
					    && objPosition.z == blockPosition.z) {
						objControl.SetCurrentBlock (block);
						objControl.SetTargetBlock (block);
						break;
					}
				}
			}
		}

	}
}
