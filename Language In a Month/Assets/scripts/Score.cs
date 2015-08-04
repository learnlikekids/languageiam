using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public Text correctTxt;
    public Text wrongTxt;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        correctTxt.text = "Correct: " + Game.state.correct;
        wrongTxt.text = "Wrong: " + Game.state.wrong;
	}
}
