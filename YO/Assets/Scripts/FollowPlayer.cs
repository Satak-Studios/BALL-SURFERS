using UnityEngine;
using Photon.Pun;

public class FollowPlayer : MonoBehaviour
{
    public Transform player = null;
    public Vector3 offset;
    public bool Comp = false;
    public bool Custom = false;

    public Material defaultObstacleMaterial;
    public Material fadedObstacleMaterial;

    public bool exception = false;
    public bool isOnline = false;

    void Start()
    {
        if (player == null)
        {
            player = FindObjectOfType<playermovement>().transform;
            return;
        }
    }
    private void Update()
    {
        transform.position = player.position + offset;

        if (!exception && !isOnline)
        {
            if (!PhotonNetwork.InRoom)
            {
                CheckVisibility();
            }
        }
    }

    void CheckVisibility()
    {
        Vector3 playerPosition = player.position;
        RaycastHit hit;

        float startAngle = Mathf.PI - Mathf.PI / 6;
        float endAngle = Mathf.PI + Mathf.PI / 6;
        int rayCount = 7;

        float angleStep = (endAngle - startAngle) / (rayCount - 1);

        for (int i = 0; i < rayCount; i++)
        {
            float angle = startAngle + i * angleStep;
            Vector3 direction = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle));
            Debug.DrawRay(playerPosition, direction * 10f, Color.green);

            if (Physics.Raycast(playerPosition, direction, out hit))
            {
                if (hit.collider.CompareTag("Obsticle"))
                {
                    ActivateFade(hit.collider.gameObject);
                }
                else if (hit.collider.CompareTag("Player"))
                {
                    DeactivateFade(hit.collider.gameObject);
                }
            }
        }
    }

    void ActivateFade(GameObject go)
    {
        Renderer obstacleRenderer = go.GetComponent<Renderer>();
        obstacleRenderer.material = fadedObstacleMaterial;
        obstacleRenderer.material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
    }

    void DeactivateFade(GameObject go)
    {
        Renderer obstacleRenderer = go.GetComponent<Renderer>();
        obstacleRenderer.material = defaultObstacleMaterial;
        obstacleRenderer.material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
    }

    public void CompleteCompReal()
    {
        if (Comp)
        {
            FindObjectOfType<CompManager>().CompleteComp();
        }
        if (Custom)
        {
            FindObjectOfType<CustomManager>().CompleteComp();
        }
    }
}