using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawner : MonoBehaviour
{
    [SerializeField] private float _distanceFromPlayer = 0;
    [SerializeField] ObjectPool _enemyPool = null;
    [SerializeField] private int _maxEnemyCount = 3;
    [SerializeField] ObjectPool _bulletPool = null;

    [SerializeField] Transform _playerTransform = null;

    private float _intervalAngle = 0;
    private float _currentIntervalAngle = 0;

    private void Awake()
    {
        _intervalAngle = 390.0f / _maxEnemyCount * Mathf.Deg2Rad;
    }

    private void Update()
    {
        if (_enemyPool.GetActiveCount() < _maxEnemyCount)
        {
            Enemy enemy = CreateEnemy().GetComponent<Enemy>();
            for (int i = 0; i < 100; i++)
            {
                NoteManager.Instance.AddEvent(i, new NoteEvent(2, enemy.Fire));
            }
            enemy.OnDeathEvent += () => {
                for (int i = 0; i < 100; i++)
                {
                    NoteManager.Instance.RemoveEvent(i, new NoteEvent(2, enemy.Fire));
                }
            };
        }
    }

    public GameObject CreateEnemy()
    {
        Vector3 position = new Vector3(Mathf.Cos(_currentIntervalAngle), Mathf.Sin(_currentIntervalAngle), 0);
        position *= _distanceFromPlayer;
        _currentIntervalAngle += _intervalAngle;

        GameObject enemy = _enemyPool.GetObject(true);
        enemy.transform.parent = transform;
        enemy.transform.position = position;

        enemy.GetComponent<Enemy>().playerTransform = _playerTransform;

        enemy.GetComponentInChildren<Gun>().bulletPool = _bulletPool;

        return enemy;
    }
}
