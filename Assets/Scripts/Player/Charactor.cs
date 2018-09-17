using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Charactor : MonoBehaviour {

	[SerializeField]
	protected float movementSpeed = 15f;
	protected bool facingRight = true;
	[SerializeField]
	protected GameObject bulletPrefab;

	[SerializeField]
	protected int health;

	public abstract bool IsDead{get; }
	
	[SerializeField]
	private EdgeCollider2D handCollider;

	[SerializeField]
	protected float jumpFrouce = 500f;

	public bool TakingDamage{get; set;}
	public bool Attack {get; set;}

	public bool Jump {get; set;}

	public Animator MyAnimator{get; private set;}

	// Use this for initialization
	public virtual void Start () {
		MyAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void ChangeDirection()
    {
		facingRight = !facingRight;
		transform.localScale= new Vector3(transform.localScale.x * -1, 1, 1);
	}

	public void LongAttack(){
		
		if(facingRight)
		{
			GameObject bullet = (GameObject)Instantiate(bulletPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 180)));
			bullet.GetComponent<fireBall>().initialize(Vector2.right);
		}
		else
		{
			GameObject bullet = (GameObject)Instantiate(bulletPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
			bullet.GetComponent<fireBall>().initialize(Vector2.left);
		}
	}
    
	public void ShortAttack(){
		MyAnimator.SetTrigger("attack");
	}

	public abstract IEnumerator TakeDamage();

	public virtual void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "FireBall")
		{
			StartCoroutine(TakeDamage());
		}
	}

	public void MeleeAttack()
	{
		handCollider.enabled = !handCollider.enabled;
	}
}
