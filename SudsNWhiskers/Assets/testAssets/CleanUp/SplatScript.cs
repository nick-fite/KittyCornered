using System.Collections.Generic;
using UnityEngine;

public class SplatScript : MonoBehaviour
{

    Color[,] floorPixels;
    Color[,] bloodFloorPixels;
    Color[,] snotFloorPixels;    
    
    void Start()
    {
        floorPixels = SplatManager.SplatManagerInstance.floorPixels;    
        bloodFloorPixels = SplatManager.SplatManagerInstance.bloodFloorPixels;
        snotFloorPixels = SplatManager.SplatManagerInstance.snotFloorPixels;
    }

    public void SplatBlood()
    {
        
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
        {

            if(hit.collider.tag == "Floor")
            {
                Texture2D textureToChange = hit.collider.GetComponent<Renderer>().material.mainTexture as Texture2D;

                Vector2 pixelUVfloat = hit.textureCoord;

                pixelUVfloat.x *= textureToChange.width;
                pixelUVfloat.y *= textureToChange.height;
                Vector2Int pixelUV = new Vector2Int((int) pixelUVfloat.x, (int) pixelUVfloat.y);

                Texture2D newTex = textureToChange;
                
                
                    Vector2Int start = new Vector2Int(pixelUV.x - 250, pixelUV.y -250);
                    List<Color32> newColors = new List<Color32>();
                    
                    for(int i = 0; i < 500; i++)
                    {
                        for(int j = 0; j < 500; j++)
                        {
                            Color color = bloodFloorPixels[start.x + i, start.y + j];
                            newColors.Add(color);
                        }
                    }
                    newTex.SetPixels32(start.x, start.y, 500, 500, newColors.ToArray());
                    textureToChange.Apply();
            }
        }
    }

    public void SplatSnot(int width)
    {
        width *= 100;
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
        {

            if(hit.collider.tag == "Floor")
            {
                Texture2D textureToChange = hit.collider.GetComponent<Renderer>().material.mainTexture as Texture2D;

                Vector2 pixelUVfloat = hit.textureCoord;

                pixelUVfloat.x *= textureToChange.width;
                pixelUVfloat.y *= textureToChange.height;
                Vector2Int pixelUV = new Vector2Int((int) pixelUVfloat.x, (int) pixelUVfloat.y);

                Texture2D newTex = textureToChange;
                Debug.Log(width);
                
                    Vector2Int start = new Vector2Int(pixelUV.x - width/2, pixelUV.y - width/2);
                    List<Color32> newColors = new List<Color32>();
                    
                    for(int i = 0; i < width; i++)
                    {
                        for(int j = 0; j < width; j++)
                        {
                            Color color = snotFloorPixels[start.x + i, start.y + j];
                            newColors.Add(color);
                        }
                    }
                    newTex.SetPixels32(start.x, start.y, width, width, newColors.ToArray());
                    textureToChange.Apply();
            }
        }

    }
}
