using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 連番で命名されてる画像ファイルでスプライトアニメーションを再生するコンポーネントの規定クラス
/// </summary>
public abstract class SpriteAnimationViewBase : MonoBehaviour
{
    /// <summary>
    /// スプライトファイルの種類
    /// </summary>
    public enum SpriteMode
    {
        /// <summary>デフォルトのスプライト</summary>
        Single = 0,
        /// <summary>単一のスプライトをスライスして複数画像として扱うタイプのスプライト</summary>
        Multiple,
    }

    private SpriteMode _spriteMode;

    private Sprite[] _sprites;

    private float _interval;

    private int _loops = -1;

    private Action<Sprite> _onUpdate;

    private Action _onComplete;

    private float _countTime;

    private int _currentSequence;

    private int _countLoop;

    private void Awake()
    {
        Initialize();
    }

    protected virtual void Initialize()
    {
        _sprites = null;
        Pause();
    }

    /// <summary>
    /// スプライトファイルの種類を設定
    /// </summary>
    /// <param name="mode">スプライトファイルの種類</param>
    public void SetSpriteMode(SpriteMode mode)
    {
        _spriteMode = mode;
    }

    /// <summary>
    /// アニメーションを再生
    /// </summary>
    /// <param name="sprites">再生するスプライト配列</param>
    /// <param name="interval">スプライトを次のコマに変えるまでのインターバル(秒)</param>
    /// <param name="loops">アニメーションをループ再生する回数 負数なら無限ループ</param>
    /// <param name="onUpdate">スプライトを次のコマに変えるときのコールバック</param>
    /// <param name="onComplete">アニメーション終了時のコールバック 無限ループ時は呼び出されません</param>
    public void Play(Sprite[] sprites, float interval = 0.1f, int loops = -1, Action<Sprite> onUpdate = null, Action onComplete = null)
    {
        _sprites = sprites;

        _interval = interval;
        _loops = loops;
        _onUpdate = onUpdate;
        _onComplete = onComplete;

        _countTime = 0f;
        _currentSequence = 0;
        _countLoop = 0;

        SetSprite(_sprites[0]);

        enabled = true;
    }

    /// <summary>
    /// アニメーションを再生
    /// </summary>
    /// <param name="spritePathFormat">再生するスプライトパスの文字列フォーマット</param>
    /// <param name="count">アニメーションに使うスプライトの数</param>
    /// <param name="startIndex">アニメーション開始位置のインデックス</param>
    /// <param name="interval">スプライトを次のコマに変えるまでのインターバル(秒)</param>
    /// <param name="loops">アニメーションをループ再生する回数 負数なら無限ループ</param>
    /// <param name="onUpdate">スプライトを次のコマに変えるときのコールバック</param>
    /// <param name="onComplete">アニメーション終了時のコールバック 無限ループ時は呼び出されません</param>
    public void Play(string spritePathFormat, int count, int startIndex = 0, float interval = 0.1f, int loops = -1, Action<Sprite> onUpdate = null, Action onComplete = null)
    {
        var sprites = new Sprite[count];

        switch (_spriteMode)
        {
            case SpriteMode.Single:
                for (int i = 0; i < count; i++)
                {
                    var sprite = Resources.Load<Sprite>(string.Format(spritePathFormat, i + startIndex));
                    sprites[i] = sprite;
                }
                break;

            case SpriteMode.Multiple:
                var originSprites = Resources.LoadAll<Sprite>(spritePathFormat);
                for (int i = 0; i < count; i++)
                {
                    var sprite = originSprites[i + startIndex];
                    sprites[i] = sprite;
                }
                break;
        }

        Play(sprites, interval, loops, onUpdate, onComplete);
    }

    /// <summary>
    /// スプライトをビューに反映する
    /// </summary>
    /// <param name="sprite">スプライトファイル</param>
    protected abstract void SetSprite(Sprite sprite);

    /// <summary>
    /// アニメーションを一時停止
    /// </summary>
    public void Pause()
    {
        enabled = false;
    }

    /// <summary>
    /// アニメーションを再開
    /// </summary>
    public void Resume()
    {
        enabled = true;
    }

    private void Update()
    {
        _countTime += Time.deltaTime;
        if (_countTime < _interval)
        {
            return;
        }
        _countTime = 0f;

        _currentSequence++;
        if (_sprites.Length <= _currentSequence)
        {
            _currentSequence = 0;
            _countLoop++;
            if (_loops >= 0 && _loops <= _countLoop)
            {
                enabled = false;
                _onComplete?.Invoke();
                return;
            }
        }

        var sprite = _sprites[_currentSequence];
        _onUpdate?.Invoke(sprite);
        SetSprite(sprite);
    }
}
