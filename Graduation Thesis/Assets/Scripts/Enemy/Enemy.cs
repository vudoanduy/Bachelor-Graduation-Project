public class Enemy
{
    #region Parameters

    protected int hp;
    public int HP{
        get{return hp;}
        set{hp = value;}
    }

    protected float speedMove;
    public float SpeedMove{
        get{return speedMove;}
        set{speedMove = value;}
    }

    protected int damage;
    public int Damage{
        get{return damage;}
        set{damage = value;}
    }

    #endregion

    public Enemy(int hp, float speedMove, int damage){
        this.hp = hp;
        this.speedMove = speedMove;
        this.damage = damage;
    }

    public Enemy(){
        this.hp = 0;
        this.speedMove = 0;
        this.damage = 0;
    }

    // Sinh ra ngau nhien vang tuy y
    public virtual int RandomCoin(int min, int max){
        int coins = UnityEngine.Random.Range(min,max);
        return coins;
    }
}
