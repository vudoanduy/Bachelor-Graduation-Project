using System;
using System.Collections;
using UnityEngine;


// Se co 1 vai che do nhu sau
// Co the chon loai quai nay phong gai, di chuyen
// 
public class TurtleNormal : MonoBehaviour
{
    [Header("Check Hit Head Turtle")]
    [SerializeField] GameObject checkHead;
    [SerializeField] GameObject launchSpikes;

    Turtle turtleNormal;
    Animator anim;
    CheckHit<Turtle> checkHit;

    [SerializeField] protected bool isStart = false, isLaunchSpikes = false, isMove = false;
    [SerializeField] protected int hpTurtle, damageTurtle, damageSpikeTurtle, minCoin, maxCoin;
    [SerializeField] protected float speedMoveTurtle, distanceLaunch, pointLeft, pointRight;

    protected bool isGrowSpike = true, isGetDamage = true;

    void Start(){
        turtleNormal = new Turtle(this.transform, hpTurtle, speedMoveTurtle, damageTurtle, damageSpikeTurtle, minCoin, maxCoin);
        anim = this.GetComponent<Animator>();

        checkHit = new()
        {
            Data = turtleNormal
        };

        turtleNormal.SetPoint(pointLeft, pointRight);
        turtleNormal.DirectionMoveStart(0.5f);
    }

    void Update(){
        if(isStart){
            if(isGrowSpike){
                isGrowSpike = false;
                StartCoroutine(GrowSpike());
            }
            if(isMove){
                turtleNormal.Move();
                turtleNormal.UpdateTrans(this.transform);
            }
        }
    }

    public void SpawnTurtle(int hpTurtle, float speedMoveTurtle, int damageTurtle, int damageSpikeTurtle, int minCoin, int maxCoin){
        turtleNormal = new Turtle(this.transform, hpTurtle, speedMoveTurtle, damageTurtle, damageSpikeTurtle, minCoin, maxCoin);
        isStart = true;
    }

    #region Grow Spike From Turtle
    IEnumerator GrowSpike(){
        if(isLaunchSpikes){
            OnLaunchSpike();
        }

        anim.SetBool("isGrow", true);
        checkHead.SetActive(false);
        yield return new WaitForSeconds(1.5f);

        OffLaunchSpike();
        anim.SetBool("isGrow", false);
        yield return new WaitForSeconds(0.65f);
        checkHead.SetActive(true);

        yield return new WaitForSeconds(3);
        isGrowSpike = true;
    }
    #endregion

    #region Check hit

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Player"))
        {
            if(FindFirstObjectByType<PlayerColision>().GetIsHeadEnemy()){
                if(turtleNormal.IsGetDamage){
                    turtleNormal.IsGetDamage = false;
                    StartCoroutine(checkHit.HitDamage());
                    if(turtleNormal.HP == 0){
                        int coin = turtleNormal.RandomCoin(turtleNormal.MinCoin, turtleNormal.MaxCoin);

                        FindFirstObjectByType<ManageCoin>().AddCoin(coin);
                        FindFirstObjectByType<AppearCoins>().AppearNotifi(coin, this.transform);

                        Invoke(nameof(Die), 0.3f);
                    }
                }
            } else {
                FindFirstObjectByType<PlayerInfo>().GetDame(turtleNormal.Damage);
            }
        }
    }

    protected void Die(){
        Destroy(this.gameObject);
    }

    #endregion

    #region Launch Spike From Turtle 

    public void OnLaunchSpike(){
        for(int i = 0; i < launchSpikes.transform.childCount; i++){
            double newPosX = 0, newPosY = 0;

            CalculatorPos(distanceLaunch, launchSpikes.transform.GetChild(i).transform.localRotation.eulerAngles.z, ref newPosX, ref newPosY);
            
            LeanTween.moveLocal(launchSpikes.transform.GetChild(i).gameObject, new Vector3((float)newPosX, (float)newPosY, 1), 1.5f).setEase(LeanTweenType.linear);
        }
    }

    public void OffLaunchSpike(){
        for(int i = 0; i < launchSpikes.transform.childCount; i++){
            launchSpikes.transform.GetChild(i).transform.localPosition = Vector3.forward;
        }
    }

    // tinh vi tri phong toi dua theo khoang cach va goc phong
    // 
    protected void CalculatorPos(float distanceLaunch, float angle, ref double newPosX, ref double newPosY){
        newPosX = distanceLaunch * Math.Sin(angle * 3.14f / 180) * -1;
        newPosY = distanceLaunch * Math.Cos(angle * 3.14f / 180);
    }

    #endregion
}
