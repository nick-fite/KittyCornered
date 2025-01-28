using UnityEngine;

public class FadeMaterialBlocking : MonoBehaviour
{
    [SerializeField] Transform player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 dir = this.transform.position - player.position;
        float dist = Vector3.Magnitude(transform.position - player.position);
        Debug.Log(dir.normalized);
        
        if (Physics.Raycast(transform.position, dir, out hit, dist))
        {
            Debug.Log(hit.collider.tag);
            if (hit.collider.tag != "Player")
            {
                Color col = hit.collider.gameObject.GetComponent<Renderer>().material.color;
                col.a = 0.5f;
                gameObject.GetComponent<Renderer>().material.color = col;
            }
        }
    }
}
