using UnityEngine;

public class ShrimpMove : MonoBehaviour
{
    public float shrimpProgress;
    public float speed;

    public GameObject shrimpObj;
    public Transform relay1;
    public Transform relay2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Animator>().SetFloat("speedMulti", speed);
    }

    // Update is called once per frame
    void Update()
    {
        if(shrimpObj!=null){
            shrimpObj.transform.position = Vector3.Lerp(relay1.position,relay2.position,shrimpProgress);
        }else Destroy(this.gameObject);
        
    }
}
