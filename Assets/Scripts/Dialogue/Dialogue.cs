using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events; // 1. 添加 using 指令
using UnityEngine.InputSystem;
using UnityEngine.UI;

[System.Serializable]
public class DialogueLine
{
    public Sprite icon;
    [TextArea(3, 10)]
    public string text;
}

public class Dialogue : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset inputActions;

    private InputAction _ClickDialogueAction;

    [Header("UI References")]
    public TextMeshProUGUI dialogueText;
    public Image DialogueIcon;

    [Header("Dialogue Content")]
    public DialogueLine[] dialogueLines; 
    public float speed;

    [Header("Events")] 
    public UnityEvent OnDialogueEnd;

    private int index;

    private void OnEnable()
    {
        if (_ClickDialogueAction != null)
        {
            _ClickDialogueAction.Enable();
            _ClickDialogueAction.performed += OnClickDialogue;
        }
    }

    private void OnDisable()
    {
        if (_ClickDialogueAction != null)
        {
            _ClickDialogueAction.performed -= OnClickDialogue;
            _ClickDialogueAction.Disable();
        }
    }

    void Start()
    {
        _ClickDialogueAction = inputActions.FindActionMap("UI").FindAction("ClickDialogue");
        OnEnable();

        dialogueText.text = string.Empty;
        StartDialogue();
    }

    private void OnClickDialogue(InputAction.CallbackContext context)
    {
        if (dialogueText.text == dialogueLines[index].text)
        {
            NextLine();
        }
        else
        {
            StopAllCoroutines();
            dialogueText.text = dialogueLines[index].text;
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        if (dialogueLines[index].icon != null)
        {
            DialogueIcon.sprite = dialogueLines[index].icon;
        }

        foreach (char c in dialogueLines[index].text.ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(speed);
        }
    }

    void NextLine()
    {
        if (index < dialogueLines.Length - 1)
        {
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            OnDialogueEnd?.Invoke();
            gameObject.SetActive(false);
        }
    }

}