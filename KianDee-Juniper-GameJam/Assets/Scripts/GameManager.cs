using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int Level;    
    public int Score;
    public int Streak;
    public int ShrimpsLeft;
    public int Pulls;
    public bool GoalComplete = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ShrimpsLeft = GameObject.FindGameObjectsWithTag("Shrimp").Length;
        if(GameObject.FindGameObjectsWithTag("Goal").Length > 0) GoalComplete = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewPull(){
        Streak = 0;
        Pulls++;
    }

    public void TargetHit(GameObject gameObject){
        Score += 10 + (Streak * 5);
        Streak++;

        if(gameObject.tag == "Shrimp")ShrimpsLeft--;
        if(ShrimpsLeft == 0 && GoalComplete)RoundComplete();
    }

    void RoundComplete(){
        Debug.Log("Round Complete!");
    }
}
