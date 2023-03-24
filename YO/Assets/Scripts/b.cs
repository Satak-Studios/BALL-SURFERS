using UnityEngine;

public class b : MonoBehaviour
{
    public playermovement movement;


    void OnCollisionEnter(Collision collisionInfo)



    {
        if (collisionInfo.collider.tag == "Obsticle")
        {
            movement.enabled = false;
            FindObjectOfType<GameManager>().EndGame();

        }
    }


}
