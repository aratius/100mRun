using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
///
/// </summary>
public class FrontCamera : MonoBehaviour
{

    private Camera _camera;

    [SerializeField]
    private float _focusStartFov;
    [SerializeField]
    private float _focusEndFov;
    [SerializeField]
    private float _focusStartZ;
    [SerializeField]
    private float _focusEndZ;

    void Start()
    {
        this._camera = this.GetComponent<Camera>();
        this._camera.fieldOfView = this._focusStartFov;

        Vector3 p = this._camera.transform.localPosition;
        p.z = this._focusStartZ;
        this._camera.transform.localPosition = p;
    }

    /// <summary>
    ///
    /// </summary>
    public void Focus()
    {
        Sequence s = DOTween.Sequence();
        s.Append(
            this.transform.DOLocalMoveZ(this._focusEndZ, 3f).SetEase(Ease.Linear)
        ).Join(
            DOTween.To(
                () => this._camera.fieldOfView,
                (value) => this._camera.fieldOfView = value,
                this._focusEndFov,
                3f
            ).SetEase(Ease.InCirc)
        );
    }

}
