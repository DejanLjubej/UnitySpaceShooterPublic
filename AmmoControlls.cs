using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoControlls : MonoBehaviour
{
    public float speed;
    public int maximumRnadomNumValueForDropChance;

    [Header("PlayerSpecific")]
    public GameObject[] upgrades;
    public int pointsForEnemy;
    public int pointsForAsteroid;

    void Start()
    {
        GetComponent<Rigidbody>().velocity = Vector3.right *speed;
        if(maximumRnadomNumValueForDropChance < 3)
        {
            maximumRnadomNumValueForDropChance = 2;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        int chanceOfUpgrade = Random.Range(0, maximumRnadomNumValueForDropChance);
        Destroy(gameObject);
        if(col.gameObject.tag != "Constraints" && col.gameObject.tag != "Player")
        {

            switch (chanceOfUpgrade)
            {
                case 0: Instantiate(upgrades[0], col.transform.position, col.transform.rotation); break;
                case 1: Instantiate(upgrades[1], col.transform.position, col.transform.rotation); break;
                case 2: Instantiate(upgrades[2], col.transform.position, col.transform.rotation); break;
                default: break;
            }

            switch (col.gameObject.tag)
            {
                case "EnemyShip": ScoreCounter.score += pointsForEnemy; break;
                case "Asteroid": ScoreCounter.score += pointsForAsteroid; break;
                default: break;
            }

            Destroy(col.gameObject);
        }
        
    }
   
}
