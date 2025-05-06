using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/* Script access the player canvas UI health bar and interactables */

public class enemyHealth : MonoBehaviour 
{

	public float enemyMAXHealth;
	public float damageModifier; // Multiplier
	public GameObject damageParticles;
	public bool drops;
	public GameObject drop; // Reward for killing the enemy
	public AudioClip deathSounds;

	float currentHealth; 

	public Slider enemyHealthIndicator;

	AudioSource enemyAS;

	// Use this for initialization
	void Awake () 
	{
		currentHealth = enemyMAXHealth;
		enemyHealthIndicator.maxValue = enemyMAXHealth;
		enemyHealthIndicator.value = currentHealth;
		enemyAS = GetComponent<AudioSource> ();

	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	/* Public access to the enemies current health. Any damaging interaction will subtract to the enemies health and visually by the GUI
	 Prevents multiple current health bars from being displayed at a time*/
	public void addDamage(float damage)
	{
		enemyHealthIndicator.gameObject.SetActive(true); // Turns on the visibility of the game object;
		damage = damage * damageModifier;
		if (damage <= 0)
			return;
		// currentHealth = currentHealth - damage;
		currentHealth -= damage;
		enemyHealthIndicator.value = currentHealth;
		enemyAS.Play ();
		if (currentHealth <= 0)
			makeDead();
	}

	void makeDead()
	{
		// turn off movement & create ragdoll
		// Destorys entire hierarchy of zombie
		/* Instantiate the zombie model to the zombie ragdoll mob as closesly to realistically as possible. 
		   Found from the ZombieController flip() method.*/


		zombieController aZombie = GetComponentInChildren<zombieController> ();
		if (aZombie != null) 
		{
			aZombie.ragdollDeath();
		}



		AudioSource.PlayClipAtPoint(deathSounds, transform.position, 0.15f);

		Destroy (gameObject.transform.root.gameObject);
		if (drops) Instantiate (drop, transform.position, transform.rotation);

	}

	public void damageFX (Vector3 point, Vector3 rotation)
	{
		Instantiate (damageParticles, point, Quaternion.Euler (rotation));
	}
}
