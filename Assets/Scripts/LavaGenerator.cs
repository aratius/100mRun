using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaGenerator : SingletonMonoBehaviour<LavaGenerator>
{

  [SerializeField] private List<GameObject> _lavaPrefabs = new List<GameObject>();
  [SerializeField] private List<GameObject> _lavaStaticPrefabs = new List<GameObject>();
  private List<GameObject> _lavas = new List<GameObject>();

  // Start is called before the first frame update
  void Start()
  {
    for (float i = -30; i < 100; i += 1f)
    {
      for (int j = 0; j <= 10; j++)
      {
        for (int k = 0; k < 2; k++)
        {
          float angle = ((float)j / 10f) * Mathf.PI - Mathf.PI / 2f;
          float rad = 3f + (float)k * 10f;
          float scale = .7f + (float)k * 1.5f;
          Vector3 p = new Vector3(Mathf.Sin(angle) * rad, Mathf.Cos(angle) * rad, i);
          Quaternion r = Quaternion.Euler(Random.Range(0, 360f), Random.Range(0, 360f), Random.Range(0, 360f));
          GameObject lava = Instantiate(this._lavaPrefabs[(int)Mathf.Floor(Random.Range(0, this._lavaPrefabs.Count))], p, r, this.transform);
          lava.transform.localScale = Vector3.one * scale;
          this._lavas.Add(lava);
        }
      }
    }

    // for (int i = -100; i < 100; i += 2)
    // {
    //   for (int j = -100; j < 100; j += 2)
    //   {
    //     float scale = 4f;
    //     Vector3 p = new Vector3((float)i, (float)j, 110f);
    //     Quaternion r = Quaternion.Euler(Random.Range(0, 360f), Random.Range(0, 360f), Random.Range(0, 360f));
    //     GameObject lava = Instantiate(this._lavaStaticPrefabs[(int)Mathf.Floor(Random.Range(0, this._lavaStaticPrefabs.Count))], p, r, this.transform);
    //     lava.transform.localScale = Vector3.one * scale;
    //   }
    // }
  }

  public void Release(float playerPosZ)
  {
    foreach (GameObject lava in this._lavas)
    {
      if (playerPosZ > lava.transform.position.z)
      {
        Rigidbody rb = lava.GetComponent<Rigidbody>();
        if (rb.isKinematic)
        {
          rb.isKinematic = false;
          rb.useGravity = true;
        }
      }
    }
  }

  public void Fall(Vector3 pos)
  {
    for (int i = 0; i < 300; i++)
      {
        float scale = .5f;
        Vector3 p = pos + Vector3.up * i * scale + Vector3.up * 10f;
        Quaternion r = Quaternion.identity;
        GameObject lava = Instantiate(this._lavaPrefabs[(int)Mathf.Floor(Random.Range(0, this._lavaPrefabs.Count))], p, r, this.transform);
        lava.transform.localScale = Vector3.one * scale;
        Rigidbody rb = lava.GetComponent<Rigidbody>();
        if (rb.isKinematic)
        {
          rb.isKinematic = false;
          rb.useGravity = true;
        }
      }
  }

}
