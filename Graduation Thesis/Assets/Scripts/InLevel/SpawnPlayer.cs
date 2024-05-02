using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

public class SpawnPlayer : MonoBehaviour
{
    [Header("")]
    [SerializeField] private GameObject playerResources;
    [SerializeField] private GameObject revival_btn;

    [Header("")]
    [Tooltip("The location where the player is spawned")]
    [SerializeField] private Vector3 startPos;

    [Header("")]
    [SerializeField] LocalizedString revival_request;
    [SerializeField] TextMeshProUGUI textRevivalRequest;
    [Tooltip("Enter the respawn price for the first time")]
    [SerializeField] private float costFirstRevival = 200;
    [SerializeField] private float increaseCoefficient = 0.5f;

    ManageCoin manageCoin;

    private readonly int costRevival; // gia tri hien thi tren bang thong bao gia hoi sinh
    private float costTotal;

    private void OnEnable(){
        revival_request.Arguments = new object[]{costRevival};
        revival_request.StringChanged += UpdateText;
    }

    private void OnDisable(){
        revival_request.StringChanged -= UpdateText;
    }

    private void Start(){
        SpawnNewPlayer(startPos);
        DefaultCost();
    }

    public void SpawnNewPlayer(Vector3 spawnPos){
        StartCoroutine(Spawn(startPos));
    }

    IEnumerator Spawn(Vector3 spawnPos){
        GameObject newPlayer = Instantiate(playerResources);

        Animator anim = newPlayer.GetComponent<Animator>();
        Rigidbody2D rbPlayer = newPlayer.GetComponent<Rigidbody2D>();

        newPlayer.transform.localPosition = new Vector3(spawnPos.x, spawnPos.y, 0);
        newPlayer.name = "Player";
        newPlayer.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0);

        rbPlayer.simulated = false;
        yield return new WaitForSeconds(2); // time close + time pause to trans scene

        newPlayer.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
        anim.Play("AppearPlayer");   
        yield return new WaitForSeconds(0.583f);

        anim.SetTrigger("appeared");    
        rbPlayer.simulated = true;
        yield break;
    }

    // Hoi sinh nguoi choi tai checkpoint hoac vi tri ban dau 
    public void RevivalPlayer(){
        if(manageCoin == null){
            manageCoin = FindObjectOfType<ManageCoin>();
        }
        if(manageCoin.CheckCoin((int)costTotal)){
            StartCoroutine(Revival(2));
            SetStateRevivalBtn(false);
            GameObject.Find("Revival_request").SetActive(false);
        } else {
            GameObject.Find("btn_No").GetComponent<MovePage>().SetPage();
        }
    }

    IEnumerator Revival(float time){
        FindObjectOfType<ManageTransitionScene>().Transition(2); // time close (1) + time pause (1) to trans scene
        yield return new WaitForSeconds(1);
        manageCoin.SubCoin((int)costTotal);
        CalculateCost();

        SpawnNewPlayer(startPos);
        yield break;
    }

    // Cap nhat va tinh toan gia tri hoi sinh
    private void UpdateText(string value){
        textRevivalRequest.text = value;
    }

    public void UpdateCost(){
        revival_request.Arguments[0] = costTotal;
        revival_request.RefreshString();
    }

    public void CalculateCost(){
        this.costTotal = (int)(costTotal * (1 + increaseCoefficient));
    }

    private void DefaultCost(){
        this.costTotal = costFirstRevival;
    }

    //
    public void SetStateRevivalBtn(bool stateBtn){
        // if(!stateBtn){
        //     if(revival_btn.activeSelf){
        //         revival_btn.SetActive(stateBtn);
        //     }
        // } else {
        //     revival_btn.SetActive(stateBtn);
        // }
        revival_btn.SetActive(stateBtn);
    }
}

