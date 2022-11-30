using System;
using System.Collections;

/// <summary>
/// マスタデータの管理
/// </summary>
public class MasterDataManager
{
    /// <summary>シングルトンインスタンス</summary>
    private static MasterDataManager _instance = new MasterDataManager();
    public static MasterDataManager Instance => _instance;

    /// <summary>
    /// 利用するスプレッドシートのID
    /// https://docs.google.com/spreadsheets/d/1VkTP6o34ZDNwhC0Cir3RpMnjlqD5lYbcaNgZ_O3wc44
    /// </summary>
    // 適宜使用するシートのIDに置き換えて利用してください
    private const string SHEET_ID = "1VkTP6o34ZDNwhC0Cir3RpMnjlqD5lYbcaNgZ_O3wc44";

    private const string COMMON_SHEET_NAME = "共通";
    public CommonMasterData CommonMaster { get; private set; }

    private MasterDataManager() { }

    /// <summary>
    /// マスタの非同期読み込み
    /// </summary>
    /// <param name="onComplete">読み込み完了時のコールバック</param>
    /// <param name="isPrint">読み込んだデータをログ出力するか否か</param>
    public IEnumerator LoadMaster(Action onComplete = null, bool isPrint = false)
    {
        yield return GoogleSheetsReader.LoadSheet(SHEET_ID, COMMON_SHEET_NAME, list => CommonMaster = new CommonMasterData(list), isPrint);

        onComplete?.Invoke();
    }
}
