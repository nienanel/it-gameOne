using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public delegate void EnemyDestroyedDelegate(Enemy enemy);
    public Action<Enemy> OnEnemyDestroyed;

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            Destroy(gameObject);
            EventManager.Instance.TriggerDestroyGameObjectEvent(gameObject, enemy.gameObject); // Trigger DestroyGameObjectEvent
            enemy.DestroyEnemy();
        }
    }

    private void DestroyEnemy()
    {
        Debug.Log("Enemy destroyed");
        OnEnemyDestroyed?.Invoke(this);
        Destroy(gameObject);
    }
}

