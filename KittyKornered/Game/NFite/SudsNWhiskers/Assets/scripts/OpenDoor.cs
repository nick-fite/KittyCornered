using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    Transform openPos;
    Transform closePos;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SlideDoor()
    {
        
    }

    void OnTriggerEnter()
    {
        StartCoroutine(SlideOpenCoroutine());
    }

    void OnTriggerExit()
    {
        StartCoroutine(SlideCloseCoroutine());
    }

    IEnumerator SlideCloseCoroutine()
    {
        float rate = Vector3.Distance(this.transform.position, closePos.position) * 10;
        float t = 0.0f;
        while(t < 1.0f)
        {
            this.transform.position = Vector3.Lerp(transform.position, closePos.position, t);
            yield return null;
        }
        yield return null;
    }
    IEnumerator SlideOpenCoroutine()
    {
        float rate = Vector3.Distance(this.transform.position, openPos.position) * 10;
        float t = 0.0f;
        while(t < 1.0f)
        {
            this.transform.position = Vector3.Lerp(transform.position, openPos.position, t);
            yield return null;
        }
        yield return null;
    }
}
