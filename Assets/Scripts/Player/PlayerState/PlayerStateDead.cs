using UnityEngine;
using DG.Tweening;

public class PlayerStateDead : PlayerStateBase
{
    public PlayerStateDead(Player player) : base(player) { }

    public override void Enter()
    {
        Time.timeScale = 1f;

        CameraManager.Instance.Param.trackTarget = null;
        CameraManager.Instance.Shake();

        _player.Rigidbody.simulated = false;
        _player.SwipeHandler.Clear();
        _player.ArrowImage.enabled = false;

        _player.Animation.Hurt(4);


        var delay = 0.7f;
        var rect = _player.MainImage.rectTransform;
        DOTween.Sequence()
            .SetDelay(delay)
            .Append(_player.Rect.DOAnchorPosY(_player.Rect.anchoredPosition.y + 200f, 0.5f).SetEase(Ease.OutCubic))
            .Append(_player.Rect.DOAnchorPosY(_player.Rect.anchoredPosition.y - 1000f, 0.75f).SetEase(Ease.InCubic));
        DOTween.Sequence()
            .SetDelay(delay)
            .Append(DOTween.To(() => rect.eulerAngles.z, z => rect.SetAngleZ(z), 360f, 0.5f).SetEase(Ease.Linear).SetLoops(-1));
    }
}
