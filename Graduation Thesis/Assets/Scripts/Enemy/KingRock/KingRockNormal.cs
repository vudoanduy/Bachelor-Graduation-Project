using UnityEngine;

public class KingRockNormal : MonoBehaviour
{
    [Header("Set parameter")]
    [SerializeField] private int hpKingSlime;
    [SerializeField] private float speedMoveKingSlime;
    [SerializeField] private int damageKingSlime;

    [Header("Set parameter of skill boss")]
    [SerializeField] private LayerMask layerPlayer;
    [SerializeField] private float rangeAttack;
    [SerializeField] private int damageMeteorite;

    [Header("Set coins")]
    [SerializeField] private int minCoin;
    [SerializeField] private int maxCoin;

    KingRock kingRockNormal;
    CheckHit<KingRock> checkHit;
    PlayerColision playerColision;
    PlayerInfo playerInfo;
    Rigidbody2D rbPlayer;
    ManageCoin manageCoin;
    AppearCoins appearCoins; 

    private bool isCallMeteorite;

    private void Start(){
        kingRockNormal = new(hpKingSlime, speedMoveKingSlime, damageKingSlime, minCoin, maxCoin){
            Anim = this.GetComponent<Animator>()
        };
        checkHit = new(){
            Data = kingRockNormal
        };
        manageCoin = FindObjectOfType<ManageCoin>();
        appearCoins = FindObjectOfType<AppearCoins>();
    }

    private void Update(){
        if(Input.GetKeyDown(KeyCode.O)){
            isCallMeteorite = false;
        }

        CheckPlayerInRangeAttack();
    }

    private void CheckPlayerInRangeAttack(){
        bool isPlayerInRangeAttack = Physics2D.OverlapCircle(this.transform.position, rangeAttack, layerPlayer);

        if(isPlayerInRangeAttack && !isCallMeteorite){
            CallMeteorite();
        }
    }

    #region Move
    private void MoveOfPhaseOne(){

    }

    #endregion

    private void CallMeteorite(){
        isCallMeteorite = true;
        Debug.Log("Call Meteorite");
    }
}
