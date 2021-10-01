using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Text : MonoBehaviour
{
    public string txt;
    public float delay;

    private TMPro.TextMeshProUGUI tmp;

    void Start()
    {
        tmp = this.GetComponent<TMPro.TextMeshProUGUI>();
        StartCoroutine(AddText());        
    }

    IEnumerator AddText()
    {
        yield return StartCoroutine(StartDelay(delay));
        
        foreach (var letter in txt)
        {
            tmp.text += letter;
            yield return StartCoroutine(Wait(0.1f));
        }
        
    }


    IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    IEnumerator StartDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        //yield return new WaitUntil(() => tmp.text == txt);
    }
    
}
