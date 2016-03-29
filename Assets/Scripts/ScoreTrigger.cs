using UnityEngine;
using System.Collections;

public class ScoreTrigger : MonoBehaviour {

    enum TargetScorer { red, blue};

    [SerializeField] TargetScorer targetScorer;


    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.transform.tag == "Ball")
        {
            switch (targetScorer)
            {
                case TargetScorer.blue:
                    References.gameManager.Cast_BlueScored(); break;
                case TargetScorer.red:
                    References.gameManager.Cast_RedScored(); break;
            }
        }
            
    }
}
