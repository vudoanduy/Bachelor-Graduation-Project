using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization;
using UnityEngine.UI;
using System.Linq;

public class SetTextBuy : MonoBehaviour
{
    [Header("Button Buy in shop")]
    [SerializeField] Button[] buttons;

    [Header("Localized string in btn_buy")]
    [SerializeField] LocalizedString[] localizedString;

    [Header("Localize string event in btn_buy")]
    [SerializeField] LocalizeStringEvent[] localizeStringEvent;

    ManageSkin manageSkin;

    int[] stateSkins;

    void Start(){
        manageSkin = GameObject.Find("ManageSkin").GetComponent<ManageSkin>();

        stateSkins = manageSkin.GetListStateSkin();

        SetUpTextBuy();
        SetText();
    }

    void SetUpTextBuy(){
        for(int i = 0; i < stateSkins.Count(); i++){
            if(stateSkins[i] == 1){
                buttons[i].interactable = false;
                SetText();
            }
        }
    }

    public void SetText(){
        bool isBeBought = manageSkin.GetIsBeBought();
        if(isBeBought){
            int idSkin = manageSkin.GetIdSkin();
            localizeStringEvent[idSkin].StringReference = localizedString[1];
        }
    }
}
