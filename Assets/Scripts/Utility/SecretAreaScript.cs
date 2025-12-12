using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SecretAreaScript : MonoBehaviour
{

    public Button btnClick, btnBackToMenu, btnGoBack;
    public TMP_InputField inputCode;
    public TextMeshProUGUI errorText;
    public TextMeshProUGUI text1, text2, text3, text4;
    public Image img1, img2, img3;
    public string myText;

    public void Start()
    {
        btnClick.onClick.AddListener(RevealArtworkByCode);
    }

    public void BackToGame(){
        SceneManager.LoadScene("Main");
    }

    public void GoBack(){
            text1.GetComponent<TextMeshProUGUI>().enabled = true;
            text2.GetComponent<TextMeshProUGUI>().enabled = true;
            text3.GetComponent<TextMeshProUGUI>().enabled = true;
            text4.GetComponent<TextMeshProUGUI>().enabled = true;
            inputCode.ActivateInputField();
            btnClick.gameObject.SetActive(true);
            btnBackToMenu.gameObject.SetActive(true);
            img1.gameObject.SetActive(false);
            img2.gameObject.SetActive(false);
            img3.gameObject.SetActive(false);
            btnGoBack.gameObject.SetActive(false);
            errorText.text = "";
    }

    public void RevealArtworkByCode(){
        myText = inputCode.text;
        if(myText == "------"){
            text1.GetComponent<TextMeshProUGUI>().enabled = false;
            text2.GetComponent<TextMeshProUGUI>().enabled = false;
            text3.GetComponent<TextMeshProUGUI>().enabled = false;
            text4.GetComponent<TextMeshProUGUI>().enabled = false;
            inputCode.DeactivateInputField();
            btnClick.gameObject.SetActive(false);
            btnBackToMenu.gameObject.SetActive(false);
            img1.gameObject.SetActive(true);
            btnGoBack.gameObject.SetActive(true);
            errorText.text = "";
        }
        else if(myText == "------"){
            text1.GetComponent<TextMeshProUGUI>().enabled = false;
            text2.GetComponent<TextMeshProUGUI>().enabled = false;
            text3.GetComponent<TextMeshProUGUI>().enabled = false;
            text4.GetComponent<TextMeshProUGUI>().enabled = false;
            inputCode.DeactivateInputField();
            btnClick.gameObject.SetActive(false);
            btnBackToMenu.gameObject.SetActive(false);
            img2.gameObject.SetActive(true);
            btnGoBack.gameObject.SetActive(true);
            errorText.text = "";
        }
        else if(myText == "------"){
            text1.GetComponent<TextMeshProUGUI>().enabled = false;
            text2.GetComponent<TextMeshProUGUI>().enabled = false;
            text3.GetComponent<TextMeshProUGUI>().enabled = false;
            text4.GetComponent<TextMeshProUGUI>().enabled = false;
            inputCode.DeactivateInputField();
            btnClick.gameObject.SetActive(false);
            btnBackToMenu.gameObject.SetActive(false);
            img3.gameObject.SetActive(true);
            btnGoBack.gameObject.SetActive(true);
            errorText.text = "";
        }
        else 
        {
            errorText.text = "ERROR - Not a valid password!";
        }
    }
}
