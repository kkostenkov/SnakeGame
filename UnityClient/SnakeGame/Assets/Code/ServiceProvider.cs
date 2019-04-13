using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputListener;

public class ServiceProvider
{
	private IInputListener inputListener;
	public IInputListener InputListener 
	{
		get 
		{ 
			if (inputListener == null)
			{
#if UNITY_STANDALONE
				inputListener = new StandaloneInputListener();
#endif
			}
			return inputListener;
		}
	}

	public void CustomUpdate(float deltaTime)
	{
		InputListener.CustomUpdate(deltaTime);
	}
}
