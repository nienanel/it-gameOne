using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private void OnEnable()
    {
        if (EventManager.Instance != null)
        {
            EventManager.Instance.DestroyGameObjectEvent += HandleDestroyGameObject;
        }
        else
        {
            Debug.LogWarning("problema game controller!!!");
        }
    }

    private void OnDisable()
    {
        if (EventManager.Instance != null)
        {
            EventManager.Instance.DestroyGameObjectEvent -= HandleDestroyGameObject;
        }
    }

    private void HandleDestroyGameObject(GameObject collidedObject, GameObject colliderObject)
    {
       
        if (GameManager.Instance != null)
        {
            if (collidedObject != null)
            {
                Destroy(collidedObject);
            }
            if (colliderObject != null)
            {
                Destroy(colliderObject);
            }

            Debug.Log("Destroyed objects: " + (collidedObject != null ? collidedObject.name : "null") + ", " + (colliderObject != null ? colliderObject.name : "null"));
        }
        else
        {
            Debug.LogWarning("GameManager is null. Cannot destroy objects.");
        }
    }

    private void OnDestroy()
    {
        if (EventManager.Instance != null)
        {
            EventManager.Instance.DestroyGameObjectEvent -= HandleDestroyGameObject;
        }
    }
}