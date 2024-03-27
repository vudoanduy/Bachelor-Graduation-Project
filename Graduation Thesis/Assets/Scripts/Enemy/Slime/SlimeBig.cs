using System.Collections;
using UnityEngine;


public class SlimeBig : MonoBehaviour
{
    // Ve mat y tuong thi con quai slime nay no di chuyen qua lai tai 2 vi tri co dinh thoi
    // Khi chet thi no sinh ra bao nhieu con slime con tuy y 
    [Header("Object Mini")]
    [SerializeField] GameObject miniSlimePrefabs;

    [Header("Parameters")]
    [SerializeField] protected int hpBigSlime, damageBigSlime;
    [SerializeField] protected float speedMoveBigSlime,pointLeft, pointRight;
    [SerializeField] protected int minCoin, maxCoin;
    [SerializeField] protected bool isSpawnMedium = false;

    Slime bigSlime;
    Animator anim;
    PlayerColision playerColision;

    private bool isGetDamage = true;


    #region Set Up

    void Start(){
        bigSlime = new Slime(this.transform, hpBigSlime, speedMoveBigSlime, damageBigSlime);
        anim = this.GetComponent<Animator>();
        playerColision = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerColision>();

        SetUpBigSlime();
    }

    void Update(){
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

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Player"))
        {
            if(playerColision.GetIsHeadEnemy()){
                if(isGetDamage){
                    isGetDamage = false;
                    StartCoroutine(HitDamage());
                }
            } else {
                FindFirstObjectByType<PlayerInfo>().GetDame(bigSlime.Damage);
            }
        }
    }

    IEnumerator HitDamage(){
        bigSlime.HP -= 1;

        yield return new WaitForSeconds(0.1f);

        isGetDamage = true;

        if(bigSlime.HP == 0){
            int coin = bigSlime.RandomCoin(minCoin, maxCoin);

            FindFirstObjectByType<ManageCoin>().AddCoin(coin);
            FindFirstObjectByType<AppearCoins>().AppearNotifi(coin, this.transform);

            if(isSpawnMedium){
                StartCoroutine(SpawnMediumSlime(0.5f, hpBigSlime/2, bigSlime.SpeedMove, pointLeft, pointRight));
                StartCoroutine(SpawnMediumSlime(-0.5f, hpBigSlime/2, bigSlime.SpeedMove, pointLeft, pointRight));
            } else {
                StartCoroutine(SpawnMiniSlime(0.5f, hpBigSlime/2, bigSlime.SpeedMove * 1.2f));
                StartCoroutine(SpawnMiniSlime(-0.5f, hpBigSlime/2, bigSlime.SpeedMove * 1.5f));
            }
            Invoke(nameof(Die), 0.2f);
        }
        anim.SetBool("isHit", true);
        Invoke(nameof(TurnOffHitAnim), 0.625f);
    }

    protected void TurnOffHitAnim(){
        anim.SetBool("isHit", false);
    }

    protected void Die(){
        Destroy(this.gameObject);
    }

    #endregion
    
    #region Spawn Medium Slime

    IEnumerator SpawnMediumSlime(float directStart, int hpMediumSlime, float speedMoveMediumSlime, float pointLeft, float pointRight){
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
            slimeMini.SetUpMiniSlime(hpMiniSlime, speedMoveMiniSlime, 1);
        } else {
            slimeMini.SetUpMiniSlime(hpMiniSlime, speedMoveMiniSlime, bigSlime.Damage/2);
        }

        slimeMini.SetPoint(pointLeft, pointRight, directStart);
    }
    #endregion

}
