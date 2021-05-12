
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask Ground, Player;

    public float health;

    public GameObject go;
    public Spawner spawn;
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

    private void Start()
    {
        active = true;
        rigibody = GetComponent<Rigidbody>();
        go = GameObject.Find("GameObject (Spawner)");
        spawn = (Spawner) go.GetComponent(typeof(Spawner));
    }
    
        
    
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Check for sight and attack range
        isSight = Physics.CheckSphere(transform.position, sightRange, Player);
        isAttack = Physics.CheckSphere(transform.position, attackRange, Player);
        //sawBullet = Physics.CheckSphere(transform.position, attackRange, 12); //bullet LayerMask
        

        if (!isSight && !isAttack) Patroling();
        if (isSight && !isAttack) ChasePlayer();
        //if (sawBullet) ChasePlayer();
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

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, Ground))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        //transform.LookAt(player);
        Quaternion LookOnPlayer = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, LookOnPlayer, rotationSpeed * Time.deltaTime);
        if (!alreadyAttacked)
        {
            ///Attack code here
            Rigidbody rb = Instantiate(bullet, agent.transform.position + agent.transform.forward, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(agent.transform.forward * bulletSpeed);
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void SphereCollor()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f)
        {
            spawn.enemyDied();
            DestroyEnemy();
        }
    }
    public void impact(Vector3 kierunek)
    {
        rigibody.AddForce(kierunek * impactForce);
    }
    public void agentStop()
    {
        
        agent.isStopped = true;
        active = false;
        rigibody.isKinematic = false;
    }
    public void agentStart()
    {
       
        agent.isStopped = false;
        active = true;
        walkPointSet = false;
        rigibody.isKinematic = true;
    }
    public bool isActive()
    {
        return active;
    }
}