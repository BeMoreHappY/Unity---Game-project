
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// Klasa, która odpowiada za obsługę przeciwnika (jego sztucznej inteligencji)
/// </summary>
public class BossAI : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask Ground, Player;

    public float health;

    public GameObject go;
    public Boss spawn;
    private bool active;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    public float bulletSpeed;

    //Attacking
    public float rotationSpeed = 5f;
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject bullet;
    private Rigidbody rigibody;

    //States
    public float sightRange, attackRange;
    public bool isSight, isAttack, sawBullet;
    public float impactForce;
    /// <summary>
    /// Funkcja, która wykonuje się w momencie gdy obiekt do którego podpięty jest skrypt został aktywowany.
    /// </summary>
    private void Start()
    {
        active = true;
        rigibody = GetComponent<Rigidbody>();
        go = GameObject.Find("GameObject(Boss)");
        spawn = (Boss) go.GetComponent(typeof(Boss));
    }
    
        
    /// <summary>
    /// Funkcja, która wykonuje się w momencie załadowania sceny z danym obiektem
    /// </summary>
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }
    /// <summary>
	/// Funkcja, która wykonuje się co klatkę
	/// </summary>
    private void Update()
    {

        isSight = Physics.CheckSphere(transform.position, sightRange, Player);
        isAttack = Physics.CheckSphere(transform.position, attackRange, Player);

        if (!isSight && !isAttack) Patroling();
        if (isSight && !isAttack) ChasePlayer();

        RaycastHit hit;
        if (isAttack && isSight)
        {
            if (Physics.Raycast(transform.position, transform.forward,out hit, attackRange))
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    AttackPlayer();
                }
                else if (hit.collider.gameObject.CompareTag("BulletEnemy"))
                {
                    AttackPlayer();
                }
                else
                {
                    ChasePlayer();
                }
            }
            else{
                ChasePlayer();
            }
        }
    }
    /// <summary>
    /// Funkcja, która odpowiada za patrolowanie obiektu po wyznaczonym obszarze
    /// </summary>
    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    /// <summary>
    /// Funkcja, która losuje współrzędne celu obiektu
    /// </summary>
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, Ground))
            walkPointSet = true;
    }
    /// <summary>
    /// Funkcja, która odpowiada za podążanie obiektu za graczem
    /// </summary>
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
    /// <summary>
    /// Funkcja, która odpowiada za atakowanie gracza
    /// </summary>
    private void AttackPlayer()
    {
        
        agent.SetDestination(transform.position);

        Quaternion LookOnPlayer = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, LookOnPlayer, rotationSpeed * Time.deltaTime);
        if (!alreadyAttacked)
        {
            Rigidbody rb = Instantiate(bullet, agent.transform.position + agent.transform.forward, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(agent.transform.forward * bulletSpeed);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    /// <summary>
    /// Funkcja, która ustawia zmianną alreadyAttacked na false
    /// </summary>
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    /// <summary>
    /// Funkcja, która niszczy obiekt
    /// </summary>
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
    /// <summary>
    /// Funkcja, która zmienia kolor sfer obiektu
    /// </summary>
    private void SphereCollor()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
    /// <summary>
    /// Funkcja, która odpowiada za przyjmowanie obrażeń przez obiekt
    /// </summary>
    /// <param name="damage">Ilość obrażeń przyjmowanych przez obiekt</param>
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f)
        {
            spawn.enemyDied();
            DestroyEnemy();
        }
    }
    /// <summary>
    /// Funkcja, która powoduje odrzut obiektu w koinkretnym kierunku
    /// </summary>
    /// <param name="kierunek">Kierunek odrzutu obiektu</param>
    public void impact(Vector3 kierunek)
    {
        rigibody.AddForce(kierunek * impactForce);
    }
    /// <summary>
    /// Funkcja, która odpowiada za dezaktywowanie obiektu i jego fizyki
    /// </summary>
    public void agentStop()
    {
        active = false;
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        
        rigibody.isKinematic = false;
    }
    /// <summary>
    /// Funkcja, która odpowiada za aktywowanie obiektu i jego fizyki
    /// </summary>
    public void agentStart()
    {
        active = true;
        agent.isStopped = false;
        walkPointSet = false;
        rigibody.isKinematic = true;
    }
    /// <summary>
    /// Funkcja, która sprawdza, czy obiekt jest aktywny
    /// </summary>
    /// <returns>Zwraca true jeśli obiekt jest aktywny. Zwraca false w przeciwnym wypadku</returns>
    public bool isActive()
    {
        return active;
    }
}

