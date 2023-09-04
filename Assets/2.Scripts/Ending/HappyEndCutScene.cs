using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HappyEndCutScene : MonoBehaviour
{
    public float sceneTime = 2.0f;
    public List<Sprite> sprites = new List<Sprite>();
    private Image image;
    private void Start()
    {
        image = gameObject.GetComponent<Image>();
        StartCoroutine(HappyEnd());

    }

    IEnumerator HappyEnd()
    {
        foreach(var sprite in sprites)
        {
            float timer = 0.0f;
            image.sprite = sprite;
            transform.localScale = new Vector3(1f, 1f, 1f);
            if (sprite == sprites[sprites.Count - 1])
            {
                sceneTime = 3.0f;
            }
            while (timer <= sceneTime)
            {
                transform.localScale += transform.localScale* 0.00015f;
                timer += Time.deltaTime;
                if (sprite == sprites[sprites.Count - 1])
                {
                    image.color = new Color(1.0f, 1.0f, 1.0f, 5.0f - transform.localScale.x*4f);
                }
                yield return null;
            }
        }        
        SceneManager.LoadScene("Ending");
    }
}
