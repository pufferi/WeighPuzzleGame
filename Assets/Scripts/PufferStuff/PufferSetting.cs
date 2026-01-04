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
    private BillboardText billboardText;

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
        
// 设置billboard跟随目标
        if (billboardText != null)
    {
     billboardText.SetTarget(pufferBehaviour.transform);
            billboardText.Hide();
     }
  
        pufferInflateTime *= 3;//timescale调的3
timeBeforeJump *= 3;

        _clickFishAction = inputActions.FindActionMap("Player").FindAction("ClickFish");
        _clickFishAction.performed += OnClickFish;

        if (shouldJump)
        {
            StartCoroutine(JumpAfterDelay());
 }
    }

 private void OnClickFish(InputAction.CallbackContext context)
    {

    if (!_canBeClicked || billboardText == null || !billboardText.IsVisible())
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
            billboardText.Hide();
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
        billboardText.Show();
        Coroutine inflateCoroutine = pufferBehaviour.PufferInflate(pufferInflateTime);
        Coroutine changeColorCoroutine = pufferBehaviour.PufferChangeColor(Color.red, pufferInflateTime);


      yield return inflateCoroutine;
        yield return changeColorCoroutine;

        pufferBehaviour.PufferSplash();
    }

    public void UpdateClickCount()
    {
        billboardText.SetText($"{_currentClickCount}");
    }
}
