using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public bool devTestng = false;
	public PhotonView photonView;
	public static float moveSpeed = 10f;
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
	private Color[] clothArray = {Color.red, Color.blue}; // poison and anitdote 
	private int currentCloth = 0;

	private bool canMove = true;

	private string recoverStand = "RecoverStand";

	private bool isRecovering = false;

	private bool canBeCharge = false;




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

		if(Input.GetKeyDown(KeyCode.T)){
			isRecovering = true;
		}
	}

	private void checkInput(){
		if(canMove){
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
				ChangeColor();
			}
			if(isRecovering && canBeCharge)
			{
				StartToRecovering();
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
		isRecovering = false;
	}


	public void ShootBullet(bool isPoison){
	
		float offset = facingRight ? 1 : -1;
		Vector3 bulletInitPlace = new Vector3(transform.position.x + offset, transform.position.y, transform.position.z);
		GameObject bulletPrefab = isPoison ? poisonPrefab : antidotePrefab;

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

	private void TakeDamage(Color color)
    {
        clothArray[currentCloth] = color;
		photonView.gameObject.GetComponent<MeshRenderer>().material.color = clothArray[currentCloth];
        //int playerID = PhotonNetwork.player.ID;
        //Tab.clothOn[playerID - 1] = color;
        int nowColor = PhotonNetwork.player.GetScore();
        //Debug.Log("hit:" + PhotonNetwork.player.ID);
        if (color == Color.red)
        {
            if (nowColor == 2)
                PhotonNetwork.player.SetScore(0);
            if (nowColor == 3)
                PhotonNetwork.player.SetScore(1);
        }
        if (color == Color.blue)
        {
            if (nowColor == 1)
                PhotonNetwork.player.SetScore(3);
            if (nowColor == 0)
                PhotonNetwork.player.SetScore(2);
        }
            
       
    }

	public void OnTriggerEnter2D(Collider2D other)
	{
        Debug.Log("sha:"+PhotonNetwork.player.ID);
        
        if (other.tag == "antidote")
		{
			TakeDamage(Color.blue);
		}else if( other.tag == "poison" ){
			TakeDamage(Color.red);
		}else if(other.gameObject.tag == recoverStand){
			canBeCharge = true;
		}
	}
   


	private void ChangeColor(){
		currentCloth = (currentCloth + 1) % 2;
		photonView.gameObject.GetComponent<MeshRenderer>().material.color = clothArray[currentCloth];
        if (PhotonNetwork.player.GetScore() == 1 || PhotonNetwork.player.GetScore() == 2)
            PhotonNetwork.player.SetScore(3 - PhotonNetwork.player.GetScore());
        /*
        Color t = Color.white;
        int playerID = PhotonNetwork.player.ID;
        Debug.Log("id"+playerID);
        t = Tab.clothOn[playerID - 1];
        Tab.clothOn[playerID - 1] = Tab.clothOff[playerID - 1];
        Tab.clothOff[playerID - 1] = t;*/
	} 
	// private void IsDead()
	// {
	// 	if(health <= 0)
	// 	{
	// 		photonView.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
	// 		//moveSpeed = moveSpeed * 1.1f;
	// 	}
	// }

	void OnCollisionEnter2D(Collision2D other){

		if(other.gameObject.tag == recoverStand){
			canBeCharge = true;
		}
	}

	void OnCollisionExit2D(Collision2D other){

		if(other.gameObject.tag == recoverStand){
			//canBeCharge = false;
		}
	}

	private void moveCount(){
		canMove = true;
		CancelInvoke();
		if(true){
			
			if( clothArray[currentCloth] == Color.red ){
				clothArray[(currentCloth + 1) % 2] = Color.blue;
				ChangeColor();
			}else{
				clothArray[(currentCloth + 1) % 2] = Color.red;
				ChangeColor();
			}
		}
	}

	private void StartToRecovering () {
		canMove = false;
		InvokeRepeating("moveCount",2f,1f);
	}
}
