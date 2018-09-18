using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class CLoadObjectFromRemoteAssetBundle : MonoBehaviour
{
    public string objectName = string.Empty;
    public string objectBundleName = string.Empty;
    public string remoteUrl = string.Empty;

    private IEnumerator Start()
    {
        string bundleUrl = Path.Combine(remoteUrl, objectBundleName);

        if (string.IsNullOrEmpty(bundleUrl))
            yield break;

        UnityWebRequest webRequest = UnityWebRequestAssetBundle.GetAssetBundle(remoteUrl);
        yield return webRequest.SendWebRequest();
        AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(webRequest);
        
        if (bundle != null)
        {
            Object prefab = bundle.LoadAsset(objectName);
            
            if (prefab != null)
            {
                GameObject.Instantiate(prefab, this.transform, false);
            }
        }
    }
}
