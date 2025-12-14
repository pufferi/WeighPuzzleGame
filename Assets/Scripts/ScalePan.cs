using UnityEngine;
using System.Collections.Generic;

public class ScalePan : MonoBehaviour
{
    private Transform pivotPoint;

    private List<WeightItem> itemsOnPan = new List<WeightItem>();

    private float totalMass = 0f;

    private Rigidbody panRb;

    void Start()
    {
        pivotPoint = GetComponent<Transform>();
        panRb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // 计算盘子上的总质量
        CalculateTotalMass();

        if (totalMass > 0)
        {
            Debug.Log(totalMass);
            // F = m * g
            Vector3 gravityForce = Physics.gravity * totalMass;

            // 在挂点位置施加合力，避免边缘力矩影响
            panRb.AddForceAtPosition(gravityForce, pivotPoint.position);
        }
    }

    // 物体进入盘子时
    void OnCollisionEnter(Collision collision)
    {
        WeightItem wi = collision.gameObject.GetComponent<WeightItem>();
        if (wi != null && !itemsOnPan.Contains(wi))
        {
            itemsOnPan.Add(wi);
        }
    }

    // 物体离开盘子时
    void OnCollisionExit(Collision collision)
    {
        WeightItem wi = collision.gameObject.GetComponent<WeightItem>();
        if (wi != null && itemsOnPan.Contains(wi))
        {
            itemsOnPan.Remove(wi);

        }
    }

    // 计算盘子上所有物体的总质量
    void CalculateTotalMass()
    {
        totalMass = 0f;
        foreach (var i in itemsOnPan)
        {
            totalMass += i.realMass;
        }
    }
}
