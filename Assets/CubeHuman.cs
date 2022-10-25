using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Formats.Alembic.Importer;
using DG.Tweening;
using Cysharp.Threading.Tasks;

enum State
{
  preparing,
  running,
  braking,
  peforming
}

[System.Serializable]
public struct AnimationRange
{
  public float startTime;
  public float endTime;
  public AnimationRange(float s, float e)
  {
    this.startTime = s;
    this.endTime = e;
  }
}

public class CubeHuman : MonoBehaviour
{

  [SerializeField]
  private AnimationRange _performAnimation;

  [SerializeField]
  private AnimationRange _prepareRunningAnimation;

  [SerializeField]
  private AnimationRange _runAnimation;
  [SerializeField]
  private AnimationRange _endRunningAnimation;

  [SerializeField]
  private float _animationSpeed = 1f;
  [SerializeField]
  private AlembicStreamPlayer _player;
  [SerializeField]
  private GameObject _dustPrefab;

  private State _state = State.running;
  private float _speed = 0f;

  // Start is called before the first frame update
  void Start()
  {
  }

  // Update is called once per frame
  void Update()
  {
  }

  /// <summary>
  ///
  /// </summary>
  public void Prepare()
  {
    this._state = State.preparing;
  }

  /// <summary>
  ///
  /// </summary>
  public async void Perform()
  {
    this._player.CurrentTime = this._performAnimation.startTime;
    this._state = State.peforming;
    DOTween.To(
      () => this._player.CurrentTime,
      (value) => this._player.CurrentTime = value,
      this._performAnimation.endTime,
      2f
    ).OnComplete(
      () => this._player.CurrentTime = this._performAnimation.startTime
    ).SetLoops(3).SetDelay(1f);
  }

  /// <summary>
  ///
  /// </summary>
  public void Run()
  {
    this._state = State.running;
    float totalTime = 9f;

    DOTween.Sequence()
    .Append(
      // Prepare running
      DOTween.To(
        (value) => this._player.CurrentTime = value,
        this._prepareRunningAnimation.startTime,
        this._prepareRunningAnimation.endTime,
        1f
      ).SetEase(Ease.Linear)
    ).Append(
      // Run
      DOTween.Sequence()
        .Append(
          DOTween.Sequence()
            .Append(this.transform.DOLocalMoveZ(10f, (totalTime * 1f) / 10f).SetEase(Ease.InSine))
            .Append(this.transform.DOLocalMoveZ(100f, (totalTime * 9f) / 10f).SetEase(Ease.Linear))
            .Append(this.transform.DOLocalMoveZ(110f, (totalTime * 1f) / 10f).SetEase(Ease.OutSine))
        ).Join(
          DOTween.Sequence()
            .Append(
              DOTween.To(
                (value) => this._player.CurrentTime = value % (this._runAnimation.endTime - this._runAnimation.startTime) + this._runAnimation.startTime,
                0,
                (this._runAnimation.endTime - this._runAnimation.startTime) * 3f,
                (totalTime * 1f) / 10f
              ).SetEase(Ease.InSine)
            )
            .Append(
              DOTween.To(
                (value) => this._player.CurrentTime = value % (this._runAnimation.endTime - this._runAnimation.startTime) + this._runAnimation.startTime,
                0,
                (this._runAnimation.endTime - this._runAnimation.startTime) * 35f,
                (totalTime * 9f) / 10f
              ).SetEase(Ease.Linear)
            )
            .Append(
              DOTween.To(
                (value) => this._player.CurrentTime = value % (this._runAnimation.endTime - this._runAnimation.startTime) + this._runAnimation.startTime,
                0,
                (this._runAnimation.endTime - this._runAnimation.startTime) * 3f,
                (totalTime * 1f) / 10f
              ).SetEase(Ease.OutSine)
            )
        ).OnUpdate(() =>
        {
          if (Random.Range(0f, 1f) < .1f) this._CreateDust();
        })
    ).Append(
      // End running
      DOTween.To(
        (value) => this._player.CurrentTime = value,
        this._endRunningAnimation.startTime,
        this._endRunningAnimation.endTime,
        1f
      ).SetEase(Ease.Linear)
    ).OnComplete(this.Perform);
  }

  private void _CreateDust()
  {
    GameObject dust = Instantiate(this._dustPrefab);
    float scale = Random.Range(.3f, 1f);
    dust.transform.localScale = Vector3.one * scale;
    dust.transform.position = this.transform.position + new Vector3(Random.Range(-.3f, .3f), -1f, -.5f);
    dust.GetComponent<Dust>().Float();
  }

}
