using UnityEngine;
using DG.Tweening;
using System.Collections;

public class Base : MonoBehaviour
{
    [SerializeField] private Vector3 upRecoil;
    [SerializeField] private Slide slide;
    [SerializeField] private Transform laserOrigin;
    [SerializeField] private float laserDur = 0.05f;
    [SerializeField] private GameObject crossair;

    private GameManager gameManager;
    private GameEvents gameEvents;
    private LineRenderer laserLine;

    private void Start()
    {
        gameEvents = GameEvents.instance;
        gameManager = GameManager.instance;

        laserLine = transform.GetComponentInChildren<LineRenderer>();

    }

    private void Update()
    {
        LaserPoint();
    }

    public void BasePos()
    {
        Sequence baseit = DOTween.Sequence();

        if (gameManager.CheckState(GameStates.BasePos))
        {
            baseit.Append(transform.DOMove(new Vector3(0f, 1f, 0.36f), 1)).Join(transform.DORotate(new Vector3(0, -42.5f, -90), 1)).SetDelay(1).
            OnComplete(() => gameManager.SetState(GameStates.PlugIn));
        }
    }

    public void BasePlugPos()
    {
        transform.DOMove(new Vector3(0, 1, 0), 1).OnComplete(() => { gameManager.SetState(GameStates.PlugPos); slide.SlidePos(); });
    }

    public void BaseFinished()
    {
        Sequence finishedSeq = DOTween.Sequence();

        finishedSeq.Append(transform.DOMove(new Vector3(0, 1, 0.25f), 1)).Append(transform.DORotate(new Vector3(0, -80, -90), 1f)).OnComplete
            (() => { gameManager.SetState(GameStates.ShootingPos); ForShooting(); });
    }

    private void ForShooting()
    {
        Sequence shootingSeq = DOTween.Sequence();

        shootingSeq.Append(transform.DORotate(new Vector3(0, 90, 0), 1)).Join(transform.DOMove(new Vector3(0, 1, 0.5f), 1)).SetDelay(1).

            OnComplete(() =>
            {
                gameManager.SetState(GameStates.Shooting);
                Camera.main.GetComponent<CameraController>().OnShooting();
                gameEvents.Shooting?.Invoke();
            });
    }

    private void AddRecoil() => transform.localEulerAngles += upRecoil;

    private void StopRecoil() => transform.localEulerAngles -= upRecoil;

    private IEnumerator RecoilWait()
    {
        AddRecoil();
        yield return new WaitForSeconds(0.1f);
        StopRecoil();
    }

    public void Recoil() => StartCoroutine(RecoilWait());

    private void LaserPoint()
    {
        if (gameManager.CheckState(GameStates.Shooting))
        {

            laserLine.SetPosition(0, laserOrigin.position);
            laserLine.SetPosition(1, laserOrigin.position + transform.forward * 50);
            crossair.SetActive(true);
        }
    }

    public void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 1000))
        {
            Shootables shootables;
            GameObject go = hit.transform.gameObject;
            shootables = go.AddComponent<Shootables>();
            shootables.Shoot();

        }

        StartCoroutine(LaserTime());

    }

    IEnumerator LaserTime()
    {
        laserLine.enabled = true;
        yield return new WaitForSeconds(laserDur);
    }
}
