using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : BasePawnController
{
    [SerializeField] private Transform _indicator;
    public Transform Indicator { get { return _indicator; } }

    public float GemCollectDistance { get; private set; } = 1.0f;

    private Vector2 _moveDir = Vector2.zero;

    public override bool Init()
    {
        if (base.Init() == false)
        { 
            return false;
        }

        ObjectType = Define.ObjectType.Player;

        // TODO : �ӽ�
        Managers.Skill.AddSkill<BulletSkill>(transform.position);
        Managers.Skill.AddSkill<SwordSkill>(transform.position);
        Speed = 6.0f;

        return true;
    }

    public override void UpdateController()
    {
        MovePlayer();
        InputKey();
        CollectGem();
    }

    private void MovePlayer()
    {
        Vector3 movement = _moveDir * Speed * Time.deltaTime;
        transform.position += movement;

        if (_moveDir != Vector2.zero)
        {
            _indicator.eulerAngles = new Vector3(0, 0, Mathf.Atan2(-movement.x, movement.y) * 180 / Mathf.PI);
        }

        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    private void InputKey()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        _moveDir = new Vector2(horizontalInput, verticalInput).normalized;
    }

    private void CollectGem()
    {
        float sqrCollectDist = GemCollectDistance * GemCollectDistance;

        var findGems = Managers.Grid.GatherObjects(transform.position, GemCollectDistance);

        foreach (var gameObject in findGems)
        {
            GemController gem = gameObject.GetComponent<GemController>();

            Vector3 dir = gem.transform.position - transform.position;
            if (dir.sqrMagnitude <= sqrCollectDist)
            {
                Managers.Object.Despawn(gem);
            }
        }
    }
}
