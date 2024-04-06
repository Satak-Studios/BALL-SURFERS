using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using Satak.Utilities;

public class ScoreboardEntry : MonoBehaviour
{
	[SerializeField] private Text pName = null;
	[SerializeField] private Text m_Scorelabel = null;
	[SerializeField] private Text m_time = null;
	public Player Player => m_player;

	public int playerPosition;
	public float Score => SatakExtensions.GetTime(m_player);

	private Player m_player;

	public void Set(Player player)
	{
		m_player = player;
		UpdateScore();
		pName.color = PhotonNetwork.LocalPlayer == m_player ? Color.black : Color.black;
		m_Scorelabel.color = PhotonNetwork.LocalPlayer == m_player ? Color.black : Color.black;
		m_time.color = PhotonNetwork.LocalPlayer == m_player ? Color.black : Color.black;
		playerPosition = 0;
		PhotonNetwork.NickName = PlayerPrefs.GetString("PlayerName");
	}

	public void UpdateScore()
	{
		pName.text = $"{m_player.NickName}";
		m_Scorelabel.text = playerPosition.ToString();
		m_time.text = ConvertSecondsToTimeString(SatakExtensions.GetTime(m_player));
	}

	void Update()
	{
		UpdateScore();
	}

	public void SetPlayerPos(int newPos)
    {
		if (!(SatakExtensions.GetTime(m_player) == 0))
		{
			playerPosition = newPos;
			SatakExtensions.SetPlayerPosition(m_player, playerPosition);
		}
		else
		{
			playerPosition = 0;
			SatakExtensions.SetPlayerPosition(m_player, 0);
		}
    }

	private string ConvertSecondsToTimeString(float seconds)
	{
		int totalMilliseconds = Mathf.RoundToInt(seconds * 1000);

		int minutes = totalMilliseconds / (60 * 1000);
		int secondsRemaining = (totalMilliseconds % (60 * 1000)) / 1000;
		int milliseconds = totalMilliseconds % 1000;

		string formattedTime = $"{minutes:D2}:{secondsRemaining:D2}:{milliseconds:D3}";

		return formattedTime;
	}
}