using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ManageSkin : MonoBehaviour
{
    Skin[] skins;
    ManageCoin manageCoin;
    Button buttonCurrent;

    string[] nameSkins = new string[]{"Virtual_Guy", "Ninja_Frog", "Mask_Dude", "Pink_Man"};
    int[] costSkins = new int[]{0, 0, 0, 1000};
    int[] stateSkins = new int[]{1, 0, 0, 0};

    int count;
    int idSkin = 0;
    bool isBeBought = true;

    void Start(){
        count = nameSkins.Count();

        skins = new Skin[4];
        manageCoin = GameObject.Find("ManageCoin").GetComponent<ManageCoin>();

        SetUpSkin();
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.A)){
            manageCoin.AddCoin(1000);
        }
    }

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

    // Manage buy skins in shop
    public void CheckCoin(){
        isBeBought = manageCoin.CheckCoin(costSkins[this.idSkin]);
        if(isBeBought){
            manageCoin.SubCoin(costSkins[this.idSkin]);
            skins[this.idSkin].ChangeState(1, ref stateSkins[this.idSkin]);
            buttonCurrent.interactable = false;
        }
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
