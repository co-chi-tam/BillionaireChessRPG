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

			GUILayout.Space (20f);
			if (GUILayout.Button ("Add All Child To List")) {
				var controller = target as CMapBlocksController;
				AddAllChildToList (controller, controller.transform.FindChild("Blocks").gameObject);
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

	}
}
