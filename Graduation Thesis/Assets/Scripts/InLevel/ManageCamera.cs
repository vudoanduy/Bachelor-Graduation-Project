using System.Collections;
using UnityEngine;

public class ManageCamera : MonoBehaviour
{
    GameObject[] listCamera;

    private void Start(){
        PickUpCamera();
    }

    private void PickUpCamera(){
        listCamera = new GameObject[transform.childCount];

        PickUpCam();
        ChangeCam(0);
    }

    private void PickUpCam(){
        for(int i = 0; i < listCamera.Length; i++){
            listCamera[i] = transform.GetChild(i).gameObject;
        }
    }

    #region Change Camera
    // Su dung khi muon thay doi cam trong 1 khoang thoi gian nao do
    // Hay su dung timeChangeTotal la so le de no co 1s dung 
    public void ChangeCams(int nextCam, int returnCam, int timeChangeTotal){
        StartCoroutine(ChangeCamera(nextCam, returnCam, timeChangeTotal));
    }

    IEnumerator ChangeCamera(int nextCam, int returnCam, int timeChangeTotal){
        ChangeCam(nextCam);

        yield return new WaitForSeconds(timeChangeTotal/2 + 1);

        ChangeCam(returnCam);

        yield break;
    }

    // Su dung de thay doi camera moi thay the camera hien tai luon
    public void ChangeCam(int nextCam){
        for(int i = 0; i < transform.childCount; i++){
            if(i == nextCam){
                listCamera[i].SetActive(true);
            } else {
                listCamera[i].SetActive(false);
            }
        }
    }

    #endregion
}
