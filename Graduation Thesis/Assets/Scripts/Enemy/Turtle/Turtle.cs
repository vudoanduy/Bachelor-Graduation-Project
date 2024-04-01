
// Ve y tuong chung cua loai rua nay thi kha giong voi slime nhung khac cai la no se di chuyen cham hon va trau hon, khong co co che sinh ra rua con (muc easy)
// Tam thoi boi vi 2 con quai nay co chung 1 cach di chuyen nen tam thoi de no ke thua cua Slime (do cung khong biet nen dat ten gi)
// Sau nay neu cos thay doi cacsh di chuyen thi se update lai sau

using UnityEngine;

public class Turtle : Slime
{
    protected int damageSpikeTurtle;
    public int DamageSpikeTurtle{get{return damageSpikeTurtle;} set{damageSpikeTurtle = value;}}

    public Turtle(Transform transSlime, int minCoin, int maxCoin) : base(transSlime, minCoin, maxCoin){
        this.damageSpikeTurtle = 1;
    }

    public Turtle(Transform transTurtle, int hp, float speedMove, int damage, int damageSpikeTurtle, int minCoin, int maxCoin)
    : base(transTurtle ,hp, speedMove, damage, minCoin, maxCoin)
    {
        this.damageSpikeTurtle = damageSpikeTurtle;
    }  
}
