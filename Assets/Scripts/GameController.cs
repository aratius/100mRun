using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

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
  private PlayerCameras _playerCameras;
  [SerializeField]
  private SideCamera _sideCamera;
  [SerializeField]
  private SlideCamera _slideCamera;

  void Start()
  {

  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.S)) this.PlayTimeline();
    else if (Input.GetKeyDown(KeyCode.I)) this.StartIntroduction();
    else if (Input.GetKeyDown(KeyCode.R)) this.StartRace();
    else if (Input.GetKeyDown(KeyCode.F)) this._playerCameras.front.GetComponent<FrontCamera>().Focus();
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

  /// <summary>
  ///
  /// </summary>
  public async void PlayTimeline()
  {
    this.StartIntroduction();

    Cameras.Instance.ActivateByIndex(3);

    await UniTask.Delay(12000);

    Cameras.Instance.ActivateByIndex(0);

    await UniTask.Delay(3000);

    Cameras.Instance.ActivateByIndex(6);
    this._playerCameras.front.GetComponent<FrontCamera>().Focus();

    await UniTask.Delay(5000);

    Cameras.Instance.ActivateByIndex(1);

    await UniTask.Delay(1000);

    this.StartRace();

    await UniTask.Delay(2500);

    Cameras.Instance.ActivateByIndex(5);

    await UniTask.Delay(1000);

    Cameras.Instance.ActivateByIndex(4);

    await UniTask.Delay(2000);

    Cameras.Instance.ActivateByIndex(5);

    await UniTask.Delay(3000);

    Cameras.Instance.ActivateByIndex(2);

    await UniTask.Delay(2000);

    Cameras.Instance.ActivateByIndex(4);

    await UniTask.Delay(5000);

    Cameras.Instance.ActivateByIndex(5);

    await UniTask.Delay(500);

    Cameras.Instance.ActivateByIndex(4);
  }

}
