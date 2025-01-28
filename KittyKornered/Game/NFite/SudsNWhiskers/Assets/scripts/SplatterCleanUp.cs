using System.Collections.Generic;
using UnityEngine;

public class SplatterCleanUp : MonoBehaviour
{
    //Texture2D textureToChange;
    Color[,] floorPixels;
    Color[,] bloodFloorPixels;
    Color[,] textureToChangePixels;
    bool active = true;
    void Start()
    {
        //textureToChange = SplatManager.SplatManagerInstance.floorTexture;
        //textureToChangePixels = SplatManager.SplatManagerInstance.floorPixels;
        //bloodFloorPixels = SplatManager.SplatManagerInstance.bloodFloorPixels;
        //floorPixels = SplatManager.SplatManagerInstance.inactiveFloorPixels;
        //Debug.Log(bloodFloorPixels.Length);
        //Debug.Log(textureToChangePixels.Length);
        //Debug.Log(floorPixels.Length);
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
     
        /*RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity) && hit.collider.tag == "Floor")
        {
            textureToChange = hit.collider.GetComponent<Renderer>().material.mainTexture as Texture2D;
            textureToChangePixels = new Color[textureToChange.width, textureToChange.height];
            Color[] allTextureToChangePixels = textureToChange.GetPixels();
            Debug.Log(allTextureToChangePixels.Length);
            int pixelCounter = 0;
            for(int i = 0; i < textureToChange.width; i++) {
                for(int j = 0; j < textureToChange.height; j++){
                    Debug.Log(i + " " + j);
                    textureToChangePixels[i,j] = allTextureToChangePixels[pixelCounter];
                    pixelCounter++;
                }
            }
            active = true;
        }*/
    }

    void LateUpdate()
    {
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out RaycastHit hit, Mathf.Infinity) && active == true)
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 
                hit.distance, Color.yellow);
            //Debug.Log(hit.collider.gameObject.name);
            if(hit.collider.tag == "Floor")
            {
                Texture2D bloodtextureToChange = SplatManager.SplatManagerInstance.BloodMaskTexture;
                Texture2D snotTexture = SplatManager.SplatManagerInstance.SnottyMaskTexture;
                Vector2 pixelUVfloat = hit.textureCoord;

                pixelUVfloat.x *= bloodtextureToChange.width;
                pixelUVfloat.y *= bloodtextureToChange.height;
                Vector2Int pixelUV = new Vector2Int((int) pixelUVfloat.x, (int) pixelUVfloat.y);

                Texture2D newTex = bloodtextureToChange;
                Texture2D snotTex = snotTexture;
                
                    Vector2Int start = new Vector2Int(pixelUV.x - 50, pixelUV.y - 50);
                    List<Color32> newColorsBlood = new List<Color32>();
                    List<Color32> newColorsSnot = new List<Color32>();
                    
                    for(int i = 0; i < 100; i++)
                    {
                        for(int j = 0; j < 100; j++)
                        {
                            newColorsBlood.Add(Color.black);
                            newColorsSnot.Add(Color.black);
                            //if(bloodtextureToChange.GetPixel(start.x + i, start.y + j) == Color.green)
                            //{
                            //    newColorsBlood.Add(Color.black);
                            //}
                            //else
                            //if(snotTexture.GetPixel(start.x +i, start.y + j) == Color.green)
                            //{
                            //    newColorsSnot.Add(Color.black);
                            //}
                            //else
                            //{
                            //    
                            //}
                        }
                    }
                    GameManager.GameManagerInstance.dirtAmount -= 1000;
                    newTex.SetPixels32(start.x, start.y, 100, 100, newColorsBlood.ToArray());
                    snotTex.SetPixels32(start.x, start.y, 100,100, newColorsSnot.ToArray());
                    bloodtextureToChange.Apply();
                    snotTexture.Apply();
            }
        }
    }
}
