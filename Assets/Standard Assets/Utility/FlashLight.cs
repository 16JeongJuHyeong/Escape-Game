using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    [SerializeField] Light light;
    [SerializeField] private AudioSource turnSound;
    [SerializeField] private Transform enemy;
    [SerializeField] private Enemy Enemy_By_Light;
    [SerializeField] private Transform Player_Body;

    //손전등으로 신호 보내기 위한 속성들
    private Collider[] rangeChecks;

    public float Distance; //검출 범위
    public float Light_Angle; //손전등의 범위
    public LayerMask targetMask;
    private Vector3 Direction_To_Enemy;
    //손전등으로 신호 보내기 위한 속성들

    void Update()
    {
        Light_On_Off();
        Enemy_In_Light_Check();
    }
    private void Light_On_Off()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            light.enabled = !light.enabled;
            turnSound.Play();
        }
    }
    void Enemy_In_Light_Check()
    {
        if(light.enabled)
        {
            rangeChecks = Physics.OverlapSphere(transform.position, Distance, targetMask);
            if(rangeChecks.Length != 0) //targetMask가 enemy인 상태에서 null이 아니다=> enemy가 있다 , 대상은 콜라이더 필요
            {
                Direction_To_Enemy = (enemy.position - transform.position).normalized; //방향
                if (Vector3.Angle(transform.forward, Direction_To_Enemy) < Light_Angle / 2)
                    Enemy_By_Light.SetDestination(Player_Body.position); 
                //* FlashLight 객체 좌표 전달했는데 확인해보니 자식 객체는 좌표가 변하지 않았음
            }
        }
    }
}