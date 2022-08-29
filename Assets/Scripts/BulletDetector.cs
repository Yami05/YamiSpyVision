using UnityEngine;

public class BulletDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<IBullet>()?.Reload();
    }
}
