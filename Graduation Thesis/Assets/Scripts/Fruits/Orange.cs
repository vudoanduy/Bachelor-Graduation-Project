using UnityEngine;

// Khi nguoi choi nhat duoc qua nay thi hoi mau theo phan tram HPtotal hien tai
public class Orange : MonoBehaviour, IFruit
{
    [Tooltip("Restores health by percentage (0->1)")]
    [SerializeField] private float hpHeal = 0.25f;

    Animator anim;
    PlayerInfo playerInfo;
    CircleCollider2D circleCollider2D;

    private void Start(){
        anim = GetComponent<Animator>();
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    public void Effect(){
        playerInfo.HealHP(hpHeal);
    }

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Player")){
            if(playerInfo == null){
                playerInfo = FindObjectOfType<PlayerInfo>();
            }
            Debug.Log(this.gameObject.name);
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
