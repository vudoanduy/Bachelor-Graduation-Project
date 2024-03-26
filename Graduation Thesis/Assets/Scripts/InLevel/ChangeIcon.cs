using UnityEngine;

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
