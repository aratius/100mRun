using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SideCamera : MonoBehaviour
{

  [SerializeField]
  private float _startAngle;
  [SerializeField]
  private float _endAngle;

  /// <summary>
  ///
  /// </summary>
  /// <param name="duration"></param>
  /// <param name="delay"></param>
  public void Pan(float duration, float delay)
  {
    DOTween.Sequence()
    .Append(this.transform.DORotate(new Vector3(-12f, this._startAngle, 0f), 0f))
    .Append(this.transform.DORotate(new Vector3(-12f, this._endAngle, 0f), duration))
    .SetDelay(delay)
    .SetEase(Ease.Linear);
  }

}
