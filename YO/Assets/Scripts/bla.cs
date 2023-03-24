using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Android;
using System;

namespace Photonyo.movement
{
    public class bla : MonoBehaviour
    {


        public playermovement pm;
        bool isgoingleft;
        bool isgoingright;
        public float forwardForce = 200f;
        public float sidewaysForce = 50f;

        private void Start()
        {
            pm = FindObjectOfType<playermovement>();
        }

        public void left()
        {
            pm.rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            
        }
        public void rightt()
        {
            pm.rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        private void FixedUpdate()
        {
            // Add a forward force
            pm.rb.AddForce(0, 0, forwardForce * Time.deltaTime);

            if (pm.rb.position.y < -1f)
            {
                FindObjectOfType<Restart>().EndGames();
            }
          if (isgoingleft)
          {
                Debug.Log("Is Going Left");
                left();
          }

            if (isgoingright)
            {
                Debug.Log("Is Going Right");
                rightt();
            }
        }

        public void pointerDownleft()
        {
            isgoingleft = true;
        }
        public void pointerUpleft()
        {
            isgoingleft = false;
        }
        public void pointerDownright()
        {
            isgoingright = true;
        }
        public void pointerUpright()
        {
            isgoingright = false;
        }
    }

}


