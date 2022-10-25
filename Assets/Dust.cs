using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Dust : MonoBehaviour
{
  public void Float()
  {
    DOTween.Sequence()
    .Append(
        this.transform.DOLocalMoveY(Random.Range(0f, 1f), 1f).SetEase(Ease.OutExpo)
    ).Join(
        this.transform.DOScale(Vector3.zero, 1f).SetEase(Ease.OutExpo)
    ).Join(
        this.transform.DORotate(new Vector3(Random.Range(-180f, 180f), Random.Range(-180f, 180f), Random.Range(-180f, 180f)), 1f).SetEase(Ease.OutExpo)
    ).OnComplete(() => Destroy(this.gameObject));
  }
}
