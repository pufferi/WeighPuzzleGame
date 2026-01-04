using System.Collections;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Timer Settings")]
    [SerializeField]
    private float initialTime = 60f;
    [SerializeField]
    private bool startOnAwake = true;

    [Header("References")]
    [SerializeField]
    private TextMeshProUGUI timerText;
    [SerializeField]
    private BasicButtonActions basicButtonActions; 

    private Coroutine _timerCoroutine;
    private float _remainingTime;
    private bool _isRunning;

    public System.Action OnTimerEnd;

    public bool HasTimer = true;

    private void Awake()
    {
    
        OnTimerEnd += basicButtonActions.OnFinishButtonClicked;

        if (startOnAwake)
        {
            StartTimer(initialTime);
        }
    }

    private void OnDestroy()
    {
        OnTimerEnd -= basicButtonActions.OnFinishButtonClicked;
    }

    private void OnDisable()
    {
        StopTimer();
    }

    public void StartTimer(float time)
    {
        if (_isRunning)
        {
            StopCoroutine(_timerCoroutine);
        }

        _remainingTime = time;
        _timerCoroutine = StartCoroutine(Countdown());
    }

    public void StopTimer()
    {
        if (_timerCoroutine != null)
        {
            StopCoroutine(_timerCoroutine);
        }
        _isRunning = false;
        }

    public void ResetTimer(float time)
    {
        StopTimer();
        _remainingTime = time;
        UpdateTimerText();
    }

    private IEnumerator Countdown()
    {
        _isRunning = true;
        UpdateTimerText(); 

        // 解决一下Timescale = 3 出现的跳时间问题 
        yield return new WaitForSecondsRealtime(0.5f); 

        while (_remainingTime > 0)
        {
            _remainingTime -= Time.unscaledDeltaTime;
            UpdateTimerText();
            
            yield return null; 
        }

        _remainingTime = 0;
        UpdateTimerText();
        _isRunning = false;
        OnTimerEnd?.Invoke();
    }

    private void UpdateTimerText()
    {
        if (timerText != null)
        {
            int seconds = Mathf.CeilToInt(_remainingTime);
            timerText.text = $"还有 {seconds} 秒";
        }
    }
}
