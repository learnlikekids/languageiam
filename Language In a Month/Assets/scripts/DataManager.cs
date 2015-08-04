using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Xml;
using System;

public class Game
{
  // managers
  public static State state;
  public static DataManager data;
  public static Navigation navigation;

  public static List<Lesson> lessons = new List<Lesson>();
}

public class Card
{
  public string text;
  public string translit;
  public string translation;
  public string image;
  public string audio1;
  public string audio2;

  public Texture2D imageTexture = new Texture2D(356, 238);
  public byte[] imageData;
  public AudioClip audioClip1;
  public AudioClip audioClip2;

#if UNITY_ANDROID
    public IEnumerator load(Action onLoaded)
    {
        yield return null;
        imageTexture = Resources.Load<Texture2D>(image.Replace(".jpg",""));
        audioClip1 = Resources.Load<AudioClip>(audio1.Replace(".ogg", ""));
        audioClip2 = Resources.Load<AudioClip>(audio2.Replace(".ogg", ""));
        yield return null;
        onLoaded();

    }
#else
  public IEnumerator Load(Action onLoaded)
  {
    //Debug.Log(Application.dataPath +","+image + audio1+ audio2);
    var www = new WWW("file://" + Application.dataPath + "/data/" + image);
    yield return www;
    imageData = www.bytes;
    www.LoadImageIntoTexture(imageTexture);

    var wwwAudio1 = new WWW("file://" + Application.dataPath + "/data/" + audio1);
    yield return wwwAudio1;
    audioClip1 = wwwAudio1.audioClip;

    var wwwAudio2 = new WWW("file://" + Application.dataPath + "/data/" + audio2);
    yield return wwwAudio2;
    audioClip2 = wwwAudio2.audioClip;
    onLoaded();
  }
#endif
}

public class Screen
{
  public int id;
  public List<Card> cards = new List<Card>();

  public void Load()
  {
    foreach (var card in cards)
    {
      //card.Load();
    }
  }
}

public class Lesson
{
  public int id;
  public List<Screen> screens = new List<Screen>();

  public static Lesson FromXml(XmlNode xml)
  {
    var lesson = new Lesson();
    lesson.id = int.Parse(xml.Attributes["id"].Value);
    for (var i = 0; i < xml.ChildNodes.Count; i++)
    {
      var screenNode = xml.ChildNodes.Item(i);
      var screen = new Screen();
      screen.id = int.Parse(screenNode.Attributes["id"].Value);
      for (var j = 0; j < screenNode.ChildNodes.Count; j++)
      {
        var cardNode = screenNode.ChildNodes.Item(j);
        var card = new Card();
        card.text = cardNode.Attributes["text"].Value;
        card.translit = cardNode.Attributes["translit"].Value;
        card.translation = cardNode.Attributes["translation"].Value;
        card.image = cardNode.Attributes["image"].Value;
        card.audio1 = cardNode.Attributes["audio1"].Value;
        card.audio2 = cardNode.Attributes["audio2"].Value;
        screen.cards.Add(card);
      }
      lesson.screens.Add(screen);
    }

    return lesson;
  }
}

public class DataManager : MonoBehaviour
{
  void Awake()
  {
    Game.data = this;
  }

  // Use this for initialization
  void Start()
  {
    ParseLessonsData();
  }

  private void ParseLessonsData()
  {
    XmlDocument xmlDoc = new XmlDocument();
#if UNITY_ANDROID
        xmlDoc.LoadXml(Resources.Load<TextAsset>("course").text);

#else
    xmlDoc.Load(Application.dataPath + "/data/course.xml");
#endif
    XmlNodeList lessonsNodes = xmlDoc.DocumentElement.SelectNodes("/Course/Lesson");

    Debug.Log(lessonsNodes.Count);

    for (int i = 0; i < lessonsNodes.Count; i++)
    {
      var lesson = Lesson.FromXml(lessonsNodes.Item(i));
      Game.lessons.Add(lesson);

    }
    Game.navigation.ShowLessonsMenu();
  }

  // Update is called once per frame
  void Update()
  {

  }
}
