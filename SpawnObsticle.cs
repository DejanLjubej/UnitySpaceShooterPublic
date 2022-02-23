using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObsticle : MonoBehaviour
{
    public float spawnRate;
    public float spawnrateIncrease;
    public float fastestSpawnrate = 0.1f;
    public float constraintValue;
    [Header("GameObjects")]
    public GameObject[] enemyObject;
       
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(1f);
        while (true)
        {
            GameObject enemy = enemyObject[Random.Range(0, enemyObject.Length)];
            Instantiate(enemy, transform.position + new Vector3(0, 0, Random.Range(-constraintValue, constraintValue)), enemy.transform.rotation);
            yield return new WaitForSeconds(spawnRate);
            if(spawnRate>=fastestSpawnrate)
            spawnRate -= spawnrateIncrease;
        }
    }
}
