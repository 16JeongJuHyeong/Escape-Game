using System.Collections;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent enemy = null;
    [SerializeField] Transform[] tfWayPoints = null;
    [SerializeField] Transform target;
    private int count;

    //fov를 위한 속성들
    public float radius;
    public float angle;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canseePlayer;

    //

    //fov
    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);
        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    canseePlayer = true;

                else
                    canseePlayer = false;
            }
            else
                canseePlayer = false;
        }
        else if (canseePlayer)
            canseePlayer = false;
    }


    void Patroling()
    {
        if ((enemy.velocity == Vector3.zero))
        {
            enemy.SetDestination(tfWayPoints[count++].position);
            if (count >= tfWayPoints.Length)
                count = 0;
        }
    }

    void Chasing()
    {
        transform.LookAt( GameObject.FindWithTag("Player").transform );
        enemy.SetDestination( GameObject.FindWithTag("Player").transform.position );
        //transform.LookAt(target.transform);
        //enemy.SetDestination(target.position);
    }

    void Awake()
    {
        count = 0;
        enemy = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        FieldOfViewCheck();
        if (!canseePlayer)
        {
            InvokeRepeating("Patroling", 0f, 0.5f);
            CancelInvoke("Chasing");
        }
        if(canseePlayer)
        {
            InvokeRepeating("Chasing", 0f, 0.5f);
            CancelInvoke("Patroling");
        }
        //FieldOfViewCheck();
        //if (!canseePlayer || target.tag != "Player")
        //{
        //    InvokeRepeating("Patroling", 0f, 0.5f);
        //    CancelInvoke("Chasing");
        //}
        //if (canseePlayer && target.tag == "Player")
        //{
        //    InvokeRepeating("Chasing", 0f, 0.5f);
        //    CancelInvoke("Patroling");
        //}
    }
}
