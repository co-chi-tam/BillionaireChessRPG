using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace BillianaireChessRPG {
	public class CUserManager : CMonoSingleton<CUserManager> {

		[SerializeField]	public CUserData user;
		[SerializeField]	public TextAsset mapSelectedText;

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
			var hero1JSON = Resources.Load<TextAsset> ("Data/Character/WarriorData");
			var hero1Data = TinyJSON.JSON.Load (hero1JSON.text).Make<CCharacterData> ();
			var hero2JSON = Resources.Load<TextAsset> ("Data/Character/PriestData");
			var hero2Data = TinyJSON.JSON.Load (hero2JSON.text).Make<CCharacterData> ();

			StartCoroutine (HandleLoadData (hero1Data, hero2Data));
		}

		private IEnumerator HandleLoadData(params CCharacterData[] heroes) {
			for (int i = 0; i < heroes.Length; i++) {
				var heroData = heroes [i];
				var heroController = Instantiate (Resources.Load <CHeroController> (heroData.modelPath));
				heroController.SetData (heroData);
				yield return heroController != null;
				if (OnLoadHero != null) {
					OnLoadHero (heroController);
				}
			}
		}

	}
}
