using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

namespace UpdateChecker
{
    struct GameData
    {
        public string Title;
        public string Description;
        public string Version;
        public string Url;
        public string isNews;
        public string newsVersion;
    }

    public class UpdateChecker: MonoBehaviour
    {
        public GameObject uiCanvas;
        public Text titleText;
        public Text descriptionText;
        public Text versionText;

        public string jsonDataURL;
        static bool isAlreadyCheckedForUpdates = false;
        GameData latestGameData;

        public GameObject UpdateBtn;
        public GameObject NotNowBtn;
        public GameObject OkayBtn;

        private void Start()
        {
            if (!isAlreadyCheckedForUpdates)
            {
                StopAllCoroutines();
                StartCoroutine(CheckForUpdates());
            }
        }

        IEnumerator CheckForUpdates()
        {
            UnityWebRequest request = UnityWebRequest.Get(jsonDataURL);
            request.disposeDownloadHandlerOnDispose = true;
            request.timeout = 60;
            yield return request.SendWebRequest();

            if (request.isDone)
                isAlreadyCheckedForUpdates = true;

            if (!request.isNetworkError && !request.isHttpError)
            {
                latestGameData = JsonUtility.FromJson<GameData>(request.downloadHandler.text);

                if (!Application.version.Equals(latestGameData.Version))
                {
                    if (latestGameData.Title != null)
                    {
                        titleText.text = latestGameData.Title;               
                    }
                    else
                    {
                        Debug.Log("Null title");
                    }
                    descriptionText.text = latestGameData.Description;
                    versionText.text = "v" + latestGameData.Version;
                    UpdateBtn.SetActive(true);
                    NotNowBtn.SetActive(true);
                    OkayBtn.SetActive(false);
                    ShowPopUp();
                    Debug.Log("Update Available = " + latestGameData.Version + ", version = " + Application.version);
                }

                if (Application.version.Equals(latestGameData.Version) && latestGameData.isNews.Equals("yes"))
                {
                    titleText.text = latestGameData.Title;
                    descriptionText.text = latestGameData.Description;
                    versionText.text = "News." + latestGameData.newsVersion[0] + "." + latestGameData.newsVersion[1];
                    UpdateBtn.SetActive(false);
                    NotNowBtn.SetActive(false);
                    OkayBtn.SetActive(true);
                    ShowPopUp();
                    Debug.Log("News Available = " + latestGameData.newsVersion);
                }
            }

            request.Dispose();
        }

        void ShowPopUp()
        {
            uiCanvas.SetActive(true);
        }

        public void HidePopUp()
        {
            uiCanvas.SetActive(false);
        }

        public void UpdateGame()
        {
            Application.OpenURL(latestGameData.Url);
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}
