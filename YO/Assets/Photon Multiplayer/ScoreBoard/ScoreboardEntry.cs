using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun.UtilityScripts;

public class ScoreboardEntry : MonoBehaviour
{
	//[SerializeField] private Text m_label;// = null;
	[SerializeField] private Text pName = null;
	[SerializeField] private Text m_Scorelabel = null;
	public Player Player => m_player;
	public int Score => m_player.GetScore();

	private Player m_player;

	//store player for this entry
	//set init value and color
	public void Set(Player player)
	{
		m_player = player;
		UpdateScore();
		//m_label.color = PhotonNetwork.LocalPlayer == m_player ? Color.green : Color.red;
		pName.color = PhotonNetwork.LocalPlayer == m_player ? Color.green : Color.black;
		m_Scorelabel.color = PhotonNetwork.LocalPlayer == m_player ? Color.green : Color.black;
	}

	//update label bases on score and name
	public void UpdateScore()
	{
		//m_player.SetScore(HighScoreFloat);
		//m_label.text = $"{m_player.NickName} : Score {m_player.GetScore()}";
		//pName.text = $"{m_player.NickName} : Score {m_player.GetScore()}";
		//m_label.text =  m_player.NickName;
		//pName.text = $"{m_player.NickName}";
		pName.text = $"{m_player.NickName}";
		m_Scorelabel.text = m_player.GetScore().ToString();
	}
	void Start()
    {
		if (PlayerPrefs.HasKey("PlayerName") == true)
        {
			m_player.NickName = PlayerPrefs.GetString("PlayerName");
			UpdateScore();
		}
        else
        {
			int rand = Random.Range(0, 10000);
			string player_name = "Player" + rand.ToString("0000");
			m_player.NickName = player_name;
			UpdateScore();
			Debug.Log("Your Name is Player" + rand);
			PlayerPrefs.SetString("PlayerName", player_name);
		}
		int Hscore = (int)PlayerPrefs.GetFloat("hiScore");
		m_player.SetScore(Hscore);
    }
}