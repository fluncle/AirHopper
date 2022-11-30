using System.Collections.Generic;

/// <summary>
/// 共通設定マスタデータ
/// </summary>
public class CommonMasterData
{
    /// <summary>
    /// データの種類
    /// </summary>
    private enum DataType
    {
        jumpCount = 1,
        jumpPower,
        gravityScale,
        swipeTimeScale,
        deadLineSpeed,
    }

    /// <summary>値を定義する行</summary>
    private const int VALUE_COLUMN = 1;

    private int _jumpCount;
    public int JumpCount => _jumpCount;

    private int _jumpPower;
    public int JumpPower => _jumpPower;

    private float _gravityScale;
    public float GravityScale => _gravityScale;

    private float _swipeTimeScale;
    public float SwipeTimeScale => _swipeTimeScale;

    private float _deadLineSpeed;
    public float DeadLineSpeed => _deadLineSpeed;

    public CommonMasterData(IList<IList<object>> sheetList)
    {
        _jumpCount = int.Parse(sheetList[(int)DataType.jumpCount][VALUE_COLUMN].ToString());
        _jumpPower = int.Parse(sheetList[(int)DataType.jumpPower][VALUE_COLUMN].ToString());
        _gravityScale = float.Parse(sheetList[(int)DataType.gravityScale][VALUE_COLUMN].ToString());
        _swipeTimeScale = float.Parse(sheetList[(int)DataType.swipeTimeScale][VALUE_COLUMN].ToString());
        _deadLineSpeed = float.Parse(sheetList[(int)DataType.deadLineSpeed][VALUE_COLUMN].ToString());
    }
}
