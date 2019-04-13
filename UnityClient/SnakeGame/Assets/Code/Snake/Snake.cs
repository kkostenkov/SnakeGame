using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
using Field;
using InputListener;

namespace Hero
{
	public class Snake 
	{
		private SnakeSettings settings;

		private List<BodySegment> segments = new List<BodySegment>();
		private Transform bodyContainer;

		private IInputListener input;
		private GameField field;
		private float timeTillStep;
		public Snake(SnakeSettings settings)
		{
			this.settings = settings;
		}

		public void SetControls(IInputListener input)
		{
			this.input = input;
		}


		public void SetField(GameField field)
		{
			this.field = field;
		}
		public void Spawn()
		{
			bodyContainer = new GameObject("Snake").transform;
			for (var i = 0; i < settings.StartSegmentsCount; i++)
			{
				var segment = new BodySegment();
				segment.Position = new FieldCoords(i, 0);
				segments.Add(segment);
				var pos = segment.Position.ToVector3();
				var segmentView = GameObject.Instantiate(settings.BodySegment, pos, Quaternion.identity, bodyContainer);
				segment.SetView(segmentView);
			}
			timeTillStep = 2f;
		}

		public void Destroy()
		{
			if (bodyContainer != null)
			{
				GameObject.Destroy(bodyContainer.gameObject);
				bodyContainer = null;
			}
			segments.Clear();
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
			CheckCollision();	
			timeTillStep = settings.StepTime;
		}

		private void MakeStep()
		{
			var firstSegmentPos = segments[0].Position;
			var direction = input.GetLastDirection();
			FieldCoords deltaPos;
			switch (direction)
			{
				case MoveDirection.Up:
					deltaPos = FieldCoords.up;
					break;
				case MoveDirection.Right:
					deltaPos = FieldCoords.right;
					break;
				case MoveDirection.Down:
					deltaPos = FieldCoords.down;
					break;
				case MoveDirection.Left:
					deltaPos = FieldCoords.left;
					break;
				default:
					throw new NotImplementedException(string.Format("Unknown move direction", direction));
			}
			FieldCoords nextPos = firstSegmentPos + deltaPos;
			if (segments.Count > 1)
			{
				var secondSegmentPos = segments[1].Position;
				if (nextPos == secondSegmentPos)
				{
					// incorrect direction input
					nextPos = firstSegmentPos - deltaPos;
				}
			}
			
			for (int i = 0; i < segments.Count; i++)
			{
				var segment = segments[i];
				var oldPos = segment.Position;
				segment.Position = nextPos;
				segment.View.position = nextPos.ToVector3();

				nextPos = oldPos;
			}
		}

		private void CheckCollision()
		{
			var firstSegmentPos = segments[0].Position;
			var isOutOfField = !field.IsInBounds(firstSegmentPos);
			if (isOutOfField)
			{
				Debug.Log("head is out of field");
				Destroy();
			}
			if (segments.Count > 1)
			{
				bool isCollidedWithSelf = false;
				for (int i = 1; i < segments.Count; i++)
				{
					if (firstSegmentPos == segments[i].Position)
					{
						isCollidedWithSelf = true;
						break;
					}
				}
				if (isCollidedWithSelf)
				{
					Debug.Log("Collided with self");
					Destroy();
				}
			}
		}
	}
}