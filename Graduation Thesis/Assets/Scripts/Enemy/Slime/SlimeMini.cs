using System.Collections;
using UnityEngine;

public class SlimeMini : MonoBehaviour
{
    Slime miniSlime;

    bool isMove = false, isGround;

    void Start(){
        miniSlime = new Slime(this.transform);
    }

    void Update(){
        if(isMove){
            miniSlime.Move();
            miniSlime.UpdateTrans(this.transform);
        }
    }

    public void SetUpMiniSlime(int hpMiniSlime, float speedMoveMiniSlime){
        miniSlime.HP = hpMiniSlime;
        miniSlime.SpeedMove = speedMoveMiniSlime;
    }

    public void SetPoint(float pointLeft, float pointRight, float directStart){
        miniSlime.SetPoint(pointLeft, pointRight);
        miniSlime.DirectionMoveStart(directStart);
        isMove = true;
    }
}
