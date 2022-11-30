using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// スプレッドシートの読み込み関連のクラス
/// </summary>
public class GoogleSheetsReader
{
    private const string URI_FORMAT = "https://docs.google.com/spreadsheets/d/{0}/gviz/tq?tqx=out:csv&sheet={1}";

    /// <summary>
    /// スプレッドシートの読み込み
    /// </summary>
    /// <param name="sheetId">シートID(スプレッドシートのURL内にあり)</param>
    /// <param name="sheetName">シート内のタブ名</param>
    /// <param name="onComplete">読み込み完了時のコールバック</param>
    /// <param name="isPrint">読み込んだデータをログ出力するか否か</param>
    public static IEnumerator LoadSheet(string sheetId, string sheetName, Action<IList<IList<object>>> onComplete = null, bool isPrint = false)
    {
        UnityWebRequest request = UnityWebRequest.Get(string.Format(URI_FORMAT, sheetId, sheetName));
        yield return request.SendWebRequest();

        if(request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            yield break;
        }

        if (isPrint)
        {
            PrintCSV(sheetName, request.downloadHandler.text);
        }

        var list = CSVToList(request.downloadHandler.text);
        onComplete?.Invoke(list);
    }

    private static IList<IList<object>> CSVToList(string csv)
    {
        var list = new List<IList<object>>();
        var reader = new StringReader(csv);
        while (reader.Peek() != -1)
        {
            // 一行ずつ読み込み
            var line = reader.ReadLine();
            var oneLine = new List<object>();
            list.Add(oneLine);

            // 行のセルは,で区切られる
            var elements = line.Split(',');
            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i] == "\"\"")
                {
                    // 空白は除去
                    continue;
                }

                elements[i] = elements[i].TrimStart('"').TrimEnd('"');
                oneLine.Add(elements[i]);
            }
        }
        return list;
    }

    private static void PrintCSV(string sheetName, string csv)
    {
        var log = $"▼{sheetName}";
        var reader = new StringReader(csv);
        while (reader.Peek() != -1)
        {
            log += "\n";

            // 一行ずつ読み込み
            var line = reader.ReadLine();
            // 行のセルは,で区切られる
            var elements = line.Split(',');
            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i] == "\"\"")
                {
                    // 空白は除去
                    continue;
                }
                elements[i] = elements[i].TrimStart('"').TrimEnd('"');

                log += elements[i];
                if(i < elements.Length - 1)
                {
                    log += ", ";
                }
            }
        }
        Debug.Log(log);
    }
}
