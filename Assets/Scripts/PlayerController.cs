using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public static int score;
    [SerializeField] GameObject BulletPrefab;
    public static int  Health = 100;
    [SerializeField] bool isground = true;
    
    public Transform spawnpoint;
    private Vector3 playermovementInput;
    private Vector2 CameraInput;
    public Transform playercam;
    Rigidbody rb;
   private float XROT;
    public float speed=600;
    private float senstivity= 3;
    [SerializeField] float jumpforce;
    private float pausemovment = 3.0f; 
    private Animator Play_anim;
    private float rotate = 400;
   

  
    //UiMangment ui ; 
   
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Play_anim =  GetComponent<Animator>();
        
    
    }

    
    // Update is called once per frame
    void Update()
    {
        
        playermovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        CameraInput = new Vector3 (Input.GetAxis("Mouse X"),Input.GetAxis("Mouse Y"));
       
            playerMove();
         
       // MoveCamera();
        if(Input.GetKeyDown(KeyCode.Space) && isground)
        {
            rb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
            isground=false;
        } 
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Instantiate(BulletPrefab,spawnpoint.transform.position,BulletPrefab.transform.rotation);
        }
        if(Input.GetKey(KeyCode.Alpha0))
        {
            Play_anim.SetInteger("Animation_int", 5);
        }
    }
    private void playerMove()
    {
        Vector3 MoveVector =  transform.TransformDirection(playermovementInput)*speed;
        rb.velocity  =  new Vector3(MoveVector.x,rb.velocity.y,MoveVector.z); 
        Play_anim.SetFloat("Speed_f",playermovementInput.x);
        Play_anim.SetFloat("Speed_f",playermovementInput.z);
        transform.Rotate(Vector3.up*playermovementInput.x*playermovementInput.z*Time.deltaTime*rotate);
        if(playermovementInput.x < 0 ||playermovementInput.z < 0 )
        {
            Play_anim.SetFloat("Speed_f", -playermovementInput.x);
            Play_anim.SetFloat("Speed_f",playermovementInput.z);
        }
    }
    void MoveCamera()
    {
        XROT -= CameraInput.y *senstivity;

        transform.Rotate(0f, CameraInput.x * senstivity, 0f);
        playercam.transform.localRotation = Quaternion.Euler(XROT,0f,0f);

    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("ground") || other.gameObject.CompareTag("Enemy") )
        {
            isground= true;
        }
        if(other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Health--;
        }
        
    }
}