using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void OnClickStart(){
        SceneManager.LoadScene(1);
    }

    public void OnClickHowTo()
    {
        SceneManager.LoadScene(5);
    }

    public void OnBack()
    {
        SceneManager.LoadScene(0);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
