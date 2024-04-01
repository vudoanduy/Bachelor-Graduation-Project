using UnityEngine;

public class SpikeTurtle : MonoBehaviour
{
    private int dameSpikeTurtle;

    PlayerInfo playerInfo;
    TurtleNormal turtleNormal; 

    void Start(){
        playerInfo = FindObjectOfType<PlayerInfo>();
        turtleNormal = FindObjectOfType<TurtleNormal>();

        dameSpikeTurtle = turtleNormal.GetDamageSpikeTurtle();
    }

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Player")){
            playerInfo.GetDame(dameSpikeTurtle);
            Destroy(this.gameObject);
        }
    }
}
