﻿using System;
using System.Collections.Generic;
using Coliseum;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NetworkConnectionManager : MonoBehaviourPunCallbacks
{

    public Button BtnConnectMaster;
    public Button BtnConnectRoom;

    public bool TriesToConnectToMaster;
    public bool TriesToConnectToRoom;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        TriesToConnectToMaster = false;
        TriesToConnectToRoom = false;
    }

    // Update is called once per frame
    void Update()
    {
        BtnConnectMaster.gameObject.SetActive(!PhotonNetwork.IsConnected && !TriesToConnectToMaster);
        BtnConnectRoom.gameObject.SetActive(PhotonNetwork.IsConnected && !TriesToConnectToRoom &&
                                            !TriesToConnectToMaster);

    }

    public void OnClickConnectToMaster()
    {
        PhotonNetwork.OfflineMode = false;
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NickName = "playername";
        PhotonNetwork.GameVersion = "1.0";

        TriesToConnectToMaster = true;
        PhotonNetwork.ConnectUsingSettings(); //connection automatique basée sur les config de Photon/PhotonUnityNetworking/Resources/PhotonServerSettings
    }

    public void OnClickConnectToRoom()
    {
        if (!PhotonNetwork.IsConnected)
        {
            return;
        }

        TriesToConnectToRoom = true;
        //PhotonNetwork.CreateRoom("roomname");
        //PhotonNetwork.JoinRoom("Roomname");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        TriesToConnectToMaster = false;
        TriesToConnectToRoom = false;
        Debug.Log(cause);
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        TriesToConnectToMaster = false;
        Debug.Log("Connected to master !");
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        TriesToConnectToRoom = false;
        Debug.Log("Master: " +PhotonNetwork.IsMasterClient+ " | Players In Room : "+PhotonNetwork.CurrentRoom.Players+ " | Room Name : "+ PhotonNetwork.CurrentRoom.Name);
        SceneManager.LoadScene("Jeu justoin");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message); //pas de room dispo
        PhotonNetwork.CreateRoom(null, new RoomOptions{MaxPlayers = 4}); //Crée une room sans nom spécifié à la place
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        Debug.Log(message);
        TriesToConnectToRoom = false;
    }
}