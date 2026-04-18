using UnityEngine;

public class Player : MonoBehaviour
{
    public LayerMask layerMask;
    [SerializeField] private Transform playerInteractPoint;
    [SerializeField] private float interactDistance; 



    private void Update()
    {
        HandleInteraction();
    }

    public void HandleInteraction()
    {

        RaycastHit hit;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if(Physics.Raycast(playerInteractPoint.position, playerInteractPoint.transform.forward, out hit, interactDistance, layerMask))
            {
            
                if(hit.transform.TryGetComponent(out IInteractable interatable))
                {
                    interatable.Interact(true, 20); 
                    
                }

            }
        }else if (Input.GetKeyDown(KeyCode.R))
        {
            if (Physics.Raycast(playerInteractPoint.position, playerInteractPoint.transform.forward, out hit, interactDistance, layerMask))
            {

                if (hit.transform.TryGetComponent(out IInteractable interatable))
                {
                    interatable.Interact(false, -20);
                }
            }
        }

        

    }

    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 origin = playerInteractPoint.position;
        Gizmos.DrawRay(origin, playerInteractPoint.transform.forward * interactDistance);
    }
    


}