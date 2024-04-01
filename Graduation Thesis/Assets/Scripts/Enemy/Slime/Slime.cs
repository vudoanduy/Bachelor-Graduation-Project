using System;
using UnityEngine;

public class Slime : Enemy
{
    Transform tranSlime;

    Vector2 posSlime, scaleSlime;

    private float pointLeft;
    public float PointLeft{get{return pointLeft;} set{pointLeft=value;}}

    private float pointRight;
    public float PointRight{get{return pointRight;} set{pointRight=value;}}

    private float currentPoint;

    #region Constructor

    public Slime(Transform tranSlime, int hp, float speedMove, int damage, int minCoin, int maxCoin) : base(hp, speedMove, damage, minCoin, maxCoin){
        UpdateTrans(tranSlime);
    }

    public Slime(Transform tranSlime, int minCoin, int maxCoin) : base(){
        UpdateTrans(tranSlime);
        base.minCoin = minCoin;
        base.maxCoin = maxCoin;
    }

    #endregion

    // Xem xet huong di chuyen ban dau cua quai vat
    // direct < 0.5 thi sang trai trc, nguoc lai thi sang phai truoc
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
        CheckDirectionMove();
    }

    // Kiem tra xem quai vat da di den gioi han co the cua no chua
    protected void CheckDirectionMove(){
        float distance = Math.Abs(this.posSlime.x - currentPoint);

        if(distance < 0.5f){
            if(currentPoint == pointLeft){
                currentPoint = pointRight;
                ChangeDirectionHead();
            } else{
                currentPoint = pointLeft;
                ChangeDirectionHead();
            }
        }

        MoveSlime();
    }

    // Ham nay de quai vat quay dau lai theo huong di chuyen
    protected void ChangeDirectionHead(){       
        this.scaleSlime.x *= -1;
        this.tranSlime.localScale = scaleSlime;
    }


    // Quai vat di chuyen sang trai hay phai
    protected void MoveSlime(){
        if(currentPoint == pointLeft){
            this.tranSlime.Translate(base.speedMove * Time.deltaTime * Vector2.left);
        } else {
            this.tranSlime.Translate(base.speedMove * Time.deltaTime * Vector2.right);
        }
    }

    #endregion
    
    // Xet khoang cach di chuyen cua quai vat co the di lai
    public void SetPoint(float pointLeft, float pointRight){
        this.pointLeft = pointLeft;
        this.pointRight = pointRight;
    }
}
