using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public enum StateType
    {
        Idle,
        Dead,
    }

    [SerializeField]
    private SwipeHandler _swipeHandler;
    public SwipeHandler SwipeHandler => _swipeHandler;

    [SerializeField]
    private RectTransform _rect;
    public RectTransform Rect => _rect;

    [SerializeField]
    private Rigidbody2D _rigidbody;
    public Rigidbody2D Rigidbody => _rigidbody;

    [SerializeField]
    private PlayerSpriteAnimation _animation;
    public PlayerSpriteAnimation Animation => _animation;

    [SerializeField]
    private Image _mainImage;
    public Image MainImage => _mainImage;

    [SerializeField]
    private Image _arrowImage;
    public Image ArrowImage => _arrowImage;

    public StateMachine<StateType, PlayerStateBase> Status { get; private set; }

    private int _jumpCount;

    private void Awake()
    {
        _rigidbody.simulated = false;

        Status = new StateMachine<StateType, PlayerStateBase>();
        Status.RegisterState(StateType.Idle, new PlayerStateDefault(this));
        Status.RegisterState(StateType.Dead, new PlayerStateDead(this));
    }

    public void Initialize()
    {
        _rigidbody.simulated = true;

        Status.Transition(StateType.Idle);

        _animation.Initialize();

        _jumpCount = MasterDataManager.Instance.CommonMaster.JumpCount;
        _rigidbody.gravityScale = MasterDataManager.Instance.CommonMaster.GravityScale;
    }

    public void InitializeJumpInput()
    {
        _swipeHandler.OnPointerDownEvent = (basePosition) =>
        {
            if(_jumpCount > 0)
            {
                Time.timeScale = MasterDataManager.Instance.CommonMaster.SwipeTimeScale;
            }
        };

        _swipeHandler.OnBeginDragEvent = (dragPosition, basePosition) =>
        {
            if (_jumpCount > 0)
            {
                _arrowImage.enabled = true;
                RefreshArrow(dragPosition, basePosition);
            }
        };

        _swipeHandler.OnDragEvent = (dragPosition, basePosition) =>
        {
            if (_jumpCount > 0)
            {
                RefreshArrow(dragPosition, basePosition);
            }
        };

        _swipeHandler.OnPointerUpEvent = (dragPosition, basePosition) =>
        {
            Time.timeScale = 1f;

            var jumpVector = -(dragPosition - basePosition).normalized;
            var jumpPower = Mathf.Lerp(0f, MasterDataManager.Instance.CommonMaster.JumpPower, _swipeHandler.Rate);
            Jump(jumpVector, jumpPower);

            _arrowImage.enabled = false;
        };
    }

    private void Update()
    {
        Status.Process();
    }

    private void Jump(Vector2 vector, float jumpPower)
    {
        if(_jumpCount <= 0 || jumpPower <= 0f)
        {
            return;
        }

        _jumpCount--;

        _rigidbody.velocity = Vector2.zero;
        _rigidbody.AddForce(vector * jumpPower, ForceMode2D.Impulse);

        _animation.SetFlipX(vector.x < 0);
        _animation.JumpUp();
    }

    public void OnGround()
    {
        _jumpCount = MasterDataManager.Instance.CommonMaster.JumpCount;
        _animation.Idle();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Status.CurrentState.OnCollisionEnter(collision);
    }

    private void RefreshArrow(Vector2 dragPosition, Vector2 basePosition)
    {
        var eulerAngles = _arrowImage.rectTransform.eulerAngles;
        eulerAngles.z = GetVectorDegree(dragPosition, basePosition);
        _arrowImage.rectTransform.eulerAngles = eulerAngles;

        _arrowImage.rectTransform.localScale = Vector3.one * Mathf.Lerp(0.3f, 1f, _swipeHandler.Rate);
    }

    private float GetVectorDegree(Vector2 basePosition, Vector2 targetPosition)
    {
        float diffX = targetPosition.x - basePosition.x;
        float diffY = targetPosition.y - basePosition.y;
        float radian = Mathf.Atan2(diffY, diffX);
        return radian * Mathf.Rad2Deg;
    }
}
