using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class BlackBars : MonoBehaviour
{
    Animator anim;
    public float duration = 1.0f;
    private float elapsed = 0.0f;
    private bool transition = false;


    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    public void MoveBarsIn() {
        
        anim.SetBool("isHidden", false);
        //GameObject.Find("VCAM").GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 4;
        StartCoroutine("ZoomIn");

    }

    IEnumerator ZoomIn() {

        for (float f = 5f; f >= 4f; f -= 0.05f)
        {
            Debug.Log(f);
            GameObject.Find("VCAM").GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = f;
            yield return null;
        }
    }

    IEnumerator ZoomOut() {

        for (float f = 4f; f <= 5f; f += 0.05f)
        {
            Debug.Log(f);
            GameObject.Find("VCAM").GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = f;
            yield return null;
        }
    }

    public void MoveBarsOut() {
        anim.SetBool("isHidden", true);
    }
}
