using TMPro;
using UnityEngine;

public class ChangeTextCoin : MonoBehaviour
{
    TextMeshProUGUI coinInfo;

    void Start(){
        SaveManage.Instance.LoadGame();
        coinInfo = this.GetComponent<TextMeshProUGUI>();

        UpdateText();
    }

    public void UpdateText(){
        int coin = SaveManage.Instance.GetCoinCurrent();
        coinInfo.text = coin.ToString();
    }
}
