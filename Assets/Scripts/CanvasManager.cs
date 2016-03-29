using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {

    [SerializeField] Text RedScore;
    [SerializeField] Text BlueScore;

    [SerializeField] Text ScoreText;
    [SerializeField] GameObject TutorialWrap;
    [SerializeField] GameObject ButtonsWrap;

    int Bscore = 0;
    int Rscore = 0;
    GameManager gm;

    void Start()
    {
        gm = References.gameManager;
        gm.Ev_BlueScored += UpdateBlueText;
        gm.Ev_RedScored += UpdateRedText;
        gm.Ev_Win += SetWinText;

        StartCoroutine(TimedToggle(3, TutorialWrap));
    }

    void OnDisable()
    {
        gm.Ev_BlueScored -= UpdateBlueText;
        gm.Ev_RedScored -= UpdateRedText;
        gm.Ev_Win -= SetWinText;
    }


    void UpdateRedText()
    {
        Rscore++;

        RedScore.text = Rscore.ToString();
        if (Rscore < GameManager.MaxScore)
        {
            ScoreText.text = "Red scored!";
            ScoreText.color = Color.red;
            ScoreTextAnim();
        }
    }

    void UpdateBlueText()
    {
        Bscore++;

        BlueScore.text = Bscore.ToString();
        if (Bscore < GameManager.MaxScore)
        {
            ScoreText.text = "Blue scored!";
            ScoreText.color = Color.blue;
            ScoreTextAnim();
        }
    }

    void ScoreTextAnim()
    {
        ScoreText.gameObject.SetActive(true);
        StartCoroutine(TimedToggle(1, ScoreText.gameObject));
    }

    IEnumerator TimedToggle(float delay, GameObject target)
    {
        yield return new WaitForSeconds(delay);
        target.SetActive(false);
    }


    void SetWinText(string msg)
    {
        if (msg == "red")
        {
            ScoreText.text = "Red has won the game!";
            ScoreText.color = Color.red;
        }
        if (msg == "blue")
        {
            ScoreText.text = "Blue has won the game!";
            ScoreText.color = Color.blue;
        }
        ScoreText.gameObject.SetActive(true);
        ButtonsWrap.SetActive(true);
    }
}
