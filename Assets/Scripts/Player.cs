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
    
   

    private Quaternion bulletQuaternion;
    private Rigidbody rigidbodyComponent;
    private bool spaceKeyPressed;
    private float horizontalInput, verticalInput;
    private int jumpScale;
    private Vector2 mouseVector;
    private float mouseSensivity = 500;
    private bool run;



    // Start is called before the first frame update
    void Start()
    {
      rigidbodyComponent = GetComponent<Rigidbody>();
      jumpScale = 6;
      Cursor.lockState = CursorLockMode.Locked;
    }
    
    void OnGUI()
    {
      Event e = Event.current;
      if(e.control){
        run = true;
      }
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
      
      bullet.GetComponent<Rigidbody>().velocity = bullet.transform.rotation * (new Vector3(0, 0, 1) * 20);
      
    }

    // Update is called once per frame
    void Update()
    {
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
      //horizontal rotation of player >> causes camera rotation also
      transform.Rotate(0, mouseVector.x * Time.deltaTime * mouseSensivity, 0);

      //vertical camera rotation
      Vector3 cameraAngles = firstPersonCamera.transform.rotation.eulerAngles;
      firstPersonCamera.transform.Rotate(-mouseVector.y*Time.deltaTime*mouseSensivity*0.3f, 0, 0);

      bulletQuaternion = firstPersonCamera.transform.rotation;
      

      Vector3 velocityBody = new Vector3(horizontalInput*3, rigidbodyComponent.velocity.y, verticalInput*3);
      velocityBody = transform.rotation *velocityBody;

      if (run && ((Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 1))){ //super dash possible
        velocityBody.x *= 5;
        velocityBody.z *= 5;
        run = false;
      }
      
      //Debug.Log("body velocity: " + velocityBody+", magnitude:"+velocityBody.magnitude);
      rigidbodyComponent.velocity = velocityBody; //body speed vector that need's to be rotated by quaternion
      
      if(Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0){
        return;
      }

      if(spaceKeyPressed){
        rigidbodyComponent.AddForce(Vector3.up*jumpScale, ForceMode.VelocityChange);
        spaceKeyPressed = false;
      }

    }

    private void OnTriggerEnter(Collider other){
      if (other.gameObject.layer == 9){
        Destroy(other.gameObject);
      }
    }
/*
    private void OnCollisionEnter(Collider collider){

    }

    private void OnCollisionExit(Collider collider){

    } */
}
