using UnityEngine;

public class SpikeTurtle : MonoBehaviour
{
    private int damageSpikeTurtle;
    public int DamageSpikeTurtle{get{return damageSpikeTurtle;} set{damageSpikeTurtle = value;}}

    PlayerInfo playerInfo; 
    Rigidbody2D rbPlayer;

    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Player")){
            if(playerInfo == null){
                playerInfo = FindObjectOfType<PlayerInfo>();
                rbPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
            }
            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, 30);
            playerInfo.GetDame(damageSpikeTurtle);
            Destroy(this.gameObject);
        }
    }
}
