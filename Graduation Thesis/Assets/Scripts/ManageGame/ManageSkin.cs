using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.SceneManagement;
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
    [SerializeField] GameObject[] skinUnlockedPrefabs;
    [SerializeField] GameObject pointerSkin;

    [Header("Info skin")]
    [SerializeField] LocalizedString[] infoSkins;

    Skin[] skins;
    ManageCoin manageCoin;
    ManageBag manageBag;
    Button buttonCurrent;

    readonly string[] nameSkins = new string[]{"Virtual_Guy", "Ninja_Frog", "Mask_Dude", "Pink_Man"};
    readonly int[] costSkins = new int[]{0, 5000, 20000, 50000};
    List<int> stateSkins = new() { 1, 0, 0, 0}; // Neu nguoi choi da mua skin thi tra ve 1, nguoc lai 0
    readonly int[] hpSkins = new []{2,3,4,5};
    readonly int[] timeImmortalSkins = new int[]{3,5,7,9};


    int count;
    int idSkin = 0, idSkinSelected = 0;
    bool isBeBought = true;

    string costSkin;

    //
    private void OnEnable(){
        localizeStringCost.Arguments = new object[]{costSkin};
        localizeStringCost.StringChanged += UpdateText;
    }

    private void OnDisable(){
        localizeStringCost.StringChanged -= UpdateText;
    }

    private void Start(){
        SaveManage.Instance.LoadGame();
        count = nameSkins.Count();
        stateSkins = SaveManage.Instance.GetStateSkins();
        idSkinSelected = SaveManage.Instance.GetIDSkinSelected();
        Debug.Log("ID Skin Selected loaded:" + idSkinSelected);

        skins = new Skin[count];
        manageCoin = GameObject.Find("ManageCoin").GetComponent<ManageCoin>();
        manageBag = GameObject.Find("ManageBag").GetComponent<ManageBag>();

        SetUpSkin();

        if(FindFirstObjectByType<ManageSkin>() != this){
            Destroy(FindFirstObjectByType<ManageSkin>().gameObject);
            return;
        }

        // isFirst = true;
    }

    #region Set Up
    // khoi tao thong so skin 
    public void SetUpSkin(){
        
        for(int i = 0; i < count; i++){
            skins[i] = new Skin(nameSkins[i], costSkins[i], stateSkins[i], hpSkins[i], timeImmortalSkins[i], spriteSkin[i]);
        }

        if(SceneManager.GetActiveScene().name == "Menu"){
            for(int i = 0; i < count; i++){
                if(stateSkins[i] == 1){
                    UnlockSkin(i);
                    Debug.Log(i);
                } else {
                    skinUnlocked[i].SetActive(false);
                }
            }
        }

        SetPointerSkin(idSkinSelected);
    }
    #endregion

    #region Change text when buy skin
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
    #endregion

    #region Skin Handle

    public void BuySkin(){
        if(isBeBought){
            // tru tien xu
            manageCoin.SubCoin(costSkins[this.idSkin]); 

            // Set thong so
            skins[this.idSkin].ChangeState(1);
            stateSkins[this.idSkin] = 1;

            // cap nhat nut mua trong cua hang va mo khoa skin trong Menu
            buttonCurrent.interactable = false;
            localizeStringEvent.StringReference = stateBuy[0];
            UnlockSkin(this.idSkin);

            //
            SaveManage.Instance.SetStateSkins(stateSkins);
            manageBag.AddSlot(this.idSkin, 0, 0, skins[idSkin].ImageSkin);

            AppearNotifiBuy();
            Invoke(nameof(DisappearNotifiBuy), 1);
        } else{
            localizeStringEvent.StringReference = stateBuy[1];

            AppearNotifiBuy();
            Invoke(nameof(DisappearNotifiBuy), 1);
        }
    }

    // Mo khoa skin va tro con tro vao skin dang su dung
    // Con tro chi xuat hien tai man hinh menu ben ngoai game
    public void UnlockSkin(int id){
        skinUnlocked[id].SetActive(true);
    }

    public void SetPointerSkin(int idSkin){
        if(pointerSkin == null){
            pointerSkin = GameObject.Find("PointerSkin");
        }

        idSkinSelected = idSkin;
        SaveManage.Instance.SetIDSkinSelected(idSkinSelected);

        if(SceneManager.GetActiveScene().name == "Menu"){
            pointerSkin.transform.localPosition = skinUnlockedPrefabs[idSkin].transform.localPosition + Vector3.up * 1.3f ;
        }

        if(FindFirstObjectByType<PlayerInfo>() != null){
            FindFirstObjectByType<PlayerInfo>().SetHPSkin(skins[idSkinSelected].HPSkin);
        }

        if(SceneManager.GetActiveScene().name != "Menu"){
            FindFirstObjectByType<PlayerMove>().SetPlayerController(idSkinSelected);
        }
    }
    #endregion

    #region State Notifi Buy
    // Goi ham show trang thai mua 
    private void AppearNotifiBuy(){
        notifiBuy.SetActive(true);
    }

    private void DisappearNotifiBuy(){
        notifiBuy.SetActive(false);
    }
    #endregion

    // Lay cac thong so can thiet
    #region Get Info
    public int GetIdSkin(){
        return this.idSkin;
    }

    // Lay thong tin ve vi tri skin vua bam nut Mua trong cua hang
    public void GetIdSkin(int idSkin){
        this.idSkin = idSkin;
        buttonCurrent = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
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

    public int ReadHpSkinCurrent(){
        Debug.Log(skins[idSkinSelected].HPSkin);
        Debug.Log(idSkinSelected);
        return skins[idSkinSelected].HPSkin;
    }

    public int ReadTimeImmortalSkin(){
        return skins[idSkinSelected].TimeImmortalSkin;
    }

    // Set cac info Skin khi nguoi dung click vao skin nao do trong tui do
    public LocalizedString GetInfoSkin(int idSkin){
        return infoSkins[idSkin];
    }

    #endregion

}
