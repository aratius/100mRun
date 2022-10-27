using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Poles
/// </summary>
public class Poles : SingletonMonoBehaviour<Poles>
{

    [SerializeField]
    private GameObject _polePrefab;

    private List<Pole> _poles = new List<Pole>();
    private float _lastProgress = 0f;

    void Start()
    {
        int COUNT = 160;
        for(int i = 0; i < COUNT; i++) {
            GameObject pole = Instantiate(this._polePrefab, this.transform);

            Vector3 pos = pole.transform.localPosition;
            pos.x = -10f;
            pos.z = (float)i / (float)COUNT * 100f;
            pole.transform.position = pos;

            Vector3 scale = pole.transform.localScale;
            scale.x = .5f;
            scale.y = 0f;
            scale.z = .5f;
            pole.transform.localScale = scale;

            this._poles.Add(pole.GetComponent<Pole>());
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="progress"></param>
    public void SetProgress(float progress)
    {
        for(int i = 0; i < this._poles.Count; i++)
        {
            float point = (float)i / (float)this._poles.Count;
            if(this._lastProgress < point && progress >= point) this._poles[i].Build(3f);
        }

        this._lastProgress = progress;
    }

}
