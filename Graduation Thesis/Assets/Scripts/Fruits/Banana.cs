using UnityEngine;

// Khi nguoi choi nhat qua kiwi, nguoi choi se duoc tang toc chay tam thoi trong thoi gian cho truoc
public class Banana : MonoBehaviour, IFruit
{
    [Tooltip("Enter the number of additional jumps")]
    [SerializeField] private int amountJump;
    [Tooltip("Enter the validity period")]
    [SerializeField] private float timeIncMaxJump;

    Animator anim;
    PlayerMove playerMove;
    CircleCollider2D circleCollider2D;

    private void Start(){
        anim = GetComponent<Animator>();
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    public void Effect()
    {
        playerMove.IncreaseMaxJump(amountJump, timeIncMaxJump);
    }

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Player")){
            if(playerMove == null){
                playerMove = FindObjectOfType<PlayerMove>();
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
