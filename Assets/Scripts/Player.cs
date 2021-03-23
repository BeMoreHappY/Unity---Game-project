using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform = null;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private Camera firstPersonCamera;
    [SerializeField] private GameObject justSphere;
    [SerializeField] private GameObject bulletHolder;

    public int bulletSpeed = 1500;

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
    public int maxNumberOfJumps;
    private int jumpsCount = 0;
    private float groundCheckSphereRadius = 0.1f;
    public float runSpeed;
    public float dashSpeed;
    private bool onTheWall = false;



    // Start is called before the first frame update
    void Start()
    {
      rigidbodyComponent = GetComponent<Rigidbody>();
      
      Cursor.lockState = CursorLockMode.Locked;
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
      
      bullet.GetComponent<Rigidbody>().velocity = bullet.transform.rotation * (new Vector3(0, 0, 1) * bulletSpeed);
    }

    // Update is called once per frame
    void Update()
    {
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
        return;
      }

      if(spaceKeyPressed){
        //Debug.Log("[JUMP] Overlapping: " + Physics.OverlapSphere(groundCheckTransform.position, 0.3f, playerMask).Length);
        //Debug.Log("Jumps: " + jumpsCount);

        if (onTheWall){
          Debug.Log("Jump Away the wall!");
          
        }else{
          rigidbodyComponent.AddForce(Vector3.up*jumpScale, ForceMode.VelocityChange);
          spaceKeyPressed = false;
          jumpsCount++;

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
/*
    private void OnCollisionEnter(Collider collider){

    }

    private void OnCollisionExit(Collider collider){

    } */
}
