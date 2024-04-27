using UnityEngine;

// Khi nguoi choi nhat duoc qua strawberry thi se duoc tang sat thuong gay ra len quai vat
public class Strawberry : MonoBehaviour
{
    [Tooltip("The amount of damage the player deals is increased")]
    [SerializeField] private int damageInc = 1;
    [SerializeField] private float timeEffect = 5;

    Animator anim;
    PlayerInfo playerInfo;
    CircleCollider2D circleCollider2D;

    private bool isCollected;

    private void Start(){
        anim = GetComponent<Animator>();
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    public void Effect(){
        playerInfo.IncreaseDamageBase(damageInc, timeEffect);
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Player")){
            if(isCollected) return;
            isCollected = true;
            if(playerInfo == null){
                playerInfo = FindObjectOfType<PlayerInfo>();
            }
            Effect();
            anim.SetTrigger("isCollected");
            circleCollider2D.isTrigger = true;
            Invoke(nameof(Destroy), 0.5f);
        }
    }
    
    private void Destroy(){
        Destroy(this.gameObject);
    }
}
