using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSphere : MonoBehaviour
{

    public GameObject Sphere;
    public Vector3 startPos;
    public Vector3 startDir;

    public Transform endOfBarrel;
    public Transform bulletObj;

    private bool canShoot = true;



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canShoot)
        {
                canShoot = false;

            //GameObject sphere = Instantiate(Sphere);
                GameObject sphere = PoolManager.SharedInstance.RequestBullet();    

                startPos = endOfBarrel.transform.position;
                startDir = endOfBarrel.transform.up;

                sphere.GetComponent<SphereMovement>().SetStartParam(startPos, startDir);

                StartCoroutine(WaitUntillNextBullet());
        }
    }

    IEnumerator WaitUntillNextBullet()
    {
        yield return new WaitForSeconds(0.25f);
        canShoot = true;
    }

}
