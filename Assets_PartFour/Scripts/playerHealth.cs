using UnityEngine;
using System.Collections;
// Access to all Unity UI types
using UnityEngine.UI;
/* 
  General script for players health and ability to take damage.
  Special acknowledgement of contribution/credited towards:
  - Ryan Johnson, RJazzJohnson of the unity forums and youtube, alumini of George Brown College in Toronto, Canada.
  provided contribute to HUD method and conversion of Quaternion.Euler. (None of which verbatium, but helpful tips).
 */

public class playerHealth : MonoBehaviour 
{
	public float fullHealth;
	float currentHealth;

	public GameObject playerDeathFX;

	//HUD
	public Slider playerHealthSlider;
	public Image damageScreen;
	// Allows us to change the color - how bright the screen flashes. Full brightness or partial brighteness
	Color flashColor = new Color(255f,255f,225f,1f);
	// Smooting speed and indication of damage for flash
	float flashSpeed = 5f;
	bool damaged = false;

	// End-game conditions
	public Text endGameText;
	public restartScript theGameController;

	//Reference to audio source
	AudioSource playerAS;

	// Use this for initialization
	void Awake () 
	{
		// Health does not need to start off at full health.
		currentHealth = fullHealth;
		//UI Value is set to fullHealth - maximum value
		playerHealthSlider.maxValue = fullHealth;
		playerHealthSlider.value = currentHealth;
		playerAS = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Are we hurt?
		if (damaged) 
		{
			damageScreen.color = flashColor;
		} else 
		{
			damageScreen.color = Color.Lerp (damageScreen.color, Color.clear, flashSpeed*Time.deltaTime);
		}
		damaged = false;
	
	}
	// Any enemy or object type that harms the player will call this script.
	public void addDamage(float damage)
	{
		currentHealth -= damage;
		// indication to show players current health during run-time
		playerHealthSlider.value = currentHealth;
		damaged = true;

		// Plays the Audio Source of AS
		playerAS.Play ();

		if (currentHealth <= 0) 
		{
			isDead();
		}
	}

	public void addHealth(int health)
	{
		currentHealth += health;
		if (currentHealth > fullHealth)
			currentHealth = fullHealth;
		playerHealthSlider.value = currentHealth;
	}

   public void isDead()
	{
		Instantiate (playerDeathFX, transform.position, Quaternion.Euler (new Vector3(-90, 0, 0)));
		damageScreen.color = flashColor;
		Destroy (gameObject);
		Animator endGameAnim = endGameText.GetComponent<Animator> ();
		endGameAnim.SetTrigger ("endGame");
		theGameController.restartTheGame();
	}
}

