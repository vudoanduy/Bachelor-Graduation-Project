using UnityEngine;

// Khi nguoi choi nhat qua kiwi, nguoi choi se duoc tang toc chay tam thoi trong thoi gian cho truoc
public class Kiwi : MonoBehaviour, IFruit
{
    [Tooltip("Enter the running speed increase in percentage")]
    [SerializeField] private float perSpeed;
    [Tooltip("Enter the validity period")]
    [SerializeField] private float timeIncSpeed;

    Animator anim;
    PlayerMove playerMove;
    CircleCollider2D circleCollider2D;

    private void Start(){
        anim = GetComponent<Animator>();
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    public void Effect()
    {
        playerMove.IncreaseSpeed(perSpeed, timeIncSpeed);
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
