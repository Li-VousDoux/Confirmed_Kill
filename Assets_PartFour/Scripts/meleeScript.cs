using UnityEngine;
using System.Collections;

public class meleeScript : MonoBehaviour 
{
	public float damage;
	public float knockBack;
	public float knockBackRadius;
	public float meleeRate;

	float nextMelee;

	int bulletMask;

	Animator myAnim;
	PlayerController myPC;
	// Use this for initialization
	void Start () 
	{
		bulletMask = LayerMask.GetMask ("Destructible");
		myAnim = transform.root.GetComponent<Animator> ();
		myPC = transform.root.GetComponent<PlayerController> ();
		nextMelee = 0f;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		float melee = Input.GetAxis ("Fire2");
	if (melee > 0 && nextMelee < Time.time && !(myPC.GetRunning())) 
		{
			myAnim.SetTrigger("GunMelee");
			nextMelee = Time.time + meleeRate;

			// do damage - Return a list of objects that collides with the sphere.
			Collider[] attacked = Physics.OverlapSphere(transform.position, knockBackRadius, bulletMask);

			// Loops through attack colliders - the length of the attack is based of the enemy, the addDamage and the enemyHealth script.
			// The effect are in regards to the enemies transform and position.
			int i=0;
			while(i < attacked.Length)
			if(attacked[i].tag == "Enemy")
			{
				enemyHealth doDamage = attacked[i].GetComponent<enemyHealth>();
				doDamage.addDamage(damage);
				doDamage.damageFX(transform.position, transform.localEulerAngles);
			}
		}
	}
}
