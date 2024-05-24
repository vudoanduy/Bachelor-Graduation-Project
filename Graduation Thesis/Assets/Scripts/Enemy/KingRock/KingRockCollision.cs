using Unity.VisualScripting;
using UnityEngine;

public class KingRockCollision : MonoBehaviour
{
    [SerializeField] GameObject checkWall;
    [SerializeField] LayerMask layerGround;

    [Header("Set space check wall")]
    [SerializeField] private float checkWallX;
    [SerializeField] private float checkWallY;

    KingRockMovement kingRockMovement;
    KingRockNormal kingRockNormal;

    private bool isWall, isCollisionPlayer, isChangeDirectMove = true;

    private void Start(){
        kingRockMovement = this.GetComponent<KingRockMovement>();
        kingRockNormal = this.GetComponent<KingRockNormal>();
    }

    private void Update(){
        CheckWall();
    }

    private void CheckWall(){
        isWall = Physics2D.OverlapBox(checkWall.transform.position, new Vector2(checkWallX, checkWallY), 0, layerGround);

        // Boss se quay dau di chuyen neu va phai nguoi choi hoac tuong, tuy nhien khi va phai nguoi choi thi boss se chi quay dau lan tiep theo neu nhu nguoi choi di chuyen ra ngoai khoang cach ma boss phat hien nguoi choi
        if((isWall || isCollisionPlayer) && isChangeDirectMove){
            kingRockMovement.ChangeDirectMove();
            isChangeDirectMove = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Player")){
            SetBoolIsCollisionPlayer(true);   
        }
    }

    private void OnCollisionExit2D(Collision2D other){
        if(other.gameObject.CompareTag("Player")){
            SetBoolIsCollisionPlayer(false);
        }
    }

    #region Get & Set
    public void SetBoolIsCollisionPlayer(bool isCollisionPlayer){
        this.isCollisionPlayer = isCollisionPlayer;
    }

    public void SetBoolIsChangeDirectMove(bool isChangeDirectMove){
        this.isChangeDirectMove = isChangeDirectMove;
    }

    #endregion


}
