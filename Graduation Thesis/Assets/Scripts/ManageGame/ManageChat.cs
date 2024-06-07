using System.Collections;
using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using Photon.Realtime;
using Unity.VisualScripting;
using UnityEngine;

public class ManageChat : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject myViewChat;
    [SerializeField] Transform viewChat;

    GameObject newViewChat;

    public void SpawnMyViewChat(){
        newViewChat = PhotonNetwork.Instantiate(myViewChat.name, Vector3.zero, Quaternion.identity);
        newViewChat.transform.SetParent(this.transform);
        
        StartCoroutine(nameof(CheckViewChat));
    }

    IEnumerator CheckViewChat(){
        yield return new WaitForSeconds(0.5f);

        RoomChat[] listRoomChat = FindObjectsOfType<RoomChat>();
        Debug.Log(listRoomChat.Length);
        for(int i = 0; i < listRoomChat.Length; i++){
            if(listRoomChat[i].gameObject != newViewChat){
                // Destroy(listRoomChat[i].gameObject);
                listRoomChat[i].gameObject.SetActive(false);
            }
        }

        yield break;
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        StartCoroutine(nameof(CheckViewChat));
    }

    public void SetScaleX(int xScale){
        Vector3 newScale = viewChat.transform.localScale;
        newScale.x = xScale;
        viewChat.transform.localScale = newScale;
    }
}
