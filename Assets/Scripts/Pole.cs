using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// Pole
/// </summary>
public class Pole : MonoBehaviour
{

    /// <summary>
    /// build
    /// </summary>
    public void Build(float height)
    {
        this.transform.DOScaleY(height, .5f).SetEase(Ease.OutElastic);
    }
}
