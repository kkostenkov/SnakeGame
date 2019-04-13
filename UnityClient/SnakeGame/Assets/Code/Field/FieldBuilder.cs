using System.Collections.Generic;
using UnityEngine;
using Data;


namespace Field
{
	public class FieldBuilder : MonoBehaviour 
	{
		[SerializeField]
		private Transform fieldTilePrefab;

		private Transform fieldRoot;
		private List<Transform> spawnedTiles = new List<Transform>();

		void Awake()
		{
			fieldRoot = this.transform;
		}
		public void Build(FieldInfo info)
		{
			var height = 0;
			for (int x = 0; x < info.Width; x++)
			{
				for (int z = 0; z < info.Length; z++)
				{
					var newTile = GameObject.Instantiate(fieldTilePrefab, fieldRoot);
					newTile.position = new Vector3(x, height, z);
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
	}
}