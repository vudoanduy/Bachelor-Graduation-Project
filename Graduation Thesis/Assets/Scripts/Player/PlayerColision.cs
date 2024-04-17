using System.Collections;
using UnityEngine;

public class PlayerColision : MonoBehaviour
{
    [Header("Layer Mask Check")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask coinLayer;

    [Header("GameObject Check")]
    [SerializeField] GameObject checkGround;
    [SerializeField] GameObject checkSliding;

    [Header("")]
    [SerializeField] private float radiusMagnet;
    [SerializeField] private float speedMagnet;

    bool isGround, isSliding, isHeadEnemy;

    private void Update(){
        CheckGround();
        CheckSliding();
        MagnetCoin();
    }

    #region Check
    protected void CheckGround(){
        isGround = Physics2D.OverlapBox(checkGround.transform.position, new Vector2(0.36f, 0.054f), 0, groundLayer);
    }

    protected void CheckSliding(){
        isSliding = Physics2D.OverlapBox(checkSliding.transform.position, new Vector2(0.12f, 0.72f), 0, groundLayer);
    }

    #endregion
    
    #region Check hit

    // Neu va cham vao dau quai vat thi se gay dame cho quai
    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("HeadEnemy"))
        {
            StartCoroutine(DelayCheckHead());
        }
        if(other.gameObject.CompareTag("Coin")){
            other.gameObject.GetComponent<Coins>().AddCoin();
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("Instruction")){
            other.transform.GetChild(0).gameObject.SetActive(true);
        }
        if(other.gameObject.CompareTag("CutScene")){
            ManageCamera manageCamera = FindObjectOfType<ManageCamera>();
            manageCamera.ChangeCams(int.Parse(other.gameObject.name), 0, 5);
            FindObjectOfType<PlayerMove>().IdlePlayer(5);
            other.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.CompareTag("Instruction")){
            other.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    IEnumerator DelayCheckHead(){
        isHeadEnemy = true;
        
        yield return new WaitForSeconds(0.1f);

        isHeadEnemy = false;
        yield break;
    }

    // Hut dong xu ve vi tri cua minh trong ban kinh
    public void MagnetCoin(){
        Collider2D[] coinCol = Physics2D.OverlapCircleAll(this.gameObject.transform.position, radiusMagnet, coinLayer);

        if(coinCol != null){
            foreach(Collider2D col in coinCol){
                col.transform.position = Vector3.MoveTowards(col.transform.position, this.transform.position, Time.deltaTime * speedMagnet);
            }
        }
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
