using UnityEngine;

public class WinArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<IInteract>()?.Interact();
    }
}
 