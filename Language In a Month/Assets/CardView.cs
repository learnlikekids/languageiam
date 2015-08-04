using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class CardView : MonoBehaviour
{
  public Text title;
  public Image image;
  private AudioSource audioSource;


  // Use this for initialization
  void Start()
  {
    audioSource = GetComponent<AudioSource>();

  }

  void OnEnable()
  {
    /*
    Game.state.card.mobileLoad();
    title.text = Game.state.card.text;
    image.sprite = Sprite.Create(Game.state.card.imageTexture, new Rect(0, 0, 356, 238), new Vector2());
    // image.sprite = Sprite.Create(Game.state.card.imageTexture, new Rect(0,0, Game.state.card.imageTexture.width, Game.state.card.imageTexture.height), new Vector2());
    StartCoroutine(playSequence());
    */
    StartCoroutine(Game.state.card.Load(() =>
    {
      title.text = Game.state.card.text;
      image.sprite = Sprite.Create(Game.state.card.imageTexture, new Rect(0, 0, 356, 238), new Vector2());
      // image.sprite = Sprite.Create(Game.state.card.imageTexture, new Rect(0,0, Game.state.card.imageTexture.width, Game.state.card.imageTexture.height), new Vector2());
      StartCoroutine(PlaySequence());
    }));


  }

  IEnumerator PlaySequence()
  {
    audioSource.PlayOneShot(Game.state.card.audioClip1);
    yield return new WaitForSeconds(1);
    audioSource.PlayOneShot(Game.state.card.audioClip2);
    yield return new WaitForSeconds(1);
    Game.navigation.ShowNextCard();
  }

  // Update is called once per frame
  void Update()
  {

  }
}
