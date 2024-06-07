using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class ReconnectManager : MonoBehaviourPunCallbacks
{
    private bool isReconnecting = false;

    private void Start()
    {
        Connect();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected from Photon. Reason: " + cause);
        if (!isReconnecting)
        {
            isReconnecting = true;
            InvokeRepeating("Reconnect", 5.0f, 5.0f); // Thử kết nối lại mỗi 5 giây
        }
    }

    private void Reconnect()
    {
        if (PhotonNetwork.ReconnectAndRejoin())
        {
            Debug.Log("Attempting to reconnect...");
        }
        else
        {
            Debug.Log("Reconnect failed, retrying...");
        }
    }

    public override void OnConnectedToMaster()
    {
        if (isReconnecting)
        {
            CancelInvoke("Reconnect");
            isReconnecting = false;
            Debug.Log("Reconnected to Photon.");

        }

        PhotonNetwork.JoinRoom("WorldChat");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom("WorldChat");
    }

    public override void OnJoinedRoom()
    {
        FindObjectOfType<ManageChat>().SpawnMyViewChat();
    }

    private void Connect()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }
}
