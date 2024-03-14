using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class MonsterController : PawnController
{
    [Tooltip("팅김 강도")]
    [SerializeField] private float _pushForce;

    private Define.MonsterState _monsterState = Define.MonsterState.None;
    protected Define.MonsterState MonsterState
    {
        get { return _monsterState; }
        set
        {
            _monsterState = value;

            // TODO : Update Animation.
        }
    }

    public override bool Init()
    {
        if (base.Init() == false)
        {
            return false;
        }

        Physics.IgnoreLayerCollision(0, 4);

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

        Vector3 moveDir = (player.transform.position - transform.position).normalized;
        Vector3 movement = moveDir * _speed * Time.deltaTime;
        transform.position += movement;
    }

    private void UpdateDead()
    {
    }

    [SerializeField] private int _maxHitCount = 3;
    private int _hitCount;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            return;
        }

        Vector2 pushDirection = (transform.position - collision.transform.position).normalized;
        GetComponent<Rigidbody2D>().AddForce(pushDirection * _pushForce, ForceMode2D.Impulse);

        // TODO : DamageHit.
        _hitCount++;
        if (_hitCount >= _maxHitCount)
        {
            Managers.Object.Despawn(this);
        }
    }
}
