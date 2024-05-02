using UnityEngine;

// Khi nguoi choi an pineApple thi nguoi choi se duoc giam sat thuong nhan vao
public class PineApple : MonoBehaviour, IFruit
{
    [Tooltip("Restores health by percentage (0->1)")]
    [SerializeField] private float damageCoefficient = 0.5f;
    [Tooltip("Enter the validity period")]
    [SerializeField] private float timeEffect = 3;

    Animator anim;
    PlayerInfo playerInfo;
    CircleCollider2D circleCollider2D;

    private bool isCollected;

    private void Start(){
        anim = GetComponent<Animator>();
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    public void Effect(){
        playerInfo.SetDamageCoefficient(damageCoefficient, timeEffect);
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
