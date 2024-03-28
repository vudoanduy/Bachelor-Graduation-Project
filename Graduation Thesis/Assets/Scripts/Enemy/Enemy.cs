using System;
using UnityEngine;

public class Enemy
{
    #region Parameters

    protected int hp;
    public int HP{get{return hp;} set{hp = value;}}

    protected float speedMove;
    public float SpeedMove{get{return speedMove;} set{speedMove = value;}}

    protected int damage;
    public int Damage{ get{return damage;} set{damage = value;}}

    protected int minCoin;
    public int MinCoin{get{return minCoin;} set{minCoin = value;}}

    protected int maxCoin;
    public int MaxCoin{get{return maxCoin;} set{maxCoin = value;}}

    protected bool isGetDamage = true;
    public bool IsGetDamage{get{return isGetDamage;} set{isGetDamage = value;}}

    protected Animator anim;
    public Animator Anim{get{return anim;} set{anim = value;}}

    #endregion

    public Enemy(int hp, float speedMove, int damage, int minCoin, int maxCoin){
        this.hp = hp;
        this.speedMove = speedMove;
        this.damage = damage;
        this.minCoin = minCoin;
        this.maxCoin = maxCoin;
    }

    public Enemy(){
        this.hp = 0;
        this.speedMove = 0;
        this.damage = 0;
        minCoin = 0;
        maxCoin = 0;
    }

    // Sinh ra ngau nhien vang tuy y
    public virtual int RandomCoin(int min, int max){
        int coins = UnityEngine.Random.Range(min,max);
        return coins;
    }

    internal void Invoke(string v1, float v2)
    {
        throw new NotImplementedException();
    }
}
