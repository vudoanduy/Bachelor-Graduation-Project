using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private int damageTrap;
    [SerializeField] private float force = 30;

    PlayerInfo playerInfo;

    public void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Player")){
            if(playerInfo == null){
                playerInfo = FindObjectOfType<PlayerInfo>();
            }
            playerInfo.GetDame(damageTrap);

            Rigidbody2D rbPlayer = other.gameObject.GetComponent<Rigidbody2D>();
            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, force);
        }
    }


}
