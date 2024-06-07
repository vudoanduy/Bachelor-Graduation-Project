using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KingRockNormal : MonoBehaviour
{
    [Header("")]
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

    [Header("Bomb of skill boss")]
    [SerializeField] GameObject bombPrefabs;
    [SerializeField] GameObject Boss;
    [SerializeField] private int numberBombKingRock = 3;
    [SerializeField] private float posBombsMinX, posBombsMaxX, posBombsY;

    KingRock kingRockNormal;
    KingRockCollision kingRockCollision;
    KingRockMovement kingRockMovement;
    CheckHit<KingRock> checkHit;
    PlayerColision playerColision;
    PlayerInfo playerInfo;
    Rigidbody2D rbPlayer;
    ManageCoin manageCoin;
    AppearCoins appearCoins;

    private bool isCallBombs, isPlayerInRangeAttack;
    private int hpMaxKingRock;
    private float cooldownTime = 10, countTime = 0;

    private void Start(){
        kingRockNormal = new(hpKingRock, speedMoveKingRock, damageKingRock, minCoin, maxCoin){
            Anim = this.GetComponent<Animator>()
        };
        checkHit = new(){
            Data = kingRockNormal
        };
        kingRockCollision = this.GetComponent<KingRockCollision>();
        kingRockMovement = this.GetComponent<KingRockMovement>();
        manageCoin = FindObjectOfType<ManageCoin>();
        appearCoins = FindObjectOfType<AppearCoins>();

        hpMaxKingRock = this.hpKingRock;

        UpdateHpBoss();
    }

    private void Update(){
        if(Input.GetKeyDown(KeyCode.O)){
            isCallBombs = false;
        }

        CheckPlayerInRangeAttack();
        UpdateCooldownTime();
    }

    private void CheckPlayerInRangeAttack(){
        isPlayerInRangeAttack = Physics2D.OverlapCircle(this.transform.position, rangeAttack, layerPlayer);

        if(isPlayerInRangeAttack && !isCallBombs){
            CallBombs();
        } 

        if(!isPlayerInRangeAttack){
            kingRockCollision.SetBoolIsChangeDirectMove(true);
            isCallBombs = false;
        }
    }

    // Ky nang goi bom roi tren troi xuong cua boss
    private void CallBombs(){
        if(countTime <= cooldownTime) return;
        isCallBombs = true;
        countTime = 0;
        kingRockMovement.SetBoolIsMove(false);

        Debug.Log("Call Bombs");
        for(int i = 0; i < numberBombKingRock; i++){
            GameObject newBomb = Instantiate(bombPrefabs, Boss.transform);
            
            float newPosXBomb = Random.Range(posBombsMinX, posBombsMaxX);
            newBomb.transform.localPosition = new Vector2(newPosXBomb, posBombsY);
        }
    }

    // Cap nhat gia tri hp cua trum
    private void UpdateHpBoss(){
        this.HPBossInfo.text = kingRockNormal.HP + " / " + this.hpMaxKingRock;
        this.HpBossBar.fillAmount = (float) kingRockNormal.HP / (float) this.hpMaxKingRock;
    }

    private void UpdateCooldownTime(){
        countTime += Time.deltaTime;
    }

    public void FullHP(){
        kingRockNormal.HP = this.hpMaxKingRock;
        UpdateHpBoss();
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
                    UpdateHpBoss();
                    if(kingRockNormal.HP == 0){
                        int coin = kingRockNormal.RandomCoin(kingRockNormal.MinCoin, kingRockNormal.MaxCoin);
                        manageCoin.AddCoin(coin);
                        appearCoins.AppearNotifi(coin, this.transform);

                        Invoke(nameof(Die), 0.3f);
                    }
                }
            } else {
                playerInfo.GetDame(kingRockNormal.Damage);
                if(playerInfo.GetHpCurrent() <= 0){
                    FindObjectOfType<MoveBridge>().ReturnStartPos();
                    this.FullHP();
                    FindObjectOfType<CheckGateBoss>().SetIsTrigger(false);
                }
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
