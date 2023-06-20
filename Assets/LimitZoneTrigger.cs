using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitZoneTrigger : MonoBehaviour

{
    //private void OnTriggerEnter(Collider other)
    //{
    //    Enemy enemy = other.GetComponent<Enemy>();
    //    if (enemy != null)
    //    {
    //        EventManager.Instance.TriggerEnemyDestroyedEvent(enemy);
    //        Destroy(enemy.gameObject);
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            EventManager.Instance.TriggerEnemyDestroyedEvent(enemy);
            Destroy(enemy.gameObject);
        }
        else
        {
            Destroy(other.gameObject);
        }
    }

}
