using System;
using UnityEngine;

public class Slime : Enemy
{
    protected float pointLeft, pointRight, currentPoint;

    Transform tranSlime;
    Vector2 posSlime, scaleSlime;
    
    public Slime(Transform tranSlime, int hp, float speedMove, int damage) : base(hp, speedMove, damage){
        UpdateTrans(tranSlime);
    }

    public Slime(Transform tranSlime) : base(){
        UpdateTrans(tranSlime);
    }

    //
    public void DirectionMoveStart(float direct){
        if(direct < 0.5f){
            currentPoint = pointLeft;
            ChangeDirectionHead();
        } else {
            currentPoint = pointRight;
        }
    }

    // Cap nhat gia tri transform Slime
    public void UpdateTrans(Transform tranSlime){
        this.tranSlime = tranSlime;
        this.posSlime = tranSlime.position;
        this.scaleSlime = tranSlime.localScale;
    }

    #region Slime Move
    public void Move(){
        this.tranSlime.Translate(Vector2.right * base.speedMove * Time.deltaTime);

        ChangeDirectionMove();
    }

    protected void ChangeDirectionMove(){
        float distance = Math.Abs(this.posSlime.x - currentPoint);

        if(distance < 0.5f){
            if(currentPoint == pointLeft){
                currentPoint = pointRight;
            } else{
                currentPoint = pointLeft;
            }

            ChangeDirectionHead();
        }
    }

    protected void ChangeDirectionHead(){
        base.speedMove *= -1;
        this.scaleSlime.x *= -1;
        this.tranSlime.localScale = scaleSlime;
    }

    #endregion
    
    public void SetPoint(float pointLeft, float pointRight){
        this.pointLeft = pointLeft;
        this.pointRight = pointRight;
    }
}
