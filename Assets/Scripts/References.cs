using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GameManager))]
public class References : MonoBehaviour {

    public static GameManager gameManager;
    public static Transform Ball;

    [SerializeField]Transform ball;

	// Use this for initialization
	void OnEnable() {
        gameManager = GetComponent<GameManager>();

        Ball = ball;
	}
	

}
