using UnityEngine;


public class Breakable : MonoBehaviour
{
    public GameObject partsys;
    public GameObject impactPrefab;
    

// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            hit(collider);
        }
    }

    void hit(Collider2D collider){
        GameObject partsysclone = Instantiate(partsys, transform.position, transform.rotation);
        partsysclone.transform.eulerAngles = new Vector3(0,0,(collider.gameObject.transform.position.x - partsysclone.transform.position.x)*30);

        collider.gameObject.GetComponent<Animator>().SetTrigger("Hit");
        GameObject impactClone = Instantiate(impactPrefab, GetComponent<CircleCollider2D>().ClosestPoint(collider.gameObject.transform.position), transform.rotation);
        Destroy(this.gameObject);
    }
}
