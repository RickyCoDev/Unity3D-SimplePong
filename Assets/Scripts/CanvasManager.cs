using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {

    [SerializeField] Text RedScore;
    [SerializeField] Text BlueScore;

    [SerializeField] Text ScoreText;

    int Bscore = 0;
    int Rscore = 0;
    GameManager gm;

    void Start()
    {
        gm = References.gameManager;
        gm.Ev_BlueScored += UpdateBlueText;
        gm.Ev_RedScored += UpdateRedText;
        gm.Ev_BlueScored += ScoreTextAnim;
        gm.Ev_RedScored += ScoreTextAnim;
    }

    void OnDisable()
    {
        gm.Ev_BlueScored -= UpdateBlueText;
        gm.Ev_RedScored -= UpdateRedText;
        gm.Ev_BlueScored -= ScoreTextAnim;
        gm.Ev_RedScored -= ScoreTextAnim;
    }


    void UpdateRedText()
    {
        Rscore++;
        RedScore.text = Rscore.ToString();

        ScoreText.text = "Red scored!";
        ScoreText.color = Color.red;
    }

    void UpdateBlueText()
    {
        Bscore++;
        BlueScore.text = Bscore.ToString();

        ScoreText.text = "Blue scored!";
        ScoreText.color = Color.blue;
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

}
