using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class LoginLink : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{    
    [SerializeField] TMP_Text Login_link;
    public void OnPointerClick(PointerEventData eventData) => Application.OpenURL(Login_link.text);
    public void OnPointerEnter(PointerEventData eventData) => Login_link.color = Color.green;    
    public void OnPointerExit(PointerEventData eventData) => Login_link.color = Color.white;
}
