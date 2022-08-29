using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform target2;

    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 offset2;

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.instance;
        transform.position = (target.position + offset);
    }

    private void Update()
    {
        if (gameManager.CheckState(GameStates.Shooting))
        {
            transform.position = Vector3.Lerp(transform.position, target.TransformPoint(offset), 0.1f);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, target.position + offset, 0.1f);
        }

        transform.LookAt(target);

    }

    public void OnShooting()
    {

        if (gameManager.CheckState(GameStates.Shooting))
        {
            target = target2;
            offset = offset2;
        }

    }
}
