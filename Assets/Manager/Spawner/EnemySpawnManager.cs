using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform respawnPoint;
    public float minRespawnTime = 1.0f;
    public float maxRespawnTime = 5f;
    public int maxEnemies = 100;

    private int activeEnemyCount = 0;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minRespawnTime, maxRespawnTime));

            if (activeEnemyCount < maxEnemies)
            {
                GameObject newEnemy = Instantiate(enemyPrefab, GetRandomSpawnPosition(), Quaternion.identity);
                activeEnemyCount++;

                Enemy enemyComponent = newEnemy.GetComponent<Enemy>();
                if (enemyComponent != null)
                {
                    enemyComponent.OnEnemyDestroyed += DecreaseActiveEnemyCount;
                }

                // Asignar estilo de movimiento según el EnemyController asociado al nuevo enemigo
                EnemyController enemyController = newEnemy.GetComponent<EnemyController>();
                if (enemyController != null)
                {
                    AssignMovementType(enemyController);
                }
            }
            else
            {
                yield break;
            }
        }
    }

    private void DecreaseActiveEnemyCount(Enemy enemy)
    {
        activeEnemyCount--; // Decrementar el contador de enemigos activos
    }

    // asignacion de estilo de movimiento, segun la nave spawneada
    private void AssignMovementType(EnemyController enemyController)
    {
        switch (enemyController.movementType)
        {
            case EnemyController.MovementType.Default:
                // No se requiere ninguna acción adicional para el movimiento por defecto
                break;

            case EnemyController.MovementType.LevelTwo:
                enemyController.speed = 100f;
                break;

            case EnemyController.MovementType.BossMovement:
                enemyController.speed = 80f;
                break;
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        // Calcula una posición aleatoria alrededor del respawnPoint
        float spawnRadius = 100.0f; 
        Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPosition = respawnPoint.position + new Vector3(randomCircle.x, 0.0f, randomCircle.y);

        return spawnPosition;
    }
}


