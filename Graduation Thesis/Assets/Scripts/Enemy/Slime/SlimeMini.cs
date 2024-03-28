using System.Collections;
using UnityEngine;

public class SlimeMini : MonoBehaviour
{
    Slime miniSlime;
    Animator anim;
    CheckHit<Enemy> checkHit;

    bool isMove = false;
    private bool isGetDamage = true;

    [SerializeField] protected int minCoin = 1, maxCoin = 5;

    void Start(){
        miniSlime = new Slime(this.transform);       
        anim = this.GetComponent<Animator>();
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
            if(FindFirstObjectByType<PlayerColision>().GetIsHeadEnemy()){
                if(miniSlime.IsGetDamage){
                    miniSlime.IsGetDamage = false;
                    StartCoroutine(checkHit.HitDamage());
                    if(miniSlime.HP == 0){
                        int coin = miniSlime.RandomCoin(miniSlime.MinCoin, miniSlime.MaxCoin);

                        FindFirstObjectByType<ManageCoin>().AddCoin(coin);
                        FindFirstObjectByType<AppearCoins>().AppearNotifi(coin, this.transform);

                        Invoke(nameof(Die), 0.3f);
                    }
                }
            } else {
                FindFirstObjectByType<PlayerInfo>().GetDame(miniSlime.Damage);
            }
        }
    }

    protected void Die(){
        Destroy(this.gameObject);
    }

    #endregion
}
