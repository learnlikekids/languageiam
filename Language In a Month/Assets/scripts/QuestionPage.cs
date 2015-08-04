using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuestionPage : MonoBehaviour {

    public Text title;

    private Image[] images;
    private AudioSource audioSource;

    private Card card;

    // Use this for initialization
    void Awake () {
        images = GetComponentsInChildren<Image>();

        Debug.Log("Images" + images.Length);
        audioSource = gameObject.AddComponent<AudioSource>();
	}

    int indexOf(Image image)
    {
        int i = 0;
        foreach (Image img in images)
        {
            if (image == img) return i;
            i++;
        }
        return -1;
    }

    void OnEnable()
    {
        int i = 0;
        foreach(Image img in images)
        {
            img.sprite = Sprite.Create(Game.state.screen.cards[i].imageTexture, new Rect(0,0,356,238), new Vector2());
//            img.sprite.texture.LoadImage(Game.state.screen.cards[i].imageData);
            i++;
        }
        card =  Game.state.screen.cards[Game.state.questions[Game.state.questionIndex]];
        title.text = card.text;
        StartCoroutine(playSqueunce());
    }

    IEnumerator playSqueunce()
    {
        audioSource.PlayOneShot(card.audioClip1);
        yield return new WaitForSeconds(1);
        audioSource.PlayOneShot(card.audioClip2);
        yield return new WaitForSeconds(1);
    }

    public void onImageClicked(int index)
    {
        Debug.Log("image clicked" + index);
        if(index == Game.state.questions[Game.state.questionIndex]){
            Game.state.correct++;
        }
        else
        {
            Game.state.wrong++;
        }
        Game.navigation.ShowNextQuestion();
        
    }

    // Update is called once per frame
    void Update () {
	
	}
}
