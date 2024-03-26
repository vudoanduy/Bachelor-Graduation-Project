using UnityEngine;


// Hien tai moi chi co 2 item la hoi mau va khien bat tu
// Voi phien ban hien tai nay thi hoi mau se mac dinh la 50% luong hp player
// Khien bat tu se tuy theo skin ma nguoi choi su dung

public class PlayerUseItem : MonoBehaviour
{   
    // List item su dung phai theo thu tu nhu trong ManageItem
    // 0 la heart
    // 1 la immortal

    public void UseItem(int idItem){
        switch(idItem){
            case 0:
                FindFirstObjectByType<PlayerInfo>().HealHP(0.5f);
                break;
            default:
                FindFirstObjectByType<PlayerInfo>().UseImmortalItem();
                break;
        }
    }
}
