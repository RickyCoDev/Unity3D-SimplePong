using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(References))]
public class GameManager : MonoBehaviour {

    int BlueScore = 0;
    int RedScore = 0;

    public const int MaxScore = 5;

    public delegate void ScoreEventHandler();
    public delegate void WinHandler(string msg);

    public event ScoreEventHandler Ev_RedScored;
    public event ScoreEventHandler Ev_BlueScored;

    public event WinHandler Ev_Win;

    public event ScoreEventHandler Ev_BallReset;

    // Use this for initialization
    void Start()
    {
        Ev_BlueScored += BlueScored;
        Ev_RedScored += RedScored;
    }

    void OnDisable()
    {
        Ev_BlueScored -= BlueScored;
        Ev_RedScored -= RedScored;
    }


    void RedScored()
    {
        RedScore++;
        CheckForWinner();
        Debug.Log("Red Scored");
    }

    void BlueScored()
    {
        BlueScore++;
        CheckForWinner();
        Debug.Log("Blue Scored");
    }

    void CheckForWinner()
    {
        if (BlueScore >= MaxScore)
            Cast_Win("blue");
        if (RedScore >= MaxScore)
            Cast_Win("red");
    }

    public void Cast_Win(string msg)
    {
        if (Ev_Win != null)
            Ev_Win(msg);
        else
            Debug.LogError("No ev win");
    }

    public void Cast_RedScored()
    {
        if (Ev_RedScored != null)
            Ev_RedScored();
        else
            Debug.LogError("No ev redScored");
    }

    public void Cast_BlueScored()
    {
        if (Ev_BlueScored != null)
            Ev_BlueScored();
        else
            Debug.LogError("No ev redScored");
    }

    public void Cast_BallReset()
    {
        if (Ev_BallReset != null)
            Ev_BallReset();
        else Debug.LogError("no ev Ballreset");
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            Cast_BallReset();

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Realoading level");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
