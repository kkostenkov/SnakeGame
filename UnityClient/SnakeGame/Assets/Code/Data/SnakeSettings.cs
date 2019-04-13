using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
	public class SnakeSettings : ScriptableObject
	{
		[HeaderAttribute("Gamefield size")]
		public int Width = 5;
		public int Length = 6;

		[HeaderAttribute("Hero")]
		public Transform BodySegment;
		public int StartSegmentsCount = 2;

		public float StepTime = 0.5f;

		[HeaderAttribute("Bonuses")]
		public List<BonusSettting> Bonuses = new List<BonusSettting>();

	}

	[Serializable]
	public class BonusSettting
	{
		public Bonus type;
		public Transform prefab;
		public int Frequency;
	}
}