using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BulletFire : MonoBehaviour 
{
	// f is used to declare a float
	public float timeBetweenBullets = 0.15f;
	public GameObject projectile;

	/* Bullet Info - maxRound max bullets a weapon can carry.*/
	public int maxRounds;
	public int startingRounds;
	public Slider playerAmmoSlider;
    int remainingRounds;

	float nextBullet;

	// Audio Info - This leave the dependency of the weapon to decide between which weapon sound will be prioritized.
	AudioSource gunMuzzleAS;
	public AudioClip shootingSound;
	public AudioClip reloadSound;

	// Use this for initialization
	void Awake () 
	{
		nextBullet = 0f;
		remainingRounds = startingRounds;
		playerAmmoSlider.maxValue = maxRounds;
		playerAmmoSlider.value = remainingRounds;
		gunMuzzleAS = GetComponent<AudioSource> ();

	}
	
	// Update is called once per frame
	void Update () 
	{ 
		// Directing a route for the gun moozle through the root of the directory of the player
		PlayerController myPlayer = transform.root.GetComponent<PlayerController>();

		/* if the person is actually firing the weapon and the next bullet has surpassed it time to live
		 * then the next bullet will be allowed to be shot. The Vector3 corrects an issue when the player is flipped the
		 * direction of the bullets projectile is misdirected. */

		if (Input.GetAxisRaw("Fire1") > 0 && nextBullet < Time.time && remainingRounds > 0) 
		{


			nextBullet = Time.time + timeBetweenBullets;
			// rot = rotation
			Vector3 rot;
			if (myPlayer.GetFacing() == -1f) 
			{
				rot = new Vector3(0, -90, 0);
			} else
				rot = new Vector3(0, 90, 0);

			/* Quaternion surface level allows the axis of rotation to not become locked up during the gameplay
		 *  and is converted to Euler - there's a larger understanding beyond the implementation in the code.
		 *  Hint: It's a mathematical coordinate system  */
			Instantiate(projectile, transform.position, Quaternion.Euler(rot));  

			/*
			// Everytime you pull the trigger, a sound is produced.
			gunMuzzleAS.clip = shootingSound;
			gunMuzzleAS.Play();
			*/

			// Simpler fix that prioritizes a function rather than producing one sound in specific to the action.
			playASound (shootingSound);

			// Players rounds is represented accurately by the AmmoSlider.
			remainingRounds -= 1;
			playerAmmoSlider.value = remainingRounds;
		}
	}

	public void reload()
	{
		remainingRounds = maxRounds;
		playerAmmoSlider.value = remainingRounds;

		/*
		// Everytime you reload your weapon, a sound is produced.
		gunMuzzleAS.clip = shootingSound;
		gunMuzzleAS.Play();
		*/

		// Simpler fix that prioritizes a function rather than producing one sound in specific to the action.
		playASound (reloadSound);
	}

	void playASound(AudioClip playTheSound)
	{
		gunMuzzleAS.clip = playTheSound;
		gunMuzzleAS.Play();
	}

}
