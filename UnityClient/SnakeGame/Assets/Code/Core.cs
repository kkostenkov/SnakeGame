using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;
using Field;
using Hero;
using InputListener;

namespace Core
{
	public class Core : MonoBehaviour 
	{
		[SerializeField] private FieldBuilder fieldBuilder;
		[SerializeField] private SnakeSettings settings;

		private Snake snake;
		private GameField field;
		private ServiceProvider services;
		void Start () 
		{
			services = new ServiceProvider();
			CreateGameField();
			CreateSnake();
		}
		
		void Update () 
		{
			var deltaTime = Time.deltaTime;
			services.CustomUpdate(deltaTime);
			snake.CustomUpdate(deltaTime);
		}

		private void CreateGameField()
		{
			var info = new FieldInfo()
			{
				Width = settings.Width,
				Length = settings.Length,
			};
			field = new GameField();
			field.Initialize(info);
			fieldBuilder.Build(info);
		}

		private void CreateSnake()
		{
			snake = new Snake(settings);
			snake.SetControls(services.InputListener);
			snake.SetField(field);
			snake.Spawn();
			
		}

		public void OnGUI()
		{
			if (GUILayout.Button("reset"))
			{	
				snake.Destroy();
				CreateSnake();	
			}
			if (GUILayout.Button("destroy"))
			{
			}
		}
	}
}