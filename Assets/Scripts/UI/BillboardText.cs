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
            textMesh.text = weightConponent.realMass.ToString();


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


    public TextMeshPro GetTextMesh()
    {
        return textMesh;
    }
}
