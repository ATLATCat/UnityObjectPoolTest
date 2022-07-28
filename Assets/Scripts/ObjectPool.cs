using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPool : MonoBehaviour
{
    public int GetActiveCount() { return _size - _pooledObjects.Count; }

    [SerializeField] private int _size = 0;
    [SerializeField] private GameObject _createPrefab = null;

    private List<GameObject> _pooledObjects = null;

    private void Awake()
    {
        _pooledObjects = new List<GameObject>(_size);

        while(_pooledObjects.Count < _size)
        {
            AddPooledObject(CreateObject());
        }
    }

    public GameObject GetObject(bool isActive = true)
    {
        GameObject obj;

        if (_pooledObjects.Count > 0)
        {
            obj = _pooledObjects[_pooledObjects.Count - 1];
            _pooledObjects.RemoveAt(_pooledObjects.Count - 1);
        }
        else
        {
            _size++;
            obj = CreateObject();
        }

        obj.SetActive(isActive);

        return obj;
    }

    public GameObject CreateObject()
    {
        GameObject obj = Instantiate(_createPrefab);
        PooledObject pooled = obj.GetComponent<PooledObject>();
        if (pooled == null)
        {
            pooled = obj.AddComponent<PooledObject>();
        }
        pooled.objectPool = this;

        return obj;
    }

    public void AddPooledObject(GameObject obj)
    {
        obj.SetActive(false);
        _pooledObjects.Add(obj);
    }

    private void OnDestroy()
    {
        DestroyPool();
    }

    public void DestroyPool()
    {
        foreach(var obj in _pooledObjects)
        {
            GameObject.Destroy(obj);
        }
    }    
}
