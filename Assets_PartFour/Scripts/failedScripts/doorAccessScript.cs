using UnityEngine;
using System.Collections;


/* Absolute Script
 * 
public class doorAccessScript2 : MonoBehaviour 
{
	// Script allows the player to access interiors through door animation sequences.
	// Animator shelterDoorAnim;
	

	// Use this for initialization
	void Start()
	{
		Animator shelterDoorAnim = GetComponentInChildren<Animator>();
	}

	// Calls animation trigger "accessDoor"
	void OnTriggerEnter(Collider other) 
	{
		if (other.tag == "Player") 
		{
			shelterDoorAnim.SetTrigger ("accessDoor");
			shelterDoorAudio.Play ();
		}
	}
	
	// Returns animation state back to forward.
	void OnTriggerExit(Collider other) 
	{
		myAnim.enabled = true;
	}

	// Pause the animation state - prevents door from constantly opening and closing.
	void pauseAnimationEvent()
	{
		myAnim.enabled = false;
	}
}
*/