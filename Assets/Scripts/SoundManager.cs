using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
	//Singleton pattern to keep the same music on the first scenes (title, main menu levels...)

	public static SoundManager Instance = null;

	private void Awake()
	{		
		// If there is not already an instance of the SoundManager, set it to this game object
		if (Instance == null)
		{
			Instance = this;
		}
		//If there is an instance, destroy yourself
		else if (Instance != this)
		{
			Destroy(gameObject);
		}
		//prevent from destroying when when loading another scene.
		DontDestroyOnLoad(gameObject);
	}


	

}
