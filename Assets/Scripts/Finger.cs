using UnityEngine;

public class Finger : MonoBehaviour
{
    [SerializeField] private float sens;

    [SerializeField] private Camera cam;
    [SerializeField] private Camera UICam;
    [SerializeField] private Transform transformOfBase;

    private GameObject shootpoint;
    private GameManager gameManager;
    private Base _base;

    private Vector3 firstClick;
    private Vector3 lastClick;
    private Vector3 diff;
    private Vector3 desiredPos;
    private Vector3 lastMousePosition;

    private void Start()
    {
        gameManager = GameManager.instance;
        _base = transformOfBase.GetComponent<Base>();

        shootpoint = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TouchDetector();
            OnClick();
        }
        if (Input.GetMouseButton(0))
        {
            OnDrag();
        }
        if (Input.GetMouseButtonUp(0))
        {
            OnUp();
        }
    }

    private void TouchDetector()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            hit.transform.GetComponent<IInteract>()?.Interact();
            if (gameManager.CheckState(GameStates.PlugIn) && hit.transform.TryGetComponent<Mag>(out Mag mag))
            {
                mag.GoIn();
            }
        }
    }

    private void OnClick()
    {
        if (gameManager.CheckState(GameStates.Shooting))
        {
            firstClick = UICam.ScreenToWorldPoint(Input.mousePosition);
            lastClick = firstClick;
        }
    }

    private void OnDrag()
    {

        lastMousePosition = Vector3.Lerp(lastMousePosition, Input.mousePosition, 0.8f);
        lastClick = UICam.ScreenToWorldPoint(Input.mousePosition);
        diff = lastClick - firstClick;
        diff *= sens;
        Move();
        firstClick = Vector3.Lerp(firstClick, lastClick, 0.1f);



    }

    private void OnUp()
    {
        diff = Vector3.zero;

        if (gameManager.CheckState(GameStates.Shooting))
        {
            _base.Shoot();
            _base.Recoil();
        }
    }

    private void Move()
    {

        if (gameManager.CheckState(GameStates.Shooting) && diff != Vector3.zero)
        {
            desiredPos = shootpoint.transform.position + (diff * 0.5f) - new Vector3(diff.x * 0.5f, 0, 0);
            shootpoint.transform.position = Vector3.Lerp(shootpoint.transform.position, desiredPos, 0.2f);
            transformOfBase.LookAt(shootpoint.transform);
        }
    }
}
