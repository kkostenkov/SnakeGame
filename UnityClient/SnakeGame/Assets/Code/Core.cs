using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
using Field;

namespace Core
{
	public class Core : MonoBehaviour 
	{
		[SerializeField] private FieldBuilder field;
		[SerializeField] private SnakeSettings settings;
		void Start () 
		{
			CreateGameField();
		}
		
		void Update () 
		{
			
		}

		private void CreateGameField()
		{
			var info = new FieldInfo()
			{
				Width = settings.Width,
				Length = settings.Length,
			};
			field.Build(info);
		}

		public void OnGUI()
		{
			if (GUILayout.Button("build"))
			{	
			}
			if (GUILayout.Button("destroy"))
			{
			}
		}
	}
}