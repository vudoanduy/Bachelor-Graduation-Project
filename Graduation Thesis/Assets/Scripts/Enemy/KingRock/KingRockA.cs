using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KingRockNormal : MonoBehaviour
{
    [SerializeField] GameObject BossInfo;
    [SerializeField] Image HpBossBar;
    [SerializeField] TextMeshProUGUI HPBossInfo;

    [Header("Set parameter")]
    [SerializeField] private int hpKingRock;
    [SerializeField] private float speedMoveKingRock;
    [SerializeField] private int damageKingRock;

    [Header("Set parameter of skill boss")]
    [SerializeField] private LayerMask layerPlayer;
    [SerializeField] private float rangeAttack;
    [SerializeField] private int damageMeteorite;

    [Header("Set coins")]
    [SerializeField] private int minCoin;
    [SerializeField] private int maxCoin;

    KingRock kingRockNormal;
    KingRockCollision kingRockCollision;
    CheckHit<KingRock> checkHit;
    PlayerColision playerColision;
    PlayerInfo playerInfo;
    Rigidbody2D rbPlayer;
    ManageCoin manageCoin;
    AppearCoins appearCoins;

    private bool isCallMeteorite, isPlayerInRangeAttack;
    private int hpMaxKingRock;

    private void Start(){
        kingRockNormal = new(hpKingRock, speedMoveKingRock, damageKingRock, minCoin, maxCoin){
            Anim = this.GetComponent<Animator>()
        };
        checkHit = new(){
            Data = kingRockNormal
        };
        kingRockCollision = this.GetComponent<KingRockCollision>();
        manageCoin = FindObjectOfType<ManageCoin>();
        appearCoins = FindObjectOfType<AppearCoins>();


        hpMaxKingRock = this.hpKingRock;

        UpdateHpBoss();
    }

    private void Update(){
        if(Input.GetKeyDown(KeyCode.O)){
            isCallMeteorite = false;
        }

        CheckPlayerInRangeAttack();
    }

    private void CheckPlayerInRangeAttack(){
        isPlayerInRangeAttack = Physics2D.OverlapCircle(this.transform.position, rangeAttack, layerPlayer);

        if(isPlayerInRangeAttack && !isCallMeteorite){
            CallMeteorite();
        } 

        if(!isPlayerInRangeAttack){
            kingRockCollision.SetBoolIsChangeDirectMove(true);
        }
    }

    // Skill boss
    private void CallMeteorite(){
        isCallMeteorite = true;
        Debug.Log("Call Meteorite");
    }

    // Cap nhat gia tri hp cua trum
    private void UpdateHpBoss(){
        this.HPBossInfo.text = kingRockNormal.HP + " / " + this.hpMaxKingRock;
        this.HpBossBar.fillAmount = (float) kingRockNormal.HP / (float) this.hpMaxKingRock;
    }

    #region Check hit

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Player"))
        {
            if(playerColision == null){
                playerColision = FindObjectOfType<PlayerColision>();
                playerInfo = FindObjectOfType<PlayerInfo>();
                rbPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
            }
            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, 30);
            
            if(playerColision.GetIsHeadEnemy()){
                if(kingRockNormal.IsGetDamage){
                    kingRockNormal.IsGetDamage = false;
                    StartCoroutine(checkHit.HitDamage(0.1f, playerInfo.GetDamageBase()));
                    if(kingRockNormal.HP == 0){
                        int coin = kingRockNormal.RandomCoin(kingRockNormal.MinCoin, kingRockNormal.MaxCoin);

                        manageCoin.AddCoin(coin);
                        appearCoins.AppearNotifi(coin, this.transform);

                        Invoke(nameof(Die), 0.3f);
                    }
                }
            } else {
                playerInfo.GetDame(kingRockNormal.Damage);
            }
        }
    }

    protected void Die(){
        Destroy(this.gameObject);
    }

    #endregion

    #region Set & Get
    public float GetMoveSpeedKingRock(){
        return speedMoveKingRock;
    }

    public bool GetIsPlayerInRangeAttack(){
        return isPlayerInRangeAttack;
    }

    #endregion
}
