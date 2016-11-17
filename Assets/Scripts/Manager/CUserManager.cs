using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace BillianaireChessRPG {
	public class CUserManager : CMonoSingleton<CUserManager> {

		[SerializeField]	public CUserData user;
		[SerializeField]	public string mapSelectedPath = "Data/Map/TutorialMap";

		public Action<CHeroController> OnLoadHero;

		protected override void Awake ()
		{
			base.Awake ();
			DontDestroyOnLoad (this.gameObject);
			LoadUserData ();
		}

		public void LoadUserData() {
			var userJSON = Resources.Load<TextAsset> ("Data/User/UserData");
			this.user = TinyJSON.JSON.Load (userJSON.text).Make<CUserData> ();
		}

		public void LoadHeroData() {
			if (user == null)
				return;
			StartCoroutine (HandleLoadData ("Prefabs/Hero/Barbarian", "Prefabs/Hero/Priest"));
		}

		private IEnumerator HandleLoadData(params string[] heroes) {
			for (int i = 0; i < heroes.Length; i++) {
				var heroController = Instantiate (Resources.Load <CHeroController> (heroes[i]));
				yield return heroController != null;
				if (OnLoadHero != null) {
					OnLoadHero (heroController);
				}
			}
		}

	}
}
