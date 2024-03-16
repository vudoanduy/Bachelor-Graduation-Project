using TMPro;
using UnityEngine;

public class ChangeTextCoin : MonoBehaviour
{
    ManageCoin manageCoin;
    TextMeshProUGUI coinInfo;

    void Start(){
        manageCoin = GameObject.Find("ManageCoin").GetComponent<ManageCoin>();
        coinInfo = this.GetComponent<TextMeshProUGUI>();

        UpdateText();
    }

    public void UpdateText(){
        int coin = SaveManage.Instance.GetCoinCurrent();
        coinInfo.text = coin.ToString();
    }
}
