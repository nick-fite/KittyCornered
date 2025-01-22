using Unity.VisualScripting;
using UnityEngine;

public class Bubble : Health
{
    [SerializeField] float _growRate = 1f;
    public float Scale; 
    SphereCollider spCollider;
    SplatScript splat;

    void Start()
    {
        spCollider = GetComponent<SphereCollider>();
        splat = GetComponent<SplatScript>();
    }

    public override void Death()
    {
        base.Death();
        splat.SplatSnot((int) Scale);        
        Destroy(gameObject);
    }

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
