using UnityEngine;

// Khi nhat duoc qua cherries nay thi se duoc cong 1 so luong tien
public class Cherries : MonoBehaviour, IFruit
{
    [SerializeField] private int coinCherries = 1000;

    Animator anim;
    CircleCollider2D circleCollider2D;
    ManageCoin manageCoin;
    AppearCoins appearCoins;

    private bool isCollected;

    private void Start(){
        anim = GetComponent<Animator>();
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    public void Effect()
    {
        manageCoin.AddCoin(coinCherries);
    }

    public void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("Player")){
            if(isCollected) return;
            isCollected = true;
            if(manageCoin == null){
                manageCoin = FindObjectOfType<ManageCoin>();
                appearCoins = FindObjectOfType<AppearCoins>();
            }
            Effect();
            appearCoins.AppearNotifi(coinCherries, this.transform);
            anim.SetTrigger("isCollected");
            circleCollider2D.isTrigger = true;
            Invoke(nameof(Destroy), 0.5f);
        }
    }

    private void Destroy(){
        Destroy(this.gameObject);
    }
}
