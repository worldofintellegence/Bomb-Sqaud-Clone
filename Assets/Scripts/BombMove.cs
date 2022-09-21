using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombMove : MonoBehaviour
{
    [SerializeField] GameObject BulletPrefab;
    Rigidbody rb;
     public GameObject Explosion;
    public float radius = 300,  power = 100, upforce = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Invoke("explosion",2);
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(new Vector3(0,0,100));
    }
    void explosion()
    {
        Instantiate(Explosion,BulletPrefab.transform.position,transform.rotation);
        Vector3 explsionpostion = BulletPrefab.transform.position;
        Collider [] colliders = Physics.OverlapSphere(explsionpostion,radius);
        foreach (Collider collide in colliders)
        {
            rb  = collide.GetComponent<Rigidbody>();
            if(rb!=null)
            {
                rb.AddExplosionForce(power, explsionpostion,radius,upforce,ForceMode.Impulse);
                Destroy(gameObject);
            }
            
        }
        Destroy(gameObject);

    }
}
