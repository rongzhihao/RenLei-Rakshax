using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public bool devTestng = false;
	public PhotonView photonView;
	public float moveSpeed = 100f;
	public float jumpForce = 800f;

	private Vector3 selfPos;

	private bool jump;
	public Text plName;
	public GameObject sceneCam;
	public GameObject plCam;

	[SerializeField]
	public Rigidbody2D myRigibody;

	[SerializeField]
	private Transform[] groundPoints;

	[SerializeField]
	private float groundRadius=0.2f;

	[SerializeField]
	private LayerMask whatisGround;

	private bool shouldShootRed;

	private bool shouldShootBlue;

	private bool facingRight = true;

	[SerializeField]
	private float health = 30;
	private bool onGround;

	[SerializeField]
	protected GameObject poisonPrefab;
	[SerializeField]
	protected GameObject antidotePrefab;
	private bool shouldSwitch = false;
	private bool isRedCloth = true; 
	private bool hasRedCloth = true;
	private bool hasBlueCloth = true;

	// Use this for initialization
	void Start () {	
		myRigibody = GetComponent<Rigidbody2D>();
	}

	private void Awake(){
		if(!devTestng && photonView.isMine){
			//sceneCam = GameObject.Find("Main Camera");
			GameObject.Find("Main Camera").SetActive(false);
			plCam.SetActive(true);
		}
	}
	
	// Update is called once per frame
	void Update () {
		HanldeInput();
		//IsDead();
	}
	void FixedUpdate () {
		if(!devTestng) {
			if(photonView.isMine){
				//HanldeInput();
				checkInput();
			}
			else
				smoothNetMovement();
		}else{
			//HanldeInput();
			checkInput();	
		}
		resetParameter();
	}

	private void HanldeInput()
	{
		// if(Input.GetKeyDown(KeyCode.Q)){
		// 	//ShortAttack();
		// }
		if(Input.GetKeyDown(KeyCode.Space)){
			jump = true;
		}

		if(Input.GetKeyDown(KeyCode.Q)){
			shouldShootRed = true;
		}

		if(Input.GetKeyDown(KeyCode.W)){
			shouldShootBlue = true;
		}

		if(Input.GetKeyDown(KeyCode.E)){
			shouldSwitch = true;
		}
	}

	private void checkInput(){
		
		float horizontal = Input.GetAxis("Horizontal");
		var move = new Vector3(horizontal, 0);
		transform.position += move * moveSpeed * Time.deltaTime;
		Flip(horizontal);
		onGround = IsGrounded();
		if(onGround && jump && myRigibody.velocity.y == 0)
		{
			onGround = false;
			myRigibody.AddForce(new Vector2(0, jumpForce));
		}
		if(shouldShootRed || shouldShootBlue)
		{
			bool isPoison = shouldShootRed ? true : false;
			ShootBullet(isPoison);
		}
		if(shouldSwitch)
		{
			if(hasRedCloth && !isRedCloth)
			{
				isRedCloth = !isRedCloth;
				ChangeColor(isRedCloth);
			}
			else if( hasBlueCloth && isRedCloth)
			{
				isRedCloth = !isRedCloth;
				ChangeColor(isRedCloth);
			}
		}

	}

	private void smoothNetMovement(){
		transform.position = Vector3.Lerp(transform.position, selfPos, Time.deltaTime * 8);
	}

	private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
		if(stream.isWriting){
			stream.SendNext(transform.position);
			stream.SendNext(health);
			
		}else{
			selfPos = (Vector3)stream.ReceiveNext();
			this.health = (float)stream.ReceiveNext();
		}
	}

	private bool IsGrounded()
	{
		if(myRigibody.velocity.y <= 0)
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

	private void resetParameter() {
		jump = false;
		shouldShootRed = false;
		shouldShootBlue = false;
		shouldSwitch = false;

	}


	public void ShootBullet(bool isPoison){
	
		float offset = facingRight ? 1 : -1;
		Vector3 bulletInitPlace = new Vector3(transform.position.x + offset, transform.position.y, transform.position.z);
		Debug.Log("isPoison:"+isPoison);
		GameObject bulletPrefab = isPoison ? poisonPrefab : antidotePrefab;
		Debug.Log(bulletPrefab);
		if(facingRight)
		{
			GameObject bullet = (GameObject)PhotonNetwork.Instantiate(bulletPrefab.name, bulletInitPlace, Quaternion.Euler(new Vector3(0, 0, 180)), 0);
			bullet.GetComponent<fireBall>().initialize(Vector2.right);
		}
		else
		{
			GameObject bullet = (GameObject)PhotonNetwork.Instantiate(bulletPrefab.name, bulletInitPlace, Quaternion.Euler(new Vector3(0, 0, 0)), 0);
			bullet.GetComponent<fireBall>().initialize(Vector2.left);
		}
	}

	private void Flip(float horizontal)
    {
        if(horizontal > 0 && !facingRight || horizontal <0 && facingRight)
		{
			facingRight = !facingRight;
			transform.localScale= new Vector3(transform.localScale.x * -1, 1, 1);
		}
    }

	private void TakeDamage()
    {
        health -= 10;
    }

	public void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Bullet")
		{
			TakeDamage();
		}
	}

	private void ChangeColor( bool isRedCloth){
		if(isRedCloth)
		{
			photonView.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
		}
		else
		{
			photonView.gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
		}
	} 
	// private void IsDead()
	// {
	// 	if(health <= 0)
	// 	{
	// 		photonView.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
	// 		//moveSpeed = moveSpeed * 1.1f;
	// 	}
	// }
}
