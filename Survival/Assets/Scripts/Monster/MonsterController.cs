using UnityEngine;

public class MonsterController : CharacterController
{
    public Transform player;
    public float rotationSpeed = 5f;

    private Monster _monster;

    private void Awake()
    {
        _monster = GetComponent<Monster>();
    }

    public override void Attack()
    {
        // 공격력 참조 
        // 랜덤으로 공격, 치명타 발동
    }

    public override void Die()
    {
        // 오브젝트 파괴
        // 아이템 드롭 함수 실행 
    }

    public override void Look()
    {
        // 타겟 방향으로 회전 
    }

    public override void Move()
    {
        // 타겟에게 이동
    }

    public void Patrol()
    {
        // 대기 상태에서 순찰
    }

    public override void TakeDamage()
    {
        
    }
}

