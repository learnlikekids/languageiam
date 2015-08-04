using UnityEngine;


public class State : MonoBehaviour
{
    public Lesson lesson;
    public Screen screen;
    public Card card;
    public int cardIndex = -1;
    public int answer = 0;
    public int screenIndex = 0;
    public int correct = 0;
    public int wrong = 0;

    public int[] questions;
    public int questionIndex = 0;


    void Awake()
    {
        Game.state = this;
    }
}

