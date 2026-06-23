using UnityEngine;
using System.Collections.Generic;

public class Move : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Rigidbody2D rigidbody;
    Camera camera;
    public float thrust;
    Vector2 vel;
    public bool pulling = false;
    LayerMask IgnoreLayerMask;

    public float drag=0.1f;

    List<ContactPoint2D> contacts = new List<ContactPoint2D>();

    Vector2 pullstart;
    Vector3 debugend;

    public float stopThresh=0.5f;
    public float timeScale=1f;
    float modifiedTime;

    public Animator anim;
    public GameObject knockPrefab;
    GameManager gm;

    void Start()
    {
        camera = Camera.main;
        rigidbody = GetComponent<Rigidbody2D>();
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

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
                    pullstart=hit.point;
                    pulling=true;
                }
            }
        }

        if(pulling==true){
            RaycastHit2D hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
            debugend=hit.point;
            Debug.DrawLine(pullstart, debugend, Color.red);
        }

        if (Input.GetMouseButtonUp(0)) 
        {
            if(pulling==true){
                pulling=false;
                RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(Camera.main.ScreenPointToRay(Input.mousePosition), Mathf.Infinity, ~IgnoreLayerMask);
                foreach(RaycastHit2D hit in hits){
                    if (hit.collider.tag == "clicker"){
                        gm.NewPull();
                        Vector2 shuntDir = Vector2.Normalize(new Vector2(pullstart.x - hit.point.x, pullstart.y - hit.point.y));
                        float shootSpeed = Mathf.Min(Vector2.Distance(pullstart, hit.point)*2, thrust);
                        vel = new Vector2(shuntDir.x * shootSpeed, shuntDir.y * shootSpeed);
                    }
                }
            }
        }

        modifiedTime = Time.deltaTime * timeScale;
        transform.position = new Vector3(transform.position.x + vel.x * modifiedTime, transform.position.y + vel.y * modifiedTime, transform.position.z);
        
        if (contacts.Count > 0) HandleCollision();
    }

    void FixedUpdate(){
        if(vel != Vector2.zero){
            if(Mathf.Abs(vel.y) + Mathf.Abs(vel.x) > 0.5f + drag ){
                vel.x = vel.x * (1-drag);
                vel.y = vel.y * (1-drag);
            }else{
                vel = Vector2.zero;
            }
        }

        anim.SetFloat("SpinMulti", (Mathf.Abs(vel.y) + Mathf.Abs(vel.x)) / thrust);
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

        GameObject knockClone = Instantiate(knockPrefab, collision.contacts[0].point, transform.rotation);
        if(knockClone.GetComponent<AudioSource>() != null) knockClone.GetComponent<AudioSource>().pitch = Random.Range(0.5f, 1.5f);
    }
}
