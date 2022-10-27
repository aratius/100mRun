using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dusts : SingletonMonoBehaviour<Dusts>
{

    [SerializeField]
    private GameObject _dustPrefab;

    /// <summary>
    ///
    /// </summary>
    /// <param name="position"></param>
    /// <param name="moveOffset"></param>
    public void Create(Vector3 position, Vector3 moveOffset)
    {
        GameObject dust = Instantiate(this._dustPrefab, this.transform);
        float scale = Random.Range(.3f, 1f);
        dust.transform.localScale = Vector3.one * scale;
        dust.transform.position = position;
        dust.GetComponent<Dust>().MoveAndDisappear(position + moveOffset);
    }

}
