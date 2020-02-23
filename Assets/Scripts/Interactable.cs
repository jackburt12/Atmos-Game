using System.Collections;
using UnityEngine;
using UnityEngine.AI;

/*	
	This component is for all objects that the player can
	interact with such as enemies, items etc. It is meant
	to be used as a base class.
*/

//[RequireComponent(typeof(ColorOnHover))]
public class Interactable : MonoBehaviour
{

    private GameTime gameTime;

    public float radius = 1f;

    bool isFocus = false;   // Is this interactable currently being focused?
    private Transform player;       // Reference to the player transform

    bool withinDistance = false;
    bool hasInteracted = false; // Have we already interacted with the object?

    private GameObject interactPrefab;
    private GameObject interactPopup;

    public virtual void Start()
    {
        gameTime = GameObject.Find("GameManager").GetComponent<GameTime>();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        interactPrefab = Resources.Load("Prefabs/UI/InteractDialog") as GameObject;

        Vector2 whereToInstantiate = new Vector2(transform.position.x, transform.position.y + 1.5f);
        interactPopup = Instantiate(interactPrefab, transform);
        interactPopup.transform.position = whereToInstantiate;
        interactPopup.GetComponent<CanvasGroup>().alpha = 0f;
    }

    public virtual void Update() {
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance <= radius)
        {
            if(!withinDistance){
                withinDistance = true;
                StartCoroutine("InteractPromptFadeIn");
            }
        } else
        {
            if(withinDistance)
            {
                withinDistance = false;
                StartCoroutine("InteractPromptFadeOut");
            }
        }

        if(withinDistance) {

            if (Input.GetButtonDown("Interact"))
            {
                Debug.Log("Interact");
                //hasInteracted = true;
                Interact();
            }
        }
    }

    // This method is meant to be overwritten
    public virtual void Interact()
    {
        gameTime.paused = true;
        GameObject.Find("Black Bars").GetComponent<BlackBars>().MoveBarsIn();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    IEnumerator InteractPromptFadeIn()
    {
        for (float f = 0.0f; f <= 1f; f += 0.05f)
        {
            interactPopup.GetComponent<CanvasGroup>().alpha = f;
            yield return null;
        }
    }

    IEnumerator InteractPromptFadeOut()
    {
        for (float f = 1f; f >= 0f; f -= 0.05f)
        {
            interactPopup.GetComponent<CanvasGroup>().alpha = f;
            yield return null;
        }

        interactPopup.GetComponent<CanvasGroup>().alpha = 0f;
    }

}