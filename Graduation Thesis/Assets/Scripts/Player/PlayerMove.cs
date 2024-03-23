using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    PlayerColision playerColision;

    Vector3 scalePlayer;

    public int speed = 5, force = 10;
    public int maxJump = 0;
    
    protected int countJump = 0;

    bool prevPressLeft, prevPressRight;
    
    //
    void Start(){
        scalePlayer = this.transform.localScale;

        rb = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        playerColision = this.GetComponent<PlayerColision>();
    }

    void Update(){
        CheckRun();
        CheckJump();
    }

    // An di chuyen ben nao thi qua ben do
    // Them mot chut tinh nang khi nguoi choi dang di chuyen qua trai ma dot ngot quay phai khong may an 2 nut cung luc thi cho di chuyen theo nut bam sau
    // VD: dang di chuyen sang trai dot ngot quay phai ma 2 nut an cung luc => van qua ben phai
    #region PlayerMove
    public void CheckRun(){
        bool pressLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        bool pressRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);

        if(pressLeft && !pressRight){
            RunLeft();
            prevPressLeft = true;
            prevPressRight = false;
        } else if(!pressLeft && pressRight){
            RunRight();
            prevPressLeft = false;
            prevPressRight = true;
        } else if(pressLeft && pressRight){
            if(prevPressLeft){
                RunRight();
                prevPressLeft = true;
            }
            if(prevPressRight){
                RunLeft();
                prevPressRight = true;
            }
        }
        anim.SetFloat("velocityX", rb.velocityX);
    }

    protected void RunRight(){
        rb.velocityX = speed;
        this.transform.localScale = scalePlayer;
    }

    protected void RunLeft(){
        rb.velocityX = -speed;
        this.transform.localScale = new Vector3(-scalePlayer.x, scalePlayer.y, scalePlayer.z);
    }

    #endregion

    // Nguoi choi an nhay => nhay
    // Truoc het xu li nguoi choi nhay duoc truoc da
    // Xu li Double jump
    // Co the Jump nhieu lan tuy y setting
    #region PlayerJump

    public void CheckJump(){
        bool isGround = playerColision.GetIsGround();
        bool pressJump = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow);

        if(isGround){
            countJump = 0;
        }

        if(pressJump){
            if(countJump == (maxJump - 1)){
                return;
            }
            countJump++;

            if(isGround){
                Jump();
            } else if(!isGround){
                anim.SetBool("isDouble", true);
                Jump();
                Invoke("Delay", 0.05f);
            }
        }

        anim.SetFloat("velocityY", rb.velocityY);
        anim.SetBool("isGround", isGround);
    }

    protected void Jump(){
        rb.velocityY = force;
    }

    private void Delay(){
        anim.SetBool("isDouble", false);
    }

    #endregion
    //
    
}
