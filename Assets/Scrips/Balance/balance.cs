using System.Collections;
using UnityEngine;


public class balance : MonoBehaviour
{
    
    public Transform pivot;


    public float left;
    public float right;

    public float angle;

    public float rotationSpeed;

    private bool isRotate = false;

    private void Update()
    {
        

        float r = pivot.transform.rotation.x;
        Debug.Log("r is"+r+"    left is "+left+"   right is "+right);
        if ((Mathf.Abs(left - right) <= 2f) &&((r > -0.000001&&(left<right)) || (r < 0.000001&&(left>right))))
        {
            isRotate=true;
            StopAllCoroutines();
            StartCoroutine(Freeze());
        }
        
        if(Mathf.Abs(left - right) <= 5f)isRotate=false;
        if (DifficultyController.isHard) hard();
        else easy();
    }

    private void easy()
    {
        //转动x轴
        left = MeasuringController.Instance.GetAllCounterWeight();
        right = MeasuringController.Instance.GetAllPufferWeight();

        float all = left + right;
        float rightRatio = right / all;


        angle = rightRatio * 60f;
        if (left > right) angle *= -1;

        rotationSpeed = 0.25f;

        if (!isRotate)
        {
            StartCoroutine(co_rotation(angle, rotationSpeed));
        }
    }


    private void hard()
    {
        //转动x轴
        left = WeightOnBalanceController.Instance.getWeighWeight();
        right = WeightOnBalanceController.Instance.getFishWeight();

        float all = left + right;
        float rightRatio = right / all;

        
        angle = rightRatio * 60f;
        if (left > right) angle *= -1;

        rotationSpeed = 0.25f;

        if (!isRotate)
        {
            StartCoroutine(co_rotation(angle, rotationSpeed));
        }

    }


    private IEnumerator co_rotation(float angle, float rotationSpeed = 1f)
    {
        isRotate = true;
        pivot.transform.Rotate(Vector3.right, angle * rotationSpeed * Time.deltaTime);

        // 获取当前的旋转角度
        Vector3 currentRotation = pivot.transform.rotation.eulerAngles;

        if (currentRotation.x > 180)
        {
            currentRotation.x -= 360;
        }
        currentRotation.x = Mathf.Clamp(currentRotation.x, -14f, 14f);

        pivot.transform.rotation = Quaternion.Euler(currentRotation);

        isRotate = false;
        yield return null;
    }


    private IEnumerator Freeze()
    {
        Debug.Log("frezzing");
        rotationSpeed = 0.01f;

        // 目标旋转角度（水平位置）
        Quaternion targetRotation = Quaternion.Euler(0, 90, 0);

        // 当前旋转角度
        Quaternion currentRotation = pivot.transform.rotation;

        // 在Update方法中逐帧调用
        pivot.transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);

        if(Mathf.Abs(left-right)<3&&Mathf.Abs(pivot.transform.rotation.x)<0.005)
        pivot.transform.rotation = Quaternion.Euler(0, 90, 0);

        yield return null;
    }

}
