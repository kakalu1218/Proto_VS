using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : BaseController
{
    [Tooltip("�߻�ü �ӵ�")]
    [SerializeField] private float _speed = 8.0f;
    [Tooltip("�߻�ü ���� �ð�")]
    [SerializeField] private float _lifeTime = 10.0f;

    private PawnBaseController _owner;
    private Vector3 _moveDir;

    public void SetInfo(PawnBaseController owner, Vector3 moveDir)
    { 
        _owner = owner;
        _moveDir = moveDir;
    }

    public override void UpdateController()
    {
        transform.position += _moveDir * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(_owner.gameObject == collision.gameObject)
        {
            return;
        }

        Type type = _owner.GetType();
        if (type == typeof(PlayerController))
        {

        }
        else if (type == typeof(MonsterController))
        { 
        
        }

        Managers.Object.Despawn(this);
    }
}
