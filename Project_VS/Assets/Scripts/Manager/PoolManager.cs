using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoolManager
{
    #region Pool
    private class Pool
    {
        public GameObject Original { get; private set; } = null;
        public Transform Root { get; set; } = null;

        private Stack<Poolable> _poolStack = new Stack<Poolable>();

        public void Init(GameObject original, int count = 1)
        {
            Original = original;

            Root = new GameObject().transform;
            Root.name = $"{original.name}._Root";

            for (int i = 0; i < count; i++)
            {
                Push(Create());
            }
        }

        private Poolable Create()
        {
            GameObject gameObject = Object.Instantiate(Original);
            gameObject.name = Original.name;

            return gameObject.GetOrAddComponent<Poolable>();
        }

        public void Push(Poolable poolable)
        {
            if (poolable == null)
            {
                return;
            }

            poolable.transform.SetParent(Root);
            poolable.gameObject.SetActive(false);
            poolable.IsUsing = false;

            _poolStack.Push(poolable);
        }

        public Poolable Pop(Transform parent)
        {
            Poolable poolable;

            if (_poolStack.Count > 0)
            {
                poolable = _poolStack.Pop();
            }
            else
            {
                poolable = Create();
            }

            poolable.gameObject.SetActive(true);

            if (parent == null)
            {
                poolable.transform.SetParent(Managers.Scene.CurrentScene.transform);
            }

            poolable.transform.SetParent(parent);
            poolable.IsUsing = true;
            return poolable;
        }
    }
    #endregion

    private Dictionary<string, Pool> m_pool = new Dictionary<string, Pool>();
    private Transform m_root = null;

    public void Init()
    {
        if (m_root == null)
        {
            m_root = new GameObject { name = "@Pool_Root" }.transform;
            Object.DontDestroyOnLoad(m_root);
        }
    }

    public void CreatePool(GameObject original, int count = 1)
    {
        Pool pool = new Pool();
        pool.Init(original, count);
        pool.Root.parent = m_root;

        m_pool.Add(original.name, pool);
    }

    public void Push(Poolable poolable)
    {
        string name = poolable.gameObject.name;
        if (m_pool.ContainsKey(name) == false)
        {
            Object.Destroy(poolable.gameObject);
            return;
        }

        m_pool[name].Push(poolable);
    }

    public Poolable Pop(GameObject original, Transform parent = null)
    {
        if (m_pool.ContainsKey(original.name) == false)
        {
            CreatePool(original);
        }

        return m_pool[original.name].Pop(parent);
    }

    public GameObject GetOriginal(string name)
    {
        if (m_pool.ContainsKey(name) == false)
        {
            return null;
        }

        return m_pool[name].Original;
    }

    public void Clear()
    {
        foreach (Transform child in m_root)
        {
            Object.Destroy(child.gameObject);
        }

        m_pool.Clear();
    }
}
