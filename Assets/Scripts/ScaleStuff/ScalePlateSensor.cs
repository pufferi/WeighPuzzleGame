using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalePlateSensor : MonoBehaviour
{
    private Transform pivotPoint;

    private List<WeightItemComponent> itemsOnPan = new List<WeightItemComponent>();

    private int totalMass = 0;

    private Rigidbody plateToAddForce;

    [SerializeField]
    private bool isBottomSensor = false;

    [SerializeField]
    private float ForceScale = 9.81f;

    private Vector3 forceDirection = new Vector3(0, -1, 0);

    void Start()
    {
        if(isBottomSensor)
        {
            forceDirection = new Vector3(0, 1, 0);
        }
        pivotPoint = transform.parent.GetComponent<Transform>();
        plateToAddForce = transform.parent.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        CalculateTotalMass();

        if (totalMass > 0)
        {
            //Debug.Log(totalMass);


            Vector3 gravityForce = forceDirection * totalMass * ForceScale;

            plateToAddForce.AddForceAtPosition(gravityForce, pivotPoint.position);
        }
    }


    // 物体进入盘子时
    void OnTriggerEnter(Collider other)
    {
        WeightItemComponent wi = other.gameObject.GetComponent<WeightItemComponent>();
        if (wi != null && !itemsOnPan.Contains(wi))
        {
            itemsOnPan.Add(wi);
        }
    }

    // 物体离开盘子时
    void OnTriggerExit(Collider other)
    {
        WeightItemComponent wi = other.gameObject.GetComponent<WeightItemComponent>();
        if (wi != null && itemsOnPan.Contains(wi))
        {
            itemsOnPan.Remove(wi);

        }
    }

    // 计算盘子上所有物体的总质量
    void CalculateTotalMass()
    {
        totalMass = 0;
        foreach (var i in itemsOnPan)
        {
            totalMass += i.realMass;
        }
    }

    public void ApplyExternalForce(Vector3 force)
    {
        plateToAddForce.AddForceAtPosition(force, pivotPoint.position);
    }
}
