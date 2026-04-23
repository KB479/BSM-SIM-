using UnityEngine;

public class Player : MonoBehaviour
{
    public LayerMask layerMask;
    [SerializeField] private Transform playerInteractPoint;
    [SerializeField] private float interactDistance; 



    private void Update()
    {
        HandleInteraction();
        EndDay(); 
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
                    interatable.ResolveCurrentTask(true); 
                    
                }

            }
        }else if (Input.GetKeyDown(KeyCode.R))
        {
            if (Physics.Raycast(playerInteractPoint.position, playerInteractPoint.transform.forward, out hit, interactDistance, layerMask))
            {

                if (hit.transform.TryGetComponent(out IInteractable interatable))
                {
                    interatable.ResolveCurrentTask(false);
                }
            }
        }

        

    }

    private void EndDay()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameManager.Instance.TryEndDay(); 
        }


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 origin = playerInteractPoint.position;
        Gizmos.DrawRay(origin, playerInteractPoint.transform.forward * interactDistance);
    }
    


}