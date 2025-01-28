using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager GameManagerInstance { get { return _instance; } }

    [SerializeField]public GameObject winText;

    [SerializeField]List<GameObject> objsThatNeedToBeDestroyed = new List<GameObject>();
    bool CleanUp = false;
    public int dirtAmount = 0;

    private void Awake()
    {
        winText.SetActive(false);
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void RemoveObject(GameObject obj)
    {
        objsThatNeedToBeDestroyed.Remove(obj);
        if (objsThatNeedToBeDestroyed.Count < 1)
        {
            CleanUp = true;
            Debug.Log("doing this");
            UpdateDirt();
        }
        Debug.Log("removing");
    }

    public void AddObject(GameObject obj)
    {
        objsThatNeedToBeDestroyed.Add(obj);
    }

    public void UpdateDirt()
    {
        for (int i = 0; i < SplatManager.SplatManagerInstance.BloodMaskTexture.height; i++)
        {
            for (int j = 0; j < SplatManager.SplatManagerInstance.BloodMaskTexture.width; j++)
            {
                if (SplatManager.SplatManagerInstance.BloodMaskTexture.GetPixel(i,j) == Color.green) {
                    dirtAmount++;
                }

            }
        }

        for (int i = 0; i < SplatManager.SplatManagerInstance.SnottyMaskTexture.height; i++)
        {
            for (int j = 0; j < SplatManager.SplatManagerInstance.SnottyMaskTexture.width; j++)
            {
                if (SplatManager.SplatManagerInstance.SnottyMaskTexture.GetPixel(i, j) == Color.green)
                {
                    dirtAmount++;
                }
            }
        }

    }

    void End()
    {
        winText.SetActive(true);
        Debug.Log("end");
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().allowedToLeave = true;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (CleanUp)
        {
            if (dirtAmount < 0)
            {
                CleanUp = false;
                End();
            }
            //UpdateDirt();
        }   
    }
}
