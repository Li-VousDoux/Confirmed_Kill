using UnityEngine;
using System.Collections;

public class randomZSkin : MonoBehaviour 
{
	/* Randomizes the textures of zombie enemies.*/

	public Material[] zombieMaterials;

	// Use this for initialization
	void Start () 
	{
		// Assigns a particular material to a zombie.
		SkinnedMeshRenderer myRenderer = GetComponent<SkinnedMeshRenderer>();
		myRenderer.material = zombieMaterials[Random.Range(0, zombieMaterials.Length)];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
