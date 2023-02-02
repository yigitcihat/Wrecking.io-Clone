using UnityEngine;
using DG.Tweening;

public class ScaleAnimation : MonoBehaviour
{
    public Ease Ease = Ease.Linear;
    public float Duration = 2f;
    public float ScaleMultiplier = .5f;

    private void Start()
    {
        transform.DOScale(transform.localScale * ScaleMultiplier, Duration).SetEase(Ease).SetLoops(-1, LoopType.Yoyo);
    }
}
