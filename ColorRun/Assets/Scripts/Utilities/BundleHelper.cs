using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BundleHelper
{

    public static void GetAssetAsync<T>(this MonoBehaviour mono, string assetName, AssetBundle _bundle, System.Action<T> onComplete)
    {
        if (_bundle == null) return;
        mono.StartCoroutine(loadAssetAsync(assetName, _bundle, onComplete));
    }


    public static T GetAsset<T>(this MonoBehaviour mono, string assetName, AssetBundle _bundle)
    {
        if (_bundle == null) return default(T);
        T temp = default(T);
        UnityEngine.Object item = _bundle.LoadAsset(assetName);
        if (typeof(T).Equals(typeof(Component)))
            temp = ((GameObject)_bundle.LoadAsset(assetName)).GetComponent<T>();
        else
        {
            try
            {
                temp = (T)System.Convert.ChangeType(item, typeof(T));
            }
            catch (System.Exception)
            {
                return default(T);
            }
        }
        return temp;
    }
    public static List<T> GetAllAsset<T>(this MonoBehaviour mono, AssetBundle _bunde)
    {
        List<T> info = new List<T>();
        UnityEngine.Object[] items = _bunde.LoadAllAssets();
        for (int i = 0; i < items.Length; i++)
        {
            T temp = default(T);
            if (typeof(T).Equals(typeof(Component)))
                temp = ((GameObject)items[i]).GetComponent<T>();
            else
            {
                try
                {
                    temp = (T)System.Convert.ChangeType(items[i], typeof(T));
                }
                catch (System.Exception)
                {
                    continue;
                }
            }
            if (temp != null) info.Add(temp);
        }
        return info;
    }

    public static void GetAllAssetAsync<T>(this MonoBehaviour mono, AssetBundle _bundle, Action<List<T>> onCompleted = null)
    {
        if (_bundle == null) return;
        mono.StartCoroutine(loadAllAssetAsync<T>(_bundle, onCompleted));
    }
    /// <summary>
    /// Load all assets in a bundle
    /// </summary>
    /// <typeparam name="T">type of asset</typeparam>
    /// <param name="monoBehaviour">mono behaviour</param>
    /// <param name="assetBundle">asset bundle which hold these asset</param>
    /// <param name="onProcess">callback on progressing bundle</param>
    /// <param name="onCompleted">callback when complete process bundle</param>
    /// <returns></returns>
    public static IEnumerator getAllAssetAsync<T>(this MonoBehaviour monoBehaviour, AssetBundle assetBundle, Action<int, int> onProcess = null, Action<List<T>> onCompleted = null) where T : class
    {
        if (assetBundle == null)
        {
            yield break;
        }
        yield return loadAllAssetAsync<T>(assetBundle, onProcess, onCompleted);
    }

    private static IEnumerator loadAllAssetAsync<T>(AssetBundle _bundle, System.Action<List<T>> onCompleted = null)
    {
        AssetBundleRequest items = _bundle.LoadAllAssetsAsync();
        yield return items;
        List<T> info = new List<T>();
        for (int i = 0; i < items.allAssets.Length; i++)
        {
            T temp = default(T);
            if (typeof(T).Equals(typeof(Component)))
                temp = ((GameObject)items.allAssets[i]).GetComponent<T>();
            else
            {
                try
                {
                    temp = (T)System.Convert.ChangeType(items.allAssets[i], typeof(T));
                }
                catch
                {
                    continue;
                }
            }
            if (temp != null) info.Add(temp);
        }
        if (onCompleted != null) onCompleted.Invoke(info);
    }
    private static IEnumerator loadAllAssetAsync<T>(AssetBundle assetBundle, Action<int, int> onProcess, Action<List<T>> onCompleted = null) where T : class
    {
        AssetBundleRequest items = assetBundle.LoadAllAssetsAsync();
        yield return items;
        int totalCount = items.allAssets.Length;
        int currentCount = 0;
        List<T> assets = new List<T>();
        for (int i = 0; i < totalCount; i++)
        {
            T asset = default(T);
            if (typeof(T).Equals(typeof(Component)))
            {
                asset = ((GameObject)items.allAssets[i]).GetComponent<T>();
            }
            else
            {
                try
                {
                    asset = Convert.ChangeType(items.allAssets[i], typeof(T)) as T;
                }
                catch
                {
                    continue;
                }
            }
            if (asset != null)
                assets.Add(asset);
            if (onProcess != null)
                onProcess.Invoke(currentCount, totalCount);
        }
        yield return new WaitForEndOfFrame();
        if (onCompleted != null)
            onCompleted.Invoke(assets);
    }
    private static IEnumerator loadAssetAsync<T>(string assetName, AssetBundle _bundle, System.Action<T> onCompleted)
    {
        AssetBundleRequest item = _bundle.LoadAssetAsync(assetName);
        yield return item;
        T info = default(T);
        if (typeof(T).Equals(typeof(Component)))
            info = ((GameObject)item.asset).GetComponent<T>();
        else
        {
            try
            {
                info = (T)System.Convert.ChangeType(item.allAssets, typeof(T));
            }
            catch (System.Exception)
            {
                yield break;
            }
        }
        if (onCompleted != null && info != null) onCompleted.Invoke(info);
    }
}
