using Unity.VisualScripting;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] float _growRate = 1f;
    public float Scale; 
    void Update()
    {
        
        transform.localScale += Vector3.one * _growRate * Time.deltaTime;
        Scale = Vector3.Magnitude(transform.localScale);
    }

    void CombineSale(Bubble otherBubble)
    {
        transform.localScale += otherBubble.transform.localScale * .5f;
        Scale = Vector3.Magnitude(transform.localScale);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("THERES SOMETHING IN ME");
        Bubble otherBubble =  other.gameObject.GetComponent<Bubble>();
        if(otherBubble)
        {
            if(otherBubble.Scale > Scale)
            {
                otherBubble.CombineSale(this);
                Destroy(gameObject);
            }
            else
            {
                CombineSale(otherBubble);
                Destroy(otherBubble.gameObject);
            }
        }
    }
}
