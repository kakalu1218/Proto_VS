using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    public Define.ObjectType ObjectType { get; private set; }

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
