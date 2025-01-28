using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Rat : Health
{
    SplatScript splat;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform player;
    [SerializeField] LayerMask ground;
    [SerializeField] LayerMask playerLayer;
    float sightRange = 2;
    bool playerInAttackRange;
    [SerializeField] List<Transform> walkpoints = new List<Transform>();
    Vector3 walkpoint;
    bool walkpointSet;
    bool canAttack;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        splat = GetComponent<SplatScript>();

    }

    // Update is called once per frame
    void Update()
    {
        playerInAttackRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        if(!playerInAttackRange)
        {
            Patrolling();
        }
        else
        {
            Attack();
            agent.isStopped = true;

        }
    }
    void Attack()
    {
        
    }

    void Patrolling()
    {
        if(!walkpointSet){ SearchWalkPoint();}
        else{
            agent.SetDestination(walkpoint);
        }
        Vector3 distanceToWalk = transform.position - walkpoint;
        //Debug.Log(distanceToWalk.magnitude);
        if(!agent.pathPending && distanceToWalk.magnitude < agent.stoppingDistance + 0.01f)
        {
            walkpointSet = false;
        }
    }

    void SearchWalkPoint()
    {
        Debug.Log("searching");
        int num = Random.Range(0,walkpoints.Count);
        walkpoint = walkpoints[num].position;
        walkpointSet = true;
        
        //walkpoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        //if(Physics.Raycast(Vector3.back, -transform.up, 2f, ground))
        //{
        //    walkpointSet = true;
        //}
    }

    public override void Death()
    {
        base.Death();
        //AudioManager.AudioInstance.PlayRatDeath();
        splat.SplatBlood();
        GameManager.GameManagerInstance.RemoveObject(gameObject);
        Destroy(gameObject);
    }
}
