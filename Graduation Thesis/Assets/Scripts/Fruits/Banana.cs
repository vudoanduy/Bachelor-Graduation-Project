using UnityEngine;

// Khi nguoi choi nhat qua kiwi, nguoi choi se duoc tang toc chay tam thoi trong thoi gian cho truoc
public class Banana : MonoBehaviour, IFruit
{
    [Tooltip("Enter the number of additional jumps")]
    [SerializeField] private int amountJump;
    [Tooltip("Enter the validity period")]
    [SerializeField] private float timeEffect;

    Animator anim;
    PlayerMove playerMove;
    CircleCollider2D circleCollider2D;
    
    private bool isCollected;

    private void Start(){
        anim = GetComponent<Animator>();
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    public void Effect()
    {
        playerMove.IncreaseMaxJump(amountJump, timeEffect);
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Player")){
            if(isCollected) return;
            isCollected = true;
            if(playerMove == null){
                playerMove = FindObjectOfType<PlayerMove>();
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
