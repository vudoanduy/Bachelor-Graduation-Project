using System;
using UnityEngine;

public class ManageCoin : MonoBehaviour
{
    private int coinCurrent = 0;

    void Start(){

    }

    // functions that interact with coins
    public bool CheckCoin(int cost){
        if(coinCurrent >= cost){
            return true;
        }
        return false;
    }

    public void SubCoin(int cost){
        this.coinCurrent -= cost;
    }

    public void AddCoin(int coin){
        this.coinCurrent += coin;
        if(this.coinCurrent >= 9999999){
            this.coinCurrent = 9999999;
        }
    }

    public int GetCoin(){
        return this.coinCurrent;
    }
}
