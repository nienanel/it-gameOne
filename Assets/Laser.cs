using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public GameObject laserPrefab;
    public float firePower = 200f;


    public void FireLasers(Transform gunTransform, Transform gun2Transform)
    {
        Rigidbody laserRigidbody1 = CreateLaserBeam(gunTransform);
        Rigidbody laserRigidbody2 = CreateLaserBeam(gun2Transform);

        laserRigidbody1.AddForce(gunTransform.forward * 500 * firePower);
        laserRigidbody2.AddForce(gun2Transform.forward * 500 * firePower);

        Debug.Log("Laser fired");
    }

    private Rigidbody CreateLaserBeam(Transform spawnTransform)
    {
        GameObject laserInstance = Instantiate(gameObject, spawnTransform.position, Quaternion.Euler(90f, 0f, 0f));
        return laserInstance.GetComponent<Rigidbody>();
    }
}
