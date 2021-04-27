using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform = null;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private Camera firstPersonCamera;
    [SerializeField] private GameObject justSphere;
    [SerializeField] private GameObject bulletHolder;

    public float bulletSpeed = 1500;

    private Quaternion bulletQuaternion;
    private Rigidbody rigidbodyComponent;
    private bool spaceKeyPressed;
    private float horizontalInput, verticalInput;
    public int jumpScale;
    private Vector2 mouseVector;
    private float mouseSensivity = 500;
    private bool run;
    private float horizontalVelo = 1;
    private float verticalVelo = 1;
    public bool airDash = true;
    public bool multiJump = true;
    public int maxNumberOfJumps = 0;
    private int jumpsCount = 0;
    private float groundCheckSphereRadius = 0.1f;
    public float runSpeed;
    public float dashSpeed;
    private bool onTheWall = false;
    public int maxHealth;
    private int currentHealth;
	public int skillPoints = 5;

    public GameObject pausePanel;
    public GameObject UiPlayer;
    public GameObject weaponPanel;

    public HealthBar healthBar;
    
    //GUI: FPS
    public Text fpsIndicator;
    private float lastUpdateDTime = 0;
    public float updatePeriod = 0.1f;
    private int framesCount = 0;
    



    // Start is called before the first frame update
    void Start()
    {
      currentHealth = maxHealth;
      healthBar.MaxHealth(currentHealth);
      fpsIndicator.text = "FPS: " + 0;
      
      rigidbodyComponent = GetComponent<Rigidbody>();
      
      Cursor.lockState = CursorLockMode.Locked;
    }

	public void skillsTreeButtonsAction(int buttonID){
		/* 
		* 0 - Double Jump;
		* 1 - Triple Jump;
		* 2 - Wall Run;
		* 3 - Wall Run - 5%G;
		* 4 - Wall Run - 5%G01;
		* 5 - Wall Run - 5%G02;
		* 6 - Air Dash;
		* 7 - Air Dash - 5%G;
		* 8 - Air Dash - 5%G01;
		* 9 - Air Dash - 5%G02;
		*/

		switch(buttonID){
			case 0:
				maxNumberOfJumps = 2;
				Debug.Log("Double Jump activated");
				break;
			case 1:
				maxNumberOfJumps = 3;
				Debug.Log("Triple Jump activated");
				break;
			case 2:
				Debug.Log("Wall Run activated");
				break;
			case 3:
				Debug.Log("Wall Run - 5%G activated");
				break;
			case 4:
				Debug.Log("Wall Run - 5%G01 activated");
				break;
			case 5:
				Debug.Log("Wall Run - 5%G02 activated");
				break;
			case 6:
				Debug.Log("Air Dash activated");
				break;
			case 7:
				Debug.Log("Air Dash -5%G activated");
				break;
			case 8:
				Debug.Log("Air Dash -5%G01 activated");
				break;
			case 9:
				Debug.Log("Air Dash -5%G02 activated");
				break;

		}

	
		
	}
   
    void OnGUI()
    {
      //TODO: DLACZEGO to się wykonuje aż dwa razy?
      //Dziwne, tutaj dobrze działa skakanie
      
      /*
      Event e = Event.current;
      if(e.control){
        run = true;
        
        // w masce wszystko oprócz plajera
        //Debug.Log("Overlapping xD: " + Physics.OverlapSphere(groundCheckTransform.position, groundCheckSphereRadius, playerMask).Length);
      } */
      
      if(Input.GetKeyDown(KeyCode.Space)){
        spaceKeyPressed = true;
      }
    }

    private void fireBullet(Quaternion bulletOrientation)
    {
      //Debug.Log("Shoot " +firstPersonCamera.transform.eulerAngles+", position: "+transform.position);
      GameObject bullet = Instantiate(justSphere) as GameObject;
      
      bullet.transform.rotation = firstPersonCamera.transform.rotation;
      bullet.transform.position = bulletHolder.transform.position;
      
      bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.rotation * (new Vector3(0,0,1)) *bulletSpeed);
      //bullet.transform.rotation * (new Vector3(0, 0, 1) * bulletSpeed *Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetKeyDown(KeyCode.Escape)){
        Debug.Log("ESCAPE!!!!");
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        UiPlayer.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;
        
      }else if (Input.GetKeyDown(KeyCode.Q)){
	      Debug.Log("Q");
	      Time.timeScale = 0;
	      weaponPanel.SetActive(true);
	      UiPlayer.SetActive(false);
	      Cursor.lockState = CursorLockMode.None;   
      }
      

      if (lastUpdateDTime < updatePeriod){
        lastUpdateDTime += Time.deltaTime;
        framesCount++;
      }else{
        fpsIndicator.text = "FPS: " + (int) (framesCount/lastUpdateDTime);
        lastUpdateDTime = 0f;
        framesCount = 0;
      }
      
      if(Input.GetKeyDown(KeyCode.LeftControl)){
        run = true;
      }
      
      if(Input.GetKeyUp(KeyCode.LeftControl))
      {
        Debug.Log("CTRL up^");
        run = false;
      }
      
     
      if (Input.GetMouseButtonDown(0))
      {
        fireBullet(bulletQuaternion);
      }
      
      horizontalInput = Input.GetAxis("Horizontal");
      verticalInput = Input.GetAxis("Vertical");
      mouseVector = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        //Debug.Log("angles: " + bulletQuaternion);
    }


    private void FixedUpdate(){
      //resetowanie przełączników
      if ((Physics.OverlapSphere(groundCheckTransform.position, groundCheckSphereRadius, playerMask).Length == 1))
      {
        //jeśli stoję na ziemi
        //na ścianie do biegania przecinam 2 collidery, i raczej zostawić jak jest, bo trzeba się jeszcze odbić od ściany (pionowej części)
        multiJump = true;
        jumpsCount = 0;
      }
        
      //horizontal rotation of player >> causes camera rotation also
      transform.Rotate(0, mouseVector.x * Time.deltaTime * mouseSensivity, 0);

      //vertical camera rotation
      Vector3 cameraAngles = firstPersonCamera.transform.rotation.eulerAngles;
      mouseVector.y = Mathf.Clamp(mouseVector.y, -90f, 90f);
      firstPersonCamera.transform.Rotate(-mouseVector.y*Time.deltaTime*mouseSensivity*0.3f, 0, 0);

      bulletQuaternion = firstPersonCamera.transform.rotation;
      

      Vector3 velocityBody = new Vector3(horizontalInput*3*verticalVelo, rigidbodyComponent.velocity.y*horizontalVelo, verticalInput*3*verticalVelo);
      velocityBody = transform.rotation *velocityBody;
      
      if (run && (Physics.OverlapSphere(groundCheckTransform.position, groundCheckSphereRadius, playerMask).Length == 1)){ 
        velocityBody.x *= runSpeed;
        velocityBody.z *= runSpeed;
        //run = false;
      }else if (run && airDash ){ 
        velocityBody.x *= dashSpeed;
        velocityBody.z *= dashSpeed;
        //velocityBody.y *= dashSpeed; //JUST FLY WHERE YOU WANT TO - trocha inaczej cza
        //run = false;
      }
      
      //Debug.Log("body velocity: " + velocityBody+", magnitude:"+velocityBody.magnitude);
      rigidbodyComponent.velocity = velocityBody; //body speed vector that need's to be rotated by quaternion

      if((Physics.OverlapSphere(groundCheckTransform.position, groundCheckSphereRadius, playerMask).Length == 0) && (!multiJump)){
		//Debug.Log("Jesteś w powietrzu i skończył się multijump");
        return;
      }

      if(spaceKeyPressed){
        //Debug.Log("[JUMP] Overlapping: " + Physics.OverlapSphere(groundCheckTransform.position, 0.3f, playerMask).Length);
        

        if (onTheWall){
          Debug.Log("Jump Away the wall!");
          
        }else{
          rigidbodyComponent.AddForce(Vector3.up*jumpScale, ForceMode.VelocityChange);
          spaceKeyPressed = false;
          jumpsCount++;
		  //Debug.Log("Jumps: " + jumpsCount);

          if (jumpsCount >= maxNumberOfJumps)
          {
            jumpsCount = 0;
            multiJump = false;
			
          }
        }

		
      }

    }

    private void OnTriggerEnter(Collider other){
      
      if (other.gameObject.layer == 9){
        Destroy(other.gameObject);
        Debug.Log("Coin");
      }
      
      if (other.gameObject.layer == 10)
      {
        horizontalVelo = 0.8f;
        verticalVelo = 3.5f;
        onTheWall = true;

        //Debug.Log("Spotkałeś się ze ścianą");
      }
    }

    private void OnTriggerExit(Collider other)
    {
      if (other.gameObject.layer == 10)
      {
        verticalVelo = 1;
        horizontalVelo = 1;
        onTheWall = false;
        // Debug.Log("Rozstanie ze ścianą");
      }
    }
    public void ResumeGame()
    {
      Time.timeScale = 1;
      pausePanel.SetActive(false);
      UiPlayer.SetActive(true);
      Cursor.lockState = CursorLockMode.Locked;

    }
    void OnDamage()
    {
      currentHealth -= 10;
      healthBar.SetHealth(currentHealth);
      Debug.Log("Auc!");
      Debug.Log(currentHealth);
    }
/*
    private void OnCollisionEnter(Collider collider){

    }

    private void OnCollisionExit(Collider collider){

    } */
}
