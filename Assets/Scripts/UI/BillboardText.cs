using UnityEngine;
using TMPro;


public class BillboardText : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro textMesh;

   [SerializeField]
    private Vector3 offset = new Vector3(0, 1.5f, 0);

    [SerializeField]
    private Transform targetTransform;

    [SerializeField]
    private bool hideOnStart = true;

    [SerializeField]
    private bool showWeight = false;

    private Camera _mainCamera;

    [SerializeField]
    private bool isLv7 = false;


    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Start()
    {
        if(hideOnStart)
            textMesh.gameObject.SetActive(false);

        if(showWeight)
        {
            WeightItemComponent weightConponent = GetComponent<WeightItemComponent>();
            if (isLv7)
            {
                textMesh.text = "1000000";
            }
            else
            {
                textMesh.text = weightConponent.realMass.ToString();
            }
        }
    }

    private void LateUpdate()
    {
        if (textMesh != null && textMesh.gameObject.activeInHierarchy && targetTransform != null)
        {
            // 更新位置
            textMesh.transform.position = targetTransform.position + offset;

        // 面向摄像机
          if (_mainCamera != null)
         {
            textMesh.transform.LookAt(_mainCamera.transform);
         }
     }
    }

    /// <summary>
    /// 设置跟随的目标物体
    /// </summary>
  public void SetTarget(Transform target)
    {
        targetTransform = target;
    }

  /// <summary>
    /// 设置偏移量
    /// </summary>
    public void SetOffset(Vector3 newOffset)
    {
        offset = newOffset;
    }


    /// </summary>
    public void Show()
    {
        if (textMesh != null)
            textMesh.gameObject.SetActive(true);
    }


    public void Hide()
    {
        textMesh.gameObject.SetActive(false);
    }


    public void SetText(string text)
    {
        textMesh.text = text;
    }


    public bool IsVisible()
    {
        return textMesh != null && textMesh.gameObject.activeInHierarchy;
    }

    /// <summary>
    /// 获取是否是 Level 7
    /// </summary>
    public bool IsLv7()
    {
        return isLv7;
    }

    /// <summary>
    /// 获取是否显示质量
    /// </summary>
    public bool IsShowingWeight()
    {
   return showWeight;
    }

    public TextMeshPro GetTextMesh()
    {
        return textMesh;
  }
}
