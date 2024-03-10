using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static UnityEngine.UI.Image;

public class ResourceManager
{
    public T Load<T>(string path) where T : Object
    {
        if (typeof(T) == typeof(GameObject))
        {
            string name = path;
            int index = name.LastIndexOf('/');
            if (index >= 0)
            { 
                name = name.Substring(index + 1);
            }

            GameObject gameObject = Managers.Pool.GetOriginal(name);
            if (gameObject != null)
            { 
                return gameObject as T;
            }
        }

        T resource = Resources.Load<T>(path);
        if (resource == null)
        {
            Debug.LogError($"���ҽ��� �ε����� ���߽��ϴ�. ���: {path}");
            return null;
        }

        return resource;
    }

    public GameObject Instantiate(GameObject prefab, Transform parent = null)
    {
        if (prefab == null)
        {
            Debug.LogError($"Prefab�� �ε����� ���߽��ϴ�. ��� : {prefab.name} ");
            return null;
        }

        if (prefab.GetComponent<Poolable>() != null)
        {
            return Managers.Pool.Pop(prefab, parent).gameObject;
        }

        GameObject gameObject = Object.Instantiate(prefab, parent);
        gameObject.name = prefab.name;
        return gameObject;
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject original = Load<GameObject>($"Prefabs/{path}");
        if (original == null)
        {
            Debug.LogError($"Prefab�� �ε����� ���߽��ϴ�. ��� : {path} ");
            return null;
        }

        if (original.GetComponent<Poolable>() != null)
        { 
            return Managers.Pool.Pop(original, parent).gameObject;
        }

        GameObject gameObject = Object.Instantiate(original, parent);
        gameObject.name = original.name;
        return gameObject;
    }

    public void Destroy(GameObject gameObject)
    {
        if (gameObject == null)
        { 
            Debug.LogError($"������ {gameObject} �� �����ϴ�.");
            return;
        }

        Poolable poolable = gameObject.GetComponent<Poolable>();
        if (poolable != null)
        {
            Managers.Pool.Push(poolable);
            return;
        }

        Object.Destroy(gameObject);
    }
}
