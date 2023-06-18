using System.Collections.Generic;
using UnityEngine;

public class GenericObjectPool<T> where T : MonoBehaviour
{
    private Queue<T> objectQueue = new Queue<T>();
    private T _object;

    public GenericObjectPool(T obj)
    {
        this._object = obj;
    }

    public T GetObject()
    {
        if (objectQueue.Count == 0)
        {
            return CreateNewObject();
        }
        else
        {
            T obj = objectQueue.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }
    }

    public void ReturnObject(T obj)
    {
        obj.gameObject.SetActive(false);
        objectQueue.Enqueue(obj);
    }

    private T CreateNewObject()
    {
        T newObj = Object.Instantiate(_object);
        newObj.gameObject.SetActive(true);
        return newObj;
    }
}
