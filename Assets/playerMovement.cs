using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerMovement : MonoBehaviour
{

    public float playerSpeed = 100f;
    public float limitZ = 100f;

    private readonly LaserController laserScript;

    public GameObject laserPrefab;
    public Transform gunTransform;
    public Transform gun2Transform;

    private LaserController laserController;

    private void Start()
    {
        laserController = gameObject.AddComponent<LaserController>();
        laserController.laserPrefab = laserPrefab;
        laserController.gunTransform = gunTransform;
        laserController.gun2Transform = gun2Transform;
        laserController.Initialize();
    }

    void Update()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalMove, 0, verticalMove);
        Vector3 directionMove = direction * playerSpeed * Time.deltaTime;
        Vector3 directionLimit = transform.position + directionMove;

        directionLimit.z = Mathf.Clamp(directionLimit.z, -limitZ, limitZ);

        transform.position = directionLimit;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            laserController.ShootLaser();
            Debug.Log("Laser fired");
        }
    }
}
