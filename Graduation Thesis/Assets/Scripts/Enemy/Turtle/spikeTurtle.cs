using UnityEngine;

public class SpikeTurtle : MonoBehaviour
{
    private int damageSpikeTurtle;
    public int DamageSpikeTurtle{get{return damageSpikeTurtle;} set{damageSpikeTurtle = value;}}

    PlayerInfo playerInfo; 

    private void Start(){
        playerInfo = FindObjectOfType<PlayerInfo>();
    }

    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Player")){
            playerInfo.GetDame(damageSpikeTurtle);
            Destroy(this.gameObject);
        }
    }
}
