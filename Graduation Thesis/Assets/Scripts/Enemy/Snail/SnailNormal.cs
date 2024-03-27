using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SnailNormal : MonoBehaviour
{
    [Header("Check Ground")]
    [SerializeField] GameObject checkGround;
    [SerializeField] LayerMask ground;

    [Header("Set parameters")]
    [SerializeField] int hpSnail;
    [SerializeField] int damageSnail;
    [SerializeField] float speedMoveSnail;

    Snail snailNormal;
    Animator anim;

    Vector2 scaleEnemy;
    Vector2 scaleCheckGround;
    Vector2 posCheckGround;

    private bool isGround, isMove = true, isChangeDirect = true;
    float angle = 0, limitAngle = 0;

    void Start(){
        SetVector();

        snailNormal = new Snail(hpSnail, speedMoveSnail, damageSnail);
        anim = this.GetComponent<Animator>();
    }

    void Update(){
        // Khi khong cham dat nua thi se xoay quai vat, den khi xoay xong thi tiep tuc di chuyen
        posCheckGround = checkGround.transform.position; 
        isGround = Physics2D.OverlapBox(posCheckGround, new Vector2(scaleCheckGround.x * scaleEnemy.x, scaleCheckGround.y * scaleEnemy.y), angle, ground);

        if(isGround){
            if(isMove){
                Move();
                isChangeDirect = true;
            }
        }

        if(!isGround){
            if(isChangeDirect){
                limitAngle += 90;
                isChangeDirect = false;
            }
            isMove = false;
        }

        if(!isMove){
            ChangeDirect();
        }

    }

    protected void SetVector(){
        scaleEnemy = this.transform.localScale;
        scaleCheckGround = checkGround.transform.localScale;
        posCheckGround = checkGround.transform.position;
    }

    #region Move Snail
    public void Move(){
        this.transform.Translate(snailNormal.SpeedMove * Time.deltaTime * Vector3.left);
    }

    protected void ChangeDirect(){
        angle += 0.3f;

        if(angle >= limitAngle){
            angle = limitAngle;
            isMove = true;
            return;
        }

        this.transform.localRotation = Quaternion.Euler(new Vector3(0,0,angle));
    }

    #endregion
}
