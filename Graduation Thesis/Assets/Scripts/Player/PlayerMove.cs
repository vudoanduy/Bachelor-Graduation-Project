using Cinemachine;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Virtual Camera")]
    [SerializeField] CinemachineVirtualCamera cam2D;

    Rigidbody2D rb;
    Animator anim;
    PlayerColision playerColision;

    Vector3 scalePlayer;

    [Header("Parameters")]
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
        CheckSliding();
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
        cam2D.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX = 0.4f;
    }

    protected void RunLeft(){
        rb.velocityX = -speed;
        this.transform.localScale = new Vector3(-scalePlayer.x, scalePlayer.y, scalePlayer.z);
        cam2D.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX = 0.6f;
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
            if(countJump == maxJump){
                return;
            }

            if(isGround){
                Jump();
            } else if(!isGround){
                anim.SetBool("isDouble", true);
                Jump();
                Invoke("DelayDoubleJump", 0.05f);
            }

            Invoke("DelayCountJump", 0.1f);
        }

        anim.SetFloat("velocityY", rb.velocityY);
        anim.SetBool("isGround", isGround);
    }

    protected void Jump(){
        rb.velocityY = force;
    }

    protected void DelayCountJump(){
        countJump++;
    }

    private void DelayDoubleJump(){
        anim.SetBool("isDouble", false);
    }

    #endregion
    //
    
    // Nguoi choi truot tuong khi khong cham dat
    #region PlayerSliding

    public void CheckSliding(){
        bool isGround = playerColision.GetIsGround();
        bool isSliding = playerColision.GetIsSliding();

        if(isSliding){
            countJump = 0;
            if(isGround){
                anim.SetBool("isSliding", false);
            }
            if(!isGround){
                anim.SetBool("isSliding", true);
            }
        } else {
            anim.SetBool("isSliding", false);
        }
    }

    #endregion
}
