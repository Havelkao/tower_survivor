//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AddressableAssets;

//public class ScriptableObjectLoader : MonoBehaviour
//{
//    public string addressableGroupName = "ScriptableObjectsGroup"; // Addressable Group Name

//    void Start()
//    {
//        LoadAllScriptableObjects();
//    }

//    void LoadAllScriptableObjects()
//    {
//        // Asynchronously load all assets from the specified Addressable Group
//        Addressables.LoadResourceLocationsAsync(addressableGroupName).Completed += OnAddressablesLoaded;
//    }

//    void OnAddressablesLoaded(AsyncOperationHandle<IList<UnityEngine.ResourceManagement.ResourceLocations.IResourceLocation>> handle)
//    {
//        if (handle.Status == AsyncOperationStatus.Succeeded)
//        {
//            foreach (var location in handle.Result)
//            {
//                // Load the ScriptableObject from the Addressable
//                Addressables.LoadAssetAsync<ScriptableObject>(location).Completed += OnAssetLoaded;
//            }
//        }
//    }

//    void OnAssetLoaded(AsyncOperationHandle<ScriptableObject> handle)
//    {
//        if (handle.Status == AsyncOperationStatus.Succeeded)
//        {
//            ScriptableObject obj = handle.Result;
//            Debug.Log("Loaded: " + obj.name);
//            // Do something with the loaded ScriptableObject (e.g., store it in a list)
//        }
//    }
//}
