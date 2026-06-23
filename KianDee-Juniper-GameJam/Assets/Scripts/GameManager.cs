using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public Animator CameraAnim;
    public GameObject Camera;
    public GameObject ScoreScreen;

    public int Level;    
    public int Score;
    public int Streak;
    public int ShrimpsLeft;
    public int Pulls;
    public int PullPar;
    public bool GoalComplete = true;

    bool waiting=false;
    float targetTime=-1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ShrimpsLeft = GameObject.FindGameObjectsWithTag("Shrimp").Length;
        if(GameObject.FindGameObjectsWithTag("Goal").Length > 0) GoalComplete = false;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        if(waiting){
            if(Time.time > targetTime){
                SceneManager.LoadScene(Level);
                waiting=false;
            }
        }
    }

    public void NewPull(){
        Streak = 0;
        Pulls++;
    }

    public void TargetHit(GameObject gameObject){
        Score += 10 + (Streak * 5);
        Streak++;

        if(gameObject.tag == "Shrimp"){
            ShrimpsLeft--;
            if(ShrimpsLeft == 0 && GoalComplete)RoundComplete();
        }
    }

    public void RoundComplete(){
        if(!waiting){
            if(GameObject.FindGameObjectWithTag("ScoreScreen")!=null){
                ScoreScreen = GameObject.FindGameObjectWithTag("ScoreScreen");
            }
            Debug.Log("Round Complete!");
            Level++;
            if(ScoreScreen!=null){
                ScoreScreen.transform.SetParent(Camera.transform);
                ScoreScreen.GetComponent<Animator>().SetTrigger("SwingIn");
            }
        }
    }
    
    public void GameStart(){
        Level++;
        CameraAnim.SetTrigger("SwingOut");
        targetTime=Time.time + 0.75f;
        waiting=true;
    }

    public void SwingOut(){
        ScoreScreen.GetComponent<Animator>().SetTrigger("SwingOut");
        CameraAnim.SetTrigger("SwingOut");
        targetTime=Time.time + 0.75f;
        waiting=true;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Destroy(ScoreScreen.gameObject);
        CameraAnim.SetTrigger("SwingIn");
        FindShrimps();

        if(GameObject.FindGameObjectWithTag("LevelInfo")!=null){
            Debug.Log("Found it!");
            LevelInfo li = GameObject.FindGameObjectWithTag("LevelInfo").GetComponent<LevelInfo>();
            Camera.transform.position = new Vector3(Camera.transform.position.x,li.CameraY,li.CameraZ);
            PullPar = li.ExpectedPulls;
        }
    }

    void FindShrimps(){
        Debug.Log("Finding Shrimps");
        ShrimpsLeft = GameObject.FindGameObjectsWithTag("Shrimp").Length;
        if(GameObject.FindGameObjectsWithTag("Goal").Length > 0) GoalComplete = false;
    }
}
