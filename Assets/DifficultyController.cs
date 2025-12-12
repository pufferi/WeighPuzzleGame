using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyController:MonoBehaviour
{
    public static bool isHard = false;

    public void setDifficulty2Hard()
    {
        isHard = true;
    }

    public void SETDIFFICULTY2easy()
    {
        isHard = false;
    }
}
