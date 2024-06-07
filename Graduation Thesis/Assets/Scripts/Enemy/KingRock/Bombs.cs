using UnityEngine;

public class Bombs : MonoBehaviour
{
    [Header("Set damage of bomb")]
    [SerializeField] private int damageBombHit;
    [SerializeField] private int damageBombExplosion;
    [SerializeField] private float radiusExplode;

    [Header("")]
    [SerializeField] private LayerMask layerPlayer;

    Animator anim;
    PlayerInfo playerInfo;

    private bool isBombExplode, isPlayerGetDamage;

    private void Start(){
        anim = this.GetComponent<Animator>();

        Invoke(nameof(BombExplosion), 4);
    }

    private void Update(){
        if(isBombExplode){
            CheckPlayerInRangeExplode();
        }
    }

    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Player")){
            if(playerInfo == null){
                playerInfo = FindObjectOfType<PlayerInfo>();
            }
            playerInfo.GetDame(damageBombHit);
        }
    }

    public void BombExplosion(){
        anim.SetTrigger("isExplode");
        Invoke(nameof(CheckPlayerInRangeExplode), 0.6f);
        Invoke(nameof(DestroyBomb), 0.9f);
    }

    private void CheckPlayerInRangeExplode(){
        bool isPlayerInRangeAttack = Physics2D.OverlapCircle(this.transform.position, radiusExplode, layerPlayer);

        if(isPlayerInRangeAttack && !isPlayerGetDamage){
            isPlayerGetDamage = true;
            if(playerInfo == null){
                playerInfo = FindObjectOfType<PlayerInfo>();
            }
            playerInfo.GetDame(damageBombExplosion);
        }
    }

    private void DestroyBomb(){
        Destroy(this.gameObject);
        FindObjectOfType<KingRockMovement>().SetBoolIsMove(true);
    }
}
