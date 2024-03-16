using System.Linq;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class BtnChangeText : MonoBehaviour
{
    [Header("List localizedString")]
    [SerializeField] LocalizedString[] textChange;
    [Header("List button impact on text (use if have 2 btn, otherwise can ignore it)")]
    [SerializeField] Button[] button;

    LocalizeStringEvent localizeStringEvent;

    int idText;
    int countText;

    [Header("Check if u don't want button to loop endlessly")]
    public bool isSetOffButton;

    void Start(){
        idText = 0;
        countText = textChange.Count();

        localizeStringEvent = this.GetComponent<LocalizeStringEvent>();

        CheckText();
    } 

    public void ChangeText(int num){
        idText += num;
        CheckText();
    }

    public void CheckText(){
        if(isSetOffButton){
            TurnOffButton();
        } else {
            if(idText < 0){
                idText = countText - 1;
            } else if(idText == countText){
                idText = 0;
            }
        }
        localizeStringEvent.StringReference = textChange[idText];
    }

    // only use when have 2 button impact on
    public void TurnOffButton(){
        if(idText == 0){
            button[0].interactable = false;
            button[1].interactable = true;
        } else if(idText == countText - 1){
            button[0].interactable = true;
            button[1].interactable = false;
        } else {
            button[0].interactable = true;
            button[1].interactable = true;
        }
    }

    public void DefautText(){
        idText = 0;
        CheckText();
    }

    //
}
