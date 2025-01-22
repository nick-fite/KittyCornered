using UnityEngine;

public class Rat : Health
{
    SplatScript splat;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        splat = GetComponent<SplatScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Death()
    {
        base.Death();
        splat.SplatBlood();
        Destroy(gameObject);
    }
}
