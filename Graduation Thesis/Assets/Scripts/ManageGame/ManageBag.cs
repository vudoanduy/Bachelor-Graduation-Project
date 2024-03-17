using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    List<int> posSlots = new List<int>();    // vi tri cua hinh anh trong skin hoac item
    List<int> quantitySlots = new List<int>();  // so luong cua moi vat pham trong slot
    List<int> markSlots = new List<int>();  // danh dau 0 la skin, 1 la item

    List<GameObject> listSlots = new List<GameObject>();
    List<Sprite> spriteSlots = new List<Sprite>();

    ManageSkin manageSkin;
    ManageItem manageItem;

    int count;
    int posExisted; // tra ve -1 neu ko ton tai, nguoc lai tra ve vi tri ma no ton tai

    void Start(){
        SaveManage.Instance.LoadGame();

        posSlots = SaveManage.Instance.GetPosSlots();
        quantitySlots = SaveManage.Instance.GetQuantitySlots();
        markSlots = SaveManage.Instance.GetMarkSlots();

        manageSkin = GameObject.Find("ManageSkin").GetComponent<ManageSkin>();
        manageItem = GameObject.Find("ManageItem").GetComponent<ManageItem>();

        count = posSlots.Count;

        SetUpBag();
    }
    
    // 
    public void SetUpBag(){
        for(int i = 0; i < count; i++){
            if(markSlots[i] == 0){
                CreateNewSlot(quantitySlots[i], manageSkin.GetSpriteSkin(posSlots[i]));
            } else {
                CreateNewSlot(quantitySlots[i], manageItem.GetSpriteItem(posSlots[i]));
            }
        }
    }


    // Them slot moi vao trong inventory
    public void AddSlot(int posSlot, int quantitySlot, int markSlot, Sprite spriteSlot){
        posExisted = CheckSlot(spriteSlot);

        if(posExisted == -1){
            this.posSlots.Add(posSlot);
            this.quantitySlots.Add(quantitySlot);
            this.markSlots.Add(markSlot);

            CreateNewSlot(quantitySlot, spriteSlot);
        } else{
            UpdateSlot(quantitySlot);
        }

        SaveManage.Instance.SetPosSlots(this.posSlots);
        SaveManage.Instance.SetQuantitySlots(this.quantitySlots);
        SaveManage.Instance.SetMarkSlots(this.markSlots);

    }

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

    public void UpdateSlot(int quantitySlot){
        listSlots[this.posExisted].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = quantitySlot.ToString();
        this.quantitySlots[this.posExisted] = quantitySlot;
    }


    // Kiem tra xem item da co trong slot chua, neu co thi ghi de, neu chua thi tao slot moi
    public int CheckSlot(Sprite spriteSlot){
        return spriteSlots.IndexOf(spriteSlot);
    }
}
