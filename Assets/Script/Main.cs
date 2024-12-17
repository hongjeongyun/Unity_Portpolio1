using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Main : MonoBehaviour
{    
    [SerializeField] Button Button_Get;
    [SerializeField] TMP_InputField UriInputField;
    [SerializeField] TMP_Text Login_link;
    [SerializeField] TMP_Text Result;
 
    void Start()
    {        
        Login_link.text = PSNIDBASE64.LOGIN_URL;
        
        Button_Get.onClick.AddListener(() => 
        {
            if (UriInputField.text == string.Empty || UriInputField.text == null)
                return;
            
            var result = PSNIDBASE64.GetBase64ID(UriInputField.text);                        
            Result.text = result;
            Result.fontSize = 50;                                                                        
        });
    }    
}