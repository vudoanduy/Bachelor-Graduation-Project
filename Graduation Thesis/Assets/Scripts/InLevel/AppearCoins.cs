using System.Collections;
using TMPro;
using UnityEngine;

public class AppearCoins : MonoBehaviour
{
    [SerializeField] GameObject notifiAddCoin;
    [SerializeField] GameObject canvasWorldGame;

    public void AppearNotifi(int coin, Transform trans){
        StartCoroutine(SetNotifi(coin, trans));
    }

    IEnumerator SetNotifi(int coin, Transform trans){
        GameObject newNotifi = Instantiate(notifiAddCoin, canvasWorldGame.transform);
        newNotifi.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "+ " + coin;
        newNotifi.transform.localPosition = new Vector3(trans.localPosition.x, trans.localPosition.y, notifiAddCoin.transform.localPosition.z);

        LeanTween.moveLocalY(newNotifi, trans.localPosition.y + 3f, 2f).setEase(LeanTweenType.linear);
        yield return new WaitForSeconds(2f);

        Destroy(newNotifi);
        yield break;
    }
}
