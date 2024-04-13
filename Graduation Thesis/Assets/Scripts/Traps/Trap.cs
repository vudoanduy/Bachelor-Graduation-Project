using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private int damageTrap;

    PlayerInfo playerInfo;

    private void Start(){
        playerInfo = FindObjectOfType<PlayerInfo>();
    }

    public void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Player")){
            playerInfo.GetDame(damageTrap);
        }
    }
}
