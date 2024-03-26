using System.Collections;
using UnityEngine;

public class SlimeMini : MonoBehaviour
{
    Slime miniSlime;
    Animator anim;
    PlayerColision playerColision;

    bool isMove = false, isGround;
    private bool isGetDamage = true;

    protected int minCoin = 1, maxCoin = 5;

    void Start(){
        miniSlime = new Slime(this.transform);
        playerColision = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerColision>();
        
        anim = this.GetComponent<Animator>();
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
                if(isGetDamage){
                    isGetDamage = false;
                    StartCoroutine(HitDamage());
                }
            }  else {
                FindFirstObjectByType<PlayerInfo>().GetDame(miniSlime.Damage);
            }
        }
    }

    IEnumerator HitDamage(){
        miniSlime.HP -= 1;

        yield return new WaitForSeconds(0.1f);

        isGetDamage = true;

        if(miniSlime.HP == 0){
            int coinSlime = miniSlime.RandomCoin(minCoin, maxCoin);

            FindFirstObjectByType<ManageCoin>().AddCoin(coinSlime);
            FindFirstObjectByType<AppearCoins>().AppearNotifi(coinSlime, this.transform);
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
}
