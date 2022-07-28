using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public delegate void DeathEventHandle();
    public event DeathEventHandle OnDeathEvent;

    public Transform playerTransform { get; set; }

    [SerializeField] private Gun _gun = null;

    private Transform _transform;

    void Awake()
    {
        _transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = playerTransform.transform.position - _transform.position;
        _transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet")
        {
            OnDeathEvent?.Invoke();

            GetComponent<PooledObject>().ReturnToPool();
        }
    }

    public void Fire(int noteIndex, float preDelay)
    {
        GameObject obj = _gun.Fire();
        NoteMover mover = obj.GetComponent<NoteMover>();
        mover.targetTransform = playerTransform;
        mover.remainTime = preDelay;
    }
}
