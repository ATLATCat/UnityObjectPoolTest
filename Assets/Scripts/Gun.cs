using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public ObjectPool bulletPool { set { _objectPool = value; } }

    [SerializeField] Transform _firePosition = null;

    [SerializeField] ObjectPool _objectPool = null;


    public GameObject Fire()
    {
        GameObject obj = _objectPool.GetObject();
        obj.transform.position = _firePosition.position;
        obj.transform.rotation = _firePosition.rotation;

        return obj;
    }
}
