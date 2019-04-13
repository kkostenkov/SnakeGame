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

	}
}