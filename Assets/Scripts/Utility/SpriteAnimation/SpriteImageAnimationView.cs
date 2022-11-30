using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Image用のスプライトアニメーションを再生するコンポーネント
/// </summary>
[RequireComponent(typeof(Image))]
public class SpriteImageAnimationView : SpriteAnimationViewBase
{
    public Image Image { get; private set; }

    private bool _isSetNativeSize;

    protected override void Initialize()
    {
        base.Initialize();
        Image = GetComponent<Image>();
    }

    /// <summary>
    /// アニメーションを再生
    /// </summary>
    /// <param name="spritePathFormat">再生するスプライトパスの文字列フォーマット</param>
    /// <param name="count">アニメーションに使うスプライトの数</param>
    /// <param name="startIndex">アニメーション開始位置のインデックス</param>
    /// <param name="interval">スプライトを次のコマに変えるまでのインターバル(秒)</param>
    /// <param name="loops">アニメーションをループ再生する回数 負数なら無限ループ</param>
    /// <param name="isSetNativeSize">スプライトを切り替えるたびにSetNativeSizeを実行するか否か</param>
    /// <param name="onUpdate">スプライトを次のコマに変えるときのコールバック</param>
    /// <param name="onComplete">アニメーション終了時のコールバック 無限ループ時は呼び出されません</param>
    public void Play(string spritePathFormat, int count, int startIndex = 0, float interval = 0.1f, int loops = -1, bool isSetNativeSize = false, Action<Sprite> onUpdate = null, Action onComplete = null)
    {
        _isSetNativeSize = isSetNativeSize;
        Play(spritePathFormat, count, startIndex, interval, loops, onUpdate, onComplete);
    }

    protected override void SetSprite(Sprite sprite)
    {
        Image.sprite = sprite;
        if (_isSetNativeSize)
        {
            Image.SetNativeSize();
        }
    }
}
