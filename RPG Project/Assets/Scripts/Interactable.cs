using UnityEngine;

    // This component is for all objects that the player can
    // interact with such as enemies, items etc. It is meant
    // to be used as a base class.

public class Interactable : MonoBehaviour{
    
    public float radius = 3f;               // How close do we need to be to interact?
    public Transform interactionTransform;  

    bool isFocus = false;       // Is this interactable current being focused? 
    Transform player;           // Refgerence to the player transform

    bool hasInteracted = false; // Have we already interacted with the object?

    public virtual void Interact (){
        // This method is meant to be overwritten
        // Debug.Log ("Interacting with " + transform.name);
    }

    void Update (){
        if (isFocus && !hasInteracted){
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if(distance <= radius){
                Interact();
                hasInteracted = true;
            }
        }
    }

    public void OnFocused (Transform playerTransform){
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void OnDefocused (){
        isFocus = false;
        player = null;
        hasInteracted = false;
    }

    void OnDrawGizmosSelected (){

        if (interactionTransform == null)
            interactionTransform = transform;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
