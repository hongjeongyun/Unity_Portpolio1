using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Main : MonoBehaviour
{    
    [SerializeField] Button Button_Get;
    [SerializeField] TMP_InputField UriInputField;
    [SerializeField] TMP_Text Login_link;
    [SerializeField] TMP_Text Result;
 
    private void Awake()
    {
#if UNITY_STANDALONE       
        var w = PlayerPrefs.GetInt("Screenmanager Resolution Width", 480);
        var h = PlayerPrefs.GetInt("Screenmanager Resolution Height", 540);
        Screen.SetResolution(w, h, false);
#endif
    }

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