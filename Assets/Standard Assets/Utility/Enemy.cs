using System.Collections;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;

public class Enemy : MonoBehaviour
{
    [SerializeField] private FirstPersonController Player;
    [SerializeField] private CharacterController m_CharacterController;

    [SerializeField] private NavMeshAgent enemy;
    [SerializeField] private Transform[] tfWayPoints;
    [SerializeField] private Transform target;
    private int count;

    public ActionController Check_Player_Hiding;

    //fov를 위한 속성들
    private Collider[] rangeChecks;
    private Vector3 directionToTarget;
    private float distanceToTarget;

    public float radius;
    public float angle;

    public LayerMask targetMask;
    public LayerMask obstructionMask;
    //fov를 위한 속성들

    public bool canseePlayer;
    public bool Checking_Light;
    private Vector3 Light_Destination;

    void Awake()
    {
        count = 1;
        StartCoroutine(Patrolling());
    }

    void Update()
    {
        FieldOfViewCheck();
        if (Checking_Light)
            Check_Light_Destination();
        if (canseePlayer && !Check_Player_Hiding.ishiding)
            Chasing();
    }

    private void FieldOfViewCheck()
    {
        rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);
        if (rangeChecks.Length != 0)
        {
            target = rangeChecks[0].transform;
            if (m_CharacterController.velocity.sqrMagnitude != 0) //발소리 듣는 거
                transform.LookAt(target.transform);
            directionToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (Physics.Raycast(transform.position, directionToTarget, distanceToTarget, targetMask)) //obstructionMask
                    canseePlayer = true;
            }
        }
        else
            canseePlayer = false;
    }

    IEnumerator Patrolling()
    {
        WaitForSeconds wait = new WaitForSeconds(0.5f);
        while(true)
        {
            if ( (!canseePlayer || Check_Player_Hiding.ishiding) && !Checking_Light)
            {
                enemy.SetDestination(tfWayPoints[count].position);
                if ((transform.position - tfWayPoints[count].position).sqrMagnitude < 1f)
                    count++;
                if (count == tfWayPoints.Length)
                    count = 0;
            }
            yield return wait;
        }
    }

    private void Chasing()
    {
        transform.LookAt(target.position);
        enemy.SetDestination(target.position);
    }

    private void Check_Light_Destination()
    {
        if (canseePlayer)
            Checking_Light = false;
        else
        {
            transform.LookAt(Light_Destination);
            enemy.SetDestination(Light_Destination);
            if ((transform.position - Light_Destination).sqrMagnitude < 2f) //Mathf.Approximately가 잘 안 됨
                Checking_Light = false;
        }
    }

    public void SetDestination(Vector3 New_Destination)
    {
        Checking_Light = true;
        Light_Destination = New_Destination;
    }

}

//IEnumerator Chasing()
//{
//    WaitForSeconds wait = new WaitForSeconds(0.1f);
//    while(true)
//    {
//        if(canseePlayer)
//        {
//            transform.LookAt(target.transform);
//            enemy.SetDestination(target.position);
//        }
//        yield return wait;
//    }
//}

//void Patrolling()
//{
//    if (Mathf.Approximately(enemy.velocity.sqrMagnitude , 0f))
//    {
//        enemy.SetDestination(tfWayPoints[count++].position);
//        if (count >= tfWayPoints.Length)
//            count = 0;
//    }
//}