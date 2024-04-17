using UnityEngine;

public class SpikeTurtle : MonoBehaviour
{
    private int damageSpikeTurtle;
    public int DamageSpikeTurtle{get{return damageSpikeTurtle;} set{damageSpikeTurtle = value;}}

    PlayerInfo playerInfo; 

    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Player")){
            if(playerInfo == null){
                playerInfo = FindObjectOfType<PlayerInfo>();
            }
            playerInfo.GetDame(damageSpikeTurtle);
            Destroy(this.gameObject);
        }
    }
}
