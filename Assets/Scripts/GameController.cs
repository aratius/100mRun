using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
struct PlayerCameras {
  public GameObject side;
  public GameObject front;
  public GameObject diagonal;
}

public class GameController : MonoBehaviour
{

  [SerializeField]
  private CubeHuman[] _players;
  [SerializeField]
  private PlayerCameras[] _playersCameras;
  [SerializeField]
  private SideCamera _sideCamera;
  [SerializeField]
  private SlideCamera _slideCamera;

  void Start()
  {

  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.I)) this.StartIntroduction();
    else if (Input.GetKeyDown(KeyCode.S)) this.StartRace();
    else if (Input.GetKeyDown(KeyCode.F)) this._playersCameras[0].front.GetComponent<FrontCamera>().Focus();
  }

  /// <summary>
  /// player introduction
  /// </summary>
  public void StartIntroduction()
  {
    this._slideCamera.Slide(10f, 0f);
  }

  /// <summary>
  ///
  /// </summary>
  public void StartRace()
  {
    foreach (CubeHuman player in this._players) player.Run();
    this._sideCamera.Pan(1.5f, 1f);
  }

  /// <summary>
  ///
  /// </summary>
  public void Reset()
  {

  }

}
