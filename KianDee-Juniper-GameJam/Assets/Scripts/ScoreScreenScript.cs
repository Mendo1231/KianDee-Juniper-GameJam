using UnityEngine;
using TMPro;

public class ScoreScreenScript : MonoBehaviour
{
    public GameManager gm;

    public Animator StarL;
    public Animator StarM;
    public Animator StarR;

    public TMP_Text scoreMesh;

    int scoreInt;
    public float scoreMulti;

    public TMP_Text pullMesh;
    public TMP_Text parMesh;

    float timeM = Mathf.Infinity;
    float timeR = Mathf.Infinity;

    public bool endSwing = false;
    bool stopSwing;
    bool animScore=false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(endSwing && !stopSwing){
            gm.startScoring();
            stopSwing = true;
            GetComponent<Animator>().SetTrigger("AnimScore");
        }

        if(Time.time > timeM){
            StarM.SetTrigger("StarAnim");
            timeM = Mathf.Infinity;
        }
        if(Time.time > timeR){ 
            StarR.SetTrigger("StarAnim");
            timeR = Mathf.Infinity;
        }

        if(animScore){
            int scoreStr = Mathf.RoundToInt(scoreInt * scoreMulti);
            scoreMesh.text = scoreStr.ToString();
        }
        
    }

    public void Next(){
        gm.SwingOut();
    }

    public void Retry(){
        gm.Level--;
        gm.SwingOut();
    }

    public void SetScore(int score, int pull, int par){
        scoreInt = score;
        animScore = true;
        
        
        pullMesh.text = pull.ToString();
        parMesh.text = par.ToString();
    }

    public void AnimStars(int i){
        StarL.SetTrigger("StarAnim");
        if(i>1){
            timeM = Time.time + 0.5f;
            if(i>2){
                timeR = Time.time + 1f;
            }
        }
    }
}
