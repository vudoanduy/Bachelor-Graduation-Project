using UnityEngine;

public class KingRockMovement : MonoBehaviour
{
    KingRockNormal kingRockNormal;

    Animator anim;
    Vector3 scaleKingRock;
    Rigidbody2D rbKingRock;

    private float speedMoveKingRock;
    private bool isMove = true;

    private void Start(){
        kingRockNormal = this.GetComponent<KingRockNormal>();
        rbKingRock = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();

        scaleKingRock = this.transform.localScale;
        speedMoveKingRock = kingRockNormal.GetMoveSpeedKingRock();
    }

    private void Update(){
        if(isMove){
            MoveOfPhaseOne();
        }
    }

    private void MoveOfPhaseOne(){
        rbKingRock.velocity = new Vector2(-speedMoveKingRock, rbKingRock.velocity.y);
    }

    public void ChangeDirectMove(){
        scaleKingRock.x *= -1;
        speedMoveKingRock *= -1;
        this.transform.localScale = scaleKingRock;
    }

    #region Set & Get
    public void SetBoolIsMove(bool isMove){
        this.isMove = isMove;
        anim.SetBool("isMove", isMove);
    }

    #endregion
}
