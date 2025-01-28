using UnityEngine;

public class SplatManager : MonoBehaviour
{
    private static SplatManager _instance;
    public static SplatManager SplatManagerInstance {get {return _instance;}}

    //[SerializeField] public Texture2D floorTexture;
    //[SerializeField] Texture2D inactiveFloorTexture;
    //public Texture2D bloodFloorTexture;
    //[SerializeField] Texture2D snotFloorTexture;
    public Texture2D SplatBrushTexture;
    public Color[,] SplatPixels;
    public Texture2D BloodMaskTexture;
    public Texture2D SnottyMaskTexture;
    //public Color[,] floorPixels;
    //public Color[,] bloodFloorPixels;
    //public Color[,] snotFloorPixels;
    //public Color[,] inactiveFloorPixels;


    void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else 
        {
            _instance = this;
        }

        /*floorPixels = new Color[floorTexture.width, floorTexture.height];
        Color[] allFloorPixels = floorTexture.GetPixels();
        int pixelCounter = 0;
        for(int i = 0; i < floorTexture.width; i++) {
            for(int j = 0; j < floorTexture.height; j++){
                floorPixels[i,j] = allFloorPixels[pixelCounter];
                pixelCounter++;
                if(floorPixels[i,j] == Color.red)
                {
                    Debug.Log("red");
                }
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
        inactiveFloorPixels = new Color[inactiveFloorTexture.width, inactiveFloorTexture.height];
        Color[] allInactiveFloorPixels = inactiveFloorTexture.GetPixels();
        pixelCounter = 0;
        for(int i = 0; i < inactiveFloorTexture.width; i++) {
            for(int j = 0; j < inactiveFloorTexture.height; j++){
                inactiveFloorPixels[i,j] = allInactiveFloorPixels[pixelCounter];
                pixelCounter++;
            }
        }*/
        
        SplatPixels = new Color[SplatBrushTexture.width, SplatBrushTexture.height];
        Color[] allSplatPixels = SplatBrushTexture.GetPixels();
        int pixelCounter = 0;
        for(int i = 0; i < SplatBrushTexture.width; i++) {
            for(int j = 0; j < SplatBrushTexture.height; j++){
                SplatPixels[i,j] = allSplatPixels[pixelCounter];
                pixelCounter++;
            }
        }
        
        //BloodMaskPixels = new Color[SplatBrushTexture.width, SplatBrushTexture.height];
        //Color[] allBloodMaskPixels = BloodMaskTexture.GetPixels();
        //pixelCounter = 0;
        //for(int i = 0; i < SplatBrushTexture.width; i++) {
        //    for(int j = 0; j < SplatBrushTexture.height; j++){
        //        SplatPixels[i,j] = allSplatPixels[pixelCounter];
        //        pixelCounter++;
        //    }
        //}
    }

    /*public void RegenerateFloor() 
    {
        floorPixels = new Color[floorTexture.width, floorTexture.height];
        Color[] allFloorPixels = floorTexture.GetPixels();
        int pixelCounter = 0;
        for(int i = 0; i < floorTexture.width; i++) {
            for(int j = 0; j < floorTexture.height; j++){
                floorPixels[i,j] = allFloorPixels[pixelCounter];
                pixelCounter++;
            }
        }
    }*/

    // Update is called once per frame
    void Update()
    {
        
    }
}
