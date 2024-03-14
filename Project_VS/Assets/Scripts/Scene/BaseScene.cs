using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{
    public Define.Scene SceneType { get; protected set; } = Define.Scene.None;

    private void Awake()
    {
        Managers.Resource.LoadAllAsync<Object>("PreLoad", (key, count, totalCount) =>
        {
            Debug.Log($"{key} Load ¿Ï·á. {totalCount}/{count}");

            if (count == totalCount)
            {
                Init();
            }
        });
    }

    protected virtual void Init()
    {
        Object eventSystem = GameObject.FindObjectOfType(typeof(EventSystem));
        if (eventSystem == null)
        {
            eventSystem = Managers.Resource.Instantiate("EventSystem.prefab");
            DontDestroyOnLoad(eventSystem);
        }
    }

    public abstract void Clear();
}
