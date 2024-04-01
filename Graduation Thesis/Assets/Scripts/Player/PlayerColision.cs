using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerColision : MonoBehaviour
{
    [Header("Layer Mask Check")]
    [SerializeField] LayerMask ground;

    [Header("GameObject Check")]
    [SerializeField] GameObject checkGround;
    [SerializeField] GameObject checkSliding;

    bool isGround, isSliding, isHeadEnemy;

    private void Update(){
        CheckGround();
        CheckSliding();
    }

    #region Check
    protected void CheckGround(){
        isGround = Physics2D.OverlapBox(checkGround.transform.position, new Vector2(0.36f, 0.054f), 0, ground);
    }

    protected void CheckSliding(){
        isSliding = Physics2D.OverlapBox(checkSliding.transform.position, new Vector2(0.12f, 0.72f), 0, ground);
    }

    #endregion
    
    #region Check hit

    // Neu va cham vao dau quai vat thi se gay dame cho quai
    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("HeadEnemy"))
        {
            StartCoroutine(DelayCheckHead());
        }
    }

    IEnumerator DelayCheckHead(){
        isHeadEnemy = true;
        
        yield return new WaitForSeconds(0.1f);

        isHeadEnemy = false;
        yield break;
    }

    #endregion

    #region Send Info

    public bool GetIsGround(){
        return this.isGround;
    }

    public bool GetIsSliding(){
        return this.isSliding;
    }

    public bool GetIsHeadEnemy(){
        return this.isHeadEnemy;
    }

    #endregion

    

}
