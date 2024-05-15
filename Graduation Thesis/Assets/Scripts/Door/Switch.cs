using System.Collections;
using Photon.Realtime;
using Unity.VisualScripting;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] GameObject bridgeTarget;
    [SerializeField] GameObject switchOn;
    [SerializeField] GameObject switchOff;

    [Header("")]
    [SerializeField] private int idNextCam;

    bool isOn;

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Player")){
            if(!isOn){
                StartCoroutine(CutSceneMoveBridge());
            }
        }
    }

    IEnumerator CutSceneMoveBridge(){
        isOn = true;
        switchOn.SetActive(true);
        switchOff.SetActive(false);

        ManageCamera manageCamera = FindObjectOfType<ManageCamera>();
        PlayerMove playerMove = FindObjectOfType<PlayerMove>();
        
        playerMove.IdlePlayer(7);
        manageCamera.ChangeCam(idNextCam);
        yield return new WaitForSeconds(2);

        bridgeTarget.GetComponent<MoveBridge>().MoveToTarget();
        yield return new WaitForSeconds(3);

        manageCamera.ChangeCam(0);
        yield break;

    }
}
