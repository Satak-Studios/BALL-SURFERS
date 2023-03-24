using UnityEngine;

public class collistion : MonoBehaviour  
{
    public playermovement movement;
    public Restart rs;
   
    
    public void OnCollisionEnter (Collision collisionInfo)

    

    {
        if (collisionInfo.collider.tag == "Obsticle")
        {
            movement.enabled = false;
            rs.EndGames();
            FindObjectOfType<Restart>().EndGames();
           
        }
    }
        
   
}
