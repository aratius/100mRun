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
    Tween t = DOTween.To(
      (value) => this._player.CurrentTime = value,
      this._performAnimation.startTime,
      this._performAnimation.endTime,
      2f
    ).SetLoops(3).SetDelay(1f).SetEase(Ease.Linear);
    t.OnUpdate(() =>
    {
      // 着地砂埃
      if (
        t.ElapsedPercentage(includeLoops: false) > .75f &&
        t.ElapsedPercentage(includeLoops: false) < .95f &&
        Random.Range(0f, 1f) < .3f
      )
      {
        float angle = Random.Range(0f, Mathf.PI * 2);
        Vector3 offset = new Vector3(Mathf.Sin(angle), -1.5f, Mathf.Cos(angle));
        Vector3 moveOffset = new Vector3(offset.x * 1.5f, Random.Range(.3f, 1f), offset.z * 1.5f);
        this._CreateDust(this.transform.position + offset, moveOffset);
      }
    });
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
            .Append(this.transform.DOLocalMoveZ(120f, (totalTime * 2f) / 10f).SetEase(Ease.OutSine))
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
                (totalTime * 2f) / 10f
              ).SetEase(Ease.OutSine)
            )
        ).OnUpdate(() =>
        {
          if (Random.Range(0f, 1f) < .15f) this._CreateDust(this.transform.position + new Vector3(Random.Range(-.5f, .5f), -1f, -.5f), new Vector3(0f, Random.Range(0.3f, 1.5f), 0f));
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

  /// <summary>
  ///
  /// </summary>
  /// <param name="pos"></param>
  /// <param name="offset"></param>
  private void _CreateDust(Vector3 pos, Vector3 offset)
  {
    GameObject dust = Instantiate(this._dustPrefab, this.transform.parent);
    float scale = Random.Range(.3f, 1f);
    dust.transform.localScale = Vector3.one * scale;
    dust.transform.position = pos;
    dust.GetComponent<Dust>().MoveAndDisappear(pos + offset);
  }

}
