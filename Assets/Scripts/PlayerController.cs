using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private static PlayerController instance;

    public static PlayerController Instance
    {
        get {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<PlayerController>();
            }

            return instance;
        }
    }


    /*
     * GET ACCESS TO PlayerPickup SCRIPT
     * After shoot, please call "shootBlueBullet" or "shootRedBullet" to update the display status of slots
     * like playerPickup.shootBlueBullet or playerPickup.shootRedBullet
     */
    public PlayerPickup playerPickup;


    private float stayTomb = 0;
    private float stayHouse = 0;
    private bool humanAdd = false;
    private bool zombieAdd = false;
    public bool devTestng = false;
    public PhotonView photonView;
    public float moveSpeed = 6f;
    public float jumpForce = 800f;
    public static float playerX;
    public static float playerY;
    private Vector3 selfPos;

    private Quaternion selfRot;
    [SerializeField]
    private EdgeCollider2D handCollider;

    public bool Jump { get; set; }
    public Text plName;
    public GameObject sceneCam;
    public GameObject plCam;

    [SerializeField]
    private Transform[] groundPoints;

    [SerializeField]
    private float groundRadius = 0.2f;

    [SerializeField]
    private LayerMask whatisGround;

    private bool shouldShootRed;

    private bool shouldShootBlue;

    private bool facingRight = true;

    //[SerializeField]
    //private float health = 30;
    public bool onGround { get; set; }

    [SerializeField]
    protected GameObject poisonPrefab;
    [SerializeField]
    protected GameObject antidotePrefab;
    private bool shouldSwitch = false;
    private string[] clothArray = { "HumanIdle", "ZombieIdle" }; // poison and anitdote 
    public int currentCloth = 0;

    public static bool canMove = true;

    private string recoverStand = "RecoverStand";

    private bool isRecovering = false;

    private bool canBeCharge = false;

    public int redBullet { get; set; }
    public int blueBullet { get; set; }

    public Animator MyAnimator { get; private set; }
    public Rigidbody2D MyRigibody { get; set; }

    [SerializeField]
    private bool hasLongAttack;
    public float humanSpeed = 6f;
    public float zombieSpeed = 10f;

    public GameObject zombiePrefab;
    public GameObject humanPrefab;

    private string humanAnimator = "HumanIdle";
    private string zombieAnimator = "ZombieIdle";

    private string humanAnimatorPath = "Controllers/HumanIdle";
    private string zombieAnimatorPath = "Controllers/ZombieIdle";

    // Use this for initialization
    void Start()
    {
        MyRigibody = GetComponent<Rigidbody2D>();
        MyAnimator = GetComponent<Animator>();

    }

    private void Awake()
    {
        if (!devTestng && photonView.isMine)
        {
            //sceneCam = GameObject.Find("Main Camera");
            GameObject.Find("Main Camera").SetActive(false);
            plCam.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.isMine)
        {
            playerX = this.GetComponent<Transform>().position.x;
            playerY = this.GetComponent<Transform>().position.y;
        }
        
        //Debug.Log(verti)
        if (GameObject.FindGameObjectsWithTag("ExitGameButton").Length != 0) {
            return;
        }
        if (humanAdd)
        {
            stayHouse += Time.deltaTime;
            
        }
        else
        {
            stayHouse = 0;
        }
        if (zombieAdd)
        {
            stayTomb += Time.deltaTime;
        }
        else
        {
            stayTomb = 0;
        }
        if (stayHouse >= 3)
        {
            TakeDamage(humanAnimator);
        }
        if (stayTomb >= 3)
        {
            TakeDamage(zombieAnimator);
        }
        HanldeInput();
        ResetLocation();
        ChangeColor();
        //IsDead();
    }
    void FixedUpdate()
    {
        if (!devTestng)
        {
            if (photonView.isMine)
            {
                //HanldeInput();
                checkInput();
            }
            else
                smoothNetMovement();
        }
        else
        {
            //HanldeInput();
            checkInput();
        }
        resetParameter();
        HandleLayer();
    }

    private void HanldeInput()
    {
        // if(Input.GetKeyDown(KeyCode.Q)){
        // 	//ShortAttack();
        // }
        Debug.Log("onGround:"+onGround);
        if ( (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDownMobile("Jump")) && onGround && (MyRigibody.velocity.y == 0) )
        {
            Jump = true;
            // onGround = false;
            MyRigibody.AddForce(new Vector2(0, jumpForce));
        }

        if (Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDownMobile("Fire1"))
        {
            shouldShootBlue = true;
        }

        if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDownMobile("Fire3"))
        {
           // shouldSwitch = true;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            isRecovering = true;
        }
    }

    private void checkInput()
    {
        if (canMove)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            float angle = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg;

            var move = new Vector3(horizontal, 0);
            transform.position += move * moveSpeed * Time.deltaTime;//movespeed
            Flip(horizontal);
            onGround = IsGrounded();
            MyAnimator.SetFloat("speed", Mathf.Abs(horizontal));
            MyAnimator.SetFloat("verticalSpeed", MyRigibody.velocity.y);


            if (onGround && Jump && MyRigibody.velocity.y == 0)
            {
                onGround = true;
            }

            if (shouldShootBlue && !hasLongAttack) {
                ShootBullet(true);
                redBullet--;
            }

            if (shouldShootBlue && blueBullet > 0 && hasLongAttack) {
                ShootBullet(false);
                blueBullet--;
            }

            // if( (shouldShootRed || shouldShootBlue) && !hasLongAttack ){
            //     ShortAttack();
            // }

            if (shouldSwitch)
            {
                currentCloth = (currentCloth + 1) % 2;
                transferCloth();
                shouldSwitch = false;
            }

            if (isRecovering && canBeCharge)
            {
                Debug.Log(isRecovering);
                Debug.Log(canBeCharge);
                StartToRecovering();
            }
        }
    }

    private void smoothNetMovement()
    {
        transform.position = Vector3.Lerp(transform.position, selfPos, Time.deltaTime * 8);
        transform.rotation = Quaternion.Lerp(transform.rotation, selfRot, Time.deltaTime * 8);
    }

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(currentCloth);
            //stream.SendNext(this.MyAnimator);
            //stream.SendNext(health);

        }
        else
        {
            selfPos = (Vector3)stream.ReceiveNext();
            selfRot = (Quaternion)stream.ReceiveNext();
            //gameObject.GetComponent<MeshRenderer>().material.color = (Color)stream.ReceiveNext();
            this.currentCloth = (int)stream.ReceiveNext();
            //this.MyAnimator = (Animator)stream.ReceiveNext
        }
    }

    private bool IsGrounded()
    {
        if (MyRigibody.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] collider = Physics2D.OverlapCircleAll(point.position, groundRadius, whatisGround);
                foreach (Collider2D colliderItem in collider)
                {
                    if (colliderItem.gameObject != gameObject)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private void resetParameter()
    {
        Jump = false;
        shouldShootRed = false;
        shouldShootBlue = false;
        isRecovering = false;
    }

    /*
       Parameter isPoison: true is red, false is blue
     */
    public void ShootBullet(bool isPoison)
    {

        float offset = facingRight ? 1.2f : -1.2f;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float angle = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg;

        float offsetx = 1.2f * Mathf.Sin(angle * Mathf.PI / 180);
        float offsety = 1.2f * Mathf.Cos(angle * Mathf.PI / 180);

        float initx = 1.5f * Mathf.Sin(angle * Mathf.PI / 180);
        float inity = 1.5f * Mathf.Cos(angle * Mathf.PI / 180);

        if (horizontal == 0 && vertical == 0)
        {
            offsetx = facingRight ? 1.2f : -1.2f;
            offsety = 0;
            initx = facingRight ? 1.5f : -1.5f;
            inity = 0;
            angle = 0;
        }

        Vector3 bulletInitPlace = new Vector3(transform.position.x + offsetx, transform.position.y + offsety, transform.position.z);//Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")
        GameObject bulletPrefab = isPoison ? poisonPrefab : antidotePrefab;
        Vector2 vector2 = new Vector2(initx, inity);

        /*if (facingRight)
        {
            GameObject bullet = (GameObject)PhotonNetwork.Instantiate(bulletPrefab.name, bulletInitPlace, Quaternion.Euler(new Vector3(0, 0, 0)), 0);
            
            bullet.GetComponent<fireBall>().initialize(vector2);
        }
        else
        {
            GameObject bullet = (GameObject)PhotonNetwork.Instantiate(bulletPrefab.name, bulletInitPlace, Quaternion.Euler(new Vector3(0, 0, 180)), 0);
            bullet.GetComponent<fireBall>().initialize(vector2);
        }*/

        GameObject bullet = (GameObject)PhotonNetwork.Instantiate(bulletPrefab.name, bulletInitPlace, Quaternion.Euler(new Vector3(0, 0, angle)), 0);

        bullet.GetComponent<fireBall>().initialize(vector2);

        MyAnimator.SetTrigger("attack");
    }

    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            GameObject nameText = GameObject.Find("Name").gameObject;
            nameText.transform.localScale = new Vector3(nameText.transform.localScale.x * -1, nameText.transform.localScale.y, nameText.transform.localScale.z);
        }
    }

    private void ChangeColor()
    {
        //currentCloth = (currentCloth + 1) % 2;
        //photonView.gameObject.GetComponent<MeshRenderer>().material.color = clothArray[currentCloth];
        if (clothArray[currentCloth] == humanAnimator) {
            hasLongAttack = true;
            moveSpeed = humanSpeed;
            photonView.gameObject.GetComponent<Animator>().runtimeAnimatorController = Resources.Load(humanAnimatorPath) as RuntimeAnimatorController;
        } else {
            hasLongAttack = false;
            moveSpeed = zombieSpeed;
            photonView.gameObject.GetComponent<Animator>().runtimeAnimatorController = Resources.Load(zombieAnimatorPath) as RuntimeAnimatorController;
        }

    }
    private void transferCloth()
    {
        //if (PhotonNetwork.player.GetScore() == 1 || PhotonNetwork.player.GetScore() == 2)
        //  PhotonNetwork.player.SetScore(3 - PhotonNetwork.player.GetScore());
        PhotonNetwork.player.SetScore(1 - PhotonNetwork.player.GetScore());
    }
    private void TakeDamage(string animator)
    {
        if (clothArray[currentCloth] != animator)
        {
            Debug.Log("CHANGE");
            currentCloth = (currentCloth + 1) % 2;
        }
        if (animator == "HumanIdle" && photonView.isMine)
        {
            PhotonNetwork.player.SetScore(1);
        }
        if (animator == "ZombieIdle" && photonView.isMine)
        {
            PhotonNetwork.player.SetScore(0);
        }
    }
    /*
    private void hittedColor(Color color)
    {
        int nowColor = PhotonNetwork.player.GetScore();

        if (photonView.isMine)
        {


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
    }
    */
  
    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("de:" + other.tag);
        if (other.name == "ToHuman")
        {
            humanAdd = true;
     
        }
        if (other.name == "ToZombie")
        {
            zombieAdd = true;
        }
        if (other.tag == "antidote" )
        {
            TakeDamage(humanAnimator);
        }
        else if (other.name.Contains("Poison") || other.tag == "posion" || other.name.Contains("handAttack"))
        {
            Debug.Log("POSION");
            TakeDamage(zombieAnimator);
        }
        else if (other.gameObject.tag == recoverStand)
        {
            canBeCharge = true;
        }

        // Debug.Log("other.name="+other.name);
        // if (other.name.Contains("Poison"))
        // {
        //     if (this.name.Contains("human"))
        //     {
        //         Debug.Log("player hit by bullet");
        //         Vector3 tempPos = transform.position;
        //         if (photonView.isMine)
        //         {
        //             PhotonNetwork.Destroy(photonView.gameObject);
        //             PhotonNetwork.Instantiate(zombiePrefab.name, tempPos, Quaternion.identity, 0);
        //         }
        //     }
        //     else
        //     {
        //         Debug.Log("zombie hit by bullet");
        //         Vector3 tempPos = transform.position;
        //         Debug.Log("photonView:" + this.GetComponent<PhotonView>().isMine);
        //         if (photonView.isMine)
        //         {
        //             Debug.Log("ismine");
        //             PhotonNetwork.Destroy(photonView.gameObject);
        //             PhotonNetwork.Instantiate(humanPrefab.name, tempPos, Quaternion.identity, 0);
        //         }
        //     }
        // }

    }

    
   public void OnTriggerExit2D(Collider2D other)
    {

        if (other.gameObject.tag == recoverStand)
        {
            canBeCharge = false; 
        }
        if (other.name == "ToHuman")
        {
            //TakeDamage(humanAnimator);
        
            humanAdd = false;
        }
        if (other.name == "ToZombie")
        {
            zombieAdd = false;
        }
    }


    /*
        0 is red; 1 is blue
     */
    public void AddBullet( int bulletType ) {

        if( (bulletType == 0) && (redBullet < 3 ) ){
            redBullet++;
        }else if ( (bulletType == 1) && ( blueBullet < 3) ){
            blueBullet++;
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

    private void movementCount()
    {
        // canMove = true;
        // CancelInvoke();
        // if (clothArray[0] == clothArray[1])
        // {
        
        // Vector3 bulletInitPlace = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        // GameObject bulletPrefab = clothArray[0] == Color.blue ? poisonPrefab : antidotePrefab;
        // GameObject bullet = (GameObject)PhotonNetwork.Instantiate(bulletPrefab.name, bulletInitPlace, Quaternion.Euler(new Vector3(0, 0, 180)), 0);
        // bullet.GetComponent<fireBall>().initialize(Vector2.right);

        //     // if (clothArray[0] == Color.red)
        //     // {
        //     //     TakeDamage(Color.blue);
        //     // }
        //     // else
        //     // {
        //     //     TakeDamage(Color.red);
        //     // }
        // }
        // Debug.Log(clothArray[0] == clothArray[1] );
    }

    private void StartToRecovering()
    {
        canMove = false;
        InvokeRepeating("movementCount", 2f, 1f);
    }

    private void ResetLocation()
    {
        if (transform.position.y < -10)
        {
            transform.position = new Vector3(0, 5, 0);
        }
    }

    private void HandleLayer()
	{
		if(!onGround)
		{
			MyAnimator.SetLayerWeight(1,1);
		}else
		{
			MyAnimator.SetLayerWeight(1,0);
		}
	}

    public void ShortAttack(){
		MyAnimator.SetTrigger("attack");
	}

    public void MeleeAttack()
	{
		handCollider.enabled = !handCollider.enabled;
	}

}
