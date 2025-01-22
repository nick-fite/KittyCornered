using UnityEngine;

public class SplatManager : MonoBehaviour
{
    private static SplatManager _instance;
    public static SplatManager SplatManagerInstance {get {return _instance;}}

    [SerializeField] Texture2D floorTexture;
    [SerializeField] Texture2D bloodFloorTexture;
    [SerializeField] Texture2D snotFloorTexture;
    public Color[,] floorPixels;
    public Color[,] bloodFloorPixels;
    public Color[,] snotFloorPixels;

    void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else 
        {
            _instance = this;
        }
   
        floorPixels = new Color[floorTexture.width, floorTexture.height];
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
        }
        
        snotFloorPixels = new Color[snotFloorTexture.width, snotFloorTexture.height];
        Color[] allSnotFloorPixels = snotFloorTexture.GetPixels();
        pixelCounter = 0;
        for(int i = 0; i < snotFloorTexture.width; i++) {
            for(int j = 0; j < snotFloorTexture.height; j++){
                snotFloorPixels[i,j] = allSnotFloorPixels[pixelCounter];
                pixelCounter++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
