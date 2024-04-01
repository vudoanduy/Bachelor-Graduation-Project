using UnityEngine;

public class SlimeMini : MonoBehaviour
{
    [Header("Set coins")]
    [SerializeField] protected int minCoin;
    [SerializeField] protected int maxCoin;

    Slime miniSlime;
    CheckHit<Enemy> checkHit;
    PlayerInfo playerInfo;
    PlayerColision playerColision;
    ManageCoin manageCoin;
    AppearCoins appearCoins;

    bool isMove = false;

    void Start(){
        miniSlime = new Slime(this.transform, minCoin, maxCoin){Anim = this.GetComponent<Animator>()};       
        playerColision = FindObjectOfType<PlayerColision>();
        playerInfo = FindObjectOfType<PlayerInfo>();
        manageCoin = FindObjectOfType<ManageCoin>();
        appearCoins = FindObjectOfType<AppearCoins>();

        checkHit = new(){
            Data = miniSlime
        };
    }

    void Update(){
        if(isMove){
            miniSlime.Move();
            miniSlime.UpdateTrans(this.transform);
        }
    }

    #region Set Parameters

    public void SetUpMiniSlime(int hpMiniSlime, float speedMoveMiniSlime, int damageMiniSlime){
        miniSlime.HP = hpMiniSlime;
        miniSlime.SpeedMove = speedMoveMiniSlime;
        miniSlime.Damage = damageMiniSlime;
    }

    public void SetPoint(float pointLeft, float pointRight, float directStart){
        miniSlime.SetPoint(pointLeft, pointRight);
        miniSlime.DirectionMoveStart(directStart);
        isMove = true;
    }

    #endregion

    #region Check hit

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Player"))
        {
            if(playerColision.GetIsHeadEnemy()){
                if(miniSlime.IsGetDamage){
                    miniSlime.IsGetDamage = false;
                    StartCoroutine(checkHit.HitDamage(0.625f));
                    if(miniSlime.HP == 0){
                        int coin = miniSlime.RandomCoin(miniSlime.MinCoin, miniSlime.MaxCoin);

                        manageCoin.AddCoin(coin);
                        appearCoins.AppearNotifi(coin, this.transform);

                        Invoke(nameof(Die), 0.3f);
                    }
                }
            } else {
                playerInfo.GetDame(miniSlime.Damage);
            }
        }
    }

    protected void Die(){
        Destroy(this.gameObject);
    }

    #endregion
}
