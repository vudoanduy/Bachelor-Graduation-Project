using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

public class SetTextBuy : MonoBehaviour
{
    // Ham nay chi de khi nguoi choi mua thi thay doi trang thai cua nut trong cua hang Buy=>Bought
    // Dong thoi thi tat trang thai hoat dong cua cac skin da mua
    [Header("Button Buy in shop")]
    [SerializeField] Button[] buttons;

    [Header("Localized string in btn_buy")]
    [SerializeField] LocalizedString[] localizedString;

    [Header("Localize string event in btn_buy")]
    [SerializeField] LocalizeStringEvent[] localizeStringEvent;

    ManageSkin manageSkin;

    List<int> stateSkins;

    private void Start(){
        manageSkin = GameObject.Find("ManageSkin").GetComponent<ManageSkin>();

        stateSkins = SaveManage.Instance.GetStateSkins();

        SetUpTextBuy();
        SetText();
    }

    private void SetUpTextBuy(){
        for(int i = 0; i < stateSkins.Count(); i++){
            if(stateSkins[i] == 1){
                buttons[i].interactable = false;
                localizeStringEvent[i].StringReference = localizedString[1];
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
