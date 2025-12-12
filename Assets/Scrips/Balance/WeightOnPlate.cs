using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightOnPlate : MonoBehaviour
{
    float weight = 0;
    public Rigidbody weight1;
    Collider fish1Collider;
    Collider fish2Collider;

    public WeightOnPlate(GameObject fish1, GameObject fish2, Rigidbody weight1)
    {
        Debug.Log("constructing");

        fish1Collider = fish1.GetComponent<Collider>();
        if (fish1Collider == null)
            Debug.LogWarning("No Collider found on fish1.");

        fish2Collider = fish2.GetComponent<Collider>();
        if (fish2Collider == null)
            Debug.LogWarning("No Collider found on fish2.");

        this.weight1 = weight1;
    }

    private void OnCollisionStay(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag("plate"))
        {
            weight += collisionInfo.rigidbody.mass;
        }
    }

    public float WeightFish()
    {
        return weight;
    }

    public float WeightWeight()
    {
        return weight1.mass;
    }
}
