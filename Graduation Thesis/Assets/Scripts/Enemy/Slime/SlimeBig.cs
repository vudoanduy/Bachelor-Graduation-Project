using System.Collections;
using ExitGames.Client.Photon.StructWrapping;
using UnityEngine;


public class SlimeBig : MonoBehaviour
{
    // Ve mat y tuong thi con quai slime nay no di chuyen qua lai tai 2 vi tri co dinh thoi
    // Khi chet thi no sinh ra bao nhieu con slime con tuy y 
    [Header("Object spawn")]
    [SerializeField] GameObject miniSlimePrefabs;

    [Header("Set parameters")]
    [SerializeField] protected int hpBigSlime;
    [SerializeField] protected float speedMoveBigSlime;
    [SerializeField] protected int damageBigSlime;

    [Header("Set coins")]
    [SerializeField] protected int minCoin;
    [SerializeField] protected int maxCoin;

    [Header("Set distance move")]
    [SerializeField] protected float pointLeft;
    [SerializeField] protected float pointRight;

    [Header("Set spawn")]
    [SerializeField] protected bool isSpawnMedium;

    Slime bigSlime;
    CheckHit<Enemy> checkHit;
    PlayerColision playerColision;
    PlayerInfo playerInfo;
    Rigidbody2D rbPlayer;
    ManageCoin manageCoin;
    AppearCoins appearCoins;

    #region Set Up

    private void Start(){
        bigSlime = new Slime(this.transform, hpBigSlime, speedMoveBigSlime, damageBigSlime, minCoin, maxCoin)
        {
            Anim = this.GetComponent<Animator>()
        };
        checkHit = new(){
            Data = bigSlime
        };
        manageCoin = FindObjectOfType<ManageCoin>();
        appearCoins = FindObjectOfType<AppearCoins>();

        SetUpBigSlime();
    }

    private void Update(){
        bigSlime.Move();
        bigSlime.UpdateTrans(this.transform);
    }

    //
    public void SetUpBigSlime(){
        float direct = UnityEngine.Random.Range(0,1);

        bigSlime.SetPoint(pointLeft, pointRight);
        bigSlime.DirectionMoveStart(direct);
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
                if(bigSlime.IsGetDamage){
                    bigSlime.IsGetDamage = false;
                    Debug.Log("Quai vat bi mat mau");
                    StartCoroutine(checkHit.HitDamage(0.625f, playerInfo.GetDamageBase()));
                    if(bigSlime.HP == 0){
                        int coin = bigSlime.RandomCoin(bigSlime.MinCoin, bigSlime.MaxCoin);

                        manageCoin.AddCoin(coin);
                        appearCoins.AppearNotifi(coin, this.transform);

                        if(isSpawnMedium){
                            StartCoroutine(SpawnMediumSlime(0.5f, hpBigSlime/2, speedMoveBigSlime));
                            StartCoroutine(SpawnMediumSlime(-0.5f, hpBigSlime/2, speedMoveBigSlime));
                        } else{
                            StartCoroutine(SpawnMiniSlime(0.5f, hpBigSlime/4, speedMoveBigSlime * 1.2f));
                            StartCoroutine(SpawnMiniSlime(-0.5f, hpBigSlime/4, speedMoveBigSlime * 1.5f));  
                        }

                        Invoke(nameof(Die), 0.3f);
                    }
                }
            } else {
                playerInfo.GetDame(bigSlime.Damage);
            }
        }
    }

    

    private void Die(){
        Destroy(this.gameObject);
    }

    #endregion
    
    #region Spawn Medium Slime

    IEnumerator SpawnMediumSlime(float directStart, int hpMediumSlime, float speedMoveMediumSlime){
        GameObject newMediumSlime = Instantiate(miniSlimePrefabs);
        SlimeMedium slimeMedium = newMediumSlime.GetComponent<SlimeMedium>();

        newMediumSlime.transform.position = this.transform.position;
        
        yield return new WaitForSeconds(0.1f);

        if(damageBigSlime / 2 < 1){
            slimeMedium.SetUpMiniSlime(hpMediumSlime, speedMoveMediumSlime, 1);
        } else {
            slimeMedium.SetUpMiniSlime(hpMediumSlime, speedMoveMediumSlime, damageBigSlime/2);
        }

        slimeMedium.SetPoint(pointLeft, pointRight, directStart);

        yield break;
    }

    #endregion

    #region Spawn Mini Slime

    // Tuy y cai dat thong so cua mini Slime theo y thich
    IEnumerator SpawnMiniSlime(float directStart, int hpMiniSlime, float speedMoveMiniSlime){
        GameObject newMiniSlime = Instantiate(miniSlimePrefabs);
        SlimeMini slimeMini = newMiniSlime.GetComponent<SlimeMini>();

        newMiniSlime.transform.position = this.transform.position;
        
        yield return new WaitForSeconds(0.1f);

        if(bigSlime.Damage / 2 < 1){
            if(hpMiniSlime < 1){
                hpMiniSlime = 1;
            }
            slimeMini.SetUpMiniSlime(hpMiniSlime, speedMoveMiniSlime, 1);
        } else {
            if(hpMiniSlime < 1){
                hpMiniSlime = 1;
            }
            slimeMini.SetUpMiniSlime(hpMiniSlime, speedMoveMiniSlime, bigSlime.Damage/2);
        }

        slimeMini.SetPoint(pointLeft, pointRight, directStart);

        yield break;
    }
    #endregion

}
