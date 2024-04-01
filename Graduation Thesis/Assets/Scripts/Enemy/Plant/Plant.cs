
public class Plant : Enemy
{
    private int damageBulletPlant;
    public int DamageBulletPlant{get{return damageBulletPlant;} set{damageBulletPlant = value;}}

    private float distanceBullet;
    public float DistanceBullet{get{return distanceBullet;} set{distanceBullet = value;}}

    private float speedMoveBullet;
    public float SpeedMoveBullet{get{return speedMoveBullet;} set{speedMoveBullet = value;}}

    public Plant(int hp, float speedMove, int damage, int minCoin, int maxCoin) : base(hp, speedMove, damage, minCoin, maxCoin){}
}
