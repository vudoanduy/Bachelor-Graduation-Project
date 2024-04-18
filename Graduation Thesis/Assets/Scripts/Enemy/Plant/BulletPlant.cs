using Unity.VisualScripting;
using UnityEngine;

public class BulletPlant : MonoBehaviour
{
    private int damageBulletPlant;
    public int DamageBulletPlant{get{return damageBulletPlant;} set{damageBulletPlant = value;}}

    private float distanceBullet;
    public float DistanceBullet{get{return distanceBullet;} set{distanceBullet = value;}}

    private float speedMoveBullet;
    public float SpeedMoveBullet{get{return speedMoveBullet;} set{speedMoveBullet = value;}}

    private bool isMove;
    public bool IsMove{get{return isMove;} set{isMove = value;}}

    PlayerInfo playerInfo;
    Rigidbody2D rbPlayer;

    private float distanceMove = 0; 

    private void Update(){
        this.transform.Translate(new Vector3(-speedMoveBullet * Time.deltaTime , 0, 0));

        distanceMove += speedMoveBullet * Time.deltaTime;

        if(distanceMove >= distanceBullet && isMove){
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Player")){
            if(playerInfo == null){
                playerInfo = FindObjectOfType<PlayerInfo>();
                rbPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
            }
            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, 30);
            playerInfo.GetDame(damageBulletPlant);
            other.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.left * 10;
            DestroyBullet();
        }
        if(other.gameObject.CompareTag("Ground")){
            DestroyBullet();
        }
    }

    private void DestroyBullet(){
        Destroy(this.gameObject);
    }
}
