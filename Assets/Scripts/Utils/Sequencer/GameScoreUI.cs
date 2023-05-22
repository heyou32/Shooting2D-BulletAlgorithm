using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameScoreUI : MonoBehaviour
{
    public RectTransform background;
    public Text score;
    public float speed;
    const float y = 2280;
    public Text message;
    public void GameEndShow()
    {
        if (background && score)
            StartCoroutine(IEGameEndShow());
    }
    IEnumerator IEGameEndShow()
    {
        Vector3 pos = transform.position;
        Vector2 size = background.sizeDelta;
        while (true)
        {
            float time = Time.deltaTime * speed * 100;
            pos.y -= time;
            size.y += time;

            transform.position = pos;
            background.sizeDelta = size;
            score.fontSize += (int)(time * 0.1);

            yield return null;
            if (pos.y < y * 0.5f) break;
        }
        pos.y = y * 0.5f;
        transform.position = pos;
        message.enabled = true;

    }
}
