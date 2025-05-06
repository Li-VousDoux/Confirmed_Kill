using UnityEngine;
using System.Collections;

public class BulletTime : MonoBehaviour 
{

	public float range = 10f;
	public float damage = 5f;

	Ray bulletRay;
	RaycastHit bulletImpact;
	int bulletMask;
	LineRenderer bulletStream;

	// Use this for initialization
	void Awake () 
	{
		bulletMask = LayerMask.GetMask("Destructible");
		bulletStream = GetComponent<LineRenderer>();

		bulletRay.origin = transform.position;
		bulletRay.direction = transform.forward;
		bulletStream.SetPosition(0, transform.position);

		if(Physics.Raycast(bulletRay, out bulletImpact, range, bulletMask)) 
		{ 
			// enemy damage goes here
			if(bulletImpact.collider.tag == "Enemy")
			{
				enemyHealth theEnemyHealth = bulletImpact.collider.GetComponent<enemyHealth>();
				if(theEnemyHealth != null)
				{
					theEnemyHealth.addDamage(damage);
					theEnemyHealth.damageFX(bulletImpact.point, -bulletRay.direction);
				}

			}
			bulletStream.SetPosition(1, bulletImpact.point);
		} else bulletStream.SetPosition(1, bulletRay.origin+bulletRay.direction*range);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
