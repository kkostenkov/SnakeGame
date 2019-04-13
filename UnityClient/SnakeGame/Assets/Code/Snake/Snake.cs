using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Data;
using Field;
using InputListener;

namespace Hero
{
	public class Snake 
	{
		private SnakeSettings settings;
		private ConsumedBonuses bonuses = new ConsumedBonuses();

		private List<BodySegment> segments = new List<BodySegment>();
		private Transform bodyContainer;

		private IInputListener input;
		private GameField field;
		private float timeTillStep;
		private float stepTime;
		private Vector3 fatSegmentScale = Vector3.one * 1.3f;
		public Snake(SnakeSettings settings)
		{
			this.settings = settings;
			stepTime = settings.StepTime;
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
			var startCoords = settings.StartCoords;
			for (var i = 0; i < settings.StartSegmentsCount; i++)
			{
				var coords = startCoords + new FieldCoords(i, 0);
				SpawnBodySegment(coords);
			}
			timeTillStep = settings.StartupPauseTime;
		}

		private void SpawnBodySegment(FieldCoords coords)
		{
			var segment = new BodySegment();
			segment.Position = coords;
			segments.Add(segment);
			var pos = segment.Position.ToVector3();
			var segmentView = GameObject.Instantiate(settings.BodySegment, pos, Quaternion.identity, bodyContainer);
			segment.SetView(segmentView);
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
			if (CheckCollision())
			{
				return;
			}
			ConsumeBonuses();
			timeTillStep = stepTime;
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
				segment.View.localScale = bonuses.Has(nextPos) ? fatSegmentScale : Vector3.one;

				nextPos = oldPos;
			}
			if (bonuses.Use(nextPos))
			{
				SpawnBodySegment(nextPos);
			}
		}

		private bool CheckCollision()
		{
			bool isCollided = false;
			var firstSegmentPos = segments[0].Position;
			var isOutOfField = !field.IsInBounds(firstSegmentPos);
			if (isOutOfField)
			{
				Debug.Log("head is out of field");
				Destroy();
				isCollided = true;
			}
			if (segments.Count > 1 && !isCollided)
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
					isCollided = true;
				}
			}
			return isCollided;
		}

		private void ConsumeBonuses()
		{
			var firstSegmentPos = segments[0].Position;
			var bonus = field.TryConsumeBonusAt(firstSegmentPos);
			var bonusSettings = settings.Bonuses.FirstOrDefault(bs => bs.type == bonus);
			if (bonusSettings != null)
			{
				stepTime -= bonusSettings.SpeedIncrease;
			}
			
			switch (bonus)
			{
				case Bonus.Growth:
					bonuses.Apply(firstSegmentPos);		
					break;
				case Bonus.Speed:
					break;
				default:
					return;
			}
		}
	}
}