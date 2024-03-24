using Unity.VisualScripting;
using UnityEngine;

public class PlayerColision : MonoBehaviour
{
    [Header("Layer Mask Check")]
    [SerializeField] LayerMask ground;

    [Header("GameObject Check")]
    [SerializeField] GameObject checkGround;
    [SerializeField] GameObject checkSliding;

    bool isGround;
    bool isSliding;

    void Update(){
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
    

    #region Send Info

    public bool GetIsGround(){
        return this.isGround;
    }

    public bool GetIsSliding(){
        return this.isSliding;
    }

    #endregion

    

}
