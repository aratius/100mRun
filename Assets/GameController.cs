using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

  [SerializeField]
  private CubeHuman[] _players;
  [SerializeField]
  private SideCamera _sideCamera;

  void Start()
  {

  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.S)) this.StartRace();
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
