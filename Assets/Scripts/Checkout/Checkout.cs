using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Checkout : MonoBehaviour
{
    [SerializeField]
    private GameObject parent_Checkout_Layout;
    public List<TMP_InputField> inputFields;

    [SerializeField]
    private PufferGetter pufferGetter;

    private List<int> realPufferMass;
    private List<int> PlayerMeasuredPufferMass;


    private List<TextMeshProUGUI> realMassText;
    private List<TextMeshProUGUI> playerMeasuredMassText;
    private List<Image> fishImgs;

    [SerializeField]
    private bool isLevel7 = false;


    [SerializeField]
    private TextMeshProUGUI InvalidInputTip_AND_Result_Text;


    public void SetupCheckoutUI()
    {
        InvalidInputTip_AND_Result_Text.gameObject.SetActive(false);
        fishImgs = parent_Checkout_Layout.GetComponentsInChildren<Image>(true)
            .Where(img => img.gameObject.name == "Image_FishIcon")
            .ToList();

        List<Color> fishColors = pufferGetter.GetAllPufferColor();
        
        int loopCount = Mathf.Min(fishImgs.Count, fishColors.Count);
        for (int i = 0; i < loopCount; i++)
        {
            Color fishColor = fishColors[i];
            fishColor.a = 1f;
            fishImgs[i].color = fishColor;
        }
    }

    public bool CheckAnsers()
    {

            
        PlayerMeasuredPufferMass = GetPlayerMeasuredMass();
        InvalidInputTip_AND_Result_Text.gameObject.SetActive(true);

        if (PlayerMeasuredPufferMass.Contains(-1))
        {
            InvalidInputTip_AND_Result_Text.text = "请输入 1 到 2,147,483,647 之间的整数值";
            return false;
        }

        if (isLevel7)
        {
            Check4lv7();
            return true;
        }

        realPufferMass = pufferGetter.GetAllPufferMass();

     

        GetMassText();


        if (PlayerMeasuredPufferMass.Count != realPufferMass.Count || PlayerMeasuredPufferMass.Count != realMassText.Count || realMassText.Count != playerMeasuredMassText.Count)
            Debug.Log("iykyk");

        for(int i = 0; i < inputFields.Count; i++)
        {
            inputFields[i].gameObject.SetActive(false);
        }


        float totalScore = 0f;
        for (int i = 0; i < realPufferMass.Count; i++)
        {
            int realMass = realPufferMass[i];
            int playerMass = PlayerMeasuredPufferMass[i];

            // 显示真实质量和玩家答案
            realMassText[i].text = "它的质量是： " + realMass.ToString();
            playerMeasuredMassText[i].text = "你的答案是：  " + playerMass.ToString();

            // 计算单项得分
            if (realMass > 0)
            {
                float absoluteError = Mathf.Abs(realMass - playerMass);
                float percentageError = absoluteError / realMass;
                // 新的计分公式，惩罚与误差的平方成正比，对小误差更宽容
                float itemScore = Mathf.Max(0f, 100f * (1f - percentageError * percentageError));
                totalScore += itemScore;

                // 根据答案的准确度设置颜色 (放宽阈值)
                if (percentageError <= 0.05f) // 误差在5%以内算完美
                {
                    playerMeasuredMassText[i].color = Color.green; // 完全正确
                }
                else if (percentageError <= 0.3f) // 误差在30%以内算比较接近
                {
                    playerMeasuredMassText[i].color = new Color(1.0f, 0.5f, 0.0f); // 橙色
                }
                else
                {
                    playerMeasuredMassText[i].color = Color.red; // 误差较大
                }
            }
            else
            {
                playerMeasuredMassText[i].color = Color.red;
            }
        }

        // 计算并显示最终平均分
        float averageScore = totalScore / realPufferMass.Count;
        InvalidInputTip_AND_Result_Text.text = $"你的平均成绩是: {averageScore:F1} / 100"; // F1表示保留一位小数

        return true;

    }


    private List<int> GetPlayerMeasuredMass()
    {
        var readList = new List<int>();
        inputFields.Clear();
        inputFields = new List<TMP_InputField>(parent_Checkout_Layout.GetComponentsInChildren<TMP_InputField>());

        foreach(var inputField in inputFields)
        {
            if(int.TryParse(inputField.text, out int value))
            {
                if(value >= 1)
                    readList.Add(value);
                else
                    readList.Add(-1);
            }
            else
            {
                readList.Add(-1);
            }
        }
        return readList;
    }

    private void GetMassText()
    {

        realMassText= parent_Checkout_Layout.GetComponentsInChildren<TextMeshProUGUI>(true) 
            .Where(textMesh => textMesh.gameObject.name == "Text_RealMass")
            .ToList();

        playerMeasuredMassText = parent_Checkout_Layout.GetComponentsInChildren<TextMeshProUGUI>(true)
            .Where(textMesh => textMesh.gameObject.name == "Text_PlayerAnser")
            .ToList();
    }

    private void Check4lv7()
    {
        GetMassText();
        
        for(int i = 0; i < inputFields.Count; i++)
        {
            inputFields[i].gameObject.SetActive(false);
        }

        int realMass = 2000000;
        int playerMass = PlayerMeasuredPufferMass.Count > 0 ? PlayerMeasuredPufferMass[0] : 0;

        if (realMassText.Count > 0)
            realMassText[0].text = "它的质量是： " + realMass.ToString();
        
        if (playerMeasuredMassText.Count > 0)
            playerMeasuredMassText[0].text = "你的答案是：  " + playerMass.ToString();

        float totalScore = 0f;
        if (realMass > 0)
        {
            float absoluteError = Mathf.Abs(realMass - playerMass);
            float percentageError = absoluteError / realMass;
            
            float itemScore = Mathf.Max(0f, 100f * (1f - percentageError * percentageError));
            totalScore = itemScore;

            if (playerMeasuredMassText.Count > 0)
            {
                if (percentageError <= 0.05f)
                {
                    playerMeasuredMassText[0].color = Color.green;
                }
                else if (percentageError <= 0.3f)
                {
                    playerMeasuredMassText[0].color = new Color(1.0f, 0.5f, 0.0f);
                }
                else
                {
                    playerMeasuredMassText[0].color = Color.red;
                }
            }
        }

        InvalidInputTip_AND_Result_Text.text = $"你的平均成绩是: {totalScore:F1} / 100";
    }
}
