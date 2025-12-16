using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Checkout : MonoBehaviour
{
    [SerializeField]
    private GameObject parent_Checkout_Layout;
    public List<TMP_InputField> inputFields;

    [SerializeField]
    private PufferMassGetter pufferMassGetter;

    private List<int> realPufferMass;
    private List<int> PlayerMeasuredPufferMass;


    private List<TextMeshProUGUI> realMassText;
    private List<TextMeshProUGUI> playerMeasuredMassText;

    public void CheckAnsers()
    {
        PlayerMeasuredPufferMass = GetPlayerMeasuredMass();
        realPufferMass = pufferMassGetter.GetAllPufferMass();

     

        GetMassText();


        if (PlayerMeasuredPufferMass.Count != realPufferMass.Count || PlayerMeasuredPufferMass.Count != realMassText.Count || realMassText.Count != playerMeasuredMassText.Count)
            Debug.Log("iykyk");

        for(int i = 0; i < inputFields.Count; i++)
        {
            inputFields[i].gameObject.SetActive(false);
        }


        for (int i = 0; i < realPufferMass.Count; i++)
        {
            realMassText[i].text = "它的质量是： " + realPufferMass[i].ToString() ;
        }


        for (int i = 0; i < realPufferMass.Count; i++)
        {
            playerMeasuredMassText[i].text = "你的答案是：  " + PlayerMeasuredPufferMass[i].ToString() ;
        }




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
                readList.Add(value);
            }
            else
            {
                readList.Add(0); 
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
}
