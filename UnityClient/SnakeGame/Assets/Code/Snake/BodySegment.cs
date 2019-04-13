using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

namespace Hero
{
	public class BodySegment 
	{
		public FieldCoords Position;
		public Transform View {get; private set;}

		public void SetView(Transform v)
		{
			View = v;
		}
	}
}