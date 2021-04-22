using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    private bool canSpawn;


    private void Start()
    {
        InvokeRepeating("SpawnEnemy", 1, 3);
    }
    // Update is called once per frame
  

    void SpawnEnemy()
    {
      //  if (canSpawn)
       // {
            
       // }
        //yield return new WaitForSeconds(3f);
        Instantiate(enemyPrefab, new Vector3((float)Random.Range(-15, 15), 3f, 23f), Quaternion.identity);
       // canSpawn = false;
        //canSpawn = true;
    }
}
