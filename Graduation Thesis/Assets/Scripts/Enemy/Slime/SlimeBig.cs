using System.Collections;
using UnityEngine;


public class SlimeBig : MonoBehaviour
{
    // Ve mat y tuong thi con quai slime nay no di chuyen qua lai tai 2 vi tri co dinh thoi
    // Khi chet thi no sinh ra bao nhieu con slime con tuy y 
    [SerializeField] GameObject miniSlimePrefabs;

    Slime bigSlime;
    Animator anim;

    [Header("Parameters")]
    public int hpBigSlime;
    public int distanceMove = 4, minMiniSlime, maxMiniSlime;
    protected int minCoin, maxCoin;

    public float speedMoveBigSlime;
    protected float pointLeft, pointRight;

    //
    void Start(){
        bigSlime = new Slime(this.transform, hpBigSlime, speedMoveBigSlime);
        anim = this.GetComponent<Animator>();

        pointLeft = this.transform.position.x - distanceMove;
        pointRight = this.transform.position.x + distanceMove;

        SetUpBigSlime();
    }

    void Update(){
        bigSlime.Move();
        bigSlime.UpdateTrans(this.transform);
    }

    //
    public void SetUpBigSlime(){
        float direct = UnityEngine.Random.Range(0,1);

        bigSlime.SetPoint(pointLeft, pointRight);
        bigSlime.DirectionMoveStart(direct);
    }

    #region Check hit

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.name == "CheckGround"){
            bigSlime.HP -= 1;
            if(bigSlime.HP == 0){
                StartCoroutine(SpawnMiniSlime(0.5f, hpBigSlime/2, speedMoveBigSlime * 2));
                StartCoroutine(SpawnMiniSlime(-0.5f, hpBigSlime/2, speedMoveBigSlime * 1.5f));
                Invoke(nameof(Die), 0.2f);
            }
            anim.SetBool("isHit", true);
            Invoke(nameof(TurnOffHitAnim), 0.625f);
        }
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

        slimeMini.SetUpMiniSlime(hpMiniSlime, speedMoveMiniSlime);
        slimeMini.SetPoint(pointLeft, pointRight, directStart);
    }

    #endregion

}
