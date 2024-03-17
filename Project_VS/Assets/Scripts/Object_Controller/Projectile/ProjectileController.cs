using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : BaseController
{
    [Tooltip("발사체 속도")]
    [SerializeField] private float _speed = 8.0f;
    [Tooltip("발사체 지속 시간")]
    [SerializeField] private float _maxLifeTime = 10.0f;

    private BasePawnController _owner;
    private Vector3 _moveDir;
    private int _damage;
    private float _lifeTime;

    public void SetInfo(BasePawnController owner, Vector3 moveDir, int damage)
    {
        _owner = owner;
        _moveDir = moveDir;
        _damage = damage;
        _lifeTime = 0.0f;
    }

    public override void UpdateController()
    {
        transform.position += _moveDir * _speed * Time.deltaTime;

        _lifeTime = Mathf.Clamp(_lifeTime + Time.deltaTime, 0.0f, _maxLifeTime);
        if (_lifeTime >= _maxLifeTime)
        {
            Managers.Object.Despawn(this);
        }
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
            MonsterController monsterController = collision.gameObject.GetComponent<MonsterController>();
            monsterController.OnDamaged(gameObject, _damage);
        }
        else if (ownerType == typeof(MonsterController))
        {
        }

        Managers.Object.Despawn(this);
    }
}
