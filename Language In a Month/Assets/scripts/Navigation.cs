using UnityEngine;
using System.Collections;

public class Navigation : MonoBehaviour
{

  public GameObject[] screens;
  public GameObject current;
  public GameObject backBtn;

  private GameObject questionTitle;
  private GameObject questionPage;


  void Awake()
  {
    Game.navigation = this;
  }

  // Use this for initialization
  void Start()
  {

  }

  public void ShowLessonsMenu()
  {
    questionTitle = Game.navigation.screens[7];
    questionTitle.SetActive(false);
    backBtn.SetActive(false);
    current.SetActive(false);
    current = screens[1];
    current.SetActive(true);
  }

  public void StartLesson(int index)
  {
    backBtn.SetActive(true);
    current.SetActive(false);
    Game.state.lesson = Game.lessons[index];

    Game.state.screenIndex = -1;
    ShowNextScreen();
    return;
    //Game.state.card = Game.state.screen.cards[0];

    current.SetActive(true);

  }

  public void ShowNextScreen()
  {
    Game.state.screenIndex++;
    Game.state.cardIndex = -1;
    if (Game.state.screenIndex == Game.state.lesson.screens.Count)
    {
      ShowLessonsMenu();
    }
    else
    {
      Game.state.screen = Game.state.lesson.screens[Game.state.screenIndex];
      ShowNextCard();
    }
  }

  public void LoadScreen()
  {

    Game.navigation.screens[0].SetActive(true);
    Game.state.screen.Load();
    Game.navigation.screens[0].SetActive(false);
  }

  public void ShowNextCard()
  {
    Debug.Log("shoNextCard");
    Game.state.cardIndex++;
    if (Game.state.cardIndex < Game.state.screen.cards.Count)
    {
      // show next card
      current.SetActive(false);
      Game.state.card = Game.state.screen.cards[Game.state.cardIndex];
      current = Game.navigation.screens[6];
      current.SetActive(true);
    }
    else
    {
      // show answer page
      PrepareQuestionPage();
      ShowNextQuestion();
    }
  }

  public void ShowNextQuestion()
  {
    Debug.Log("showNextQuestion" + Game.state.questionIndex + "," + Game.state.questions.Length);
    current.SetActive(false);
    Game.state.questionIndex++;
    if (Game.state.questionIndex < Game.state.questions.Length)
    {
      // show next question
      current = questionPage;
      questionPage.SetActive(true);
    }
    else
    {
      // go to lessons menu
      screens[7].SetActive(false);
      ShowNextScreen();
    }
  }

  public void PrepareQuestionPage()
  {
    var cardsCount = Game.state.screen.cards.Count;
    switch (cardsCount)
    {
      case 2:
        questionPage = screens[2];
        Game.state.questions = new int[] { 0, 1 };
        break;
      case 4:
        questionPage = screens[3];
        Game.state.questions = new int[] { 0, 1, 2, 3 };
        break;
      case 6:
        questionPage = screens[4];
        Game.state.questions = new int[] { 0, 1, 2, 3, 4, 5 };
        break;
    }
    Reshuffle(Game.state.questions);
    Game.state.questionIndex = -1;
    screens[7].SetActive(true);
  }

  void Reshuffle(int[] texts)
  {
    // Knuth shuffle algorithm
    for (int t = 0; t < texts.Length; t++)
    {
      int tmp = texts[t];
      int r = Random.Range(t, texts.Length);
      texts[t] = texts[r];
      texts[r] = tmp;
    }
  }

  public void OnBackClick()
  {

    ShowLessonsMenu();
  }
  // Update is called once per frame
  void Update()
  {

  }
}
