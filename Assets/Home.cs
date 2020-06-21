using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : Interactable
{

    private bool isIndoors = false;
    public GameObject dimmerCanvas;

    public override void Interact()
    {
        base.Interact();
        enterExitHouse();
    }

    public void enterExitHouse()
    {

        //UnityEngine.Experimental.Rendering.Universal.Light2D globalLight = GameObject.Find("LightManager").GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
        if(!isIndoors)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            GameTime.instance.paused = true;
            //globalLight.intensity = globalLight.intensity / 4;
            dimmerCanvas.SetActive(true);
            isIndoors = true;
        }
        else
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            GameTime.instance.paused = false;
            //globalLight.intensity = globalLight.intensity * 4;
            dimmerCanvas.SetActive(false);

            isIndoors = false;
        }
    }

}
