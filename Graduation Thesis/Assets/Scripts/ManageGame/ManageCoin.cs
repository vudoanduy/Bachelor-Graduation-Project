using UnityEngine;

public class ManageCoin : MonoBehaviour
{
    private int coinCurrent; // xu hien tai cua nguoi choi, duoc su dung trong toan tro choi

    void Start(){
        SaveManage.Instance.LoadGame();
        this.coinCurrent = SaveManage.Instance.GetCoinCurrent();

        FindFirstObjectByType<ChangeTextCoin>().UpdateText();

        if(GameObject.FindFirstObjectByType<ManageCoin>() != this){
            Destroy(GameObject.FindFirstObjectByType<ManageCoin>().gameObject);
            return;
        }   
    }

    #region TestXu

    void Update(){
        if(Input.GetKeyDown(KeyCode.U)){
            AddCoin(10000);
        }
    }

    #endregion


    #region Interact with Coin
    // Kiem tra xem so xu co du de mua hay khong
    // tra ve true neu du, false neu nguoc lai
    public bool CheckCoin(int cost){
        if(this.coinCurrent >= cost){
            return true;
        }
        return false;
    }

    // 2 ham tuong tac voi xu
    public void SubCoin(int cost){
        this.coinCurrent -= cost;
        SaveManage.Instance.SetCoinCurrent(this.coinCurrent);
        FindFirstObjectByType<ChangeTextCoin>().UpdateText();
    }

    public void AddCoin(int coin){
        this.coinCurrent += coin;
        if(this.coinCurrent >= 9999999){
            this.coinCurrent = 9999999;
        }
        SaveManage.Instance.SetCoinCurrent(this.coinCurrent);
        FindFirstObjectByType<ChangeTextCoin>().UpdateText();
    }

    #endregion

    // lay cac thong so can thiet
    #region Get Info
    
    public int GetCoin(){
        return this.coinCurrent;
    }

    #endregion
}
