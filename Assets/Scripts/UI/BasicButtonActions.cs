using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class BasicButtonActions : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset inputActions;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnRestartButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    [SerializeField]
    private GameObject CheckOutUI;

    [SerializeField]
    private Button FinishButton;
    public void OnFinishButtonClicked()
    {
        CheckOutUI.SetActive(true);
        FinishButton.enabled = false;
        inputActions.FindActionMap("Player").Disable();
    }



    [SerializeField]
    private Checkout checkout;

    public void OnSubmitButtonClicked()
    {
        checkout.CheckAnsers();
    }

}
