using UnityEngine;

// Khi nguoi choi nhat dc qua tao nay thi duoc hoi HP theo so co dinh
public class Apple : MonoBehaviour, IFruit
{
    [Tooltip("Restores health in a fixed amount")]
    [SerializeField] private int hpHeal = 1;

    Animator anim;
    PlayerInfo playerInfo;
    CircleCollider2D circleCollider2D;

    private bool isCollected;

    private void Start(){
        anim = GetComponent<Animator>();
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    public void Effect(){
        playerInfo.HealHP(hpHeal);
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
