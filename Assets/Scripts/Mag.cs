using UnityEngine;
using DG.Tweening;

public class Mag : Singleton<Mag>, IInteract
{
    [SerializeField] private Transform target;
    [SerializeField] GameObject baseObj;

    private GameManager gameManager;

    private GameObject bulletDetector;
    public GameObject lastBullet;


    private void Start()
    {
        gameManager = GameManager.instance;
        lastBullet = transform.GetChild(0).gameObject;
        bulletDetector = transform.GetChild(1).gameObject;
    }

    public void Interact()
    {
        MegPos1();
    }

    private void MegPos1()
    {

        if (gameManager.CheckState(GameStates.Meg1))
        {
            Sequence posofMeg = DOTween.Sequence();
            posofMeg.Append(transform.DOMove(target.position, 1, false))
                    .Join(transform.DORotate(new Vector3(0, -90, -95), 1, RotateMode.Fast))
                    .OnComplete(() => { gameManager.SetState(GameStates.Bullet); bulletDetector.SetActive(true); });
        }
    }

    public void MegPos2()
    {
        if (gameManager.CheckState(GameStates.Meg2))
        {
            transform.DORotate(new Vector3(0, -60, -90), 1f).SetDelay(1).OnComplete(() =>
            { lastBullet.SetActive(true); gameManager.SetState(GameStates.BasePos); baseObj.GetComponent<Base>().BasePos(); });
        }
    }

    public void GoIn()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(new Vector3(0, 1f, 0.33f), 1)).Join(transform.DORotate(new Vector3(0, -60, -90), 1)).
            OnComplete(() => { transform.SetParent(baseObj.transform); baseObj.GetComponent<Base>().BasePlugPos(); });
    }
}



