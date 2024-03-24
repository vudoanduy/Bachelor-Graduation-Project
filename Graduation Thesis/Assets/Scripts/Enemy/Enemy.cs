public class Enemy
{
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

    public Enemy(int hp, float speedMove){
        this.hp = hp;
        this.speedMove = speedMove;
    }

    public Enemy(){
        this.hp = 0;
        this.speedMove = 0;
    }

    // Sinh ra ngau nhien vang tuy y
    public virtual int RandomCoin(int min, int max){
        int coins = UnityEngine.Random.Range(min,max);
        return coins;
    }
}
