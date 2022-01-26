using UnityEngine.EventSystems;
using UnityEngine;

// Controls the player. Here we choose our "focus" and where to move. 
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour{
    
    public Interactable focus;  // Our current focus: Item, Enemy etc.

    public LayerMask movementMask;  // Filter out everything not walkable
    
    Camera cam;         // Reference to our camera           
    PlayerMotor motor;  // Reference to our motor

    // Get references
    void Start(){
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update(){

        if (EventSystem.current.IsPointerOverGameObject())
            return;
            
        // If we press left mouse
        if (Input.GetMouseButtonDown(0)){

            //  We create a ray
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;


            // If the ray hits
            if (Physics.Raycast(ray, out hit, 100, movementMask)){
               motor.MoveToPOint(hit.point); // Move to where we hit

                // Stop focusing any objects 
                RemoveFocus();
            }
        }
        
        // If we press right mouse
        if (Input.GetMouseButtonDown(1)){
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100)){
               // Check if we hit an interactable
               Interactable interactable = hit.collider.GetComponent<Interactable>();

               // If we did set it as our focus
                if (interactable != null){
                    SetFocus(interactable);
                }

            }
        }
    }

    void SetFocus (Interactable newFocus){
        if (newFocus != focus){
            if (focus != null)
                focus.OnDefocused();
            
            focus = newFocus;
            motor.FollowTarget(newFocus);
        }

        newFocus.OnFocused(transform);
    }

    void RemoveFocus (){
        if (focus != null)
            focus.OnDefocused();
            
        focus = null;
        motor.StopFollowingTarget();
    }
}
