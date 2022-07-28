using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// NoteTime에 맞춰서 목적지로 이동
/// </summary>
public class NoteMover : MonoBehaviour
{
    public float remainTime { get; set; }
    public Transform targetTransform { get; set; }
    public Vector3 targetPosition { get; set; }
    public float offset { get { return _offset; } set { _offset = value; } }
    [SerializeField] private float _offset = 0;

    private Transform _transform = null;

    // Start is called before the first frame update
    void Awake()
    {
        _transform = transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPosition;
        if (targetTransform != null)
        {
            targetPosition = targetTransform.position;
        }
        else
        {
            targetPosition = this.targetPosition;
        }

        float speed = (Vector2.Distance(_transform.position, targetPosition) - offset) / remainTime;
        Vector3 direction = targetPosition - _transform.position;
        _transform.position += direction.normalized * speed * Time.fixedDeltaTime;

        remainTime -= Time.fixedDeltaTime;

        if(remainTime <= 0)
        {
            GetComponent<PooledObject>().ReturnToPool();
        }
    }
}
