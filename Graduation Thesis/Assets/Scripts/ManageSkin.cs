using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class ManageSkin : MonoBehaviour
{
    [Header("Notifi Cost Skin")]
    [SerializeField] LocalizedString localizeStringCost;
    [SerializeField] TextMeshProUGUI textCostSkin;

    [Header("Notifi state buy")]
    [SerializeField] GameObject notifiBuy;
    [SerializeField] LocalizedString[] stateBuy;
    [SerializeField] LocalizeStringEvent localizeStringEvent;

    Skin[] skins;
    ManageCoin manageCoin;
    Button buttonCurrent;
    

    string[] nameSkins = new string[]{"Virtual_Guy", "Ninja_Frog", "Mask_Dude", "Pink_Man"};
    int[] costSkins = new int[]{0, 5000, 20000, 50000};
    int[] stateSkins = new int[]{1, 0, 0, 0};

    int count;
    int idSkin = 0;
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
        count = nameSkins.Count();

        skins = new Skin[count];
        manageCoin = GameObject.Find("ManageCoin").GetComponent<ManageCoin>();

        SetUpSkin();
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.A)){
            manageCoin.AddCoin(10000);
        }
    }

    //
    public void SetUpSkin(){
        for(int i = 0; i < count; i++){
            skins[i] = new Skin(nameSkins[i], costSkins[i], stateSkins[i]);
        }
    }

    //
    public void GetIdSkin(int idSkin){
        this.idSkin = idSkin;
        buttonCurrent = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
    }

    //
    public void UpdateText(string value){
        textCostSkin.text = value;
    }

    public void UpdateCost(){
        costSkin = costSkins[this.idSkin].ToString();
        localizeStringCost.Arguments[0] = costSkin;
        localizeStringCost.RefreshString();
    }

    // Manage buy skins in shop
    public void CheckCoin(){
        isBeBought = manageCoin.CheckCoin(costSkins[this.idSkin]);
        UpdateCost();
    }

    public void BuySkin(){
        if(isBeBought){
            manageCoin.SubCoin(costSkins[this.idSkin]);
            skins[this.idSkin].ChangeState(1, ref stateSkins[this.idSkin]);
            buttonCurrent.interactable = false;
            localizeStringEvent.StringReference = stateBuy[0];
            AppearNotifiBuy();
            Invoke("DisappearNotifiBuy", 1);
        } else{
            localizeStringEvent.StringReference = stateBuy[1];
            AppearNotifiBuy();
            Invoke("DisappearNotifiBuy", 1);
        }
    }

    //
    private void AppearNotifiBuy(){
        notifiBuy.gameObject.SetActive(true);
    }

    private void DisappearNotifiBuy(){
        notifiBuy.gameObject.SetActive(false);
    }

    //
    public int GetIdSkin(){
        return this.idSkin;
    }
    public int[] GetListStateSkin(){
        return this.stateSkins;
    }
    public bool GetIsBeBought(){
        return this.isBeBought;
    }
}
