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

    private void Start(){
        kingRockMovement = this.GetComponent<KingRockMovement>();
    }

    private void Update(){
        CheckWall();
    }

    private void CheckWall(){
        bool isWall = Physics2D.OverlapBox(checkWall.transform.position, new Vector2(checkWallX, checkWallY), 0, layerGround);

        if(isWall){
            kingRockMovement.ChangeDirectMove();
        }
    }
}
