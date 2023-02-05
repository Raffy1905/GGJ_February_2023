using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeManager : MonoBehaviour
{
    public static ThemeManager instance;

    private int currentTheme = 0;
    private int nextTheme = 1;
    private bool fadeBusy = false;

    [SerializeField] private AudioSource[] themes;
    public void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void Start()
    {
        StopAllCoroutines();
        StartCoroutine(FadeIn());
    }

    public void NextTheme()
    {
        if (fadeBusy) return;
        StopAllCoroutines();
        StartCoroutine(FadeToNext());
    }

    public void ThemeID(int index)
    {
        if (fadeBusy) return;
        if (index == currentTheme)
        {
            //Debug.Log("Theme " + currentTheme + " is already Playing!");
            return;
        }
        Debug.Log("Switching to Theme " + index);
        StopAllCoroutines();
        StartCoroutine(FadeTo(index));
    }

    private IEnumerator FadeToNext()
    {
        fadeBusy = true;
        float timeToFade = 3.0f;
        float timeElapsed = 0;

        while (timeElapsed < timeToFade)
        {
            themes[currentTheme].volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);
            //Debug.Log(themes[currentTheme].volume);
            themes[nextTheme].volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
            //Debug.Log(themes[nextTheme].volume);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        currentTheme = nextTheme;
        nextTheme++;
        if (nextTheme >= themes.Length)
        {
            nextTheme = 0;
        }
        fadeBusy = false;
    }

    private IEnumerator FadeIn() 
    {
        fadeBusy = true;
        float timeToFade = 1.5f;
        float timeElapsed = 0;

        while (timeElapsed < timeToFade)
        {
            themes[currentTheme].volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
            //Debug.Log(themes[currentTheme].volume);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        fadeBusy = false;
    }

    private IEnumerator FadeTo(int next)
    {
        if (next < themes.Length)
        {
            fadeBusy = true;
            float timeToFade = 3.0f;
            float timeElapsed = 0;

            while (timeElapsed < timeToFade)
            {
                themes[currentTheme].volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);
                //Debug.Log(themes[currentTheme].volume);
                themes[next].volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
                //Debug.Log(themes[next].volume);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            currentTheme = next;
            nextTheme = next + 1;
            if (nextTheme >= themes.Length)
            {
                nextTheme = 0;
            }
            fadeBusy = false;
        } else
        {
            Debug.LogError("No Theme found for index " + next);
        }
    }
}
