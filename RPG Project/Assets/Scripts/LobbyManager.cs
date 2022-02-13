using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class LobbyManager : MonoBehaviourPunCallbacks{

    public TMP_InputField createInput;
    public TMP_InputField joinInput;

    // Max players per room
    public byte maxPlayers;

    public void CreateBtn(){
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = maxPlayers; 

        PhotonNetwork.CreateRoom(createInput.text, roomOptions);
    }

    public void JoinBtn(){
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom(){
        PhotonNetwork.LoadLevel("Main");
    }
}
