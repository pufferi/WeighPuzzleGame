using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class BasicButtonActions : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset inputActions;



    public void OnRestartButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //----------------------------------


    [SerializeField]
    private GameObject CheckOutUI;

    [SerializeField]
    private Button FinishButton;

    public void OnFinishButtonClicked()
    {
        checkout.SetupCheckoutUI(); 
        
        CheckOutUI.SetActive(true);
        FinishButton.enabled = false;
        inputActions.FindActionMap("Player").Disable();
    }


    //-----------------------------------

    [SerializeField]
    private Checkout checkout;

    [SerializeField]
    private Button CheckoutButton;


    private int checkout_clickTimes = 0;

    public void OnSubmitButtonClicked()
    {
        if (checkout_clickTimes == 0)
        {
            if(!checkout.CheckAnsers())
                return;

            checkout_clickTimes++;

            CheckoutButton.GetComponentInChildren<TextMeshProUGUI>().text = "下一关";
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }

}
