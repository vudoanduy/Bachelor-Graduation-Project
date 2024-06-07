using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGateBoss : MonoBehaviour
{
    BoxCollider2D boxGateBoss;

    [SerializeField] GameObject switchGateBoss;
    [SerializeField] GameObject bossInfo;

    private void Start(){
        boxGateBoss = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Player")){
            SetIsTrigger(true);
            switchGateBoss.GetComponent<Switch>().CutSceneMoveBridge();
        }
    }

    public void SetIsTrigger(bool isTrigger){
        boxGateBoss.isTrigger = isTrigger;
        bossInfo.SetActive(isTrigger);
    }
}
