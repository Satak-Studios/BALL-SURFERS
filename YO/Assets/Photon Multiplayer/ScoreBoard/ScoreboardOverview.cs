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

#region Callbacks
	public override void OnJoinedRoom()
	{
		CreateNewEntry(PhotonNetwork.LocalPlayer);
		UpdateScoreboard();
	}

	public override void OnPlayerEnteredRoom(Player newPlayer)
	{
		CreateNewEntry(newPlayer);
		UpdateScoreboard();
	}
	
	public override void OnPlayerLeftRoom(Player targetPlayer)
	{
		RemoveEntry(targetPlayer);

		UpdateScoreboard();
	}

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

    private void Update()
    {
		SortEntries();

	}

    private void SortEntries()
	{
		List<ScoreboardEntry> sortedEntries = new List<ScoreboardEntry>(m_entries);

		sortedEntries.Sort((a, b) =>
		{
			if (a.Score == 0f && b.Score == 0f)
			{
				return 0;
			}
			else if (a.Score == 0f)
			{
				return 1;
			}
			else if (b.Score == 0f)
			{
				return -1;
			}
			else
			{
				return a.Score.CompareTo(b.Score);
			}
		});

		for (var i = 0; i < sortedEntries.Count; i++)
		{
			sortedEntries[i].transform.SetSiblingIndex(i);
			sortedEntries[i].SetPlayerPos(i + 1);
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
}