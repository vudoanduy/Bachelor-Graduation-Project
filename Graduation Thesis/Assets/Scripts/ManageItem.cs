using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Localization;
using TMPro;
using System;
using UnityEngine.Localization.Components;
using System.Collections.Generic;

public class ManageItem : MonoBehaviour
{
    [Header("Set Text Input Field")]
    [SerializeField] LocalizedString quantity;
    [SerializeField] TMP_InputField textField;
    [SerializeField] TextMeshProUGUI textNotifi;


    [Header("Notifi state buy")]
    [SerializeField] GameObject notifiBuy;
    [SerializeField] LocalizedString[] stateBuy;
    [SerializeField] LocalizeStringEvent localizeStringEvent;
    [SerializeField] TextMeshProUGUI textNotifiBuy;


    ManageCoin manageCoin;
    Item[] items;

    string[] nameItems = new string[]{"Heart", "Immortal"};
    int[] costItems = new int[]{1000, 3000};
    List<int> quantityItems = new List<int>{0,0};
    int[] maxQuantityItems = new int[]{99,10};

    int count, idItem, numItem, coinTotal;

    bool isNumber;

    string costTotal;

    string _textNotifiBuy;
    //
    void OnEnable(){
        quantity.Arguments = new object[]{costTotal};
        quantity.StringChanged += UpdateText;

        stateBuy[2].Arguments = new object[]{_textNotifiBuy};
        stateBuy[2].StringChanged += UpdateTextBuy;
    }

    void OnDisable(){
        quantity.StringChanged -= UpdateText;
    }

    void Start(){
        SaveManage.Instance.LoadGame();
        count = nameItems.Count();
        quantityItems = SaveManage.Instance.GetQuantityItems();

        items = new Item[count];
        manageCoin = GameObject.Find("ManageCoin").GetComponent<ManageCoin>();

        SetUpItem();
    }

    //
    public void SetUpItem(){
        for(int i = 0; i < count; i++){
            items[i] = new Item(nameItems[i], costItems[i], quantityItems[i], maxQuantityItems[i]);
        }
    }

    //
    private void UpdateText(string value){
        textNotifi.text = value;
    }

    public void SetTextNotifi(){
        coinTotal = Multiple(costItems[idItem], numItem);
        costTotal = coinTotal.ToString();
        quantity.Arguments[0] = costTotal;
        quantity.RefreshString();
    }

    //
    private void UpdateTextBuy(string value){
        textNotifiBuy.text = value;
    }
    public void SetTextNotifiBuy(){
        _textNotifiBuy = maxQuantityItems[idItem].ToString();
        stateBuy[2].Arguments[0] = _textNotifiBuy;
        stateBuy[2].RefreshString();
    }

    // when click yes
    public void GetNumItem(){
        isNumber = Int32.TryParse(textField.text, out numItem);
        if(isNumber){
            SetTextNotifi();
        } else {
            Application.Quit();
        }
    }

    public void BuyItems(){
        if(numItem + quantityItems[idItem] <= maxQuantityItems[idItem]){
            if(coinTotal <= manageCoin.GetCoin()){
                manageCoin.SubCoin(coinTotal);
                localizeStringEvent.StringReference = stateBuy[0];
                items[idItem].ChangeQuantity(quantityItems[idItem] + numItem);
                quantityItems[idItem] += numItem;
                SaveManage.Instance.SetQuantityItems(quantityItems);
            } else {
                localizeStringEvent.StringReference = stateBuy[1];
            }
        } else {
            localizeStringEvent.StringReference = stateBuy[2];
            SetTextNotifiBuy();
        }
        AppearNotifiBuy();
        Invoke("DisappearNotifiBuy", 1);
    }

    // Calculate cointotal
    public int Multiple(int a, int b){
        return a * b;
    }
    
    //
    private void AppearNotifiBuy(){
        notifiBuy.gameObject.SetActive(true);
    }

    private void DisappearNotifiBuy(){
        notifiBuy.gameObject.SetActive(false);
    }

    //
    public void GetIdItem(int idItem){
        this.idItem = idItem;
    }

    //
    public int GetIdItem(){
        return this.idItem;
    }
}
