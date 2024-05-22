using UnityEngine;

public class KingRockMovement : MonoBehaviour
{
    KingRockNormal kingRockNormal;

    Vector3 scaleKingRock;
    Rigidbody2D rbKingRock;

    private float speedMoveKingRock;

    private void Start(){
        kingRockNormal = this.GetComponent<KingRockNormal>();
        rbKingRock = this.GetComponent<Rigidbody2D>();

        scaleKingRock = this.transform.localScale;
        speedMoveKingRock = kingRockNormal.GetMoveSpeedKingRock();
    }

    private void Update(){
        MoveOfPhaseOne();
    }

    private void MoveOfPhaseOne(){
        rbKingRock.velocity = new Vector2(-speedMoveKingRock, rbKingRock.velocity.y);
    }

    public void ChangeDirectMove(){
        scaleKingRock.x *= -1;
        speedMoveKingRock *= -1;
        this.transform.localScale = scaleKingRock;
    }
}
