using UnityEngine;
using System.Collections;

public class Cleaner : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	// if player fall into box collider, destroy the player.
	// prevents player from falling forever.
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") 
		{
			playerHealth playerDead = other.gameObject.GetComponent<playerHealth>();
			playerDead.isDead();
		} else Destroy (other.transform.root.gameObject);
	}
}
