using UnityEngine;

public class SnailNormal : MonoBehaviour
{
    [Header("Check ground")]
    [SerializeField] GameObject checkGround;
    [SerializeField] LayerMask ground;

    [Header("Set parameters")]
    [SerializeField] protected int hpSnail;
    [SerializeField] protected int damageSnail;
    [SerializeField] protected float speedMoveSnail;

    [Header("Set coins")]
    [SerializeField] protected int minCoin;
    [SerializeField] protected int maxCoin;

    Snail snailNormal;
    CheckHit<Enemy> checkHit;
    PlayerColision playerColision;
    PlayerInfo playerInfo;
    ManageCoin manageCoin;
    AppearCoins appearCoins;

    Vector2 scaleEnemy;
    Vector2 scaleCheckGround;
    Vector2 posCheckGround;

    private bool isGround, isMove = true, isChangeDirect = true;
    private float angle, limitAngle;

    #region Set Up

    private void Start(){
        SetVector();
        angle = limitAngle = this.transform.eulerAngles.z;

        snailNormal = new Snail(hpSnail, speedMoveSnail, damageSnail, minCoin, maxCoin){
            Anim = this.GetComponent<Animator>()
        };
        checkHit = new(){
            Data = snailNormal
        };
        manageCoin = FindObjectOfType<ManageCoin>();
        appearCoins = FindObjectOfType<AppearCoins>();
    }

    private void Update(){
        // Khi khong cham dat nua thi se xoay quai vat, den khi xoay xong thi tiep tuc di chuyen
        posCheckGround = checkGround.transform.position; 
        isGround = Physics2D.OverlapBox(posCheckGround, new Vector2(scaleCheckGround.x * scaleEnemy.x, scaleCheckGround.y * scaleEnemy.y), angle, ground);

        if(isGround){
            if(isMove){
                Move();
                isChangeDirect = true;
            }
        } else {
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

    #endregion

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

    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Player"))
        {
            if(playerColision == null){
                playerColision = FindObjectOfType<PlayerColision>();
                playerInfo = FindObjectOfType<PlayerInfo>();   
            }
            if(playerColision.GetIsHeadEnemy()){
                if(snailNormal.IsGetDamage){
                    snailNormal.IsGetDamage = false;
                    StartCoroutine(checkHit.HitDamage(0.417f, playerInfo.GetDamageBase()));
                    if(snailNormal.HP == 0){
                        int coin = snailNormal.RandomCoin(snailNormal.MinCoin, snailNormal.MaxCoin);

                        manageCoin.AddCoin(coin);
                        appearCoins.AppearNotifi(coin, this.transform);

                        Invoke(nameof(Die), 0.3f);
                    }
                }
            } else {
                playerInfo.GetDame(snailNormal.Damage);
            }
        }
    }

    private void Die(){
        Destroy(this.gameObject);
    }

    #endregion
}
