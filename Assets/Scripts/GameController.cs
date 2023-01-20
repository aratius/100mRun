using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using UnityEngine.Rendering.PostProcessing;
using Unity.Custom;
using DG.Tweening;

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
  [SerializeField]
  private PostProcessVolume _volume;
  private DepthOfField _dof;
  private AutoExposure _autoExposure;
  private Sequence _seq;

  void Start()
  {
    this._dof = this._volume.profile.GetSetting<DepthOfField>();
    this._autoExposure = this._volume.profile.GetSetting<AutoExposure>();
  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.S)) this.PlayTimeline();
    else if (Input.GetKeyDown(KeyCode.I)) this.StartIntroduction();
    else if (Input.GetKeyDown(KeyCode.R)) this.StartRace();
    else if (Input.GetKeyDown(KeyCode.F)) this._playerCameras.front.GetComponent<FrontCamera>().Focus();
    else if (Input.GetKeyDown(KeyCode.L)) SceneManager.LoadScene (SceneManager.GetActiveScene().name);
  }

  /// <summary>
  /// player introduction
  /// </summary>
  public void StartIntroduction()
  {
    this._slideCamera.Slide(3f, 0f);
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
    this._ApplyDof(DofSettings.slide);
    await UniTask.Delay(4500);

    Cameras.Instance.ActivateByIndex(0);
    this._ApplyDof(DofSettings.lookUp);
    await UniTask.Delay(2000);

    // Cameras.Instance.ActivateByIndex(6);
    // this._ApplyDof(DofSettings.focus);
    // this._playerCameras.front.GetComponent<FrontCamera>().Focus();
    // await UniTask.Delay(3500);

    Cameras.Instance.ActivateByIndex(1);
    this._ApplyDof(DofSettings.start);
    await UniTask.Delay(1000);

    this.StartRace();
    await UniTask.Delay(2800);

    Cameras.Instance.ActivateByIndex(5);
    this._ApplyDof(DofSettings.side);
    await UniTask.Delay(1200);

    Cameras.Instance.ActivateByIndex(7);
    this._ApplyDof(DofSettings.back);
    await UniTask.Delay(1500);

    Cameras.Instance.ActivateByIndex(5);
    this._ApplyDof(DofSettings.side);
    await UniTask.Delay(2500);

    Cameras.Instance.ActivateByIndex(7);
    this._ApplyDof(DofSettings.back);
    await UniTask.Delay(1000);

    Cameras.Instance.ActivateByIndex(2);
    this._ApplyDof(DofSettings.goal);
    await UniTask.Delay(1500);

    Cameras.Instance.ActivateByIndex(4);
    this._ApplyDof(DofSettings.diagonalBefore);
    this._ApplyDof(DofSettings.diagonal, 1.5f, 3.5f, Ease.OutBack);
    await UniTask.Delay(5000);

    Cameras.Instance.ActivateByIndex(5);
    this._ApplyDof(DofSettings.focus);
    await UniTask.Delay(500);

    Cameras.Instance.ActivateByIndex(4);
    this._ApplyDof(DofSettings.diagonal);

    await UniTask.Delay(5000);

    LavaGenerator.Instance.Fall(this._players[0].transform.position);
  }

  private void _ApplyDof(Settings settings, float duration = 0, float delay = 0, Ease ease = Ease.Linear)
  {
    if(this._seq != null) this._seq.Kill();
    this._seq = DOTween.Sequence().SetDelay(delay).SetEase(ease);
    this._seq.Append(
      DOTween.To(
        () => this._dof.focusDistance.value,
        v => this._dof.focusDistance.value = v,
        settings.focusDistance,
        duration
      )
    ).Join(
      DOTween.To(
        () => this._dof.focalLength.value,
        v => this._dof.focalLength.value = v,
        settings.focalLength,
        duration
      )
    );
  }

  private void _ApplyExposure(float min, float max)
  {
    this._autoExposure.maxLuminance.value = min;
    this._autoExposure.minLuminance.value = max;
  }

}
