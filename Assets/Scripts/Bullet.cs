using UnityEngine;
using DG.Tweening;

public class Bullet : MonoBehaviour, IBullet
{
    [SerializeField] private Transform pos1;
    [SerializeField] private Transform pos2;

    private GameManager gameManager;
    private Mag mag;
    private Rigidbody rb;
    private Bullet[] bullets;
    private Camera cam;

    private float mZCoord;
    private Vector3 mousePoint;

    private void Start()
    {
        mag = Mag.instance;
        cam = Camera.main;
        gameManager = GameManager.instance;
        rb = GetComponent<Rigidbody>();
        bullets = GetComponentsInChildren<Bullet>();
        GetInList();
    }

    private void OnMouseDown()
    {
        mZCoord = cam.WorldToScreenPoint(gameObject.transform.position).z;
    }

    private Vector3 GetMouseAsWorldPoint()
    {
        mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return cam.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag()
    {
        if (gameManager.CheckState(GameStates.Bullet))
        {
            rb.isKinematic = true;
            transform.position = Vector3.Lerp(transform.position, GetMouseAsWorldPoint()
                - new Vector3(0, -1.3f + GetMouseAsWorldPoint().y, 0), 0.1f);
        }
    }

    private void OnMouseUp()
    {
        rb.isKinematic = false;
    }

    public void Reload()
    {
        Sequence bulletSeq = DOTween.Sequence();

        bulletSeq.Append(transform.DOMove(pos1.position, 1)).Join(transform.DORotate(new Vector3(0, -90, -95), 1)).Append(transform.DOMove(pos2.position, 0.2f)).

            OnComplete(() =>
            {
                transform.SetParent(pos2); rb.isKinematic = true;
                gameManager.RemoveBullet(gameObject);
                gameObject.SetActive(false);

                if (gameManager.GetBulletsCount() == 0) gameManager.SetState(GameStates.Meg2);
                mag.lastBullet.SetActive(true);
                mag.MegPos2();
            });
    }

    private void GetInList()
    {
        for (int i = 0; i < bullets.Length; i++)
        {
            gameManager.AddBullet(bullets[i].gameObject);
        }
    }
}
