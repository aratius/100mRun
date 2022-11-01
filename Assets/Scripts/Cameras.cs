using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
class CameraData
{
  public GameObject camera;
  public bool isActive;
  public UnityEvent onActive = new UnityEvent();

  private bool _lastIsActive = false;

  public CameraData(GameObject _camera, bool _isActive)
  {
    this.camera = _camera;
    this.isActive = _isActive;
  }

  public void Update()
  {
    if (!this._lastIsActive && this.isActive) this.onActive.Invoke();
    this._lastIsActive = this.isActive;
  }

  public void On()
  {
    this.camera.SetActive(true);
    this.isActive = true;
  }

  public void Off()
  {
    this.camera.SetActive(false);
    this.isActive = false;
  }

}

public class Cameras : SingletonMonoBehaviour<Cameras>
{

  [SerializeField]
  private int _defaultActiveIndex;
  [SerializeField]
  private CameraData[] _cameraData;

  // Start is called before the first frame update
  void Start()
  {
    foreach (CameraData thisCamera in this._cameraData)
    {
      thisCamera.Off();
      thisCamera.onActive.AddListener(() =>
      {
        foreach (CameraData otherCamera in this._cameraData)
        {
          if (thisCamera == otherCamera) otherCamera.On();
          else otherCamera.Off();
        }
      });
    }
    this._cameraData[this._defaultActiveIndex].On();
  }

  // Update is called once per frame
  void Update()
  {
    foreach (CameraData thisCamera in this._cameraData) thisCamera.Update();
  }

  /// <summary>
  ///
  /// </summary>
  /// <param name="index"></param>
  public void ActivateByIndex(int index)
  {
    this._cameraData[index].isActive = true;
  }

}
