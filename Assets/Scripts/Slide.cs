using UnityEngine;
using DG.Tweening;

public class Slide : MonoBehaviour, IInteract
{
    [SerializeField] private GameObject baseObj;
    GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;
    }

    public void Interact()
    {
        if (gameManager.CheckState(GameStates.SlidePlug))
        {
            transform.DOMove(new Vector3(0.05f, 1, 0.218f), 1).OnComplete(() =>
            {
                transform.SetParent(baseObj.transform); gameManager.SetState(GameStates.CompleteGun);
                baseObj.GetComponent<Base>().BaseFinished();
            });
        }
    }

    public void SlidePos()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(transform.DOMove(new Vector3(-0.38f, 1, 0.7f), 1)).Append(transform.DORotate(new Vector3(0, -43.5f, -90), 0.6f)).
            Append(transform.DOMove(new Vector3(-0.19F, 1, 0.5f), 1).
            OnComplete(() => gameManager.SetState(GameStates.SlidePlug)));
    }
}
