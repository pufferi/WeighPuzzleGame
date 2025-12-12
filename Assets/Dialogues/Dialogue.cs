using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI mytext;
    public string[] lines;
    public float speed;

    private int index;


    void Start()
    {
        mytext.text=string.Empty;
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
        foreach(char c in lines[index].ToCharArray()) 
        {
            mytext.text += c;
            yield return new WaitForSeconds(speed);
        }
    }

    void NextLine()
    {
        if(index < lines.Length - 1)
        {
            index++;
            mytext.text=string.Empty ;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

}
