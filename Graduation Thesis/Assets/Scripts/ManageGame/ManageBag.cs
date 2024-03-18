using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

public class ManageBag : MonoBehaviour
{
    // Ve mat y tuong, ham nay quan ly cac vat pham nam ben trong tui
    // Neu la skin, khi nguoi dung bam vao se them o su dung skin dong thoi cac thong so lien quan
    // Neu la vat pham, tuong tu nhu tren nhung co them so luong hien co
    // Khi nguoi choi mua bat cu vat pham gi se tu dong vao tui do

    [Header("Item (Prefabs)")]
    [SerializeField] GameObject item;

    [Header("List Slot")]
    [SerializeField] GameObject listSlot;

    [Header("Info Slot")]
    [SerializeField] GameObject infoSlot;

    [Header("text info Slot")]
    [SerializeField] LocalizeStringEvent textInfoSlot;

    List<int> posSlots = new List<int>(){0};    // vi tri cua hinh anh trong skin hoac item
    List<int> quantitySlots = new List<int>(){0};  // so luong cua moi vat pham trong slot
    List<int> markSlots = new List<int>(){0};  // danh dau 0 la skin, 1 la item

    List<GameObject> listSlots = new List<GameObject>();
    List<Sprite> spriteSlots = new List<Sprite>();

    LocalizedString textSlot;

    ManageSkin manageSkin;
    ManageItem manageItem;

    int rowSlot = 3, columnSlot = 4;
    int posExisted; // tra ve -1 neu ko ton tai, nguoc lai tra ve vi tri ma no ton tai
    int previdSlot = -1, idSlot;

    bool isShowInfo = false;

    void Start(){
        SaveManage.Instance.LoadGame();

        posSlots = SaveManage.Instance.GetPosSlots();
        quantitySlots = SaveManage.Instance.GetQuantitySlots();
        markSlots = SaveManage.Instance.GetMarkSlots();

        manageSkin = GameObject.Find("ManageSkin").GetComponent<ManageSkin>();
        manageItem = GameObject.Find("ManageItem").GetComponent<ManageItem>();

        SetUpBag();
    }
    
    // 
    public void SetUpBag(){
        int count = posSlots.Count;
        if(count == 0){
            count = 1;
            posSlots.Add(0);
            quantitySlots.Add(0);
            markSlots.Add(0);
        }

        for(int i = 0; i < count; i++){
            if(markSlots[i] == 0){
                CreateNewSlot(quantitySlots[i], manageSkin.GetSpriteSkin(posSlots[i]));
            } else {
                CreateNewSlot(quantitySlots[i], manageItem.GetSpriteItem(posSlots[i]));
            }
        }
    }


    // Cap nhat gia tri trong slot
    public void AddSlot(int posSlot, int quantitySlot, int markSlot, Sprite spriteSlot){
        posExisted = CheckSlot(spriteSlot);

        if(posExisted == -1){
            this.posSlots.Add(posSlot);
            this.quantitySlots.Add(quantitySlot);
            this.markSlots.Add(markSlot);

            CreateNewSlot(quantitySlot, spriteSlot);
        } else{
            UpdateSlot(this.posExisted, quantitySlot);
        }

        SaveValue();

    }

    public void UpdateSlot(int pos, int quantitySlot){
        listSlots[pos].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = quantitySlot.ToString();
        this.quantitySlots[pos] = quantitySlot;
    }

    // Them mot slot moi hoan toan vao tui do
    public void CreateNewSlot(int quantitySlot, Sprite spriteSlot){
        GameObject obj = Instantiate(item);
        obj.transform.SetParent(listSlot.transform.GetChild(listSlots.Count));
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localScale = Vector3.one;
        obj.transform.GetChild(0).GetComponent<Image>().sprite = spriteSlot;
        if(quantitySlot != 0){
            obj.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = quantitySlot.ToString();
        }
        listSlots.Add(obj);
        spriteSlots.Add(spriteSlot);
    }

    // Kiem tra xem item da co trong slot chua, neu co thi ghi de, neu chua thi tao slot moi
    public int CheckSlot(Sprite spriteSlot){
        return spriteSlots.IndexOf(spriteSlot);
    }

    //
    public void ShowInfo(){
        // kiem tra nguoi dung click vao slot cu hay moi
        // neu slot cu thi tat di, neu slot moi thi thay bang moi
        if(isShowInfo){
            if(idSlot == previdSlot){
                infoSlot.SetActive(false);
                isShowInfo = false;
                return;
            }
        } else {
            infoSlot.SetActive(true);
            isShowInfo = true;
        }

        // Check vi tro slot de hien bang in
        // Neu o mep trai hoac mep phai thi bang info gan hon cho de tiep can
        // Nua trai bang hien ben trai, nua phai bang hien ben phai
        // Tuong tu voi nua tren va nua duoi
        int posRow = this.idSlot / columnSlot;
        int posColumn = this.idSlot % columnSlot;
        if(posColumn < columnSlot / 2){
            if(posColumn == 0){
                SetPosInfoSlot(-180, 0);
            } else {
                SetPosInfoSlot(-230, 0);
            }
            CheckPosYSlot(posRow, 30);
        } else {
            if(posColumn == columnSlot - 1){
                SetPosInfoSlot(180, 0);
            } else {
                SetPosInfoSlot(230, 0);
            }
            CheckPosYSlot(posRow, 30);
        }

        SetInfoSlot();

        previdSlot = idSlot;
    }

    // Lay thong tin ve nut vua bam
    public void GetIdSlot(int idSlot){
        this.idSlot = idSlot; 
        if(this.idSlot < spriteSlots.Count){
            ShowInfo();
        } else {
            ResetInfoBag();
        }
    }

    // Set cac thuoc tinh cua bang info hien len
    public void SetPosInfoSlot(int posX, int posY){
        infoSlot.transform.localPosition = EventSystem.current.currentSelectedGameObject.transform.localPosition + new Vector3(posX, posY, -1);
    }

    public void CheckPosYSlot(int posRow, int posY){
        if(posRow < (rowSlot / 2)){
            infoSlot.transform.localPosition += new Vector3(0, -posY, 0);
        } else {
            infoSlot.transform.localPosition += new Vector3(0, posY, 0);
        }
    }

    public void SetInfoSlot(){
        infoSlot.transform.GetChild(0).GetComponent<Image>().sprite = spriteSlots[idSlot];
        if(markSlots[this.idSlot] == 0){
            textSlot = manageSkin.GetInfoSkin(posSlots[idSlot]);
        } else {
            textSlot = manageItem.GetInfoItem(posSlots[idSlot]);
        }
        textInfoSlot.StringReference = textSlot;
    }

    // Su dung khi an nut close se set cac gia tri ve mac dinh
    public void ResetInfoBag(){
        isShowInfo = false;
        infoSlot.SetActive(false);
        previdSlot = -1;
        textInfoSlot.StringReference = null;
    }

    // bam nut su dung trong tui do
    public void UseSlot(){
        if(markSlots[this.idSlot] == 0){
            manageSkin.SetPointerSkin(posSlots[this.idSlot]);
        } else {
            manageItem.UseItem(posSlots[this.idSlot]);
            UpdateSlot(this.idSlot, --quantitySlots[this.idSlot]);
            if(quantitySlots[this.idSlot] == 0){
                RemoveSlot(this.idSlot);
            }
        }
    }

    public void RemoveSlot(int id){
        posSlots.RemoveAt(id);
        quantitySlots.RemoveAt(id);
        markSlots.RemoveAt(id);
        SaveValue();
        DestroySlot();
    }

    public void DestroySlot(){
        int count = posSlots.Count;
        for(int i = 0; i <= count; i++){
            Destroy(listSlots[i].gameObject);
        }
        listSlots = new List<GameObject>();
        spriteSlots = new List<Sprite>();

        SetUpBag();
    }

    public void SaveValue(){
        SaveManage.Instance.SetPosSlots(this.posSlots);
        SaveManage.Instance.SetQuantitySlots(this.quantitySlots);
        SaveManage.Instance.SetMarkSlots(this.markSlots);
    }
}
