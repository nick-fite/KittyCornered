using System.Collections.Generic;
using UnityEngine;

public class SplatterCleanUp : MonoBehaviour
{
    [SerializeField] Texture2D bloodMaskTexture;
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
                Texture2D newTex = tex;
                
                Vector2 pixelUV = hit.textureCoord;
                pixelUV.x *= tex.width;
                pixelUV.y *= tex.height;

                Vector2Int start = new Vector2Int((int)pixelUV.x -5, (int)pixelUV.y -5);
                List<Color32> test = new List<Color32>();
                for(int i = 0; i < 10; i++)
                {
                    for(int j = 0; j < 10; j++)
                    {
                        test.Add(Color.black);
                    }
                }
                tex.SetPixels32(start.x, start.y, 10,10, test.ToArray());

                tex.Apply();

                //Texture2D mask = mat.GetTexture("_BloodMask") as Texture2D;
                //Vector2Int start = new Vector2Int(textCoord.x-5, textCoord.y -5);
                //for(int i = 0; i < 100; i++)
                //{

                //}

                //mask.SetPixel(textCoord.x, textCoord.y, Color.black);
                //mat.SetTexture("_BloodMask",bloodMaskTexture);
                //bloodMaskTexture.Apply();
                
                //Texture2D newMask = mask;

                /*Texture2D maskBase = mat.GetTexture("_BloodMask") as Texture2D;
                Color bloodPixel = maskBase.GetPixel(textCoord.x, textCoord.y);
                Debug.Log(bloodPixel);
                Texture2D mask = new Texture2D(maskBase.width, maskBase.height);
                mask.SetPixels(maskBase.GetPixels());
                mask.SetPixel(textCoord.x, textCoord.y, Color.black);
                mask.Apply();
                mat.SetTexture("_BloodMask", mask);*/
            }
            

            //Vector2 textCoord = hit.textureCoord;
            //int pixelX = (int)(textCoord.x * bloodMaskTexture.width);
            //int pixelY = (int)(textCoord.x * bloodMaskTexture.height);

            //Vector2Int paintPixelPos = new Vector2Int(pixelX, pixelY);
        }
    }
}
