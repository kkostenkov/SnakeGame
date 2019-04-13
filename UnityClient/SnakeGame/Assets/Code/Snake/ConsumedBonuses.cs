using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

namespace Hero
{
	public class ConsumedBonuses
	{
		private List<FieldCoords> consumedBonuses = new List<FieldCoords>();
		public void Apply(FieldCoords coords)
		{
			consumedBonuses.Add(coords);
		}
	}
}