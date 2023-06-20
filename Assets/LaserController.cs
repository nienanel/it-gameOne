using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public GameObject laserPrefab;
    public Transform gunTransform;
    public Transform gun2Transform;

    private List<GameObject> laserBeans = new List<GameObject>();
    private int maxBeans = 12;
    private GameObject lastFiredLaserBean;
    public float firePower = 200f;

    // Initialize the LaserController
    public void Initialize()
    {
        for (int i = 0; i < maxBeans; i++)
        {
            GameObject laserBean = Instantiate(laserPrefab);
            laserBean.SetActive(false);
            laserBeans.Add(laserBean);
        }
    }

    public void ShootLaser()
    {
        int startIndex = 0; // Default start index

        if (lastFiredLaserBean != null && laserBeans.Contains(lastFiredLaserBean))
        {
            startIndex = laserBeans.IndexOf(lastFiredLaserBean) + 1; // Get the index of lastFiredLaserBean + 1
            if (startIndex >= maxBeans)
                startIndex = 0; // Wrap around to the beginning if the index exceeds maxBeans
        }

        int count = 0; // Counter to track fired lasers

        for (int i = startIndex; count < maxBeans; i++)
        {
            if (i >= maxBeans) // If we reach the end of the list, wrap around to the beginning
                i = 0;

            GameObject laserBean = laserBeans[i];

            if (!laserBean.activeSelf)
            {
                laserBean.SetActive(true);

                if (count % 2 == 0) // Si el contador es par, usar gunTransform
                {
                    laserBean.transform.position = gunTransform.position;
                    laserBean.transform.rotation = Quaternion.Euler(90f, gunTransform.rotation.eulerAngles.y, gunTransform.rotation.eulerAngles.z);
                    Rigidbody laserRigidbody = laserBean.GetComponent<Rigidbody>();
                    laserRigidbody.velocity = gunTransform.forward * firePower;
                }
                else // Si el contador es impar, usar gun2Transform
                {
                    laserBean.transform.position = gun2Transform.position;
                    laserBean.transform.rotation = Quaternion.Euler(90f, gun2Transform.rotation.eulerAngles.y, gun2Transform.rotation.eulerAngles.z);
                    Rigidbody laserRigidbody = laserBean.GetComponent<Rigidbody>();
                    laserRigidbody.velocity = gun2Transform.forward * firePower;
                }

                StartCoroutine(DisableLaser(laserBean));

                lastFiredLaserBean = laserBean; // Actualizar el último láser disparado
                count++; // Incrementar el contador

                if (count >= maxBeans)
                    break; // Si se alcanza el máximo de láseres disparados, salir del bucle
            }
        }
    }

    IEnumerator DisableLaser(GameObject laserBean)
    {
        yield return new WaitForSeconds(1f);
        laserBean.SetActive(false);
    }
}
