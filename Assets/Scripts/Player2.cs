using UnityEngine;
using System;
using System.Collections;
 
 
public class Player2 : MonoBehaviour {
 
	[Serializable]
    public struct PlayerMovement
    {
        public float moveSpeed;
		public float runSpeed;
        public float jumpForce;
		public int maxJump;
		public int maxDash;
        public float mouseSens;
		public bool smooth;
		public float smooth_mouseSens;
    }
	[SerializeField] private PlayerMovement stats;
    private Rigidbody RB;
	public Camera cam;
	public LayerMask GroundMask;
	public GameObject GroundCheck;
	public HealthBar healthBar;
	public int maxHealth = 100;
	private int currentHealth;
	private float maxVelocityChange = 10.0f;
	private float currentSpeed;
	private bool dash = false;
	private int jumpCount;
	private int dashCount;
	private bool spacePressed = false;
	private bool grounded = false;
	private float RotationX;
	private float RotationY;
	
	private float ButtonCooldownW = 0.3F;
	private int ButtonCountW = 0;
	private float ButtonCooldownA = 0.3F;
	private int ButtonCountA = 0;
	private float ButtonCooldownS = 0.3F;
	private int ButtonCountS = 0;
	private float ButtonCooldownD = 0.3F;
	private int ButtonCountD = 0;

	Quaternion quatRotationY;
	Quaternion quatRotationX;
	Vector3 velocity;
	Vector3 kierunek;

	void Start() 
	{
		startingStats();
        RB = GetComponent<Rigidbody>();
	    RB.freezeRotation = true;
	    RB.useGravity = false;
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		cam.transform.localRotation = Quaternion.identity;
		currentHealth = maxHealth;
      	healthBar.MaxHealth(currentHealth);
	}
	void Update()
	{
		CooldownDash();
		ButtonCheck();

	}
	void FixedUpdate() 
	{
		if (is_grounded()){
			jumpCount = stats.maxJump;
			dashCount = stats.maxDash;
		}
		Dash();
		Jump();
		Movement();
	    RB.AddForce(new Vector3 (0, Physics.gravity.y , 0));
	}
	void LateUpdate()
	{
		MouseInput();
		if (stats.smooth){
			transform.localRotation = Quaternion.Slerp(transform.localRotation, quatRotationX, Time.deltaTime * stats.smooth_mouseSens);
			cam.transform.localRotation = Quaternion.Slerp (cam.transform.localRotation, quatRotationY, Time.deltaTime * stats.smooth_mouseSens);
		}
		else{
			transform.localRotation = quatRotationX;
			cam.transform.localRotation = quatRotationY;
		}
	}
	
	
	private void Movement()
	{
		Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		targetVelocity = transform.TransformDirection(targetVelocity);
		targetVelocity *= currentSpeed;
		velocity = RB.velocity;
		Vector3 velocityChange = (targetVelocity - velocity);
		velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
		velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
		velocityChange.y = 0;
		RB.AddForce(velocityChange, ForceMode.VelocityChange);
		
	}
	private void Jump()
	{
		if (spacePressed) 
		{
			Debug.Log("JUmp");
			spacePressed = false;
			RB.velocity = new Vector3(velocity.x, CalculateVerticalSpeed(), velocity.z);
			jumpCount--;
		}
	}
	private void Dash()
	{
		if (dash && dashCount > 0)
		{
			dash = false;
			kierunek = kierunek.normalized;
			RB.AddForce(kierunek * 100, ForceMode.Impulse);
			dashCount--;
		}
	}
	
	private void ButtonCheck()
	{
		if (Input.GetKeyDown(KeyCode.LeftShift) && is_grounded())
		{
			currentSpeed = stats.moveSpeed * stats.runSpeed;
		}
		if (Input.GetKeyDown(KeyCode.W) && dashCount > 0)
		{
			ButtonCountA = 0;
			ButtonCountD = 0;
			ButtonCountS = 0;
			if (ButtonCooldownW>0 && ButtonCountW==1){
				dash=true;
				kierunek = transform.forward;
				ButtonCountW=0;
				}
			else{
				ButtonCountW++;
				ButtonCooldownW=0.3F;
			}
		}
		if (Input.GetKeyDown(KeyCode.A) && dashCount > 0)
		{
			ButtonCountW = 0;
			ButtonCountD = 0;
			ButtonCountS = 0;
			if (ButtonCooldownA>0 && ButtonCountA==1){
				dash=true;
				kierunek = -transform.right;
				ButtonCountA=0;
				}
			else{
				ButtonCountA++;
				ButtonCooldownA=0.3F;
				}
		}
		if (Input.GetKeyDown(KeyCode.S) && dashCount > 0)
		{
			ButtonCountA = 0;
			ButtonCountD = 0;
			ButtonCountW = 0;
			if (ButtonCooldownS>0 && ButtonCountS==1){
				dash=true;
				kierunek = -transform.forward;
				ButtonCountS=0;
				}
			else{
				ButtonCountS++;
				ButtonCooldownS=0.3F;
				}
		}
		if (Input.GetKeyDown(KeyCode.D) && dashCount > 0)
		{
			ButtonCountA = 0;
			ButtonCountW = 0;
			ButtonCountS = 0;
			if (ButtonCooldownD>0 && ButtonCountD==1){
				dash=true;
				kierunek = transform.right;
				ButtonCountD=0;
				}
			else{
				ButtonCountD++;
				ButtonCooldownD=0.3F;
				}
		}
		if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			currentSpeed = stats.moveSpeed;
		}
		if (Input.GetKeyDown(KeyCode.Space) && is_grounded())
		{
			jumpCount--;
			spacePressed = true;
		}
		if (Input.GetKeyDown(KeyCode.Space) && (jumpCount > 0))
		{
			jumpCount--;
			spacePressed = true;
		}
	}
	private void MouseInput()
	{
		Vector2 input = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
		RotationY += input.x * stats.mouseSens;
		RotationX -= input.y * stats.mouseSens;
		RotationX = Mathf.Clamp( RotationX, -80, 80);
		quatRotationY = Quaternion.Euler(RotationX,0,0);
		quatRotationX = Quaternion.Euler(0, RotationY, 0);
	}
	public void TakeDamage(int damage)
	{
		currentHealth -= damage;
		healthBar.SetHealth(currentHealth);
	}
	float CalculateVerticalSpeed() 
	{
	    return Mathf.Sqrt(2 * stats.jumpForce * -Physics.gravity.y);
	}
	bool is_grounded()
	{
		if(Physics.OverlapSphere(GroundCheck.transform.position, 0.1f, GroundMask).Length == 0){
			return false;
		}
		return true; 
	}	
	private void CooldownDash()
	{
		if (ButtonCooldownW>0){
			ButtonCooldownW -= 1*Time.deltaTime;
		}
		else{ButtonCountW=0;}
		if (ButtonCooldownA>0){
			ButtonCooldownA -= 1*Time.deltaTime;
		}
		else{ButtonCountA=0;}
		if (ButtonCooldownS>0){
			ButtonCooldownS -= 1*Time.deltaTime;
		}
		else{ButtonCountS=0;}
		if (ButtonCooldownD>0){
			ButtonCooldownD -= 1*Time.deltaTime;
		}
		else{ButtonCountD=0;}
	}
	private void startingStats()
	{
		stats.mouseSens = 1.0f;
		stats.smooth_mouseSens = 10.0f;
		stats.jumpForce = 2.5f;
		stats.moveSpeed = 10.0f;
		stats.runSpeed = 1.5f;
		stats.maxJump = 2;
		stats.maxDash = 1;
		currentSpeed = stats.moveSpeed;
	}

}