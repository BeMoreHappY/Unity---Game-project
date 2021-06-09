using UnityEngine;
using System;
using System.Collections;
 
 /// <summary>
 /// Klasa, która odpowiada za obsługę gracza i jego interfejsu
 /// </summary>
public class Player2 : MonoBehaviour {
 
	[Serializable]
	///Struktura, która przechowuje zmienne ruchu gracza
    public struct PlayerMovement
    {
        public float moveSpeed;
		public float runSpeed;
        public float jumpForce;
		public int maxJump;
		public int maxDash;
		public int dashForce;
        public float mouseSens;
		public bool smooth;
		public float smooth_mouseSens;
    }

    [Serializable]
	///Struktura, która przechowuje zmienne interfejsu
    public struct InterfaceElements
    {
	    public GameObject pausePanel;
	    public GameObject UiPlayer;
	    public GameObject weaponPanel;
    }
    
	[SerializeField] private PlayerMovement stats;
	[SerializeField] private InterfaceElements interfaceElements;
	private Rigidbody RB;
	public Camera cam;
	public LayerMask GroundMask;
	public GameObject GroundCheck;
	public HealthBar healthBar;
	public int skillPoints = 15;
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
	public GameObject weapon;
	
	private float ButtonCooldownW = 0.3F;
	private int ButtonCountW = 0;
	private float ButtonCooldownA = 0.3F;
	private int ButtonCountA = 0;
	private float ButtonCooldownS = 0.3F;
	private int ButtonCountS = 0;
	private float ButtonCooldownD = 0.3F;
	private int ButtonCountD = 0;
	public bool gameStopped = false;

	Quaternion quatRotationY;
	Quaternion quatRotationX;
	Vector3 velocity;
	Vector3 kierunek;
	public bool[] idWeaponIsActive = new bool[7];

	/// <summary>
	/// Funkcja, która wykonuje się w momencie gdy obiekt do którego podpięty jest skrypt został aktywowany.
	/// </summary>
	void Start() 
	{
		idWeaponIsActive[0] = true;
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
	/// <summary>
	/// Funkcja, która wykonuje się co klatkę
	/// </summary>
	void Update()
	{
		CooldownDash();
		ButtonCheck();
		if(currentHealth==0)gameOver();
	}
	/// <summary>
	/// Funkcja, która wykonuje się co klatkę fizyki
	/// </summary>
	void FixedUpdate() 
	{
		if (!gameStopped)
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
	}
	/// <summary>
	/// Funkcja, która wykonuje się po innych metodach aktualizacji
	/// </summary>
	void LateUpdate()
	{
		if (!gameStopped)
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
	}
	
	/// <summary>
	/// Funkcja, która odpowiada za ruch postaci
	/// </summary>
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
	/// <summary>
	/// Funkcja, która odpowiada za skok postaci
	/// </summary>
	private void Jump()
	{
		if (spacePressed) 
		{
			Debug.Log("JUmp; JumpCount: "+jumpCount+"; MaxJUMP: "+stats.maxJump);
			spacePressed = false;
			RB.velocity = new Vector3(velocity.x, CalculateVerticalSpeed(), velocity.z);
			jumpCount--;
		}
	}
	/// <summary>
	/// Funkcja, która odpowiada za nadanie nagłego pędu w konkretnym kierunku
	/// </summary>
	private void Dash()
	{
		if (dash && dashCount > 0)
		{
			dash = false;
			kierunek = kierunek.normalized;
			RB.AddForce(kierunek * stats.dashForce, ForceMode.Impulse);
			dashCount--;
		}
	}
	/// <summary>
	/// Funkcja, która sprawdza przyciski z klawiatury
	/// </summary>
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
		
		if (Input.GetKeyDown(KeyCode.Space) && (jumpCount > 0))
		{
			//jumpCount--;
			spacePressed = true;
		}
		
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			showPauseMenu();
		}
		if (Input.GetKeyDown(KeyCode.Q))
		{
			showWeaponMenu();
		}
		if (Input.GetKeyUp(KeyCode.Q))
		{
			ResumeGame();
		}
	}
	/// <summary>
	/// Funkcja, ktróra zajmuje się ruchem myszki
	/// </summary>
	private void MouseInput()
	{
		Vector2 input = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
		RotationY += input.x * stats.mouseSens;
		RotationX -= input.y * stats.mouseSens;
		RotationX = Mathf.Clamp( RotationX, -80, 80);
		quatRotationY = Quaternion.Euler(RotationX,0,0);
		quatRotationX = Quaternion.Euler(0, RotationY, 0);
	}
	/// <summary>
	/// Funkcja, która odpowiada za otrzymywanie obrażeń przez gracza
	/// </summary>
	/// <param name="damage">Ilość obrażeń otrzymanych przez gracza</param>
	public void TakeDamage(int damage)
	{
		currentHealth -= damage;
		healthBar.SetHealth(currentHealth);
	}
	/// <summary>
	/// Funkcja, która odpowiada za leczenie gracza
	/// </summary>
	/// <param name="heal">Ilość dodawanych punktów życia</param>
	public void Heal(int heal){
		if(currentHealth + heal <= maxHealth)currentHealth+=heal;
		else currentHealth = maxHealth;
		healthBar.SetHealth(currentHealth);
	}
	/// <summary>
	/// Funkcja, która oblicza prędkość wertykalną gracza
	/// </summary>
	/// <returns>Zwraca prędkość wertykalną</returns>
	float CalculateVerticalSpeed() 
	{
	    return Mathf.Sqrt(2 * stats.jumpForce * -Physics.gravity.y);
	}
	/// <summary>
	/// Funkcja, która sprawdza, czy gracz na czymś stoi
	/// </summary>
	/// <returns>Zwraca true jeśli stoi na czymś. Zwraca false w przeciwnym wypadku</returns>
	bool is_grounded()
	{
		if(Physics.OverlapSphere(GroundCheck.transform.position, 0.1f, GroundMask).Length == 0){
			return false;
		}
		return true; 
	}
	/// <summary>
	/// Funkcja, która oblicza opóźnienie między wciskaniem przycisku
	/// </summary>
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
	/// <summary>
	/// Funkcja, która nadaje początkowe wartości zmiennym ruchu.
	/// </summary>
	private void startingStats()
	{
		stats.mouseSens = 1.0f;
		stats.smooth_mouseSens = 10.0f;
		stats.jumpForce = 2.5f;
		stats.moveSpeed = 10.0f;
		stats.runSpeed = 1.5f;
		stats.maxJump = 1;
		stats.maxDash = 1;
		stats.dashForce = 0;
		currentSpeed = stats.moveSpeed;
	}
	/// <summary>
	/// Funckja, która odpowiada za wyświetlanie okna pauzy
	/// </summary>
	private void showPauseMenu()
	{
		gameStopped = true;
		Debug.Log("P2ESCAPE");
		Time.timeScale = 0;
		Cursor.lockState = CursorLockMode.Confined;
		interfaceElements.pausePanel.SetActive(true);
		interfaceElements.UiPlayer.SetActive(false);
	}
	/// <summary>
	/// Funkcja, która odpowiada za wyświetlenie okna wyboru broni
	/// </summary>
	private void showWeaponMenu()
	{
		Cursor.visible = true;
		gameStopped = true;
		Debug.Log("Q");
		Time.timeScale = 0;
		interfaceElements.weaponPanel.SetActive(true);
		interfaceElements.UiPlayer.SetActive(false);
		dostepneBronie();
		Cursor.lockState = CursorLockMode.None;   
	}
	/// <summary>
	/// Funkcja, która odpowiada za wznawianie gry
	/// </summary>
	public void ResumeGame()
	{
		Cursor.visible = false;
		gameStopped = false;
		Time.timeScale = 1;
		interfaceElements.pausePanel.SetActive(false);
		interfaceElements.weaponPanel.SetActive(false);
		interfaceElements.UiPlayer.SetActive(true);
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	/// <summary>
	/// Finkcja która odpowiada za zwiększanie statystyk umiejętności
	/// </summary>
	/// <param name="buttonID">Przyjmuje ID wcyśniętego przycisku</param>
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
				stats.maxJump = 2;
				Debug.Log("Double Jump activated");
				break;
			case 1:
				stats.maxJump = 3;
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
				stats.dashForce = 50;
				Debug.Log("Air Dash activated");
				break;
			case 7:
				stats.dashForce += 20;
				Debug.Log("Air Dash -5%G activated");
				break;
			case 8:
				stats.dashForce += 20;
				Debug.Log("Air Dash -5%G01 activated");
				break;
			case 9:
				stats.dashForce += 20;
				Debug.Log("Air Dash -5%G02 activated");
				break;
		}
	}
	/// <summary>
	/// Funkcja, która zmienia broń przeciwnika
	/// </summary>
	/// <param name="buttonID">Przyjmuje ID broni</param>
	public void weaponChoosePanelAction(int buttonID)
	{
		
		weapon.GetComponent<weaponSwitcher>().SwitchWeapon(buttonID);
		switch (buttonID)
		{
			case 0:
				Debug.Log("Wybrano przedmiot" + buttonID);
				break;
			case 1:
				Debug.Log("Wybrano przedmiot" + buttonID);
				break;
			case 2:
				Debug.Log("Wybrano przedmiot" + buttonID);
				break;
			case 3:
				Debug.Log("Wybrano przedmiot" + buttonID);
				break;
			case 4:
				Debug.Log("Wybrano przedmiot" + buttonID);
				break;
			case 5:
				Debug.Log("Wybrano przedmiot" + buttonID);
				break;
			case 6:
				Debug.Log("Wybrano przedmiot" + buttonID);
				break;
		}
	}
	/// <summary>
	/// Funkcja, która odblokowuje lub blokuje bronie na podstawie tablicy bool - idWeaponIsActive
	/// </summary>
	void dostepneBronie()
    {
		for (int i = 0; i < 7; i++)
        {
			if(idWeaponIsActive[i] == true)
            {
				GameObject.Find("Canvas/WyborBroni").GetComponent<OdblokowaneBronie>().odblokuj(i);
			}
            else
            {
				GameObject.Find("Canvas/WyborBroni").GetComponent<OdblokowaneBronie>().zablokuj(i);
			}
        }			
    }
	/// <summary>
	/// Funkcja, która aktualizuje tablicę dostępnych broni 
	/// </summary>
	void pobranieStatusuBroni()
    {
		idWeaponIsActive = GameObject.Find("Canvas/WyborBroni").GetComponent<OdblokowaneBronie>().status();
	}
	/// <summary>
	/// Funkcja, która kończy grę
	/// </summary>
	void gameOver()
	{

		Application.LoadLevel("gameOver");
	}
}