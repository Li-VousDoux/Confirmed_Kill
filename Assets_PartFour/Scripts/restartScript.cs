using UnityEngine;
using System.Collections;

public class restartScript : MonoBehaviour 
{
	public float restartTime;
	bool resetNow = false;
	float resetTime;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (resetNow && resetTime <= Time.time) 
		{
			Application.LoadLevel(Application.loadedLevel);
		}

		if (Input.GetKey("escape"))
		{
		   Application.Quit();
		}
	}

	public void restartTheGame()
	{
		resetNow = true;
		resetTime = restartTime + Time.time;
	}

	// Ability to close the application
	public void quitGame()
	{
		Application.Quit();
	}
}
