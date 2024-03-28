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
    CheckHit<Enemy> checkHit;

    #region Set Up

    void Start(){
        bigSlime = new Slime(this.transform, hpBigSlime, speedMoveBigSlime, damageBigSlime, minCoin, maxCoin)
        {
            Anim = this.GetComponent<Animator>()
        };

        checkHit = new(){
            Data = bigSlime
        };

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
            if(FindFirstObjectByType<PlayerColision>().GetIsHeadEnemy()){
                if(bigSlime.IsGetDamage){
                    bigSlime.IsGetDamage = false;
                    StartCoroutine(checkHit.HitDamage());
                    if(bigSlime.HP == 0){
                        int coin = bigSlime.RandomCoin(bigSlime.MinCoin, bigSlime.MaxCoin);

                        FindFirstObjectByType<ManageCoin>().AddCoin(coin);
                        FindFirstObjectByType<AppearCoins>().AppearNotifi(coin, this.transform);

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
                FindFirstObjectByType<PlayerInfo>().GetDame(bigSlime.Damage);
            }
        }
    }

    protected void Die(){
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
