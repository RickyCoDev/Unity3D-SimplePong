using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Utility : MonoBehaviour {

    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
