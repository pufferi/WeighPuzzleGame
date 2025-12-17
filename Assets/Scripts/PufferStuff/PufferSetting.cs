using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem;

public class PufferSetting : MonoBehaviour
{
    [SerializeField]
    private bool shouldJump = false;

    [SerializeField]
    private float timeBeforeJump = 10f;

    [SerializeField]
    private float pufferInflateTime = 3f;

    [SerializeField]
    private float pufferClickCount = 5;

    [SerializeField]
    private TextMeshPro clickCountTextMesh;

    [SerializeField]
    private Vector3 _textOffset = new Vector3(0, 1.5f, 0);

    private PufferBehaviour pufferBehaviour;

    [SerializeField]
    private InputActionAsset inputActions;

    private InputAction _clickFishAction;
    private float _currentClickCount;
    private bool _canBeClicked = false;

    private void OnEnable()
    {
        _clickFishAction?.Enable();
    }

    private void OnDisable()
    {
        _clickFishAction?.Disable();
    }

    void Start()
    {
        pufferBehaviour = GetComponent<PufferBehaviour>();
        clickCountTextMesh.gameObject.SetActive(false);
  
        pufferInflateTime *= 3;//timescale调的3
        timeBeforeJump *= 3;

        _clickFishAction = inputActions.FindActionMap("Player").FindAction("ClickFish");
        _clickFishAction.performed += OnClickFish;

        if (shouldJump)
        {
            StartCoroutine(JumpAfterDelay());
        }
    }

    private void Update()
    {
        if (clickCountTextMesh != null && clickCountTextMesh.gameObject.activeInHierarchy)
        {
            clickCountTextMesh.transform.position = pufferBehaviour.transform.position + _textOffset;
            clickCountTextMesh.transform.LookAt(Camera.main.transform);
        }
    }

    private void OnClickFish(InputAction.CallbackContext context)
    {

        if (!_canBeClicked || !clickCountTextMesh.gameObject.activeInHierarchy)
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                _currentClickCount--;
                UpdateClickCount();

                if (_currentClickCount <= 0)
                {
                    pufferBehaviour.PufferDeflate(pufferInflateTime);
                    pufferBehaviour.PufferResetColor();
                    pufferBehaviour.PufferStopSplash();
                    clickCountTextMesh.gameObject.SetActive(false);
                    StartCoroutine(JumpAfterDelay());
                }
            }
        }
    }


    IEnumerator JumpAfterDelay()
    {
        _currentClickCount = pufferClickCount;
        yield return new WaitForSeconds(timeBeforeJump);

        _canBeClicked = true;
        UpdateClickCount();
        clickCountTextMesh.gameObject.SetActive(true);
        Coroutine inflateCoroutine = pufferBehaviour.PufferInflate(pufferInflateTime);
        Coroutine changeColorCoroutine = pufferBehaviour.PufferChangeColor(Color.red, pufferInflateTime);


        yield return inflateCoroutine;
        yield return changeColorCoroutine;

        pufferBehaviour.PufferSplash();
    }

    public void UpdateClickCount()
    {
        clickCountTextMesh.text = $"{_currentClickCount}";
    }
}
