using System.Collections.Generic;
using UnityEngine;

public class SplatterCleanUp : MonoBehaviour
{
    [SerializeField] Texture2D floorTexture;
    void Start()
    {

    }

    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 
                hit.distance, Color.yellow);
            if(hit.collider.tag == "Floor")
            {
                

                //Vector2Int textCoord = new Vector2Int((int)hit.textureCoord.x * bloodMaskTexture.width, (int)hit.textureCoord.y * bloodMaskTexture.height);
                Material mat = hit.collider.GetComponent<Renderer>().material;
                Texture2D tex = mat.mainTexture as Texture2D;
                
                Vector2 pixelUV = hit.textureCoord;
                pixelUV.x *= tex.width;
                pixelUV.y *= tex.height;

                Vector2Int start = new Vector2Int((int)pixelUV.x -5, (int)pixelUV.y -5);
                List<Color32> newColors = new List<Color32>();
                for(int i = 0; i < 10; i++)
                {
                    for(int j = 0; j < 10; j++)
                    {
                        Color color = floorTexture.GetPixel(start.x + i, start.y + j);
                        newColors.Add(color);
                    }
                }
                tex.SetPixels32(start.x, start.y, 10,10, newColors.ToArray());

                tex.Apply();
            }
        }
    }
}
