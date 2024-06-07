using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomChat : MonoBehaviourPunCallbacks
{      
    [SerializeField] private GameObject myChat;
    [SerializeField] private GameObject otherChat;

    private Transform contentViewChat;
    private TMP_InputField inputFieldChat;
    private PhotonView myPV;

    private void Start(){
        myPV = GetComponent<PhotonView>();

        contentViewChat = GameObject.Find("Content").GetComponent<Transform>();
        inputFieldChat = GameObject.Find("InputField").GetComponent<TMP_InputField>();

        // inputFieldChat.onEndEdit.AddListener(SendMyMessage);
    }

    public void SendMyMessage(){
        Debug.Log("my");
        GameObject newChat = Instantiate(myChat, contentViewChat);
        newChat.transform.GetComponentInChildren<TextMeshProUGUI>().text = inputFieldChat.text;

        myPV.RPC(nameof(RPC_SendMessage), RpcTarget.Others, inputFieldChat.text);

        inputFieldChat.text = "";
    }

    [PunRPC]
    public void RPC_SendMessage(string myMessage){
        Debug.Log("your");
        GameObject newChat;
        newChat = Instantiate(otherChat, contentViewChat);

        newChat.transform.GetComponentInChildren<TextMeshProUGUI>().text = myMessage;
    }
}
