using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : BaseController
{
    private Define.MonsterState _monsterState = Define.MonsterState.None;
    protected Define.MonsterState MonsterState
    {
        get { return _monsterState; }
        set
        { 
            _monsterState = value;
            // TODO : Update Animation
        }
    }

    public override bool Init()
    {
        if (base.Init() == false)
        { 
            return false;
        }

        ObjectType = Define.ObjectType.Monster;
        MonsterState = Define.MonsterState.Moving;

        return true;
    }

    public override void UpdateController()
    {
        switch (MonsterState)
        {
            case Define.MonsterState.Idle:
                UpdateIdle();
                break;
            case Define.MonsterState.Moving:
                UpdateMoving();
                break;
            case Define.MonsterState.Dead:
                UpdateMoving();
                break;
        }
    }

    private void UpdateIdle()
    {
    }

    private void UpdateMoving()
    {
        // TODO : 임시로 Tag 사용중 영락이 작업끝나면 ObjectManger로 관리하게.
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null)
        { 
            return;
        }

        Vector3 moveDir = player.transform.position - transform.position;
        Vector3 movement = transform.position + moveDir.normalized * _speed * Time.deltaTime;
        GetComponent<Rigidbody2D>().MovePosition(movement);
    }

    private void UpdateDead()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
        {
            return;
        }

        // TODO : 데미지 처리
    }
}
