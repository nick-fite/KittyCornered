using Unity.VisualScripting;
using UnityEngine;

public class Bubble : Health
{
    [SerializeField] float _growRate = 1f;
    public float Scale; 
    public float Radius;
    SphereCollider spCollider;
    SplatScript splat;
    public bool good = false;

    void Start()
    {
        spCollider = GetComponent<SphereCollider>();
        splat = GetComponent<SplatScript>();
    }

    public override void Death()
    {
        base.Death();
        if (!good)
        { 
            splat.SplatSnot(Radius);    
        }
        AudioManager.AudioInstance.PlayBubblePop();
        GameManager.GameManagerInstance.RemoveObject(gameObject);
        Destroy(gameObject);
    }

    void Update()
    {
        if (!good)
        {

            transform.position += transform.forward * 5 * Time.deltaTime;
        }
        else
        {
            
            transform.position += transform.forward * 2 * Time.deltaTime;
        }
        //transform.localScale += Vector3.one * _growRate * Time.deltaTime;
        //Radius = transform.localScale.y;
        //Scale = Vector3.Magnitude(transform.localScale);
    }

    void OnTriggerEnter(Collider other)
    {
        transform.forward = new Vector3(-transform.forward.x, -transform.forward.y, -transform.forward.z);
        if (other.tag == "Player" && !good)
        {
            other.transform.GetComponentInParent<Health>().AddHealth(-2);
            Destroy(gameObject);
        }
        if (good)
        {
            Destroy(gameObject);
        }

    }

    void CombineSale(Bubble otherBubble)
    {
        //transform.localScale += otherBubble.transform.localScale * .5f;
        //Scale = Vector3.Magnitude(transform.localScale);
    }

    /*private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("THERES SOMETHING IN ME");
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
    }*/
}
