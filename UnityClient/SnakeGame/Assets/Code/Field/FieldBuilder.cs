using System.Collections.Generic;
using UnityEngine;
using Data;


namespace Field
{
	public class FieldBuilder : MonoBehaviour 
	{
		[SerializeField]
		private Transform fieldTilePrefab;

		[SerializeField]
		private float bonusSpawnHeight = 2.7f;
		[SerializeField]
		private float tilesSpawnHeight = 0f;

		private Dictionary<Bonus, Transform> bonusPrefabs;

		private Transform fieldRoot;
		private List<Transform> spawnedTiles = new List<Transform>();
		private Dictionary<FieldCoords, Transform> spawnedBonuses = new Dictionary<FieldCoords, Transform>();

		void Awake()
		{
			fieldRoot = this.transform;
		}

		public void SetBonusPrefabs(Dictionary<Bonus, Transform> bonusPrefabs)
		{
			this.bonusPrefabs = bonusPrefabs;
		}
		public void Build(FieldInfo info)
		{
			for (int x = 0; x < info.Width; x++)
			{
				for (int z = 0; z < info.Length; z++)
				{
					var newTile = GameObject.Instantiate(fieldTilePrefab, fieldRoot);
					newTile.position = new Vector3(x, tilesSpawnHeight, z);
					spawnedTiles.Add(newTile);
				}
			}
		}

		public void DestroyTiles()
		{
			for (int i = 0; i < spawnedTiles.Count; i++)
			{
				var tile = spawnedTiles[i];
				GameObject.Destroy(tile.gameObject);
			}
			spawnedTiles.Clear();
		}

		public void SpawnBonus(Bonus what, FieldCoords where)
		{
			Transform prefab;
			if (bonusPrefabs.TryGetValue(what, out prefab))
			{
				var pos = where.ToVector3();
				pos.y = bonusSpawnHeight;
				var bonusView = GameObject.Instantiate(prefab, pos, Quaternion.identity, fieldRoot);
				spawnedBonuses.Add(where, bonusView);
			}
		}

		public void DestroyBonus(FieldCoords where)
		{
			Transform bonusView;
			if (spawnedBonuses.TryGetValue(where, out bonusView))
			{
				spawnedBonuses.Remove(where);
				GameObject.Destroy(bonusView.gameObject);
			}
		}
	}
}