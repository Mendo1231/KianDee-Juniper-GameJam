using UnityEngine;


public class Breakable : MonoBehaviour
{
    public GameObject partsys;
    

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
            hit(collider.gameObject);
        }
    }

    void hit(GameObject player){
        GameObject partsysclone = Instantiate(partsys, transform.position, transform.rotation);
        partsysclone.transform.eulerAngles = new Vector3(0,0,(player.transform.position.x - partsysclone.transform.position.x)*30);

        Destroy(this.gameObject);
    }
}
