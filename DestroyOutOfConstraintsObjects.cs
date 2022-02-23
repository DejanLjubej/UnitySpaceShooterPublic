using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfConstraintsObjects : MonoBehaviour
{
    void OnTriggerExit(Collider collider)
    {
        Destroy(collider.gameObject);
    }
}
