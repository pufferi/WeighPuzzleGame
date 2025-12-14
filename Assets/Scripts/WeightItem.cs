using UnityEngine;

public class WeightItem : MonoBehaviour
{
    // 真实质量（用于称重逻辑，而不是物理引擎的质量）
    public float realMass = 1.0f;

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            // 将物体的物理质量设为极小，避免在盘子边缘产生不真实的力矩
            rb.mass = 0.0001f;

            // 增加阻力，弥补因质量过小导致摩擦不足的问题
            rb.drag = 5f;        // 线性阻力，防止物体乱飞
            rb.angularDrag = 5f; // 角阻力，防止物体乱转
        }
    }
}
