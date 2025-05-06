using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
/* 
  Special acknowledgement of contribution/credited towards:
  - Ryan Johnson, RJazzJohnson of the unity forums and youtube, alumini of George Brown College in Toronto, Canada.
  provided contribute in correction toward getfacing(), fixedUpdate() and flip() method (None of which verbatium, but helpful tips).
 */
{
	// movement variable
	public float runSpeed;
	public float walkSpeed;
	public bool running;

	Rigidbody myRB;
	Animator myAnim;
	bool facingRight;

	// for jumping
	bool Grounded = false;
	Collider[] groundCollisions;
	float groundCheckRadius = 0.2f;
	public LayerMask groundLayer;
	public Transform groundCheck;
	public float jumpHeight;


	// Use this for initialization
	void Start() 
	{
		myRB = GetComponent<Rigidbody>();
		myAnim = GetComponent<Animator>();
		facingRight = true;
	}
	
	// Update is called once per frame (constant)
	void Update() 
	{
	
	}
	// Update is called after physics engine has been run (# of frames determined)
	void FixedUpdate()
	{
		running = false;

		if (Grounded && Input.GetAxis("Jump") > 0) 
		{
			Grounded = false;
			myAnim.SetBool ("Grounded", Grounded);
			myRB.velocity = new Vector3(myRB.velocity.x, 0, 0);
			myRB.AddForce(new Vector3(0, jumpHeight, 0));
		}
			            
		groundCollisions = Physics.OverlapSphere (groundCheck.position, groundCheckRadius, groundLayer);
		if (groundCollisions.Length > 0)
			Grounded = true;
		else
			Grounded = false;

		myAnim.SetBool ("Grounded", Grounded);

		//jumping animation blending
		myAnim.SetFloat ("VerticalSpeed", myRB.velocity.y);

		float move = Input.GetAxis("Horizontal");
		myAnim.SetFloat("Speed",Mathf.Abs(move));

		float Sneaking = Input.GetAxisRaw ("Fire3");
		// Coordinated to the left-shift button
		myAnim.SetFloat ("Sneaking", Sneaking);

		float firing = Input.GetAxis("Fire1");
		myAnim.SetFloat("Shooting", firing);
		 
		if (Sneaking > 0 || firing > 0 && Grounded ) 
		{ 
			myRB.velocity = new Vector3 (move * walkSpeed, myRB.velocity.y, 0);
		} else {
			myRB.velocity = new Vector3 (move * runSpeed, myRB.velocity.y, 0);
			if (Mathf.Abs(move) > 0) running = true;
		}

		if (move > 0 && !facingRight) Flip();
		else if (move < 0 && facingRight) Flip();

	}

	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.z *= -1;
		transform.localScale = theScale;
	}

	public float GetFacing()
	{
		if(facingRight) return 1;
		else return -1;
	}

	public bool GetRunning()
	{
		return (running);
	}
}
