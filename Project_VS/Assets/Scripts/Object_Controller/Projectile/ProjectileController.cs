using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : BaseController
{
    [Tooltip("발사체 속도")]
    [SerializeField] private float _speed = 8.0f;
    [Tooltip("발사체 지속 시간")]
    [SerializeField] private float _lifeTime = 10.0f;

    private BasePawnController _owner;
    private Vector3 _moveDir;

    public void SetInfo(BasePawnController owner, Vector3 moveDir)
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
        if (_owner.gameObject == collision.gameObject ||
            collision.gameObject.tag == "MainCamera")
        {
            return;
        }

        Type ownerType = _owner.GetType();
        if (ownerType == typeof(PlayerController))
        {

        }
        else if (ownerType == typeof(MonsterController))
        {

        }

        Managers.Object.Despawn(this);
    }
}
