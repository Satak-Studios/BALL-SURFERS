using System;
using UnityEngine;
using UnityEngine.UI;

namespace UnityStandardAssets.Utility
{
    //[RequireComponent(typeof (Text))]
    public class FPSCounter : MonoBehaviour
    {
        const float fpsMeasurePeriod = 0.5f;
        private int m_FpsAccumulator = 0;
        private float m_FpsNextPeriod = 0;
        private int m_CurrentFps;
        const string display = "{0} FPS";
        public Text m_Text;
        public GameObject Text_Obj;


        private void Start()
        {
            
            m_FpsNextPeriod = Time.realtimeSinceStartup + fpsMeasurePeriod;
            //m_Text = GetComponent<Text>();
        }


        private void Update()
        {
            //Debug.Log("FPS Text = " + PlayerPrefs.GetInt("fps"));

            //FPS ON or OFF
            if (PlayerPrefs.GetInt("fps") == 1)
            {
                Text_Obj.SetActive(true);
                //Debug.Log("FPS Text = " + PlayerPrefs.GetInt("fps"));
            }

            if (PlayerPrefs.GetInt("fps") == 0)
            {
                Text_Obj.SetActive(false);
            }

            // measure average frames per second
            m_FpsAccumulator++;
            if (Time.realtimeSinceStartup > m_FpsNextPeriod)
            {
                m_CurrentFps = (int) (m_FpsAccumulator/fpsMeasurePeriod);
                m_FpsAccumulator = 0;
                m_FpsNextPeriod += fpsMeasurePeriod;
                m_Text.text = string.Format(display, m_CurrentFps);
            }
        }
    }
}
