using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderBehaviour : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    // 当前绑定的目标组件
    private SliderChangeWeightComponent _currentTarget;

    // 质量范围
    private float _minMass = 1f;
    private float _maxMass = 2f;
    private float _initialMass = 1f;

    // 初始缩放（用于计算缩放比例）
    private Vector3 _initialScale;

    // 目标缩放范围
    private float _targetMinScale;
    private float _targetMaxScale;

    void Start()
    {
        if (slider != null)
        {
            slider.onValueChanged.AddListener(OnSliderValueChanged);
        }

        // 默认隐藏 slider
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 绑定到指定的 SliderChangeWeightComponent
    /// </summary>
    public void BindTo(SliderChangeWeightComponent target, float realMass, Vector3 initialScale, float minScale, float maxScale)
    {
        _currentTarget = target;
        _initialMass = realMass;
        _initialScale = initialScale;
        _targetMinScale = minScale;
        _targetMaxScale = maxScale;

        // 设置质量范围：最小1，最大2倍真实质量
        _minMass = 1f;
        _maxMass = realMass * 2f;

        // 设置 slider 范围
        if (slider != null)
        {
            slider.minValue = _minMass;
            slider.maxValue = _maxMass;
            // 将滑块放在中间（当前真实质量位置）
            slider.value = realMass;
        }

        // 显示 slider
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 解除绑定
    /// </summary>
    public void Unbind()
    {
        _currentTarget = null;
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 获取当前绑定的目标
    /// </summary>
    public SliderChangeWeightComponent GetCurrentTarget()
    {
        return _currentTarget;
    }

    /// <summary>
    /// 当 slider 值改变时调用
    /// </summary>
    private void OnSliderValueChanged(float value)
    {
        if (_currentTarget != null)
        {
            // 计算当前质量在范围内的比例 (0到1)
            // 范围是 _minMass 到 _maxMass
            float t = Mathf.InverseLerp(_minMass, _maxMass, value);

            // 使用 Lerp 在 minScale 和 maxScale 之间插值
            float currentScaleFactor = Mathf.Lerp(_targetMinScale, _targetMaxScale, t);
            
            // 应用缩放因子到初始缩放
            // 注意：这里假设 minScale 和 maxScale 是相对于初始缩放的倍数，或者是绝对缩放值
            // 根据需求描述 "调节slider时同步调節realMass和物體的scale"，通常意味着線性插值
            // 如果 minScale/maxScale 是绝对值（例如 0.5 到 2.0），则直接乘以初始缩放的单位向量可能更合适
            // 但为了保持物体比例，我们通常缩放初始 Scale
            
            // 假设 minScale 和 maxScale 是缩放倍率 (例如 0.5x 到 2.0x)
            Vector3 newScale = _initialScale * currentScaleFactor;
            
            // 通知目标更新质量和缩放
            _currentTarget.OnMassChanged((int)value, newScale);
        }
    }

    /// <summary>
    /// 获取当前 slider 值
    /// </summary>
    public float GetCurrentValue()
    {
        return slider != null ? slider.value : 0f;
    }
}
