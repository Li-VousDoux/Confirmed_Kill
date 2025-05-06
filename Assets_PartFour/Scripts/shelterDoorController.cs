using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class shelterDoorController : MonoBehaviour {

	AudioSource shelterDoorAS;

	bool inShelter = false;

	public Text endGameText;
	public restartScript theGameController;

	// Use this for initialization
	void Start () 
	{
		shelterDoorAS = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	// Prevents enemies from triggering door. inShelter prevents a player from triggering endGameText more than once.
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" && inShelter == false) 
		{
			Animator shelterDoorAnim = GetComponentInChildren<Animator>();
			shelterDoorAnim.SetTrigger("shelterTrigger");
			shelterDoorAS.Play();

			endGameText.text = "Found Shelter";

			Animator endGameAnim = endGameText.GetComponent<Animator> ();
			endGameAnim.SetTrigger ("endGame");
			theGameController.restartTheGame();
			inShelter = true;
		}
	}
}
