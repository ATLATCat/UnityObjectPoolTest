using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public enum DamageTarget
    {
        Enemy,
        Player,
    }

    public float speed { get { return _speed; } set { _speed = value; } }
    public float power { get { return _power; } set { _power = value; } }
    public DamageTarget _damageTarget { get; set; }

    [SerializeField] private float _speed = 0;
    [SerializeField] private float _power = 0;

    Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    void Start()
    {
    }

    private void OnBecameInvisible()
    {
        GetComponent<PooledObject>().ReturnToPool();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        _transform.Translate(_speed * _transform.right * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "PlayerBullet")
        {
            gameObject.SetActive(false);
        }
    }
}
