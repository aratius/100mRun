using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[System.Serializable]
public enum CameraStatus {
  wiggle,
  shock
}

/// <summary>
/// Wiggle camera
/// </summary>
public class WiggleCamera : MonoBehaviour
{

  public static List<WiggleCamera> instances = new List<WiggleCamera>();

  [SerializeField]
  private CameraStatus _defaultStatus;

  private Vector3 _defaultLocalPosition;
  private Tween _cameraTween;

  void Awake()
  {
    WiggleCamera.instances.Add(this);
  }

  void Start()
  {
    this._defaultLocalPosition = this.transform.localPosition;

    if(this._defaultStatus == CameraStatus.wiggle)
    {
      this.Wiggle();
    } else if (this._defaultStatus == CameraStatus.shock)
    {
      this.Shock();
    }
  }

  /// <summary>
  /// wiggle
  /// </summary>
  public void Wiggle()
  {
    if(this._cameraTween != null) this._cameraTween.Kill();
    this._cameraTween = DOTween.To(
      setter: (_) => {}, startValue: 0f, endValue: 1f, duration: 1f
    )
    .SetLoops(-1)
    .OnUpdate(() => {
      float t = Time.time;
      float range = .001f;
      Vector3 p = this._defaultLocalPosition + new Vector3(
        Mathf.Sin(t * .8f) + Mathf.Sin(t * .9f),
        Mathf.Cos(t * .81f) + Mathf.Cos(t * .91f),
        Mathf.Sin(t * .82f) + Mathf.Cos(t * .92f)
      ) * range;
      this.transform.DOLocalMove(p, .1f);
    });
  }

  /// <summary>
  /// shock
  /// </summary>
  public void Shock()
  {
    if(this._cameraTween != null) this._cameraTween.Kill();
    this._cameraTween = this.transform.DOShakePosition(duration: .5f, strength: 0.005f, vibrato: 10, randomness: 100f, snapping: false, fadeOut: true);
    this._cameraTween.OnComplete(this.Wiggle);
  }

}
