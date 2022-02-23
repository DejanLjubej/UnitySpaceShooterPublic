using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControlls : MonoBehaviour
{
    [Header("Player controlls")]
    public float baseSpeed;
    private float speed;
    private RaycastHit hit;

    [Header("Player fighting")]
    public GameObject baseAmmo;
    public GameObject ammoSpawnPoint;
    public float baseFireRate;
    private float nextShot;
    private float fireRate;
    
    [Header("Player Upgrades")]
    public float upgradedSpeed;
    public float upgradedFireRate;
    public bool upgradedAmmo=false;
    public GameObject shield;
    private bool shieldsOn=false;

    [Header("Animations")]
    private Animator shieldAnimator;

    [Header("Misclenious")]
    public static bool isGameOver;
    public GameObject panel;
    void Start()
    {
        panel.SetActive(false);
        isGameOver = false;
        fireRate = baseFireRate;
        speed = baseSpeed;
        shield.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform != null)
                {
                    Vector3 newPosition = new Vector3(hit.point.x, 0, hit.point.z);
                    float step = speed * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, newPosition, step);
                }
            }
            if (Time.time >= nextShot)
            {
                Instantiate(baseAmmo, ammoSpawnPoint.transform.position, baseAmmo.transform.rotation);
                nextShot = Time.time + fireRate;
            }
        }
    }
    
    void OnTriggerEnter(Collider col)
    {
       if (col.gameObject.tag == "Asteroid" || col.gameObject.tag == "EnemyShip" || col.gameObject.tag=="EnemyAmmo")
        {
            if (!shieldsOn)
            {
                panel.SetActive(true);
                Destroy(gameObject);
            }
            else
            {
                Destroy(col.gameObject);
            }
        }
        if (col.gameObject.tag == "ShieldUpgrade")
        {
            shieldAnimator = shield.GetComponent<Animator>();

            shieldsOn = true;
            shield.SetActive(true);
            HandleUpgradeMutualities(col.gameObject, col.gameObject.tag);
        }
        if (col.gameObject.tag == "SpeedUpgrade")
        {
            speed = upgradedSpeed;
            HandleUpgradeMutualities(col.gameObject, col.gameObject.tag);
        }
        if (col.gameObject.tag == "FireRateUpgrade")
        {
            fireRate = upgradedFireRate;
            HandleUpgradeMutualities(col.gameObject, col.gameObject.tag);
        }
    }

    void HandleUpgradeMutualities(GameObject go, string upgradeTag)
    {
        Destroy(go);
        StopCoroutine($"TimerFor{upgradeTag}");
        StartCoroutine($"TimerFor{upgradeTag}");
    }

    //IEnumerators
    #region 
    IEnumerator TimerForFireRateUpgrade()
    {
        yield return new WaitForSeconds(5f);
        fireRate = baseFireRate;
    }
    IEnumerator TimerForSpeedUpgrade()
    {
        yield return new WaitForSeconds(5f);
        speed = baseSpeed;
    } 
    IEnumerator TimerForShieldUpgrade()
    {
        
        yield return new WaitForSeconds(5f);
        shieldAnimator.SetTrigger("ShieldOff");
        yield return new WaitForSeconds(1f);
        shield.SetActive(false);
        shieldsOn=false;
    }
    #endregion
}
