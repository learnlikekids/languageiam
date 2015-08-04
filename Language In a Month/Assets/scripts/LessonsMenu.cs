using UnityEngine;
using UnityEngine.UI;

using System.Collections.Generic;

public class LessonsMenu : MonoBehaviour
{
  public GameObject menuItemPrefab;
  private float height = 70;
  private float margin = 5;

  private List<GameObject> items = new List<GameObject>();

  private bool ready = false;
  // Use this for initialization
  void OnEnable()
  {
    if (!ready)
    {
      int i = 0;
      foreach (Lesson lesson in Game.lessons)
      {
        var button = Instantiate(menuItemPrefab);
        button.SetActive(true);

        items.Add(button);
        button.GetComponentInChildren<Button>().onClick.AddListener(() =>
        {
          Game.navigation.StartLesson(items.IndexOf(button));
        });
        button.transform.SetParent(transform);
        var rectTransform = button.GetComponent<RectTransform>();
        rectTransform.offsetMax = new Vector2(0, -i * (height + margin));
        rectTransform.offsetMin = new Vector2(0, -i * (height + margin) - height);
        button.GetComponentInChildren<Text>().text = "Lesson " + i;
        i++;
        Debug.Log("Lesson");
      }
      ready = true;
    }

  }

  // Update is called once per frame
  void Update()
  {

  }
}
