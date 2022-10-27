using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WiggleCamera : MonoBehaviour
{

  void Start()
  {
    Sequence s = DOTween.Sequence().SetLoops(-1);
    s.Append(this.transform.DOShakePosition(duration: 1.0f, strength: 0.001f, vibrato: 2, randomness: 100f, snapping: false, fadeOut: false));
  }

}
