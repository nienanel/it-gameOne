using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum MovementType
    {
        Default,
        LevelTwo,
        BossMovement
    }

    [SerializeField]
    public MovementType movementType = MovementType.Default;

    public float speed = 200f;

    private float initialXPosition;
    private float directionX = 1.0f;
    private float movementRange = 100f;
    private float movementSpeed = 100f;

    private void Start()
    {
        initialXPosition = transform.position.x;
        StartRandomMovement();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        switch (movementType)
        {
            case MovementType.Default:
                // Movimiento por defecto (hacia abajo)
                float newXPosition = transform.position.x + (directionX * movementSpeed * Time.deltaTime);
                float newZPosition = transform.position.z - (movementSpeed * Time.deltaTime);
                transform.position = new Vector3(newXPosition, transform.position.y, newZPosition);

                if (Mathf.Abs(newXPosition - initialXPosition) >= movementRange)
                {
                    ChangeMovementDirection();
                }
                break;

            case MovementType.LevelTwo:
                float newXPositionTwo = transform.position.x + (directionX * movementSpeed * Time.deltaTime);
                float newZPositionTwo = transform.position.z - (movementSpeed * Time.deltaTime);
                transform.position = new Vector3(newXPositionTwo, transform.position.y, newZPositionTwo);

                if (Mathf.Abs(newXPositionTwo - initialXPosition) >= movementRange)
                {
                    ChangeMovementDirection();
                }
                break;

            case MovementType.BossMovement:
                float distanceFromInitialPosition = Mathf.Abs(transform.position.x - initialXPosition);
                float threshold = 0.1f;

                if (directionX > 0 && distanceFromInitialPosition >= movementRange - threshold)
                {
                    // Si está cerca de la posición máxima, cambia la dirección
                    ChangeMovementDirection();
                }
                else if (directionX < 0 && distanceFromInitialPosition <= threshold)
                {
                    // Si está cerca de la posición inicial, cambia la dirección
                    ChangeMovementDirection();
                }

                float newXPositionTree = transform.position.x + (directionX * movementSpeed * Time.deltaTime);
                float newZPositionTree = transform.position.z - (movementSpeed * Time.deltaTime);
                transform.position = new Vector3(newXPositionTree, transform.position.y, newZPositionTree);
                break;
        }
    }

    private void StartRandomMovement()
    {
        directionX = Random.value < 0.5f ? -1.0f : 1.0f;
    }

    private void ChangeMovementDirection()
    {
        directionX *= -1.0f;
    }
}