using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToLevel2 : MonoBehaviour
{
    public int i=1;
    public void toLV2()
    {
        SceneManager.LoadSceneAsync(i);
    }
}
