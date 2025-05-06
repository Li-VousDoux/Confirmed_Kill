using UnityEngine;
using System.Collections;

public class zombieController : MonoBehaviour 
{

	public GameObject flipModel; // Portion of the zombie model to flip direction

	// Audio Options - Array of zombie sounds
	public AudioClip[] idleSounds;
	public float idleSoundTime; // How often a zombie can make an idle sound
	AudioSource zombieMovementAS;

	public GameObject ragdollDead;

	float nextIdleSound = 0f; // 0f so that it occurs immediatetly
	public float detectionTime; // How much time between when the player is detected
	float startRun; // When the zombie begins to run
	bool firstDetection; // Once detected - the player is always detected, but the decision between animation is based on this.

	// Movement Option - How fast the zombie can run.
	public float runSpeed;
	public float walkSpeed;
	public bool facingRight = true; // Zombies are not generated (They're placed facing right) so they have to be mapped to cycle direction.

	float moveSpeed;
	bool running;

	Rigidbody myRB;
	Animator myAnim;
	Transform detectedPlayer; // Save the position in relation to the zombie.
	bool Detected; // True or False - player is detected

	// Use this for initialization
	void Start () 
	{
		myRB = GetComponentInParent<Rigidbody> ();
		myAnim = GetComponentInParent<Animator> ();
		zombieMovementAS = GetComponent<AudioSource> ();

		running = false; // Intial movmemnt will not be running;
		Detected = false;
		firstDetection = false; 
		moveSpeed = walkSpeed; // Changes depending what state detection is in.

		if (Random.Range(0, 10) > 5) 
		{
			Flip();
		}
	}

	// Physics Object
	void FixedUpdate () 
	{
		if (Detected) 
		{
			if(detectedPlayer.position.x < transform.position.x && facingRight)
			{
				Flip ();
			} 
			else if (detectedPlayer.position.x > transform.position.x && !facingRight)
			{
				Flip();
			}
		}

		if (!firstDetection) 
		{
			startRun = Time.time + detectionTime;
			firstDetection = true;
		}

		if (Detected && !facingRight) 
		{
			myRB.velocity = new Vector3((moveSpeed *- 1), myRB.velocity.y, 0); // Changes X, Gravity effects Y and Z is not changed.
		}
		else if (Detected && facingRight) 
		{
			myRB.velocity = new Vector3(moveSpeed, myRB.velocity.y, 0);
		}

		// Gets the zombie moving if the player is detected
		if(!running && Detected)
		{
			if(startRun < Time.time)
			{
				moveSpeed = runSpeed;
				myAnim.SetTrigger("Run");
				running = true;
			}
		}

		// idle or walking sounds, cycles through an array of idle noises provided the zombie is not running  
		// Provided by:
		if(!running)
		{
			if(Random.Range (0, 10) > 5 && nextIdleSound < Time.time)
			{
				AudioClip tempClip = idleSounds[Random.Range (0, idleSounds.Length)];
				zombieMovementAS.clip = tempClip;
				zombieMovementAS.Play ();
				nextIdleSound = idleSoundTime + Time.time;
			}
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player" && !Detected) 
		{
			Detected = true; // detects player;
			detectedPlayer = other.transform; // finds players position and direction.
			myAnim.SetBool("Detection", Detected); // Moves from animation idle to walking after direction.

			/* Players position vs Zombies Position and is not facing current direction - it will flip the zombie to face the player. */
			if(detectedPlayer.position.x < transform.position.x && facingRight)
			{
				Flip ();
			} else if (detectedPlayer.position.x > transform.position.x && !facingRight)
			{
				Flip();
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player") 
		{
			firstDetection = false;
		}
		if (running) 
		{
			myAnim.SetTrigger("Run");
			moveSpeed = walkSpeed;
			running = false;
		}
	}

	/* Intially flips the Zombie model left or right once (random direction) based off the "Z" direction of the zombie model
		   This is achieved by multiplying the scale by -1 and reassigning the new direction with the "-Z". Same as the playerController.*/
	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = flipModel.transform.localScale;
		theScale.z *=-1;
		flipModel.transform.localScale = theScale;
	}

	/* 
	 * Having a hard time instantiating this script correctly --
	 * 
	 * */

	public void ragdollDeath()
	{
		GameObject ragDoll = Instantiate (ragdollDead, transform.root.transform.position, Quaternion.identity) as GameObject;

		// Find the master of the root node of the rig of the ragdoll and the zombie - syncs the orientation.
		Transform ragDollMaster = ragDoll.transform.Find ("master");
		Transform zombieMaster = transform.root.Find ("master");

		// Working around issue that comes with Flip function.
		bool wasFacingRight = true;
		if (!facingRight) 
		{
			wasFacingRight = false;
			Flip ();
		}

		// Find tranforms of the joint of the ragdoll & current zombie
		Transform[] ragdollJoints = ragDollMaster.GetComponentsInChildren<Transform> ();
		Transform[] zombieJoints = zombieMaster.GetComponentsInChildren<Transform> ();

		// Iterates through all joints and set them to 0.
		for (int i = 0; i < ragdollJoints.Length; i++) 
		{
			for (int q = 0; q < zombieJoints.Length; q++) 
			{
				if(zombieJoints[q].name.CompareTo(ragdollJoints[i].name) == 0)
				{
					ragdollJoints[i].position = zombieJoints[q].position;
					ragdollJoints[i].rotation = zombieJoints[q].rotation;
					break;
				}
			}
		}

		// Making rotations of zombie and ragdoll the same.
		if (wasFacingRight) 
		{
			Vector3 rotVector = new Vector3 (0, 0, 0);
			ragDoll.transform.rotation = Quaternion.Euler(rotVector);
		}
		else 
		{
			Vector3 rotVector = new Vector3(0, 90, 0);
			ragDoll.transform.rotation = Quaternion.Euler(rotVector);
		}

		// Matching the randomiztion of zombie material/mesh information
		Transform zombieMesh = transform.root.transform.Find ("zombieSoilder");
		Transform ragdollMesh = ragDoll.transform.Find ("zombieSoilder");

		/* Theres an occuring issue where - when the player kills the zombie and it attempts to match the same skin - it instantiates.
		 * A new zombie in its place which has no HP and still pursues and kills the player while dropping ragdolls. */

		// ragdollMesh.GetComponent<Renderer>().material = zombieMesh.GetComponent<Renderer>().material;
	}

}
