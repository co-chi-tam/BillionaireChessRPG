using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BillianaireChessRPG {
	public class CMapBlocksController : CBaseController {

		[SerializeField]	private string m_MapName = "TutorialMap";
		[SerializeField]	public CBlockController targetBlock;
		[SerializeField]	public List<CBlockController> blocks;

		protected override void Awake ()
		{
			base.Awake ();
		}

		public CBlockController GetBlock(int index) {
			if (index < 0 || index >= blocks.Count)
				return null;
			return blocks[index];
		}

		public CBlockController GetBlockStep(CBlockController current, int step) {
			if (current == null || step <= 0 || step >= blocks.Count)
				return null;
			var currentIndex = blocks.IndexOf(current);
			if (currentIndex == -1)
				return null;
			var maxStep = currentIndex + step;
			if (maxStep >= blocks.Count)
				return blocks[blocks.Count - 1];
			return blocks[maxStep];
		}

		public CBlockController GetBlockPath(CBlockController current, CBlockController target) {
			if (current == null || target == null || current == target)
				return null;
			var currentIndex = blocks.IndexOf(current);
			var tagetIndex = blocks.IndexOf(target);
			if (currentIndex == -1 || tagetIndex == -1)
				return null;
			if (currentIndex + 1 >= blocks.Count)
				return blocks[blocks.Count - 1];
			var step = currentIndex < tagetIndex ? currentIndex + 1 : currentIndex - 1;
			step = step < 0 ? 0 : step >= blocks.Count ? blocks.Count - 1 : step;
			return blocks[step];
		}

	}
}
