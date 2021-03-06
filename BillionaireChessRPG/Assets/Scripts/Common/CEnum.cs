﻿using System;
using System.Collections;

public class CEnum {

	public enum EObjectType: byte {
		None 		= 0,
		Hero 		= 1,
		Monster 	= 2,
		Trap 		= 3,
		Chest 		= 4,
		Object		= 5,
		User 		= 6
	}

	public enum EGameState: byte {
		None 		= 0,
		Loading 	= 1,
		StartGame 	= 2,
		Processing 	= 3,
		StartTurn	= 4,
		UpdateTurn	= 5,
		EndTurn		= 6,
		EndGame 	= 7
	}

	public enum ETeam: byte {
		None 		= 0,
		Team_A 		= 1,
		Team_B 		= 2,
		Neutron 	= 10
	}

	public enum EUIState: byte {
		Idle 		= 0,
		Battle		= 10,
		RollDice	= 20,
		EndGame		= 30,
		CloseGame 	= 40
	}

	public enum ETurnState: byte {
		None 		= 0,
		StartTurn 	= 1,
		UpdateTurn 	= 2,
		EndTurn 	= 3
	}

	public enum EAnimation : int {
		Idle 		= 0,
		Attack_1 	= 10,
		Attack_2 	= 11,
		Attack_3 	= 12,
		Attack_4 	= 13,
		Attack_5 	= 14,
		Attack_6 	= 15,
		Attack_7 	= 16,
		Attack_8 	= 17,
		Attack_9 	= 18,
		Attack_10 	= 19,
		Move 		= 20,
		Death 		= 100
	}

	public enum EAttackType : int  {
		None 		= 0,
		Physic 		= 1,
		Magic 		= 2,
		Pure 		= 3
	}

	public enum EStatusType : int  {
		None 		= 0,
		Health 		= 1,
		Mana 		= 2,
		Lucky 		= 3
	}

	public enum EEnviromentType : int {
		None 		= 0,
		Nature 		= 1,
		Magmar 		= 2,
		Ice 		= 3,
		Desert 		= 4,
		Marsh 		= 5
	}

	public enum EContructionLevel : int {
		None = 0,
		Level1 = 1,
		Level2 = 2,
		Level3 = 3
	}

	public enum EItemType: byte {
		None = 0,
		Common = 1,
		Uncommon = 2,
	}

	public enum EItem: int {
		None = 0,
		// Food
		Rice = 1,
		Fish = 2,

		// Nature
		Nature = 20,
		Water = 21,
		Fire = 22
	}

	public enum EBlockDirection: byte {
		Center = 0,
		Left = 1,
		Top = 2,
		Right = 3,
		Bottom = 4,
		TopLeft = 5,
		TopRight = 6,
		BottomRight = 7,
		BottomLeft = 8
	}

}
