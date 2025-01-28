using UnityEngine;

public class letLeave : MonoBehaviour
{
    [SerializeField] int Scene;
    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement mv = other.GetComponent<PlayerMovement>();
        if (mv)
        {
            mv.scene = Scene;
            mv.atDoor = true;    
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerMovement mv = other.GetComponent<PlayerMovement>();
        if (mv)
        {
            mv.atDoor = true;
        }
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
