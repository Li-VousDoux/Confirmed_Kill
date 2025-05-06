using UnityEngine;
using System.Collections;

public class healthPickupController : MonoBehaviour 
{
	public int healthAmount;
	public AudioClip healthPickupSound;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") 
		{
			// Calls the PlayerHealth script and destroys objectt so that the player only uses it once.
			other.GetComponent<playerHealth>().addHealth(healthAmount);
			Destroy (transform.root.gameObject);
			// Allow the sound to played from a specific location regardless of the gameObject destruction.
			AudioSource.PlayClipAtPoint(healthPickupSound, transform.position, 1f);
		}
	}
}
