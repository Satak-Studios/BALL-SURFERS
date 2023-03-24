using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun.UtilityScripts;

public class ScoreboardOverview : MonoBehaviourPunCallbacks
{
	[SerializeField] private ScoreboardEntry m_entry = null;
	private List<ScoreboardEntry> m_entries = new List<ScoreboardEntry>();
	//private Player newPlayer;
	//public bool isinMenu;

#region Callbacks

	//creates and entry for local player and udpates the board
	public override void OnJoinedRoom()
	{
		CreateNewEntry(PhotonNetwork.LocalPlayer);
		UpdateScoreboard();
	}

	//creates entry foreach new player and updates the board
	public override void OnPlayerEnteredRoom(Player newPlayer)
	{
		CreateNewEntry(newPlayer);
		UpdateScoreboard();
	}
	
	//removes entry from player that left the room and updates the board
	public override void OnPlayerLeftRoom(Player targetPlayer)
	{
		RemoveEntry(targetPlayer);

		UpdateScoreboard();
	}

	//using this callback to update the scoreboard only if the score property changed
	public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
	{
		if (changedProps.ContainsKey(PunPlayerScores.PlayerScoreProp))
		{
			UpdateScoreboard();
		}
	}
    #endregion
    private void Start()
    {
        if (PhotonNetwork.InRoom == true)
        {
			UpdateScoreboard();
        }
    }

    private ScoreboardEntry CreateNewEntry(Player newPlayer)
	{
		var newEntry = Instantiate(m_entry, transform, false);
		newEntry.Set(newPlayer);
		m_entries.Add(newEntry);
		return newEntry;
	}

	private void UpdateScoreboard()
	{
		//iterate through all player to update score
		//if no entry exists create one
		foreach (var targetPlayer in PhotonNetwork.CurrentRoom.Players.Values)
		{
			var targetEntry = m_entries.Find(x => x.Player == targetPlayer);

			if (targetEntry == null)
			{
				targetEntry = CreateNewEntry(targetPlayer);
			}

			targetEntry.UpdateScore();
		}

		SortEntries();
	}

	private void SortEntries()
	{
		//sort entries in list
		m_entries.Sort((a, b) => b.Score.CompareTo(a.Score));

		//sort child order
		for (var i = 0; i < m_entries.Count; i++)
		{
			m_entries[i].transform.SetSiblingIndex(i);
		}
	}

	private void RemoveEntry(Player targetPlayer)
	{
		var targetEntry = m_entries.Find(x => x.Player == targetPlayer);
		m_entries.Remove(targetEntry);
		Destroy(targetEntry.gameObject);
	}

	public void ResetPlayer()
	{
		PlayerPrefs.DeleteKey("hiScore");
		PhotonNetwork.LocalPlayer.SetScore(0);
		Debug.Log("HighScore Deleted");
	}

	public void Back()
    {
		SceneManager.LoadScene("Lobby 1");
    }

	public void MenuBack()
	{
		PhotonNetwork.LeaveLobby();
		PhotonNetwork.LeaveRoom();
		PhotonNetwork.Disconnect();
	}
}