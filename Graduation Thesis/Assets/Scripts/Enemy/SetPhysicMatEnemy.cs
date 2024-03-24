
using System.Collections;
using ExitGames.Client.Photon.StructWrapping;
using UnityEngine;

public class SetPhysicMatEnemy : MonoBehaviour
{
    [Header("Set Physic Material")]
    [SerializeField] PhysicsMaterial2D physicMatEnemy;
    [SerializeField] GameObject checkGroundEnemy;
    [SerializeField] LayerMask ground;

    Collider2D colliderEnemy;

    bool isGround;

    void Start(){
        colliderEnemy = this.GetComponent<Collider2D>();
    }

    void Update(){
        Vector2 posCheck = checkGroundEnemy.transform.position;
        Vector2 scaleThis = this.transform.localScale;
        Vector2 scaleCheck = this.transform.localScale;

        isGround = Physics2D.OverlapBox(posCheck, new Vector2(scaleThis.x * scaleCheck.x, scaleThis.y * scaleCheck.y), 0, ground);

        if(isGround){
            StartCoroutine(ChangePhysicMat());
        }
    }

    IEnumerator ChangePhysicMat(){
        yield return new WaitForSeconds(0.5f);

        colliderEnemy.sharedMaterial = physicMatEnemy;
    }
}
