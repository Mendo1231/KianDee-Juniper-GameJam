using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class Move : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Rigidbody2D rigidbody;
    Camera camera;
    public float thrust = 1;
    Vector2 vel;
    public bool pulling = false;
    LayerMask IgnoreLayerMask;

    List<ContactPoint2D> contacts = new List<ContactPoint2D>();

    void Start()
    {
        camera = Camera.main;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    Vector3 debugstart;
    Vector3 debugend;

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0)) 
        {
            //Debug.Log("pew");
            //Debug.Log(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(Camera.main.ScreenPointToRay(Input.mousePosition), Mathf.Infinity, ~IgnoreLayerMask);
            foreach(RaycastHit2D hit in hits){
                if (hit.collider.gameObject.tag == "Player"){
                    debugstart=hit.point;
                    pulling=true;
                }
            }
        }

        if(pulling==true){
            RaycastHit2D hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
            debugend=hit.point;
            Debug.DrawLine(debugstart, debugend, Color.red);
        }

        if (Input.GetMouseButtonUp(0)) 
        {
            if(pulling==true){
                pulling=false;
                RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(Camera.main.ScreenPointToRay(Input.mousePosition), Mathf.Infinity, ~IgnoreLayerMask);
                foreach(RaycastHit2D hit in hits){
                    if (hit.collider.tag == "clicker"){
                        Vector2 shuntDir = Vector2.Normalize(new Vector2(transform.position.x - hit.point.x, transform.position.y - hit.point.y));
                        Debug.Log(Mathf.Min(Vector2.Distance(transform.position, hit.point), thrust));
                        float shootSpeed = Mathf.Min(Vector2.Distance(transform.position, hit.point), thrust);
                        vel = new Vector2(shuntDir.x * shootSpeed, shuntDir.y * shootSpeed);
                    }
                }
            }
        }

        transform.position = new Vector3(transform.position.x + vel.x * Time.deltaTime, transform.position.y + vel.y * Time.deltaTime, transform.position.z);

        
        if (contacts.Count > 0) HandleCollision();
    }

    void FixedUpdate(){
        
    }

    void HandleCollision(){
        Vector2 combinedNormal = Vector2.zero;

        foreach(ContactPoint2D contact in contacts){
            combinedNormal += contact.normal;
        }

        combinedNormal = Vector2.Normalize(combinedNormal);

        vel = Vector2.Reflect(vel, combinedNormal);

        contacts = new List<ContactPoint2D>();
    }

    void OnCollisionEnter2D(Collision2D collision) 
    {
        contacts.Add(collision.contacts[0]);
    }
}
