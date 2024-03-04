using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTooltip : MonoBehaviour
{
    private RectTransform rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        StartCoroutine(HideTutorial());
    }

    private IEnumerator HideTutorial()
    {
        //print("hide ");
        yield return new WaitForSeconds(3f);

        while (rectTransform.anchoredPosition.y < 150)
        {
            rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, rectTransform.anchoredPosition + new Vector2(0, 500), 5f);
            yield return new WaitForSeconds(0.01f);
        }
        //print("hidden");
    }
}
