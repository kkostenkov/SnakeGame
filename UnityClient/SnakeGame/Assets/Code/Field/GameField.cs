using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

namespace Field
{
	public class GameField
	{
		private FieldInfo info;
		public void Initialize(FieldInfo info)
		{
			this.info = info;
		}

		public bool IsInBounds(FieldCoords coords)
		{
			return    0 <= coords.X && coords.X <= info.Width 
				&& 0 <= coords.Y && coords.Y <= info.Length;
		}

	}
}