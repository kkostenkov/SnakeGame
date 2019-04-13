using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Data;

namespace Field
{
	public class GameField
	{
		private FieldInfo info;
		private SnakeSettings settings;
		private FieldBuilder builder;
		private Dictionary<FieldCoords, Bonus> bonusesOnMap = new Dictionary<FieldCoords, Bonus>();
		private System.Random random = new System.Random();
		public void Initialize(FieldBuilder builder, SnakeSettings settings)
		{
			this.settings = settings;
			this.info = new FieldInfo()
			{
				Width = settings.Width,
				Length = settings.Length,
			};
			this.builder = builder;
			var bonusPrefabs = new Dictionary<Bonus, Transform>();
			for(int i = 0; i < settings.Bonuses.Count; i++)
			{
				var b= settings.Bonuses[i];
				bonusPrefabs.Add(b.type, b.prefab);
			}
			builder.SetBonusPrefabs(bonusPrefabs);
		}

		public void Build()
		{
			builder.Build(info);
			var firstBonusLocation = new FieldCoords(info.Width / 2, info.Length / 2);
			SpawnBonus(Bonus.Growth, firstBonusLocation);
		}

		public void Reset()
		{
			bonusesOnMap.Clear();
			builder.Destroy();
			Build();
		}

		public void SpawnBonus(Bonus bonus)
		{
			var where = new FieldCoords()
			{
				X = random.Next(info.Width),
				Y = random.Next(info.Length),
			};
			SpawnBonus(bonus, where);
		}

		public void SpawnBonus(Bonus bonus, FieldCoords where)
		{
			if (bonusesOnMap.ContainsKey(where))
			{
				Debug.Log("Bonus spawn location is occupied.");
				return;
			}
			bonusesOnMap.Add(where, bonus);
			builder.SpawnBonus(bonus, where);
		}

		public bool IsInBounds(FieldCoords coords)
		{
			return    0 <= coords.X && coords.X < info.Width 
				&& 0 <= coords.Y && coords.Y < info.Length;
		}

		private int consumedCount;
		public Bonus TryConsumeBonusAt(FieldCoords coords)
		{
			Bonus res = Bonus.None;
			if (bonusesOnMap.TryGetValue(coords, out res))
			{
				bonusesOnMap.Remove(coords);
				builder.DestroyBonus(coords);
				consumedCount++;
				// check respawn
				var bonusesToSpawn = settings.Bonuses.Where(b => consumedCount % b.Frequency == 0);
				foreach(var bonus in bonusesToSpawn)
				{
					SpawnBonus(bonus.type);
				}
			}
			return res;
		}
	}
}