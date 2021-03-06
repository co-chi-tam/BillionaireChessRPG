﻿using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

namespace BillianaireChessRPG {
	[CanEditMultipleObjects]
	[CustomEditor (typeof(CObjectController), true)]
	public class CObjectInspector : Editor {

		private IStatus m_Target;

		public override void OnInspectorGUI ()
		{
			base.OnInspectorGUI ();
			m_Target = target as IStatus;
			if (Application.isPlaying == false)
				return;
			GUILayout.Label ("***Status***");
			EditorGUILayout.LabelField ("Active:", m_Target.GetActive ().ToString ());
			EditorGUILayout.LabelField ("Team:", m_Target.GetTeam ().ToString ());
			EditorGUILayout.LabelField ("Animation:", m_Target.GetAnimation ().ToString ());
			EditorGUILayout.LabelField ("Turn State:", m_Target.GetTurnState ().ToString ());
			EditorGUILayout.LabelField ("Object Type:", m_Target.GetObjectType ().ToString ());
			EditorGUILayout.LabelField ("Team:", m_Target.GetTeam ().ToString ());
			EditorGUILayout.LabelField ("FSM State Name:", m_Target.GetFSMStateName ());
			EditorGUILayout.LabelField ("FSM Path:", m_Target.GetFSMName ());
			GUILayout.Label ("***Data***");
			EditorGUILayout.LabelField ("HP:", m_Target.GetCurrentHealth() + " / " + m_Target.GetMaxHealth());
			EditorGUILayout.LabelField ("MP:", m_Target.GetCurrentMana() + " / " + m_Target.GetMaxMana());
			EditorGUILayout.LabelField ("LP:", m_Target.GetLuckyPoint().ToString ());
			EditorGUILayout.LabelField ("Pure Damage:", m_Target.GetPureDamage().ToString ());
			EditorGUILayout.LabelField ("Physic Damage:", m_Target.GetPhysicDamage().ToString ());
			EditorGUILayout.LabelField ("Magic Damage:", m_Target.GetMagicDamage().ToString ());
			EditorGUILayout.LabelField ("Physic Defend:", m_Target.GetPhysicDefend().ToString ());
			EditorGUILayout.LabelField ("Magic Defend:", m_Target.GetMagicDefend().ToString ());
			EditorGUILayout.LabelField ("Gold Reward:", m_Target.GetGoldReward().ToString ());
		}

	}
}
