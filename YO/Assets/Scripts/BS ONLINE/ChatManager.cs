using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviourPunCallbacks, IPunObservable
{
    public InputField messageInput;
    public Text chatText;
    public ScrollRect scrollRect;

    // Maximum number of messages to display in the chat
    public int maxMessages = 50;

    private void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            Debug.LogError("Photon is not connected!");
            return;
        }

        // Subscribe to player left room event
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDestroy()
    {
        // Unsubscribe from player left room event
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SendChatMessage();
        }
    }

    public void SendChatMessage()
    {
        if (messageInput.text.Length > 0)
        {
            string playerName = PhotonNetwork.LocalPlayer.NickName;
            string message = $"{playerName}: {messageInput.text}";

            // Call an RPC to send the chat message to all clients
            photonView.RPC("ReceiveChatMessage", RpcTarget.All, message);

            // Clear the input field
            messageInput.text = "";

            // Scroll to the bottom to show the latest message
            Canvas.ForceUpdateCanvases();
            scrollRect.verticalNormalizedPosition = 0f;

            // Limit the number of messages displayed
            LimitChatMessages();
        }
    }

    [PunRPC]
    void ReceiveChatMessage(string message)
    {
        // Display the received chat message on all clients
        chatText.text += message + "\n";

        // Scroll to the bottom to show the latest message
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;

        // Limit the number of messages displayed
        LimitChatMessages();
    }

    void LimitChatMessages()
    {
        // Check if the number of lines in the chat text exceeds the limit
        string[] lines = chatText.text.Split('\n');
        if (lines.Length > maxMessages)
        {
            // Remove the oldest messages to stay within the limit
            int removeCount = lines.Length - maxMessages;
            chatText.text = string.Join("\n", lines, removeCount, maxMessages);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // Implement serialization if needed
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        // Clear the chat when a player leaves the room
        chatText.text = "";
    }
}
