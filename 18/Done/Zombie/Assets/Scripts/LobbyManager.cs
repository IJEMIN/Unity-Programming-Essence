using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks {
    private string gameVersion = "1";

    public Text connectionInfoText;
    public Button joinButton;

    void Start() {
        joinButton.interactable = false;

        connectionInfoText.text = "Connecting To Match Making Server...";

        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }


    public void Connect() {
        joinButton.interactable = false;

        if (PhotonNetwork.IsConnected)
        {
            connectionInfoText.text = "Joining Random Room...";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            connectionInfoText.text =
                "Off-line : Lost Connection with Match Making Server...\nRe-Connecting...";
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster() {
        joinButton.interactable = true;
        connectionInfoText.text =
            "On-line : Connected with Match Making Server";
    }

    public override void OnDisconnected(DisconnectCause cause) {
        joinButton.interactable = false;
        connectionInfoText.text =
            "Off-line : Lost Connection with Match Making Server...\nRe-Connecting...";

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnJoinRandomFailed(short returnCode, string message) {
        connectionInfoText.text =
            "No available room, Creating new room...";

        PhotonNetwork.CreateRoom(null, new RoomOptions {MaxPlayers = 4});
    }

    public override void OnJoinedRoom() {
        connectionInfoText.text = "Joining Room Success";

        PhotonNetwork.LoadLevel("Main");
    }
}