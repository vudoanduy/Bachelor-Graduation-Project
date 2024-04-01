using System.Collections;
using UnityEngine;

public class PlantNormal : MonoBehaviour
{
    [Header("Bullet Plant")]
    [SerializeField] GameObject bulletPlantPrefabs;

    [Header("Set parameters")]
    [SerializeField] protected int hpPlant;
    [SerializeField] protected int damagePlant, damageBulletPlant;
    [SerializeField] protected float distanceLaunch, speedMoveBullet;
    

    [Header("Set coin")]
    [SerializeField] protected int minCoin;
    [SerializeField] protected int maxCoin;

    Plant plantNormal;
    CheckHit<Enemy> checkHit;
    PlayerColision playerColision;
    PlayerInfo playerInfo;
    ManageCoin manageCoin;
    AppearCoins appearCoins;

    private void Start(){
        plantNormal = new Plant(hpPlant, 0, damagePlant, minCoin, maxCoin){
            Anim = this.GetComponent<Animator>(),
            DamageBulletPlant = damageBulletPlant,
            DistanceBullet = distanceLaunch,
            SpeedMoveBullet = speedMoveBullet
        };
        playerColision = FindObjectOfType<PlayerColision>();
        playerInfo = FindObjectOfType<PlayerInfo>();
        manageCoin = FindObjectOfType<ManageCoin>();
        appearCoins = FindObjectOfType<AppearCoins>();

        checkHit = new()
        {
            Data = plantNormal
        };

        InvokeRepeating(nameof(SpawnBullet), 2, 3);
    }

    #region Attack

    public void SpawnBullet(){
        GameObject newBullet = Instantiate(bulletPlantPrefabs, this.transform);
        newBullet.GetComponent<BulletPlant>().DamageBulletPlant = plantNormal.DamageBulletPlant;
        newBullet.GetComponent<BulletPlant>().DistanceBullet = plantNormal.DistanceBullet;
        newBullet.GetComponent<BulletPlant>().SpeedMoveBullet = plantNormal.SpeedMoveBullet;
        newBullet.GetComponent<BulletPlant>().IsMove = true;
        newBullet.transform.position = this.transform.position + new Vector3(0.32f, 0.4f, 0);

        StartCoroutine(LaunchBullet());
    }

    IEnumerator LaunchBullet(){
        plantNormal.Anim.SetBool("isAttack", true);        
        yield return new WaitForSeconds(0.667f);

        plantNormal.Anim.SetBool("isAttack", false);
        yield break;
    }

    #endregion

    #region Check hit

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Player"))
        {
            if(playerColision.GetIsHeadEnemy()){
                if(plantNormal.IsGetDamage){
                    plantNormal.IsGetDamage = false;
                    StartCoroutine(checkHit.HitDamage(0.417f));
                    if(plantNormal.HP == 0){
                        int coin = plantNormal.RandomCoin(plantNormal.MinCoin, plantNormal.MaxCoin);

                        manageCoin.AddCoin(coin);
                        appearCoins.AppearNotifi(coin, this.transform);

                        Invoke(nameof(Die), 0.3f);
                    }
                }
            } else {
                playerInfo.GetDame(plantNormal.Damage);
            }
        }
    }

    protected void Die(){
        Destroy(this.gameObject);
    }

    #endregion
}
