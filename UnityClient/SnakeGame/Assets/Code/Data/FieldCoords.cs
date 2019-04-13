using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
	public struct FieldCoords
	{
		public int X;
		public int Y;

		public FieldCoords(int x, int y)
		{
			X = x;
			Y = y;
		}

		public Vector3 ToVector3()
		{
			return new Vector3(X, 0, Y);
		}

		// public FieldCoords Up {get {return new FieldCoords(X, Y + 1);}}
		// public FieldCoords Right {get {return new FieldCoords(X + 1, Y);}}
		// public FieldCoords Down {get {return new FieldCoords(X, Y - 1);}}
		// public FieldCoords Left {get {return new FieldCoords(X - 1, Y);}}

		public static FieldCoords up {get {return new FieldCoords(0, 1);}}
		public static FieldCoords right {get {return new FieldCoords(1, 0);}}
		public static FieldCoords down {get {return new FieldCoords(0, -1);}}
		public static FieldCoords left {get {return new FieldCoords(-1, 0);}}

		public static bool operator == (FieldCoords a, FieldCoords b)
		{
			return (a.X == b.X) && (a.Y == b.Y);
		}

		public static bool operator != (FieldCoords a, FieldCoords b)
		{
			return !(a == b);
		}

		public static FieldCoords operator + (FieldCoords a, FieldCoords b)
		{
			return new FieldCoords(a.X + b.X, a.Y + b.Y);
		}

		public static FieldCoords operator - (FieldCoords a, FieldCoords b)
		{
			return new FieldCoords(a.X - b.X, a.Y - b.Y);
		}
	}
}
