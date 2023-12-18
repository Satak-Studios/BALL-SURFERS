using Chat;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

[RequireComponent(typeof(RoomChat))]
public class RoomChatUtility : MonoBehaviourPunCallbacks
{
    [SerializeField] private RoomChat roomChat = null;

    public override void OnJoinedRoom()
    {
        var colorCode = ColorUtility.ToHtmlStringRGB(Color.blue);
        roomChat.CreateLocalMessage($"<color=#{colorCode}>You joined the Game. </color>", PhotonNetwork.LocalPlayer);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        var colorCode = ColorUtility.ToHtmlStringRGB(Color.blue);
        roomChat.CreateLocalMessage($"<color=#{colorCode}>{newPlayer.NickName} joined the Game. </color>", newPlayer);
        // Provide the 'newPlayer' parameter here
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        var colorCode = ColorUtility.ToHtmlStringRGB(new Color(1f, 0.5f, 0f));
        roomChat.CreateLocalMessage($"<color=#{colorCode}>{otherPlayer.NickName} left the Game.</color>", otherPlayer);
        // Provide the 'otherPlayer' parameter here
    }
}
