using System.Collections;
using UnityEngine;

public class CheckHit<T> where T: Enemy
{
    private T data;
    public T Data{
        get{return data;}
        set{data = value;}
    }

    #region Check hit

    public IEnumerator HitDamage(float timeDelay){
        Data.HP -= 1;
        
        yield return new WaitForSeconds(0.1f);

        Data.IsGetDamage = true;       
        Data.Anim.SetBool("isHit", true);

        yield return new WaitForSeconds(timeDelay);

        Data.Anim.SetBool("isHit", false);

        yield break;
    }

    #endregion
}
