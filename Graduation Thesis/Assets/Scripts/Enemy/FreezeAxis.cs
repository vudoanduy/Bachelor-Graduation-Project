using System.Collections;
using UnityEngine;


// Khi quai duoc goi ra tu 1 loai quai nao do no se tu dong co thuoc tinh Physic Mat Enemy (do nay)
// Khi lan dau cham vao dat se delay va set physicMat
// Sau do dong bang truc y danh cho nhung loai quai di chuyen co dinh ko nhay?

public class FreezeAxis : MonoBehaviour
{
    [SerializeField] GameObject checkGround;
    [SerializeField] PhysicsMaterial2D enemyPhysicMat;
    [SerializeField] LayerMask ground;

    Rigidbody2D rb;
    PolygonCollider2D col2D;

    Vector2 scaleEnemy;
    Vector2 scaleCheckGround;
    Vector2 posCheckGround;

    bool isGround;

    void Start(){
        rb = this.GetComponent<Rigidbody2D>();
        col2D = this.GetComponent<PolygonCollider2D>();
    }

    void Update(){
        scaleEnemy = this.transform.localScale;
        scaleCheckGround = checkGround.transform.localScale;
        posCheckGround = checkGround.transform.position;

        isGround = Physics2D.OverlapBox(posCheckGround, new Vector2(scaleCheckGround.x * scaleEnemy.x, scaleCheckGround.y * scaleEnemy.y), 0, ground);

        if(isGround){
            StartCoroutine(ChangePhysicMat());
        }
    }

    IEnumerator ChangePhysicMat(){
        yield return new WaitForSeconds(0.2f);

        col2D.sharedMaterial = enemyPhysicMat;
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        yield break;
    }
}
