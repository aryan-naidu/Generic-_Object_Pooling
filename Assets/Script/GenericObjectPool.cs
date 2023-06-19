using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private readonly T _prefab;
    private readonly List<T> _objectPool = new List<T>();

    public ObjectPool(T prefab)
    {
        _prefab = prefab;
    }

    public T GetObject()
    {
        T obj = SearchPool();

        if (obj == null)
        {
            obj = CreateNewObject();
            _objectPool.Add(obj);
        }

        obj.gameObject.SetActive(true);
        return obj;
    }

    public void ReturnObject(T obj)
    {
        obj.gameObject.SetActive(false);
    }

    private T SearchPool()
    {
        foreach (T obj in _objectPool)
        {
            if (!obj.gameObject.activeSelf)
            {
                return obj;
            }
        }
        return null;
    }

    private T CreateNewObject()
    {
        T obj = GameObject.Instantiate(_prefab);
        obj.gameObject.SetActive(false);
        return obj;
    }
}
