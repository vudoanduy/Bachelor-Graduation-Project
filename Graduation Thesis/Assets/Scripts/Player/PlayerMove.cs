using Cinemachine;
using ExitGames.Client.Photon.StructWrapping;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    [Header("Set Skin Move")]
    [SerializeField] RuntimeAnimatorController[] playerController;
    [SerializeField] GameObject ghostResources;

    [Header("Parameters")]
    [SerializeField] private int speed = 5;
    [SerializeField] private int force = 10;
    [SerializeField] private int maxJump = 0;
    [SerializeField] private int countMaxToSpawnGhost = 6;

    Rigidbody2D rb;
    Animator anim;
    PlayerColision playerColision;
    SpriteRenderer spriteRenderer;
    ParticleControllers particleControllers;
    CinemachineVirtualCamera cam2D;

    Vector3 scalePlayer;
    
    protected int countJump = 0;

    private int defaultSpeed, defaultForce, defaultMaxJump;
    private int countToSpawnGhost = 0;
    private float countTime = 0;
    bool prevPressLeft, prevPressRight, isFlip, isTurnOnGhost;
    
    //
    private void Start(){
        SaveManage.Instance.LoadGame();
        scalePlayer = this.transform.localScale;
        defaultSpeed = this.speed;
        defaultForce = this.force;
        defaultMaxJump = this.maxJump;

        rb = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        playerColision = this.GetComponent<PlayerColision>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        cam2D = GameObject.Find("ManageCamera").transform.GetChild(0).GetComponent<CinemachineVirtualCamera>();
        cam2D.Follow = this.transform;

        SetPlayerController(SaveManage.Instance.GetIDSkinSelected());
    }

    private void Update(){
        countTime += Time.deltaTime;
        if(isTurnOnGhost){
            countToSpawnGhost++;
            if(countToSpawnGhost > countMaxToSpawnGhost){
                GameObject newGhost = Instantiate(ghostResources);
                newGhost.GetComponent<Ghost>().CreateGhost(spriteRenderer.sprite, this.transform, isFlip);
                countToSpawnGhost = 0;
            }
        }
        CheckRun();
        CheckJump();
        CheckSliding();

        if(Input.GetKeyDown(KeyCode.M)){
            speed = 0;
            force = 0;
        }
        if(Input.GetKeyDown(KeyCode.N)){
            speed = defaultSpeed;
            force = defaultForce;
        }
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
        anim.SetFloat("velocityX", rb.velocity.x);
    }

    protected void RunRight(){
        isFlip = false;
        rb.velocity = new Vector2(speed, rb.velocity.y);
        this.transform.localScale = scalePlayer;
        cam2D.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX = 0.4f;
    }

    protected void RunLeft(){
        isFlip = true;
        rb.velocity = new Vector2(-speed, rb.velocity.y);
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
                Invoke(nameof(DelayDoubleJump), 0.05f);
            }

            Invoke(nameof(DelayCountJump), 0.1f);
        }

        anim.SetFloat("velocityY", rb.velocity.y);
        anim.SetBool("isGround", isGround);
    }

    protected void Jump(){
        rb.velocity = new Vector2(rb.velocity.x, force);

        if(particleControllers == null){
            particleControllers = FindObjectOfType<ParticleControllers>();
        }

        if(!playerColision.GetIsGround()){
            particleControllers.JumpParticlePlay();
        }
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
            if(particleControllers == null){
                particleControllers = FindObjectOfType<ParticleControllers>();
            }
            countJump = 0;
            if(isGround){
                anim.SetBool("isSliding", false);
            }
            if(!isGround){
                if(rb.velocity.y < 0 && countTime > 0.1f){
                    particleControllers.SlidingParticlePlay();
                    countTime = 0;
                }
                anim.SetBool("isSliding", true);
            }
        } else {
            anim.SetBool("isSliding", false);
        }
    }

    #endregion

    public void SetPlayerController(int idControl){
        anim.runtimeAnimatorController = playerController[idControl];
    }

    #region Change parameters
    // Inc speed
    public void IncreaseSpeed(float times,float timeInc){
        isTurnOnGhost = true;
        this.speed = (int)(this.speed * times);
        Invoke(nameof(DefaultSpeed), timeInc);
    }

    private void DefaultSpeed(){
        isTurnOnGhost = false;
        this.speed = defaultSpeed;
    }

    // Inc force
    public void IncreaseForce(float times,float timeInc){
        this.force = (int)(this.force * times);
        Invoke(nameof(DefaultForce), timeInc);
    }

    private void DefaultForce(){
        this.force = defaultForce;
    }

    // Inc maxJump
    public void IncreaseMaxJump(int times,float timeInc){
        this.maxJump += times;
        Invoke(nameof(DefaultMaxJump), timeInc);
    }

    private void DefaultMaxJump(){
        this.maxJump = defaultMaxJump;
    }

    // Cho nhan vat dung yen
    public void IdlePlayer(float timeIdle){
        speed = 0;
        force = 0;
        Invoke(nameof(DefaultAllParameter), timeIdle);
    }

    private void DefaultAllParameter(){
        speed = defaultSpeed;
        force = defaultForce;
    }
    #endregion

}
