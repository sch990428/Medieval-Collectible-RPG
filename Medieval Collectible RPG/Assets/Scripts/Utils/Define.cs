using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
	// 영웅의 직군을 정의합니다
	public enum HeroClass
	{
		None,
		Tanker,
		Warrior,
		Sorcerer,
		Ranger,
		Support
	}

	// 영웅의 속성을 정의합니다
	public enum HeroType
	{
		None,
		Water,
		Fire,
		Earth,
		Light,
		Dark
	}
}
