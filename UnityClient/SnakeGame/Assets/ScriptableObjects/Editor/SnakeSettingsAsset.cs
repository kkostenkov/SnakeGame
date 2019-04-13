using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Data;

namespace EditorData
{
	public class SnakeSettingsAsset 
	{

		[MenuItem("Assets/Create/SnakeSettings")]
		public static void CreateAsset ()
		{
			ScriptableObjectUtility.CreateAsset<SnakeSettings> ();
		}
	}
}