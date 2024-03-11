using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    private Vector2 _moveDir = Vector2.zero;
    private float _speed = 4.0f;

    [SerializeField] private Transform _indicator;

    public override bool Init()
    {
        if (base.Init() == false)
        { 
            return false;
        }
        
        return true;
    }

    public override void UpdateController()
    {
        MovePlayer();
        InputKey();
    }

    private void MovePlayer()
    {
        Vector3 movement = _moveDir * _speed * Time.deltaTime;
        transform.position += movement;

        if (_moveDir != Vector2.zero)
        {
            _indicator.eulerAngles = new Vector3(0, 0, Mathf.Atan2(-movement.x, movement.y) * 180 / Mathf.PI);
        }
    }

    private void InputKey()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        _moveDir = new Vector2(horizontalInput, verticalInput).normalized;
    }
}
