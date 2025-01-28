using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SplatScript : MonoBehaviour
{
    Color[,] inactiveFloorPixels;
    Color[,] bloodFloorPixels;
    Color[,] snotFloorPixels;
    [SerializeField] Transform raycastPos;

    void Start()
    {
     //   inactiveFloorPixels = SplatManager.SplatManagerInstance.inactiveFloorPixels; 
     //   bloodFloorPixels = SplatManager.SplatManagerInstance.bloodFloorPixels;
     //   snotFloorPixels = SplatManager.SplatManagerInstance.snotFloorPixels;
    }

    void Update()
    {
        //transform.position += transform.forward * 5 * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        //if(other.tag == "Player")
        //{
        //    other.transform.GetComponentInParent<Health>().AddHealth(-2);
        //    Destroy(gameObject);
        //}
        //transform.forward = new Vector3(-transform.forward.x, -transform.forward.y, -transform.forward.z);
    }

    public void SplatBlood()
    {
        RaycastHit hit;
        Debug.Log(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity));
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
        {
            if(hit.collider.tag == "Floor")
            {
                Debug.Log("splat");
                Texture2D textureToChange = SplatManager.SplatManagerInstance.BloodMaskTexture;
                
                Vector2 pixelUVfloat = hit.textureCoord;

                pixelUVfloat.x *= textureToChange.width;
                pixelUVfloat.y *= textureToChange.height;
                Vector2Int pixelUV = new Vector2Int((int) pixelUVfloat.x, (int) pixelUVfloat.y);

                Texture2D newTex = textureToChange;
                
                    Vector2Int start = new Vector2Int(pixelUV.x - 125, pixelUV.y - 125);
                    List<Color32> newColors = new List<Color32>();
                    
                    for(int i = 0; i < 250; i++)
                    {
                        for(int j = 0; j < 250; j++)
                        {
                            if(SplatManager.SplatManagerInstance.BloodMaskTexture.GetPixel(start.x + j, start.y + i) != Color.green)
                            {
                                Color splatBrushColor = SplatManager.SplatManagerInstance.SplatPixels[i,j];
                                if(splatBrushColor == Color.white)
                                {
                                    Color color = Color.green;
                                    newColors.Add(color);
                                }
                                else
                                {
                                    newColors.Add(Color.black);
                                }
                            }
                            else
                            {
                                newColors.Add(Color.green);
                            }
                        }
                    }
                    Debug.Log(newColors.Count);
                    newTex.SetPixels32(start.x, start.y, 250, 250, newColors.ToArray());
                    textureToChange.Apply();
            }
        }
    }

    public void SplatSnot(float width)
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
        {
            if(hit.collider.tag == "Floor")
            {
                Texture2D textureToChange = SplatManager.SplatManagerInstance.SnottyMaskTexture;
                
                Vector2 pixelUVfloat = hit.textureCoord;

                pixelUVfloat.x *= textureToChange.width;
                pixelUVfloat.y *= textureToChange.height;
                Vector2Int pixelUV = new Vector2Int((int) pixelUVfloat.x, (int) pixelUVfloat.y);

                Texture2D newTex = textureToChange;
                
                    Vector2Int start = new Vector2Int(pixelUV.x - 125, pixelUV.y - 125);
                    List<Color32> newColors = new List<Color32>();
                    
                    for(int i = 0; i < 250; i++)
                    {
                        for(int j = 0; j < 250; j++)
                        {
                            if(SplatManager.SplatManagerInstance.BloodMaskTexture.GetPixel(start.x + j, start.y + i) != Color.green)
                            {
                                Color splatBrushColor = SplatManager.SplatManagerInstance.SplatPixels[i,j];
                                if(splatBrushColor == Color.white)
                                {
                                    Color color = Color.green;
                                    newColors.Add(color);
                                }
                                else
                                {
                                    newColors.Add(Color.black);
                                }
                            }
                            else
                            {
                                newColors.Add(Color.green);
                            }
                        }
                    }
                    Debug.Log(newColors.Count);
                    newTex.SetPixels32(start.x, start.y, 250, 250, newColors.ToArray());
                    textureToChange.Apply();
            }
        }

    }
}
