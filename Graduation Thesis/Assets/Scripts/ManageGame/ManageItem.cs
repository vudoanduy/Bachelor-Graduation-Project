using System.Linq;
using UnityEngine;
using UnityEngine.Localization;
using TMPro;
using System;
using UnityEngine.Localization.Components;
using System.Collections.Generic;

public class ManageItem : MonoBehaviour
{
    // Truong nhap thong tin ve so luong item can mua
    [Header("Set Text Input Field")]
    [SerializeField] LocalizedString quantity;
    [SerializeField] TMP_InputField textField;
    [SerializeField] TextMeshProUGUI textNotifi;


    // Bang trang thai mua item
    [Header("Notifi state buy")]
    [SerializeField] GameObject notifiBuy;
    [SerializeField] LocalizedString[] stateBuy;
    [SerializeField] LocalizeStringEvent localizeStringEvent;
    [SerializeField] TextMeshProUGUI textNotifiBuy;

    [Header("Sprite Item in Shop")]
    [SerializeField] Sprite[] spriteItems;

    [Header("Info item in slot")]
    [SerializeField] LocalizedString[] infoItems;

    ManageCoin manageCoin;
    ManageBag manageBag;

    Item[] items;

    readonly string[] nameItems = new string[]{"Heart", "Immortal"};
    readonly int[] costItems = new int[]{1000, 3000};
    List<int> quantityItems = new() { 0,0}; // so luong cua tung item trong tui hien co
    readonly int[] maxQuantityItems = new int[]{99,10}; // so luong toi da cua cac phan tu tuong ung

    int count, idItem, numItem, coinTotal;

    bool isNumber;

    string costTotal;
    string _textNotifiBuy;

    //
    private void OnEnable(){
        quantity.Arguments = new object[]{costTotal};
        quantity.StringChanged += UpdateText;

        stateBuy[2].Arguments = new object[]{_textNotifiBuy};
        stateBuy[2].StringChanged += UpdateTextBuy;
    }

    private void OnDisable(){
        quantity.StringChanged -= UpdateText;
    }

    private void Start(){
        SaveManage.Instance.LoadGame();
        count = nameItems.Count();
        quantityItems = SaveManage.Instance.GetQuantityItems();

        items = new Item[count];
        manageCoin = GameObject.Find("ManageCoin").GetComponent<ManageCoin>();
        manageBag = GameObject.Find("ManageBag").GetComponent<ManageBag>();

        SetUpItem();

        if(GameObject.FindFirstObjectByType<ManageItem>() != this){
            Destroy(GameObject.FindFirstObjectByType<ManageItem>().gameObject);
            return;
        }
    }

    // Khoi tao cac items
    public void SetUpItem(){
        for(int i = 0; i < count; i++){
            items[i] = new Item(nameItems[i], costItems[i], quantityItems[i], maxQuantityItems[i]);
        }
    }

    // Cap nhat gia tri ve tong gia nguoi choi can phai mua
    // Cong thuc: so luong item nhap vao truong nhap du lieu * gia tuong ung
    private void UpdateText(string value){
        textNotifi.text = value;
    }

    public void SetTextNotifi(){
        coinTotal = Multiple(costItems[idItem], numItem);
        costTotal = coinTotal.ToString();
        quantity.Arguments[0] = costTotal;
        quantity.RefreshString();
    }

    #region Items Handle
    // Khi nguoi choi an yes tai bang nhap so luong se goi toi ham nay
    // Neu nguoi choi nhap gia tri khong phai la so nguyen se tu dong thoat tro choi
    public void GetNumItem(){
        isNumber = Int32.TryParse(textField.text, out numItem);
        if(isNumber){
            SetTextNotifi();
        } else {
            Application.Quit();
        }
    }

    // Sau khi hien gia can phai tra, nguoi choi an yes se tien hanh xem xet
    // Co 3 truong hop xay ra: 
    // 1. nguoi choi mua thanh cong => state thanh cong
    // 2. nguoi choi khong du xu => state that bai
    // 3. nguoi choi nhap mua qua so luong quy dinh. 
    //      VD: current 50, max 99, Input: 50 => thong bao nguoi choi chi duoc mua toi da 99
    public void BuyItems(){
        if(numItem + quantityItems[idItem] <= maxQuantityItems[idItem]){
            if(coinTotal <= manageCoin.GetCoin() && numItem > 0){
                manageCoin.SubCoin(coinTotal);
                localizeStringEvent.StringReference = stateBuy[0];

                items[idItem].ChangeQuantity(quantityItems[idItem] + numItem);
                quantityItems[idItem] += numItem;

                manageBag.AddSlot(idItem, (int)quantityItems[idItem], 1, spriteItems[idItem]);
                SaveManage.Instance.SetQuantityItems(quantityItems);
            } else {
                localizeStringEvent.StringReference = stateBuy[1];
            }
        } else {
            localizeStringEvent.StringReference = stateBuy[2];
            SetTextNotifiBuy();
        }
        AppearNotifiBuy();
        Invoke(nameof(DisappearNotifiBuy), 1);
    }

    // nguoi dung su dung item se giam so luong di 1
    public void UseItem(int idItem){
        quantityItems[idItem]--;
        FindFirstObjectByType<PlayerUseItem>().UseItem(idItem);
        SaveManage.Instance.SetQuantityItems(quantityItems);
    }

    // Tinh toan tong gia xu phai tra
    public int Multiple(int a, int b){
        return Math.Abs(a * b);
    }
    #endregion
    
    
    // Bang thong bao trang thai mua
    #region State of NotifiBuy

    // Cai dat bang thong bao khi nguoi choi mua qua so luong toi da
    private void UpdateTextBuy(string value){
        if(textNotifiBuy != null){
            textNotifiBuy.text = value;
        }
    }

    public void SetTextNotifiBuy(){
        _textNotifiBuy = maxQuantityItems[idItem].ToString();
        stateBuy[2].Arguments[0] = _textNotifiBuy;
        stateBuy[2].RefreshString();
    }

    private void AppearNotifiBuy(){
        notifiBuy.SetActive(true);
    }

    private void DisappearNotifiBuy(){
        notifiBuy.SetActive(false);
    }
    #endregion

    #region SetInfo
    public void SetIdItem(int idItem){
        this.idItem = idItem;
    }
    #endregion

    // Lay cac thong so can thiet
    #region Get Info
    public int GetIdItem(){
        return this.idItem;
    }

    public Sprite GetSpriteItem(int idItem){
        return this.spriteItems[idItem];
    }

    // Set cac info Item khi nguoi dung click vao item nao do trong tui do
    public LocalizedString GetInfoItem(int idItem){
        return infoItems[idItem];
    }
    #endregion


}
