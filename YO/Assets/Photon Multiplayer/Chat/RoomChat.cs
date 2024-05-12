using System.Collections;
using System.Collections.Generic;
using System.Text;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Chat
{
    [RequireComponent(typeof(PhotonView))]
    public class RoomChat : MonoBehaviourPunCallbacks, IPointerDownHandler
    {
        [SerializeField] private Text messagePrefab = null;
        [SerializeField] private Transform content = null;
        [SerializeField] private InputField messageInput = null;

        private Queue<string> messageQueue = new Queue<string>();
        private bool canSend = true;
        public int QueueCapacity = 50;
        float SendRate = 1f;

        private void Start()
        {
            messageInput.text = string.Empty;
        }

        private void Update()
        {
            // You can add any necessary updates here
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!messageInput.IsInteractable())
            {
                Open();
            }
        }

        private void Open()
        {
            messageInput.interactable = true;
            EventSystem.current.SetSelectedGameObject(messageInput.gameObject);
        }

        public void SendAndClose()
        {
            if (!string.IsNullOrEmpty(messageInput.text))
            {
                SendMessage();
            }
            string achievementKey = "Achievement_23";
            if (PlayerPrefs.GetInt(achievementKey) == 0)
            {
                FindObjectOfType<Achiever>().AchievementUnlocked(23);
            }
            messageInput.text = string.Empty;
            EventSystem.current.SetSelectedGameObject(null);
        }

        private void SendMessage()
        {
            if (canSend)
            {
                HandleQueueLimit(messageInput.text);
                StartCoroutine(HandleMessageLimit());
                string achievementKey = "Achievement_23";
                if (PlayerPrefs.GetInt(achievementKey) == 0)
                {
                    FindObjectOfType<Achiever>().AchievementUnlocked(23);
                }
            }
            else
            {
                HandleQueueLimit(messageInput.text);
            }
        }

        private void HandleQueueLimit(string msg)
        {
            if (messageQueue.Count < QueueCapacity)
            {
                messageQueue.Enqueue(msg);
            }
            else
            {
                Debug.Log("Message Queue is full. Wait a moment.");
            }
        }

        private IEnumerator HandleMessageLimit()
        {
            canSend = false;
            yield return new WaitForSeconds(SendRate);

            StringBuilder messageBuilder = new StringBuilder();
            while (messageQueue.Count > 0)
            {
                messageBuilder.AppendLine(messageQueue.Dequeue());
            }

            if (messageBuilder.Length > 0)
            {
                photonView.RPC(nameof(SendMessageRPC), RpcTarget.All, messageBuilder.ToString());
            }

            canSend = true;
        }


        [PunRPC]
        private void SendMessageRPC(string text, PhotonMessageInfo info)
        {
            CreateLocalMessage(text, info.Sender);
        }

        public void CreateLocalMessage(string text, Player sender)
        {
            var senderName = FormatName(sender);
            var messageText = Instantiate(messagePrefab, content, false);
            messageText.text = $"{senderName}: {text}";
        }

        private string FormatName(Player player)
        {
            var senderName = string.IsNullOrEmpty(player.NickName) ? "Someone" : player.NickName;
            var color = Equals(player, PhotonNetwork.LocalPlayer) ? "green" : "black";
            return $"<color={color}>[{senderName}]</color>";
        }
    }
}
