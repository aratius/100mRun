using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

  [SerializeField]
  private CubeHuman[] _players;
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
    if (Input.GetKeyDown(KeyCode.S)) this.StartRace();
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
