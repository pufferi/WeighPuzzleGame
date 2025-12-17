using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPage : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset inputActions;

    [SerializeField]
    private GameObject DialoguePart;

    [SerializeField]
    private GameObject LevelImgAndText;

    void Start()
    {
        inputActions.FindActionMap("UI").Disable();
        StartCoroutine(LevelStartProcess());
    }

    IEnumerator LevelStartProcess()
    {
        yield return new WaitForSeconds(3f);
        DialoguePart.SetActive(true);
        LevelImgAndText.SetActive(false);
        yield return new WaitForSeconds(1f);
        inputActions.FindActionMap("UI").Enable();
        gameObject.SetActive(false);
        
    }
}
