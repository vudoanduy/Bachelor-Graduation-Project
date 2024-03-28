using System.Collections;
using UnityEngine;

// Ve mat y tuong thi con medium nay cung kha giong voi con big

public class SlimeMedium : MonoBehaviour
{
    [SerializeField] GameObject miniSlimePrefabs;

    Slime mediumSlime;
    Animator anim;
    CheckHit<Enemy> checkHit;

    bool isMove = false;

    private int hpMediumSlime;

    [SerializeField] protected int minCoin = 1, maxCoin = 5;

    void Start(){
        mediumSlime = new Slime(this.transform, minCoin, maxCoin){Anim = this.GetComponent<Animator>()};   
        anim = this.GetComponent<Animator>();
        checkHit = new(){
            Data = mediumSlime
        };
    }

    void Update(){
        if(isMove){
            mediumSlime.Move();
            mediumSlime.UpdateTrans(this.transform);
        }
    }

    #region Set Parameters

    public void SetUpMiniSlime(int hpMediumSlime, float speedMoveMediumSlime, int damageMediumSlime){
        mediumSlime.HP = hpMediumSlime;
        mediumSlime.SpeedMove = speedMoveMediumSlime;
        mediumSlime.Damage = damageMediumSlime;

        this.hpMediumSlime = hpMediumSlime;
    }

    public void SetPoint(float pointLeft, float pointRight, float directStart){
        mediumSlime.SetPoint(pointLeft, pointRight);
        mediumSlime.DirectionMoveStart(directStart);
        isMove = true;
    }

    #endregion

    #region Check hit

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Player"))
        {
            if(FindFirstObjectByType<PlayerColision>().GetIsHeadEnemy()){
                if(mediumSlime.IsGetDamage){
                    mediumSlime.IsGetDamage = false;
                    StartCoroutine(checkHit.HitDamage());
                    if(mediumSlime.HP == 0){
                        int coin = mediumSlime.RandomCoin(mediumSlime.MinCoin, mediumSlime.MaxCoin);

                        FindFirstObjectByType<ManageCoin>().AddCoin(coin);
                        FindFirstObjectByType<AppearCoins>().AppearNotifi(coin, this.transform);

                        StartCoroutine(SpawnMiniSlime(0.5f, mediumSlime.HP/2, mediumSlime.SpeedMove * 1.2f));
                        StartCoroutine(SpawnMiniSlime(-0.5f, mediumSlime.HP/2, mediumSlime.SpeedMove * 1.5f));  

                        Invoke(nameof(Die), 0.3f);
                    }
                }
            } else {
                FindFirstObjectByType<PlayerInfo>().GetDame(mediumSlime.Damage);
            }
        }
    }

    protected void Die(){
        Destroy(this.gameObject);
    }

    #endregion

    #region Spawn Mini Slime

    // Tuy y cai dat thong so cua mini Slime theo y thich
    IEnumerator SpawnMiniSlime(float directStart, int hpMiniSlime, float speedMoveMiniSlime){
        GameObject newMiniSlime = Instantiate(miniSlimePrefabs);
        SlimeMini slimeMini = newMiniSlime.GetComponent<SlimeMini>();

        newMiniSlime.transform.position = this.transform.position;
        
        yield return new WaitForSeconds(0.1f);

        if(mediumSlime.Damage / 2 < 1){
            if(hpMiniSlime < 1){
                hpMiniSlime = 1;
            }
            slimeMini.SetUpMiniSlime(hpMiniSlime, speedMoveMiniSlime, 1);
        } else {
            if(hpMiniSlime < 1){
                hpMiniSlime = 1;
            }
            slimeMini.SetUpMiniSlime(hpMiniSlime, speedMoveMiniSlime, mediumSlime.Damage/2);
        }

        slimeMini.SetPoint(mediumSlime.PointLeft, mediumSlime.PointRight, directStart);

        yield break;
    }
    #endregion
}
