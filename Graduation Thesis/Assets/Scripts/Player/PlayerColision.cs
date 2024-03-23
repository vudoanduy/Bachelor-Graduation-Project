using UnityEngine;

public class PlayerColision : MonoBehaviour
{
    [Header("Layer Mask Check")]
    [SerializeField] LayerMask ground;

    [Header("GameObject Check")]
    [SerializeField] GameObject checkGround;

    bool isGround;

    void Update(){
        CheckGround();
    }

    #region Check
    protected void CheckGround(){
        isGround = Physics2D.OverlapBox(checkGround.transform.position, new Vector2(0.36f, 0.108f), 0, ground);
    }

    #endregion
    

    #region Send Info

    public bool GetIsGround(){
        return this.isGround;
    }

    #endregion
}
