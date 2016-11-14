using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BillianaireChessRPG {
	 public class CMapManager : CMonoSingleton<CMapManager> {

		[SerializeField]	public CMapData map;
		[SerializeField]	public List<CMapBlocksController> mapBlockControllers;

		protected override void Awake ()
		{
			base.Awake ();
			mapBlockControllers = new List<CMapBlocksController> ();
		}

		public void LoadData() {
			if (map == null)
				return;
			StartCoroutine (HandleLoadData ());
		}

		private IEnumerator HandleLoadData() {
			for (int i = 0; i < map.mapModels.Length; i++) {
				var mapBlocks = Instantiate (Resources.Load <CMapBlocksController> (map.mapModels[i]));
				yield return mapBlocks != null;
				mapBlocks.transform.position = Vector3.zero;
				mapBlockControllers.Add (mapBlocks);
			}
		}

		public CBlockController GetTargetBlock(int mapIndex) {
			return mapBlockControllers [mapIndex].targetBlock;
		}

		public CBlockController GetBlock(int mapIndex, int index) {
			return mapBlockControllers[mapIndex].GetBlock(index);
		}

		public CBlockController GetBlockStep(int mapIndex, CBlockController current, int step) {
			return mapBlockControllers[mapIndex].GetBlockStep(current, step);
		}

		public CBlockController GetBlockPath(int mapIndex, CBlockController current, CBlockController target) {
			return mapBlockControllers [mapIndex].GetBlockPath (current, target);
		}

	}
}
