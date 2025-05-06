using UnityEngine;
using System.Collections;

public class explosionController : MonoBehaviour 
{

	public Light explosionLight;
	public float power; // Explosive Power
	public float radius; // Radius of the explosion
	public float damage; // Damage the explosion will cause
	// Use this for initialization
	void Start () 
	{
		Vector3 explosionPosition = transform.position;
		Collider[] colliders = Physics.OverlapSphere (explosionPosition, radius);
		foreach (Collider hit in colliders) 
		{
			Rigidbody RB = hit.GetComponent<Rigidbody>(); // Looking for a RigidBody

			if(RB != null) RB.AddExplosionForce(power, explosionPosition, radius, 3.0f, ForceMode.Impulse);
			// Apply a force to a rigidbody inside the radius with Force Impulse
			if(hit.tag == "Player")
			{
				playerHealth thePlayerHealth = hit.gameObject.GetComponent<playerHealth>();
				thePlayerHealth.addDamage(damage);
			} else if (hit.tag == "Enemy")
			{
				enemyHealth theEnemyHealth = hit.gameObject.GetComponent<enemyHealth>();
				theEnemyHealth.addDamage(damage);
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Light goes from full intensity then decreases overtime.
		explosionLight.intensity = Mathf.Lerp (explosionLight.intensity, 0f, 5 * Time.time);
	}
}
