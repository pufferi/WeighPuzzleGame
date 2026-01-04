using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SliderChangeWeightComponent : MonoBehaviour
{
  [SerializeField]
    private InputActionAsset inputActions;

    [SerializeField]
    private SliderBehaviour sliderBehaviour;

    [SerializeField]
    private BillboardText billboardText;

    private InputAction _clickWeightAction;
    private WeightItemComponent _weightItem;
    private Vector3 _initialScale;

    [SerializeField]
    private float minScale;

    [SerializeField]
    private float maxScale;

    void Start()
    {
        _weightItem = GetComponent<WeightItemComponent>();
        _initialScale = transform.localScale;

        // 设置输入事件
        _clickWeightAction = inputActions.FindActionMap("Player").FindAction("ClickWeight");
   _clickWeightAction.performed += OnClickWeight;
    }

    private void OnEnable()
    {
        _clickWeightAction?.Enable();
    }

    private void OnDisable()
    {
        _clickWeightAction?.Disable();
    }

    /// <summary>
    /// 当点击物体时触发
    /// </summary>
    private void OnClickWeight(InputAction.CallbackContext context)
    {
        // 检测鼠标点击的是否是当前物体
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
    if (hit.collider.gameObject == gameObject)
    {
        // 如果 slider 已经绑定到其他物体，先解除
      if (sliderBehaviour.GetCurrentTarget() != null && sliderBehaviour.GetCurrentTarget() != this)
      {
     sliderBehaviour.Unbind();
         }

       // 如果已经绑定到自己，则解除绑定（切换效果）
 if (sliderBehaviour.GetCurrentTarget() == this)
   {
         sliderBehaviour.Unbind();
         return;
         }

             // 绑定 slider 到当前物体
                sliderBehaviour.BindTo(this, _weightItem.realMass, _initialScale, minScale, maxScale);
            }
        }
    }

    /// <summary>
    /// 当质量通过 slider 改变时调用
    /// </summary>
    public void OnMassChanged(int newMass, Vector3 newScale)
    {
        // 更新真实质量
        if (_weightItem != null)
 {
    _weightItem.realMass = newMass;
    }

        // 更新物体缩放
        transform.localScale = newScale;

   // 如果 BillboardText 显示质量，同步更新
        if (billboardText != null && billboardText.IsShowingWeight())
        {
      billboardText.SetText(newMass.ToString());
        }
    }

    /// <summary>
    /// 获取当前真实质量
    /// </summary>
    public int GetRealMass()
    {
        return _weightItem != null ? _weightItem.realMass : 0;
 }

    /// <summary>
    /// 获取初始缩放
    /// </summary>
public Vector3 GetInitialScale()
    {
        return _initialScale;
    }
}
