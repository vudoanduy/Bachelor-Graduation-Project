using UnityEngine;

public class ManageCoin : MonoBehaviour
{
    private int coinCurrent = 0;

    void Start(){
        SaveManage.Instance.LoadGame();
        this.coinCurrent = SaveManage.Instance.GetCoinCurrent();
    }

    // functions that interact with coins
    public bool CheckCoin(int cost){
        if(this.coinCurrent >= cost){
            return true;
        }
        return false;
    }

    public void SubCoin(int cost){
        this.coinCurrent -= cost;
        SaveManage.Instance.SetCoinCurrent(this.coinCurrent);
    }

    public void AddCoin(int coin){
        this.coinCurrent += coin;
        if(this.coinCurrent >= 9999999){
            this.coinCurrent = 9999999;
        }
        SaveManage.Instance.SetCoinCurrent(this.coinCurrent);
    }

    public int GetCoin(){
        return this.coinCurrent;
    }
}
