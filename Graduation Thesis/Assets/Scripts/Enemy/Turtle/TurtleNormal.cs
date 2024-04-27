using System;
using System.Collections;
using ExitGames.Client.Photon.StructWrapping;
using UnityEngine;


// Se co 1 vai che do nhu sau
// Co the chon loai quai nay phong gai, di chuyen
// 
public class TurtleNormal : MonoBehaviour
{
    [Header("Check hit head of Turtle")]
    [SerializeField] GameObject checkHead;
    [SerializeField] GameObject launchSpikes;

    [Header("Spikes")]
    [SerializeField] GameObject[] spikeTurtlePrefabs;

    [Header("Set parameters")]
    [SerializeField] protected bool isStart;
    [SerializeField] protected int hpTurtle;
    [SerializeField] protected float speedMoveTurtle;
    [SerializeField] protected int damageTurtle;

    [Header("Set launch spikes")]
    [SerializeField] protected bool isLaunchSpikes;
    [SerializeField] protected int damageSpikeTurtle;
    [SerializeField] protected float distanceLaunch;

    [Header("Set coins")]
    [SerializeField] protected int minCoin;
    [SerializeField] protected int maxCoin;

    [Header("Set distance move")]
    [SerializeField] protected bool isMove;
    [SerializeField] protected float pointLeft;
    [SerializeField] protected float pointRight;


    Turtle turtleNormal;
    CheckHit<Enemy> checkHit;
    PlayerColision playerColision;
    PlayerInfo playerInfo;
    Rigidbody2D rbPlayer;
    ManageCoin manageCoin;
    AppearCoins appearCoins;

    protected bool isGrowSpike = true;

    #region Set Up

    void Start(){
        turtleNormal = new Turtle(this.transform, hpTurtle, speedMoveTurtle, damageTurtle, damageSpikeTurtle, minCoin, maxCoin){
            Anim = this.GetComponent<Animator>()
        };
        manageCoin = FindObjectOfType<ManageCoin>();
        appearCoins = FindObjectOfType<AppearCoins>();

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

    #endregion

    #region Grow Spike From Turtle
    IEnumerator GrowSpike(){
        if(isLaunchSpikes){
            SpawnSpikes();
        }

        turtleNormal.Anim.SetBool("isGrow", true);
        checkHead.SetActive(false);
        yield return new WaitForSeconds(1.5f);

        OffLaunchSpike();
        turtleNormal.Anim.SetBool("isGrow", false);
        yield return new WaitForSeconds(0.65f);
        checkHead.SetActive(true);

        yield return new WaitForSeconds(3);
        isGrowSpike = true;

        yield break;
    }
    #endregion

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
                if(turtleNormal.IsGetDamage){
                    turtleNormal.IsGetDamage = false;
                    StartCoroutine(checkHit.HitDamage(0.417f, playerInfo.GetDamageBase()));
                    if(turtleNormal.HP == 0){
                        int coin = turtleNormal.RandomCoin(turtleNormal.MinCoin, turtleNormal.MaxCoin);

                        manageCoin.AddCoin(coin);
                        appearCoins.AppearNotifi(coin, this.transform);

                        Invoke(nameof(Die), 0.3f);
                    }
                }
            } else {
                playerInfo.GetDame(turtleNormal.Damage);
            }
        }
    }

    protected void Die(){
        Destroy(this.gameObject);
    }

    #endregion

    #region Launch Spike From Turtle 

    public void SpawnSpikes(){
        for(int i = 0; i < spikeTurtlePrefabs.Length; i++){
            GameObject newSpike = Instantiate(spikeTurtlePrefabs[i], launchSpikes.transform);
            newSpike.GetComponent<SpikeTurtle>().DamageSpikeTurtle = turtleNormal.DamageSpikeTurtle;
        }

        OnLaunchSpike();
    }

    public void OnLaunchSpike(){
        for(int i = 0; i < launchSpikes.transform.childCount; i++){
            double newPosX = 0, newPosY = 0;

            CalculatorPos(distanceLaunch, launchSpikes.transform.GetChild(i).transform.localRotation.eulerAngles.z, ref newPosX, ref newPosY);
            
            LeanTween.moveLocal(launchSpikes.transform.GetChild(i).gameObject, new Vector3((float)newPosX, (float)newPosY, 1), 1.5f).setEase(LeanTweenType.linear);
        }
    }

    public void OffLaunchSpike(){
        for(int i = 0; i < launchSpikes.transform.childCount; i++){
            // launchSpikes.transform.GetChild(i).transform.localPosition = Vector3.forward;
            Destroy(launchSpikes.transform.GetChild(i).gameObject);
        }
    }

    // tinh vi tri phong toi dua theo khoang cach va goc phong
    // 
    protected void CalculatorPos(float distanceLaunch, float angle, ref double newPosX, ref double newPosY){
        newPosX = distanceLaunch * Math.Sin(angle * 3.14f / 180) * -1;
        newPosY = distanceLaunch * Math.Cos(angle * 3.14f / 180);
    }

    #endregion

    #region Get Value

    public int GetDamageSpikeTurtle(){
        return this.damageSpikeTurtle;
    }

    #endregion
}
