using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SnailNormal : MonoBehaviour
{
    [Header("Check Ground")]
    [SerializeField] GameObject checkGround;
    [SerializeField] LayerMask ground;

    [Header("Set parameters")]
    [SerializeField] int hpSnail;
    [SerializeField] int damageSnail;
    [SerializeField] float speedMoveSnail;
    [SerializeField] int minCoin;
    [SerializeField] int maxCoin;

    Snail snailNormal;
    Animator anim;
    CheckHit<Enemy> checkHit;

    Vector2 scaleEnemy;
    Vector2 scaleCheckGround;
    Vector2 posCheckGround;

    private bool isGround, isMove = true, isChangeDirect = true, isGetDamage = true;
    float angle , limitAngle;

    [Obsolete]
    void Start(){
        SetVector();
        angle = limitAngle = this.transform.eulerAngles.z;

        snailNormal = new Snail(hpSnail, speedMoveSnail, damageSnail, minCoin, maxCoin);
        anim = this.GetComponent<Animator>();
        checkHit = new(){
            Data = snailNormal
        };
    }

    void Update(){
        // Khi khong cham dat nua thi se xoay quai vat, den khi xoay xong thi tiep tuc di chuyen
        posCheckGround = checkGround.transform.position; 
        isGround = Physics2D.OverlapBox(posCheckGround, new Vector2(scaleCheckGround.x * scaleEnemy.x, scaleCheckGround.y * scaleEnemy.y), angle, ground);

        if(isGround){
            if(isMove){
                Move();
                isChangeDirect = true;
            }
        }

        if(!isGround){
            if(isChangeDirect){
                limitAngle += 90;
                isChangeDirect = false;
            }
            isMove = false;
        }

        if(!isMove){
            ChangeDirect();
        }

    }

    protected void SetVector(){
        scaleEnemy = this.transform.localScale;
        scaleCheckGround = checkGround.transform.localScale;
        posCheckGround = checkGround.transform.position;
    }

    #region Move Snail
    public void Move(){
        this.transform.Translate(snailNormal.SpeedMove * Time.deltaTime * Vector3.left);
    }

    protected void ChangeDirect(){
        angle += 0.4f;

        if(angle >= limitAngle){
            angle = limitAngle;
            isMove = true;
            return;
        }

        this.transform.localRotation = Quaternion.Euler(new Vector3(0,0,angle));
    }

    #endregion

    #region Check hit

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Player"))
        {
            if(FindFirstObjectByType<PlayerColision>().GetIsHeadEnemy()){
                if(snailNormal.IsGetDamage){
                    snailNormal.IsGetDamage = false;
                    StartCoroutine(checkHit.HitDamage());
                    if(snailNormal.HP == 0){
                        int coin = snailNormal.RandomCoin(snailNormal.MinCoin, snailNormal.MaxCoin);

                        FindFirstObjectByType<ManageCoin>().AddCoin(coin);
                        FindFirstObjectByType<AppearCoins>().AppearNotifi(coin, this.transform);

                        Invoke(nameof(Die), 0.3f);
                    }
                }
            } else {
                FindFirstObjectByType<PlayerInfo>().GetDame(snailNormal.Damage);
            }
        }
    }

    protected void Die(){
        Destroy(this.gameObject);
    }

    #endregion
}
