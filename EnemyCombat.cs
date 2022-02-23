using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{

    public float minFireRate;
    public float maxFireRate;
    public GameObject ammo;
    public GameObject ammoSpawn;

    private float nextShot;

    void Start()
    {
        nextShot = Time.time + Random.Range(minFireRate, maxFireRate);
    }
    void Update()
    {
        if(Time.time > nextShot)
        {
            Instantiate(ammo, ammoSpawn.transform.position, ammo.transform.rotation);
            nextShot += Time.time + Random.Range(minFireRate, maxFireRate);
        }
    }
}
