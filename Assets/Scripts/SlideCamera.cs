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
  [SerializeField]
  private float[] _fixPosXs;

  /// <summary>
  ///
  /// </summary>
  /// <param name="duration"></param>
  /// <param name="delay"></param>
  public void Slide(float duration, float delay)
  {
    Sequence s = DOTween.Sequence();
    s.Append(this.transform.DOLocalMoveX(this._startPosX, 0));
    foreach(float x in this._fixPosXs)
    {
      s.Append(this.transform.DOLocalMoveX(x, duration / this._fixPosXs.Length).SetEase(Ease.InOutQuart));
    }
    s.Append(this.transform.DOLocalMoveX(this._endPosX, duration / this._fixPosXs.Length).SetEase(Ease.InOutQuart));
    s.SetDelay(delay);
    s.SetEase(Ease.Linear);
  }
}
