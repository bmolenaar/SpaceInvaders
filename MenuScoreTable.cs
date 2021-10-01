using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScoreTable : MonoBehaviour
{

    public TMPro.TextMeshProUGUI tmp;
    public TMPro.TextMeshProUGUI flashingText;

    private string txt;
    private bool flash;

    void Start()
    {
        StartCoroutine(Display());
        InvokeRepeating(nameof(FlashText), 1, 1);
    }

    void Update()
    {
        txt = tmp.text;
    }

    void FlashText()
    {
        if (!flash)
        {
            flashingText.enabled = false;
            flash = true;
        }
        else
        {
            flashingText.enabled = true;
            flash = false;
        }
    }

    IEnumerator Display()
    {
        yield return new WaitUntil(() => txt == "Space Invaders");
        yield return new WaitForSeconds(0.3f);
        this.GetComponent<Canvas>().enabled = true;
    }
}
