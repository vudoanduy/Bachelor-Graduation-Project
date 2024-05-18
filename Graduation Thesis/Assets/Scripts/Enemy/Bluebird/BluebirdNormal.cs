using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BluebirdNormal : MonoBehaviour
{
    [Header("Set parameters")]
    [SerializeField] protected int hpBluebird;
    [SerializeField] protected int damageBluebird;
    [SerializeField] protected float speedMoveBluebird;

    [Header("Set moving space")]
    [SerializeField] protected float minX;
    [SerializeField] protected float maxX;
    [SerializeField] protected float minY;
    [SerializeField] protected float maxY;

    [Header("Set coin")]
    [SerializeField] protected int minCoin;
    [SerializeField] protected int maxCoin;

    Bluebird bluebird;
    CheckHit<Enemy> checkHit;
    ManageCoin manageCoin;
    AppearCoins appearCoins;
    PlayerColision playerColision;
    PlayerInfo playerInfo;
    Rigidbody2D rbPlayer;
    
    private bool isMoving;

    private float posTargetX, posTargetY;
    private float posTargetXNext, posTargetYNext;


    private void Start(){
        bluebird = new(hpBluebird, speedMoveBluebird, damageBluebird, minCoin, maxCoin){
            Anim = this.GetComponent<Animator>()
        };
        checkHit = new(){
            Data = bluebird
        };
        manageCoin = FindObjectOfType<ManageCoin>();
        appearCoins = FindObjectOfType<AppearCoins>();

        posTargetX = this.transform.position.x;
        posTargetY = this.transform.position.y;

        Move();
    }

    private void Update(){
        if(isMoving){
            MoveToTargetPoint();
        }
    }

    #region Move
    private void Move(){
        isMoving = true;
        StartCoroutine(MoveBlueBird());
    }

    IEnumerator MoveBlueBird(){
        posTargetXNext = Random.Range(minX, maxX);
        posTargetYNext = Random.Range(minY, maxY);

        CheckFlipBluebird();

        yield return new WaitUntil(() => this.transform.position == new Vector3(posTargetX, posTargetY, this.transform.position.z));
        isMoving = false;

        yield return new WaitForSeconds(2);
        Move();

        yield break;
    }

    private void CheckFlipBluebird(){
        if(posTargetXNext > posTargetX){
            this.transform.localScale = new Vector3(-6,6,1);
        } else {
            this.transform.localScale = new Vector3(6,6,1);
        }

        posTargetX = posTargetXNext;
        posTargetY = posTargetYNext;
    }

    private void MoveToTargetPoint(){
        transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(posTargetX, posTargetY, this.transform.position.z),speedMoveBluebird * Time.deltaTime);
    }

    #endregion

    #region Check hit

    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Player"))
        {
            if(playerColision == null){
                playerColision = FindObjectOfType<PlayerColision>();
                playerInfo = FindObjectOfType<PlayerInfo>();
                rbPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
            }
            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, 30);
            if(playerColision.GetIsHeadEnemy()){
                if(bluebird.IsGetDamage){
                    bluebird.IsGetDamage = false;
                    Debug.Log("Quai vat bi mat mau");
                    StartCoroutine(checkHit.HitDamage(0.417f, playerInfo.GetDamageBase()));
                    if(bluebird.HP == 0){
                        int coin = bluebird.RandomCoin(bluebird.MinCoin, bluebird.MaxCoin);

                        manageCoin.AddCoin(coin);
                        appearCoins.AppearNotifi(coin, this.transform);

                        Invoke(nameof(Die), 0.3f);
                    }
                }
            } else {
                playerInfo.GetDame(bluebird.Damage);
            }
        }
    }

    private void Die(){
        Destroy(this.gameObject);
    }

    #endregion
}
