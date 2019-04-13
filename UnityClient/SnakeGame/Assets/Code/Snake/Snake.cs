using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
using InputListener;

namespace Hero
{
	public class Snake 
	{
		private SnakeSettings settings;

		private Transform bodyContainer;
		private List<Transform> bodySegments = new List<Transform>();

		private IInputListener input;
		private float timeTillStep;
		public Snake(SnakeSettings settings)
		{
			this.settings = settings;
		}

		public void SetControls(IInputListener input)
		{
			this.input = input;
		}

		public void Spawn()
		{
			bodyContainer = new GameObject("Snake").transform;
			var containerPos = bodyContainer.position;
			for (var i = 0; i < settings.StartSegmentsCount; i++)
			{
				var pos = new Vector3(containerPos.x + i, containerPos.y, containerPos.z);
				var segment = GameObject.Instantiate(settings.BodySegment, pos, Quaternion.identity, bodyContainer);
				bodySegments.Add(segment);
			}
			timeTillStep = 2f;
		}

		public void Destroy()
		{
			GameObject.Destroy(bodyContainer.gameObject);
			bodyContainer = null;
			bodySegments.Clear();
		}

		public void CustomUpdate(float deltaTime)
		{
			if (bodyContainer == null)
			{
				return;
			}
			timeTillStep -= deltaTime;
			if (timeTillStep > 0)
			{
				return;
			}
			MakeStep();
			timeTillStep = settings.StepTime;
		}

		private void MakeStep()
		{
			var firstSegmentPos = bodySegments[0].position;
			var direction = input.GetLastDirection();
			var deltaPos = Vector3.zero;
			switch (direction)
			{
				case MoveDirection.Up:
					deltaPos += Vector3.forward;
					break;
				case MoveDirection.Right:
					deltaPos += Vector3.right;
					break;
				case MoveDirection.Down:
					deltaPos += Vector3.back;
					break;
				case MoveDirection.Left:
					deltaPos += Vector3.left;
					break;
				default:
					throw new NotImplementedException(string.Format("Unknown move direction", direction));
			}
			Vector3 nextPos = firstSegmentPos + deltaPos;
			if (bodySegments.Count > 1)
			{
				var secondSegmentPos = bodySegments[1].position;
				if (nextPos == secondSegmentPos)
				{
					// incorrect direction input
					nextPos = firstSegmentPos - deltaPos;
				}
			}
			
			for (int i = 0; i < bodySegments.Count; i++)
			{
				var segment = bodySegments[i];
				var oldPos = segment.position;
				segment.position = nextPos;
				nextPos = oldPos;
			}
		}
	}
}