using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController : Charactor {

	//private Rigidbody2D myRigibody;
	
	private static characterController instance;

	public static characterController Instance
	{
		get{
			if(instance == null)
			{
				instance = GameObject.FindObjectOfType<characterController>();
			}

			return instance;
		}
	}
	

	[SerializeField]
	private Transform[] groundPoints;
	[SerializeField]
	private float groundRadius;

	[SerializeField]
	private LayerMask whatisGround;

	public Rigidbody2D MyRigibody {get; set;}

	

	public bool OnGround {get; set;}


	public override void Start () {
		base.Start();
		MyRigibody = GetComponent<Rigidbody2D>();
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		HanldeInput();
	}
	void FixedUpdate () 
	{
		float horizontal = Input.GetAxis("Horizontal");
		OnGround = IsGrounded();
		HandleMovement(horizontal);
		Flip(horizontal);
		HandleLayer();
		//HandleAttack();
		//ResetParameters();
	}

    private void Flip(float horizontal)
    {
        if(horizontal > 0 && !facingRight || horizontal <0 && facingRight)
		{
			base.ChangeDirection();
		}
    }

    private void HandleMovement(float horizontal)
    {
		if(MyRigibody.velocity.y < 0){
			MyAnimator.SetBool("land", true);
		}
		if(!Attack){
			MyRigibody.velocity = new Vector2(horizontal * movementSpeed, MyRigibody.velocity.y);
		}
		// if(!this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack") )
		// {
		// 	MyRigibody.velocity = new Vector2(horizontal * movementSpeed, MyRigibody.velocity.y);
	    // 	myAnimator.SetFloat("speed", Mathf.Abs(horizontal));
		// }

		if(OnGround && Jump && MyRigibody.velocity.y == 0){
			OnGround = false;
			MyRigibody.AddForce(new Vector2(0, jumpFrouce));
		}

		MyAnimator.SetFloat("speed", Mathf.Abs(horizontal));
	}

	private void HanldeInput()
	{
		if(Input.GetKeyDown(KeyCode.Q))
		{
			// if( !attack && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
			// {
			// 	attack = true;
			// }
			ShortAttack();
		}
		if(Input.GetKeyDown(KeyCode.Space)){
			MyAnimator.SetTrigger("jump");
		}

		if(Input.GetKeyDown(KeyCode.W)){
			LongAttack();
		}
		
	}
	// private void HandleAttack(){
	// 	if(attack){
	// 		myAnimator.SetTrigger("zombieAttack");
	// 		myRigibody.velocity = new Vector2(0, myRigibody.velocity.y);
	// 	}

	// }

	// private void ResetParameters()
	// {
	// 	attack = false;
	// 	jump = false;
	// }

	private bool IsGrounded()
	{
		if(MyRigibody.velocity.y <= 0)
		{
			foreach(Transform point in groundPoints)
			{
				Collider2D[] collider = Physics2D.OverlapCircleAll(point.position, groundRadius, whatisGround);
				foreach(Collider2D colliderItem in collider){
					if(colliderItem.gameObject != gameObject){
						
						return true;
					}
				}
			}	
		}
		return false;
	}

	private void HandleLayer()
	{
		if(!OnGround)
		{
			MyAnimator.SetLayerWeight(1,1);
		}else
		{
			MyAnimator.SetLayerWeight(1,0);
		}
	}
}
