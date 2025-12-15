using System.Collections;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timerText;

    private Coroutine _timerCoroutine;
    private float _remainingTime;
    private bool _isRunning;

    public System.Action OnTimerEnd;

    private void OnDisable()
    {
        StopTimer();
    }

    public void StartTimer(float time = 30f)
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

    public void ResetTimer(float time = 30f)
    {
        StopTimer();
        _remainingTime = time;
        UpdateTimerText();
    }

    private IEnumerator Countdown()
    {
        _isRunning = true;
        while (_remainingTime > 0)
        {
            UpdateTimerText();
            //_remainingTime -= Time.deltaTime;  timeScale改成3了
            _remainingTime -= Time.unscaledDeltaTime;

            yield return null;
        }

        _remainingTime = 0;
        UpdateTimerText();
        _isRunning = false;
        OnTimerEnd?.Invoke();
    }

    private void UpdateTimerText()
    {
        int seconds = Mathf.CeilToInt(_remainingTime);
        timerText.text = $"还有 {seconds} 秒";
    }
}
