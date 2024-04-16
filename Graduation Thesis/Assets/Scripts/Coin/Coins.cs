using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField] private int coin = 3;

    ManageCoin manageCoin;
    AppearCoins appearCoins;

    public void AddCoin(){
        if(manageCoin == null){
            manageCoin = FindObjectOfType<ManageCoin>();
            appearCoins = FindObjectOfType<AppearCoins>();
        }
        manageCoin.AddCoin(coin);
        appearCoins.AppearNotifi(coin, this.transform);
    }
}
