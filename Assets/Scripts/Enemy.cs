using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Charactor {

	private IEnemyState currentState;

	public GameObject Target{get; set;}

	[SerializeField]
	public float meleeRange = 3f;
	public float longRange = 10f;

	public bool InLongRange
	{
		get{
			if(Target != null )
			{
				return Vector2.Distance(transform.position, Target.transform.position) <= longRange && Vector2.Distance(transform.position, Target.transform.position) > meleeRange;
			}
			return false;
		}

	}
	public bool InMeleeRange
	{
		get{
			if(Target != null )
			{
				return Vector2.Distance(transform.position, Target.transform.position) <= meleeRange;
			}
			return false;
		}

	}

    public override bool IsDead
    {
        get
        {
            return health <= 0;
        }
    }

    // Use this for initialization
    public override void Start () {
		
		base.Start();
		ChangeState(new IdleState());
	}
	
	// Update is called once per frame
	void Update () {
		if(!IsDead)
		{
			if(!TakingDamage)
			{
				currentState.Execute();
			}
			
		}
		
		LookAtTarget();
	}

	public void ChangeState(IEnemyState newState)
	{
		if(currentState != null){
			currentState.Exit();
		}
		currentState = newState;
		currentState.Enter(this);
	}

	public void Move()
	{
		MyAnimator.SetFloat("speed", 1);
		transform.Translate( GetDirection() * movementSpeed * Time.deltaTime);
	}

	public Vector2 GetDirection()
	{
		return facingRight ? Vector2.right : Vector2.left;
	}

	
	public override void OnTriggerEnter2D(Collider2D other)
	{
		base.OnTriggerEnter2D(other);
		currentState.OnTriggerEnter(other);

	}

	private void LookAtTarget(){
		if(Target != null )
		{
			float xDir = Target.transform.position.x -  transform.position.x;
			if(xDir < 0 && facingRight || xDir > 0 && !facingRight)
			{
				ChangeDirection();
			}
		}
	}

    public override IEnumerator TakeDamage()
    {
        health -= 10;

		if(!IsDead)
		{
			MyAnimator.SetTrigger("damage");
		}
		else
		{
			MyAnimator.SetTrigger("dead");
			yield return null;
		}
    }


}
