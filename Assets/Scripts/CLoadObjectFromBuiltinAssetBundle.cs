using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CLoadObjectFromBuiltinAssetBundle : MonoBehaviour
{
    public string objectName = string.Empty;
    public string bundleName = string.Empty;
    public string bundlePath = string.Empty;

    private IEnumerator Start()
    {
        if (!string.IsNullOrEmpty(bundlePath))
        {
            if (!Directory.Exists(bundlePath))
            {
                Debug.Log("경로가 존재하지 않습니다.");
                yield break;
            }

            string pathAndName = Path.Combine(bundlePath, bundleName);

            AssetBundleCreateRequest bundleRequest = AssetBundle.LoadFromFileAsync(pathAndName);
            yield return bundleRequest;
            AssetBundle bundle = bundleRequest.assetBundle;

            if (bundle != null)
            {
                AssetBundleRequest assetRequest = bundle.LoadAssetAsync<GameObject>(objectName);
                yield return assetRequest;

                if (assetRequest.asset != null)
                {
                    Instantiate(assetRequest.asset, this.transform, false);
                }
                else
                {
                    Debug.Log("에셋이 존재하지 않습니다.");
                }
            }
            else
            {
                Debug.Log("번들이 존재하지 않습니다.");
            }
        }
    }

    private void OnDestroy()
    {
        AssetBundle.UnloadAllAssetBundles(true);
    }
}
