using UnityEngine;

public class WeightItemComponent : MonoBehaviour
{
    // 真实质量（用于称重逻辑，不是物理引擎的质量）
    public int realMass = 1;

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.mass = 0.0001f;

            // 摩擦
            rb.drag = 5f;        // 线性阻力，防止物体乱飞
            rb.angularDrag = 5f; // 角阻力，防止物体乱转
        }
    }
}
