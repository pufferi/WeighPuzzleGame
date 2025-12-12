using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue4Beginning : MonoBehaviour
{
    public TextMeshProUGUI mytext;
    public string[] lines;
    public float speed;

    private int index;


    public GameObject normal;
    public GameObject angry;
    public GameObject sunglass;

    void Start()
    {
        mytext.text = string.Empty;
        StartDialogue();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (mytext.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                mytext.text = lines[index];
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            mytext.text += c;
            yield return new WaitForSeconds(speed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            if(index==2)angry.SetActive(true);
            else if(index==8)sunglass.SetActive(true);
            mytext.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
