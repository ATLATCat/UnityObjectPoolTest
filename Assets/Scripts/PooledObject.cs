using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    [SerializeField] public ObjectPool objectPool { get; set; }
    
    public void ReturnToPool()
    {
        objectPool.AddPooledObject(gameObject);
    }
}
