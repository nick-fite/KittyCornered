using UnityEngine;

public class StayAtTransform : MonoBehaviour
{
    [SerializeField] Transform transformToCopy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = transformToCopy.position;
        gameObject.transform.rotation = transformToCopy.rotation;
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.localRotation = Quaternion.identity;
    }
}
