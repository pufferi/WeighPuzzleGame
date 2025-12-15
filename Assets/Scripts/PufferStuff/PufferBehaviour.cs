using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PufferBehaviour : MonoBehaviour
{
    [SerializeField]
    private Vector3 inflateScale = new Vector3(1f, 2f, 2f);

    [SerializeField]
    private Vector3 splashDirection = new Vector3(0f, 1f, 0f);

    [SerializeField]
    private float minSplashForce = 5f;
    [SerializeField]
    private float maxSplashForce = 15f;

    [SerializeField]
    private float maxSideSplashForce = 5f;

    //鱼转动力范围
    [SerializeField]
    private float maxTorqueForce = 2f;

    private float splashForceScale = 0.01f;

    private Vector3 _originalScale;
    private Coroutine _inflateDeflateCoroutine;
    private Coroutine _splashCoroutine;
    private bool _isGrounded = true;
    private ScalePlateSensor _currentPlateSensor;

    private Material _pufferMat;
    private Color _pufferDefaultColor;
    private Coroutine _pufferChangeColorCoroutine;

    void Start()
    {
        _originalScale = transform.localScale;
        _pufferMat = GetComponent<Renderer>().material;
        _pufferDefaultColor = _pufferMat.color;
    }

    void Update()
    {
        
    }


    public Coroutine PufferChangeColor(Color color, float time = 2f)
    {
        if (_pufferChangeColorCoroutine != null)
        {
            StopCoroutine(_pufferChangeColorCoroutine);
        }

        _pufferChangeColorCoroutine = StartCoroutine(PufferChangeColorCoroutine(color, time));
        return _pufferChangeColorCoroutine;
    }

    IEnumerator PufferChangeColorCoroutine(Color color, float time)
    {
        Color initialColor = _pufferMat.color;
        float timer = 0f;
        while (timer < time)
        {
            _pufferMat.color = Color.Lerp(initialColor, color, timer / time);
            timer += Time.deltaTime;
            yield return null;
        }
        _pufferMat.color = color;
    }

    public void PufferResetColor()
    {
        if(_pufferChangeColorCoroutine != null)
        {
            StopCoroutine(_pufferChangeColorCoroutine);
        }
        _pufferChangeColorCoroutine = StartCoroutine(PufferChangeColorCoroutine(_pufferDefaultColor, 2f));

    }

    public Coroutine PufferInflate(float inflateTime)
    {
        if (_inflateDeflateCoroutine != null)
        {
            StopCoroutine(_inflateDeflateCoroutine);
        }

        _inflateDeflateCoroutine = StartCoroutine(PufferInflateCoroutine(inflateTime));
        return _inflateDeflateCoroutine;
    }
    IEnumerator PufferInflateCoroutine(float inflateTime)
    {
        Vector3 initialScale = transform.localScale;
        Vector3 targetScale = new Vector3(_originalScale.x * inflateScale.x, _originalScale.y * inflateScale.y, _originalScale.z * inflateScale.z);

        float timer = 0f;
        while (timer < inflateTime)
        {
            transform.localScale = Vector3.Lerp(initialScale, targetScale, timer / inflateTime);
            timer += Time.deltaTime;
            yield return null;
        }
        transform.localScale = targetScale;
        _inflateDeflateCoroutine = null;
        PufferSplash();
    }


    public Coroutine PufferDeflate(float deflateTime)
    {
        if (_inflateDeflateCoroutine != null)
        {
            StopCoroutine(_inflateDeflateCoroutine);
        }

        _inflateDeflateCoroutine = StartCoroutine(PufferDeflateCoroutine(deflateTime));
        return _inflateDeflateCoroutine;
    }

    IEnumerator PufferDeflateCoroutine(float deflateTime = 2f)
    {
        Vector3 initialScale = transform.localScale;

        float timer = 0f;
        while (timer < deflateTime)
        {
            transform.localScale = Vector3.Lerp(initialScale, _originalScale, timer / deflateTime);
            timer += Time.deltaTime;
            yield return null;
        }
        transform.localScale = _originalScale;
        _inflateDeflateCoroutine = null;
    }


    public void PufferSplash(int splashTime = 99999)
    {
        if (_splashCoroutine != null)
        {
            StopCoroutine(_splashCoroutine);
        }
        _splashCoroutine = StartCoroutine(PufferSplashCoroutine(splashTime));
    }
    IEnumerator PufferSplashCoroutine(int splashTime)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component not found. Cannot perform splash.");
            yield break;
        }
        rb.useGravity = true;

        for (int i = 0; i < splashTime; i++)
        {
            yield return new WaitUntil(() => _isGrounded);
            yield return new WaitForSeconds(Random.Range(0.5f, 2f));

            _isGrounded = false;

            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

            // 力：自己的力和，如果有盘子的话，盘子的反作用力
            float splashForceAmount = Random.Range(minSplashForce, maxSplashForce);
            Vector3 upForce = splashDirection * splashForceAmount * splashForceScale;

            float sideSplashForceAmount = Random.Range(-maxSideSplashForce, maxSideSplashForce);
            Vector3 sideForce = transform.right * sideSplashForceAmount * splashForceScale;

            Vector3 totalForce = upForce + sideForce;

            rb.AddForce(totalForce, ForceMode.Force);
            if (_currentPlateSensor != null)
            {
                _currentPlateSensor.ApplyExternalForce(-totalForce * 4000f);
            }

            float torqueAmount = Random.Range(-maxTorqueForce, maxTorqueForce);
            rb.AddTorque(transform.right * torqueAmount* splashForceScale, ForceMode.Force);
            rb.AddTorque(transform.forward * torqueAmount* splashForceScale, ForceMode.Force);
        }
        _splashCoroutine = null;
    }

    public void PufferStopSplash()
    {
        if (_splashCoroutine != null)
        {
            StopCoroutine(_splashCoroutine);
            _splashCoroutine = null;
            _isGrounded = true;
        }
    }

    //处理施加给盘子的反作用力
    void OnCollisionEnter(Collision collision)
    {
        if (_splashCoroutine != null)
        {
            _isGrounded = true;
        }

        ScalePlateSensor plate = collision.gameObject.GetComponentInChildren<ScalePlateSensor>();
        if (plate != null)
        {
            _currentPlateSensor = plate;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        ScalePlateSensor plate = collision.gameObject.GetComponentInChildren<ScalePlateSensor>();
        if (plate != null && plate == _currentPlateSensor)
        {
            _currentPlateSensor = null;
        }
    }
}
