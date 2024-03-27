using System.Collections;
using UnityEngine;

// Ve mat y tuong thi con medium nay cung kha giong voi con big

public class SlimeMedium : MonoBehaviour
{
    [SerializeField] GameObject miniSlimePrefabs;

    Slime mediumSlime;
    Animator anim;
    PlayerColision playerColision;

    bool isMove = false;
    private bool isGetDamage = true;

    private int hpMediumSlime;

    [SerializeField] protected int minCoin = 1, maxCoin = 5;

    void Start(){
        mediumSlime = new Slime(this.transform);
        playerColision = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerColision>();
        
        anim = this.GetComponent<Animator>();
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
            if(playerColision.GetIsHeadEnemy()){
                if(isGetDamage){
                    isGetDamage = false;
                    StartCoroutine(HitDamage());
                }
            }  else {
                FindFirstObjectByType<PlayerInfo>().GetDame(mediumSlime.Damage);
            }
        }
    }

    IEnumerator HitDamage(){
        mediumSlime.HP -= 1;
        Debug.Log(mediumSlime.HP);

        yield return new WaitForSeconds(0.1f);

        isGetDamage = true;

        if(mediumSlime.HP == 0){
            int coinSlime = mediumSlime.RandomCoin(minCoin, maxCoin);

            FindFirstObjectByType<ManageCoin>().AddCoin(coinSlime);
            FindFirstObjectByType<AppearCoins>().AppearNotifi(coinSlime, this.transform);

            StartCoroutine(SpawnMiniSlime(0.5f, hpMediumSlime/2, mediumSlime.SpeedMove * 1.2f));
            StartCoroutine(SpawnMiniSlime(-0.5f, hpMediumSlime/2, mediumSlime.SpeedMove * 1.5f));
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

    #region Spawn Mini Slime

    // Tuy y cai dat thong so cua mini Slime theo y thich
    IEnumerator SpawnMiniSlime(float directStart, int hpMiniSlime, float speedMoveMiniSlime){
        GameObject newMiniSlime = Instantiate(miniSlimePrefabs);
        SlimeMini slimeMini = newMiniSlime.GetComponent<SlimeMini>();

        newMiniSlime.transform.position = this.transform.position;
        
        yield return new WaitForSeconds(0.1f);

        if(mediumSlime.Damage / 2 < 1){
            slimeMini.SetUpMiniSlime(hpMiniSlime, speedMoveMiniSlime, 1);
        } else {
            slimeMini.SetUpMiniSlime(hpMiniSlime, speedMoveMiniSlime, mediumSlime.Damage/2);
        }

        slimeMini.SetPoint(mediumSlime.PointLeft, mediumSlime.PointRight, directStart);
    }
    #endregion
}
