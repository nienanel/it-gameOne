using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            Debug.Log("GameManager initialized.");

            if (EventManager.Instance != null)
            {
                EventManager.Instance.DestroyGameObjectEvent += DestroyGameObject;
            }
            else
            {
                Debug.LogWarning("Problema con el Game Controller: EventManager no está inicializado correctamente.");
            }
        }
    }

    private void OnDisable()
    {
        if (EventManager.Instance != null)
        {
            EventManager.Instance.DestroyGameObjectEvent -= DestroyGameObject; // Darse de baja del evento DestroyGameObjectEvent al desactivar el script
        }
    }

    private void DestroyGameObject(GameObject collidedObject, GameObject colliderObject)
    {
        int laserBeanLayer = LayerMask.NameToLayer("LaserBean");

        if (collidedObject != null && collidedObject.layer == laserBeanLayer)
        {
            Destroy(collidedObject);
        }

        if (colliderObject != null && colliderObject.layer == laserBeanLayer)
        {
            Destroy(colliderObject);
        }
    }
}
