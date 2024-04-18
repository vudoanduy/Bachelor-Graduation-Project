using System;
using System.Collections;
using UnityEngine;

public class RinoNormal : MonoBehaviour
{
    [Header("Check wall")]
    [SerializeField] GameObject checkWall;
    [SerializeField] LayerMask ground;

    [Header("Set parameters")]
    [SerializeField] protected int hpRino;
    [SerializeField] protected float speedMoveRino;
    [SerializeField] protected float growthSpeed;
    [SerializeField] protected int damageRino;

    [Header("Set coin")]
    [SerializeField] protected int minCoin;
    [SerializeField] protected int maxCoin;


    Rino rinoNormal;
    Rigidbody2D rb;
    CheckHit<Enemy> checkHit;
    PlayerColision playerColision;
    PlayerInfo playerInfo;
    Rigidbody2D rbPlayer;
    ManageCoin manageCoin;
    AppearCoins appearCoins;

    Vector2 scaleRino, scaleCheckWall, posCheckWall;

    private bool isWall, isRun = true;

    private void Start(){
        rinoNormal = new(hpRino, speedMoveRino, damageRino, minCoin, maxCoin){
            Anim = this.GetComponent<Animator>()
        };
        checkHit = new(){
            Data = rinoNormal
        };
        rb = this.GetComponent<Rigidbody2D>();
        manageCoin = FindObjectOfType<ManageCoin>();
        appearCoins = FindObjectOfType<AppearCoins>();

        scaleRino = this.transform.localScale;
        scaleCheckWall = checkWall.transform.localScale;
        posCheckWall = checkWall.transform.position;
    }

    private void Update(){
        isWall = Physics2D.OverlapBox(checkWall.transform.position, new Vector2(scaleRino.x * scaleCheckWall.x, scaleRino.y * scaleCheckWall.y), 0, ground);

        if(isWall && isRun){
            isRun = false;
            rinoNormal.Anim.SetBool("isHitWall", true);
            StartCoroutine(ChangeDirectMove());
        }

        if(isRun){
            rinoNormal.SpeedMove += Time.deltaTime * growthSpeed;
            rb.velocity = new(rinoNormal.SpeedMove, rb.velocity.y);
        }
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
                if(rinoNormal.IsGetDamage){
                    rinoNormal.IsGetDamage = false;
                    StartCoroutine(checkHit.HitDamage(0.417f, playerInfo.GetDamageBase()));
                    if(rinoNormal.HP == 0){
                        int coin = rinoNormal.RandomCoin(rinoNormal.MinCoin, rinoNormal.MaxCoin);

                        manageCoin.AddCoin(coin);
                        appearCoins.AppearNotifi(coin, this.transform);

                        Invoke(nameof(Die), 0.3f);
                    }
                }
            } else {
                playerInfo.GetDame(rinoNormal.Damage);
            }
        }
    }

    protected void Die(){
        Destroy(this.gameObject);
    }

    #endregion

    #region Move

    IEnumerator ChangeDirectMove(){
        rb.velocity = new Vector2(-rinoNormal.SpeedMove / 5, rb.velocity.y);

        yield return new WaitForSeconds(0.33f);

        speedMoveRino *= -1;
        growthSpeed *= -1;
        rinoNormal.SpeedMove = speedMoveRino;

        scaleRino.x *= -1;
        this.transform.localScale = scaleRino;

        rinoNormal.Anim.SetBool("isHitWall", false);
        isRun = true;

        yield break;
    }

    #endregion
}
