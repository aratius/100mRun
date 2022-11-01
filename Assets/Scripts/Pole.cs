using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// Pole
/// </summary>
public class Pole : MonoBehaviour
{

    void Start()
    {
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// build
    /// </summary>
    public void Build(float height)
    {
        this.gameObject.SetActive(true);
        this.transform.DOScaleY(height, .5f).SetEase(Ease.OutElastic);
    }
}
