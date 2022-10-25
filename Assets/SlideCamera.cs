using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SlideCamera : MonoBehaviour
{
  [SerializeField]
  private float _startPosX;
  [SerializeField]
  private float _endPosX;

  /// <summary>
  ///
  /// </summary>
  /// <param name="duration"></param>
  /// <param name="delay"></param>
  public void Slide(float duration, float delay)
  {
    DOTween.Sequence()
    .Append(this.transform.DOLocalMoveX(this._startPosX, 0))
    .Append(this.transform.DOLocalMoveX(this._endPosX, duration))
    .SetDelay(delay)
    .SetEase(Ease.Linear);
  }
}
