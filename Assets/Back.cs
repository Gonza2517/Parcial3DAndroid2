using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Back : MonoBehaviour
{
    private bool isPaused = false;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (IsTouchingRawImage(touch.position))
                {
                    SceneManager.LoadScene("Menu");
                }
            }
        }
    }

    bool IsTouchingRawImage(Vector2 touchPosition)
    {
        RectTransform rawImageRectTransform = GetComponent<RectTransform>();
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rawImageRectTransform, touchPosition, null, out localPoint);

        Rect rawImageRect = new Rect(Vector2.zero, rawImageRectTransform.rect.size);
        return rawImageRect.Contains(localPoint);
    }
}