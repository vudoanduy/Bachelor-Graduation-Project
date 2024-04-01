using UnityEngine;

// Thay doi icon(anh dai dien) cua nhan vat dang chon cho phu hop
public class ChangeIcon : MonoBehaviour
{
    [Header("Set icon")]
    [SerializeField] SpriteRenderer iconPlayer;
    [SerializeField] Sprite[] iconPlayerPrefabs;

    public void Start(){
        ChangeIconPlayer(SaveManage.Instance.GetIDSkinSelected());
    }

    public void ChangeIconPlayer(int idIcon){
        iconPlayer.sprite = iconPlayerPrefabs[idIcon];
    }
}
