using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class ManageSkin : MonoBehaviour
{
    // Bang thong bao gia skin can mua
    [Header("Notifi Cost Skin")]
    [SerializeField] LocalizedString localizeStringCost;
    [SerializeField] TextMeshProUGUI textCostSkin;

    // Thong bao trang thai mua thanh cong hay that bai
    [Header("Notifi state buy")]
    [SerializeField] GameObject notifiBuy;
    [SerializeField] LocalizedString[] stateBuy;
    [SerializeField] LocalizeStringEvent localizeStringEvent;

    [Header("Sprite Skin")]
    [SerializeField] Sprite[] spriteSkin;

    [Header("Skin Unlocked")]
    [SerializeField] GameObject[] skinUnlocked;
    [SerializeField] GameObject pointerSkin;

    [Header("Info skin")]
    [SerializeField] LocalizedString[] infoSkins;

    Skin[] skins;
    ManageCoin manageCoin;
    ManageBag manageBag;
    Button buttonCurrent;
    

    string[] nameSkins = new string[]{"Virtual_Guy", "Ninja_Frog", "Mask_Dude", "Pink_Man"};
    int[] costSkins = new int[]{0, 5000, 20000, 50000};
    List<int> stateSkins = new List<int>{1, 0, 0, 0}; // Neu nguoi choi da mua skin thi tra ve 1, nguoc lai 0
    int[] hpSkins = new []{2,3,4,5};
    int[] timeImmortalSkins = new int[]{3,5,7,9};


    int count;
    int idSkin = 0, idSkinSelected = 0;
    bool isBeBought = true;

    string costSkin;

    //
    void OnEnable(){
        localizeStringCost.Arguments = new object[]{costSkin};
        localizeStringCost.StringChanged += UpdateText;
    }

    void OnDisable(){
        localizeStringCost.StringChanged -= UpdateText;
    }

    void Start(){
        SaveManage.Instance.LoadGame();
        count = nameSkins.Count();
        stateSkins = SaveManage.Instance.GetStateSkins();
        idSkinSelected = SaveManage.Instance.GetIDSkinSelected();

        skins = new Skin[count];
        manageCoin = GameObject.Find("ManageCoin").GetComponent<ManageCoin>();
        manageBag = GameObject.Find("ManageBag").GetComponent<ManageBag>();

        SetUpSkin();
    }

    // khoi tao thong so skin 
    public void SetUpSkin(){
        
        for(int i = 0; i < count; i++){
            skins[i] = new Skin(nameSkins[i], costSkins[i], stateSkins[i], hpSkins[i], timeImmortalSkins[i], spriteSkin[i]);
        }

        for(int i = 0; i < count; i++){
            if(stateSkins[i] == 1){
                UnlockSkin(idSkin);
            } else {
                skinUnlocked[i].SetActive(false);
            }
        }

        SetPointerSkin(idSkinSelected);
    }

    // Lay thong tin ve vi tri skin vua bam nut Mua trong cua hang
    public void GetIdSkin(int idSkin){
        this.idSkin = idSkin;
        buttonCurrent = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
    }

    // Theo doi gia tri cua skin luc vua bam vao va hien thi len bang thong bao gia
    public void UpdateText(string value){
        textCostSkin.text = value;
    }

    public void UpdateCost(){
        costSkin = costSkins[this.idSkin].ToString();
        localizeStringCost.Arguments[0] = costSkin;
        localizeStringCost.RefreshString();
    }

    // Khi bam nut mua se goi toi ham kiem tra xem so xu hien co co du mua hay khong 
    // Neu nguoi choi bam yes, se goi toi ham BuySkin va kiem tra tung dieu kien
    // Khi mua thanh cong se hien bang thong bao mua thanh cong, nguoc lai se thong bao loi
    public void CheckCoin(){
        isBeBought = manageCoin.CheckCoin(costSkins[this.idSkin]);
        UpdateCost();
    }

    public void BuySkin(){
        if(isBeBought){
            // tru tien xu
            manageCoin.SubCoin(costSkins[this.idSkin]); 

            // Set thong so
            skins[this.idSkin].ChangeState(1);
            stateSkins[this.idSkin] = 1;

            //
            buttonCurrent.interactable = false;
            localizeStringEvent.StringReference = stateBuy[0];
            UnlockSkin(this.idSkin);
            //
            SaveManage.Instance.SetStateSkins(stateSkins);
            manageBag.AddSlot(this.idSkin, 0, 0, skins[idSkin].GetSpriteSkin());

            AppearNotifiBuy();
            Invoke("DisappearNotifiBuy", 1);
        } else{
            localizeStringEvent.StringReference = stateBuy[1];

            AppearNotifiBuy();
            Invoke("DisappearNotifiBuy", 1);
        }
    }

    // Goi ham show trang thai mua 
    private void AppearNotifiBuy(){
        notifiBuy.gameObject.SetActive(true);
    }

    private void DisappearNotifiBuy(){
        notifiBuy.gameObject.SetActive(false);
    }

    // Lay cac thong so can thiet
    public int GetIdSkin(){
        return this.idSkin;
    }
    public List<int> GetListStateSkin(){
        return this.stateSkins;
    }
    public bool GetIsBeBought(){
        return this.isBeBought;
    }

    public Sprite GetSpriteSkin(int id){
        return this.spriteSkin[id];
    }

    // Can mieu ta info skin nao thi ghi vao phan ben duoi
    private void SetInfoSkin(){
        // ghi cac thong tin muon ghi vao day
    }

    // Mo khoa skin va tro con tro vao skin dang su dung
    public void UnlockSkin(int id){
        skinUnlocked[id].SetActive(true);
    }

    public void SetPointerSkin(int idSkin){
        idSkinSelected = idSkin;
        SaveManage.Instance.SetIDSkinSelected(idSkinSelected);
        pointerSkin.transform.localPosition = skinUnlocked[idSkin].transform.localPosition + new Vector3(0, 1.3f, 0);
    }

    // Set cac info Skin khi nguoi dung click vao skin nao do trong tui do
    public LocalizedString GetInfoSkin(int idSkin){
        return infoSkins[idSkin];
    }
}
