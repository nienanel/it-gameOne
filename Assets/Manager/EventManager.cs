using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    public event Action<GameObject, GameObject> DestroyGameObjectEvent;
    public event Action<Enemy> EnemyDestroyedEvent;  //eliminacion al salir de la escena


  public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            Debug.Log("EventManager initialized");
        }
    }

    public void TriggerDestroyGameObjectEvent(GameObject collidedObject, GameObject colliderObject)
    {
        DestroyGameObjectEvent?.Invoke(collidedObject, colliderObject);
    }

    //limit zone
    public void TriggerEnemyDestroyedEvent(Enemy enemy)
    {
        EnemyDestroyedEvent?.Invoke(enemy);
    }

    private void OnDestroy()
    {
        DestroyGameObjectEvent = null;
        EnemyDestroyedEvent = null;
    }
}
