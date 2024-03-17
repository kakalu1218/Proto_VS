using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : BasePawnController
{
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

        // TODO : 임시
        Hp = 3;
        Speed = 4.0f;

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
        GameObject player = Managers.Object.Player.gameObject;
        if (player == null)
        {
            return;
        }

        Vector3 moveDir = (player.transform.position - transform.position).normalized;
        Vector3 movement = moveDir * Speed * Time.deltaTime;
        transform.position += movement;
    }

    private void UpdateDead()
    {
    }

    public override void OnDead()
    {
        Managers.Object.Despawn(this);
        Managers.Object.Spawn<GemController>(transform.position);

        // TODO : 임시
        Hp = 3;
    }
}
