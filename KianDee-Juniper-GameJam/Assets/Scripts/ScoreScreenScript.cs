using UnityEngine;

public class ScoreScreenScript : MonoBehaviour
{
    public GameManager gm;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Next(){
        gm.SwingOut();
    }
}
