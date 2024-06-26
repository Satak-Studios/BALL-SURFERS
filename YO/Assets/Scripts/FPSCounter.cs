using System;
using UnityEngine;
using UnityEngine.UI;

namespace Satak.Utilities
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

        public bool must = false;
        public int fps;

        private void Start()
        {
            m_FpsNextPeriod = Time.realtimeSinceStartup + fpsMeasurePeriod;
        }


        private void Update()
        {
            //FPS ON or OFF
            if (PlayerPrefs.GetInt("fps") == 1)
            {
                Text_Obj.SetActive(true);
            }

            if (PlayerPrefs.GetInt("fps") == 0 && !must)
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
                fps = m_CurrentFps;
            }
        }
    }
}
