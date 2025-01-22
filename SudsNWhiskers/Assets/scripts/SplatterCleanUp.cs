using System.Collections.Generic;
using UnityEngine;

public class SplatterCleanUp : MonoBehaviour
{
    [SerializeField] Texture2D floorTexture;
    [SerializeField] Texture2D bloodFloorTexture;
    Texture2D textureToChange;
    Color[,] floorPixels;
    Color[,] bloodFloorPixels;
    Color[,] textureToChangePixels;
    bool active;
    void Start()
    {
        floorPixels = SplatManager.SplatManagerInstance.floorPixels;
        bloodFloorPixels = SplatManager.SplatManagerInstance.bloodFloorPixels;
        /*floorPixels = new Color[floorTexture.width, floorTexture.height];
        Color[] allFloorPixels = floorTexture.GetPixels();
        int pixelCounter = 0;
        for(int i = 0; i < floorTexture.width; i++) {
            for(int j = 0; j < floorTexture.height; j++){
                floorPixels[i,j] = allFloorPixels[pixelCounter];
                pixelCounter++;
            }
        }

        bloodFloorPixels = new Color[bloodFloorTexture.width, bloodFloorTexture.height];
        Color[] allBloodFloorPixels = bloodFloorTexture.GetPixels();
        pixelCounter = 0;
        for(int i = 0; i < bloodFloorTexture.width; i++) {
            for(int j = 0; j < bloodFloorTexture.height; j++){
                bloodFloorPixels[i,j] = allBloodFloorPixels[pixelCounter];
                pixelCounter++;
            }
        }*/
     
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity) && hit.collider.tag == "Floor")
        {
            textureToChange = hit.collider.GetComponent<Renderer>().material.mainTexture as Texture2D;

            textureToChangePixels = new Color[floorTexture.width, floorTexture.height];
            Color[] allTextureToChangePixels = floorTexture.GetPixels();
            int pixelCounter = 0;
            for(int i = 0; i < textureToChange.width; i++) {
                for(int j = 0; j < textureToChange.height; j++){
                    textureToChangePixels[i,j] = allTextureToChangePixels[pixelCounter];
                    pixelCounter++;
                }
            }
            active = true;
        }
    }

    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity) && active == true)
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 
                hit.distance, Color.yellow);
            if(hit.collider.tag == "Floor")
            {
                Texture2D newTex = textureToChange;

                Vector2 pixelUVfloat = hit.textureCoord;

                pixelUVfloat.x *= textureToChange.width;
                pixelUVfloat.y *= textureToChange.height;
                Vector2Int pixelUV = new Vector2Int((int) pixelUVfloat.x, (int) pixelUVfloat.y);
                if(textureToChangePixels[pixelUV.x, pixelUV.y] == bloodFloorPixels[pixelUV.x, pixelUV.y])
                {
                    Vector2Int start = new Vector2Int(pixelUV.x -5, pixelUV.y -5);
                    List<Color32> newColors = new List<Color32>();
                    for(int i = 0; i < 10; i++)
                    {
                        for(int j = 0; j < 10; j++)
                        {
                            Color color = floorPixels[start.x + i, start.y + j];
                            newColors.Add(color);
                        }
                    }
                    newTex.SetPixels32(start.x, start.y, 10,10, newColors.ToArray());
                    textureToChange.Apply();
                }

            }
        }
    }
}
