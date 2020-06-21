using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class CinematicBars : MonoBehaviour
{

    private RectTransform topBar, bottomBar;
    private float changeSizeAmount, targetSize;
    private bool isActive;

    // Start is called before the first frame update
    void Awake()
    {
        GameObject gameObject = new GameObject("topBar", typeof(Image));
        gameObject.transform.SetParent(transform, false);
        gameObject.GetComponent<Image>().color = Color.black;
        topBar = gameObject.GetComponent<RectTransform>();
        topBar.anchorMin = new Vector2(0,1);
        topBar.anchorMax = new Vector2(1,1);
        topBar.sizeDelta = new Vector2(0,0);

        gameObject = new GameObject("bottomBar", typeof(Image));
        gameObject.transform.SetParent(transform, false);
        gameObject.GetComponent<Image>().color = Color.black;
        bottomBar = gameObject.GetComponent<RectTransform>();
        bottomBar.anchorMin = new Vector2(0,0);
        bottomBar.anchorMax = new Vector2(1,0);
        bottomBar.sizeDelta = new Vector2(0,0);
    }

    void Update() {
        if(isActive) {
            Vector2 sizeDelta = topBar.sizeDelta;
            sizeDelta.y += changeSizeAmount * Time.deltaTime;
            if(changeSizeAmount > 0) {
                if(sizeDelta.y >= targetSize) {
                    sizeDelta.y = targetSize;
                    isActive = false;
                }
            } else {
                if(sizeDelta.y <= targetSize) {
                    sizeDelta.y = targetSize;
                    isActive = false;
                }
            }
            topBar.sizeDelta = sizeDelta;
            bottomBar.sizeDelta = sizeDelta;
        }
    }

    public void Show(float targetSize, float time)
    {
        this.targetSize = targetSize;
        changeSizeAmount = (targetSize - topBar.sizeDelta.y)/time;
        isActive = true;
    }

    public void Hide(float time) {
        targetSize = 0f;
        changeSizeAmount = (targetSize - topBar.sizeDelta.y) /time;
        isActive = true;
    }

    IEnumerator ZoomIn() {

        for (float f = 5f; f >= 4f; f -= 0.05f)
        {
            GameObject.Find("VCAM").GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = f;
            yield return null;
        }
    }

    IEnumerator ZoomOut() {

        for (float f = 4f; f <= 5f; f += 0.05f)
        {
            GameObject.Find("VCAM").GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = f;
            yield return null;
        }
    }
}


