using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    public float minimum = 0.0f;
    public float maximum = 1f;
    public float duration = 10.0f;

    bool faded;

    private float startTime;
    public SpriteRenderer rend;

    void Start()
    {
        startTime = Time.time;
        rend = GetComponent<SpriteRenderer>();
        faded = true;

    }

    IEnumerator Fade()
    {
        float t = (Time.time - startTime) / duration;


        float fadeDurationInSeconds = 3f;
        float timeout = 0.05f;
        float fadeAmount = 1 / (fadeDurationInSeconds / timeout);

        for (float f = fadeAmount; f <= 1; f += fadeAmount)
        {
            Color c = rend.material.color;
            c.a = f;
            rend.material.color = c;
            yield return new WaitForSeconds(timeout);
        }
    }
}