using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    public Define.ObjectType ObjectType { get; protected set; }

    // юс╫ц ╫╨ещ
    [SerializeField] protected float _speed = 1.0f;

    private bool _init = false;
    
    private void Awake()
    {
        Init();
    }

    public virtual bool Init()
    {
        if (_init)
        {
            return false;
        }

        _init = true;
        return true;
    }

    private void Update()
    {
        UpdateController();
    }

    public abstract void UpdateController();
}
