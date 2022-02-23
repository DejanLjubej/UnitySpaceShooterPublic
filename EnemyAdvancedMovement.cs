using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAdvancedMovement : MonoBehaviour
{
    public Vector2 maneuverTime;
    public Vector2 startWait;
    public Vector2 zEdge;
    public Vector2 xEdge;
    public float dodge;
    public float smoothing;
    
    private float currentSpeed;
    private float targetManeuver;
    void Start()
    {
        currentSpeed = GetComponent<Rigidbody>().velocity.x;
        StartCoroutine(ShipMovement());
    }

    void FixedUpdate()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        float newManeuver = Mathf.MoveTowards(rb.velocity.z, targetManeuver, smoothing * Time.fixedDeltaTime);
        rb.velocity = new Vector3(currentSpeed, 0f, newManeuver);
        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, xEdge.x, xEdge.y),
            0f,
            Mathf.Clamp(rb.position.z, zEdge.x, zEdge.y)
        );
    }

    IEnumerator ShipMovement()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));
        while (true)
        {
            targetManeuver = Random.Range(0, dodge) * Mathf.Sign(transform.position.z);
            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
            targetManeuver = 0;
            yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));
        }
    }
}
