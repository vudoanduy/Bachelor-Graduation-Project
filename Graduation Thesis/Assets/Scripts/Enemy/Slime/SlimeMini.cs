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
    Rigidbody2D rbPlayer;
    ManageCoin manageCoin;
    AppearCoins appearCoins;

    bool isMove = false;

    private void Start(){
        miniSlime = new Slime(this.transform, minCoin, maxCoin){Anim = this.GetComponent<Animator>()};       

        manageCoin = FindObjectOfType<ManageCoin>();
        appearCoins = FindObjectOfType<AppearCoins>();

        checkHit = new(){
            Data = miniSlime
        };
    }

    private void Update(){
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

    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Player"))
        {
            if(playerColision == null){
                playerColision = FindObjectOfType<PlayerColision>();
                playerInfo = FindObjectOfType<PlayerInfo>();
                rbPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
            }
            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, 30);
            if(playerColision.GetIsHeadEnemy()){
                if(miniSlime.IsGetDamage){
                    miniSlime.IsGetDamage = false;
                    StartCoroutine(checkHit.HitDamage(0.625f, playerInfo.GetDamageBase()));
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

    private  void Die(){
        Destroy(this.gameObject);
    }

    #endregion
}
