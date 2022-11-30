using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private LoagingLayer _loagingLayer;

    [SerializeField]
    private CameraManager _caneraMgr;

    [SerializeField]
    private RectTransform _canvasRect;

    [SerializeField]
    private Player _player;

    [SerializeField]
    private Image _deadLine;

    private bool _isInGame;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        _loagingLayer.Play();
        StartCoroutine(MasterDataManager.Instance.LoadMaster(OnLoadComplete));
    }

    private void Start()
    {
        var baseSize = _caneraMgr.BaseSize / (_canvasRect.localScale.x / 0.01f);
        _caneraMgr.SetBaseSize(baseSize);
    }

    public void Reset()
    {
        SceneManager.LoadScene(0);
    }

    private void OnLoadComplete()
    {
        _loagingLayer.End();
        _player.Initialize();
        _isInGame = true;
    }

    public void Update()
    {
        if (!_isInGame)
        {
            return;
        }

        var fixedDeltaTime = Time.deltaTime / Time.timeScale;
        var height = _deadLine.rectTransform.sizeDelta.y + fixedDeltaTime * MasterDataManager.Instance.CommonMaster.DeadLineSpeed;
        _deadLine.rectTransform.SetSizeDeltaY(height);

        height += _deadLine.rectTransform.anchoredPosition.y;

        if (_player.Status.CurrentStateType != Player.StateType.Dead && _player.Rect.anchoredPosition.y < height)
        {
            _player.Status.Transition(Player.StateType.Dead);
            _isInGame = false;
        }
    }
}
