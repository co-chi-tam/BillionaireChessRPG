using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using FSM;

namespace BillianaireChessRPG {
	public class CMapManager : CMonoSingleton<CMapManager>, IContext {

		[SerializeField]	private string m_StateName;
		[SerializeField]	public CMapData mapData;
		[SerializeField]	public List<CMapBlocksController> mapBlockControllers;

		private FSMManager m_FSMManager;

		protected override void Awake ()
		{
			base.Awake ();
			mapBlockControllers = new List<CMapBlocksController> ();
			m_FSMManager = new FSMManager ();	
		}

		protected override void Start ()
		{
			base.Start ();
			OnRegisterFSM ();
		}

		protected override void UpdateBaseTime (float dt)
		{
			base.UpdateBaseTime (dt);
			m_FSMManager.UpdateState (dt);
			m_StateName = m_FSMManager.currentStateName;
		}

		protected virtual void OnRegisterFSM() {
			var mapIdle = new FSMMapIdleState (this);
		
			m_FSMManager.RegisterState ("MapIdleState", mapIdle);
		}

		public void LoadMapObject() {
			if (mapData == null)
				return;
			StartCoroutine (HandleLoadMapObject ());
		}

		public void LoadMapData() {
			if (mapData == null)
				return;
			var jsonText 		= Resources.Load<TextAsset>(mapData.mapFSMPath);
			m_FSMManager.LoadFSM (jsonText.text);
		}

		private IEnumerator HandleLoadMapObject() {
			for (int i = 0; i < mapData.mapModels.Length; i++) {
				var mapBlocks = Instantiate (Resources.Load <CMapBlocksController> (mapData.mapModels[i]));
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
