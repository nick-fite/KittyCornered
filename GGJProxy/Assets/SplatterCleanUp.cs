using UnityEngine;

public class SplatterCleanUp : MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 
                hit.distance, Color.yellow);
            GameObject obj = hit.collider.gameObject;
            Texture2D tex = (Texture2D) obj.GetComponent<Renderer>().material.mainTexture;
            Debug.Log(tex.GetPixelBilinear(hit.textureCoord.x, hit.textureCoord.y));
            tex.SetPixel((int)hit.textureCoord.x, (int)hit.textureCoord.y, Color.white);
            obj.GetComponent<Renderer>().material.mainTexture = tex;
            tex.Apply(); 
        }
    }
}
