using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace InputListener
{
#if UNITY_STANDALONE
public class StandaloneInputListener : IInputListener
	{
		public void CustomUpdate(float deltaTime) 
		{
			if (Input.GetKeyDown(KeyCode.UpArrow))
			{
				lastInputDirection = MoveDirection.Up;
			}
			if (Input.GetKeyDown(KeyCode.RightArrow))
			{
				lastInputDirection = MoveDirection.Right;
			}
			if (Input.GetKeyDown(KeyCode.DownArrow))
			{
				lastInputDirection = MoveDirection.Down;
			}
			if (Input.GetKeyDown(KeyCode.LeftArrow))
			{
				lastInputDirection = MoveDirection.Left;
			}
		}

		private MoveDirection lastInputDirection;

		public MoveDirection GetLastDirection()
		{
			return lastInputDirection;
		}
	}
#endif
}