using System;
using System.Collections;
using UnityEngine;

namespace GAMO.Hesman.Utilities
{
    public static class ResourcesHelper
    {
        public static void Load<T>(this MonoBehaviour monoBehaviour, string path, Action<T> onLoaded, Action onLoading = null, ThreadPriority threadPriority = ThreadPriority.Low) where T : class
        {
            monoBehaviour.StartCoroutine(load(path, onLoaded, onLoading, threadPriority));
        }
        public static void Load<T, T1>(this MonoBehaviour monoBehaviour, string path, T1 arg1, Action<T, T1> onLoaded, Action onLoading = null, ThreadPriority threadPriority = ThreadPriority.Low) where T : class
        {
            monoBehaviour.StartCoroutine(load(path, arg1, onLoaded, onLoading, threadPriority));
        }
        public static void Load<T, T1, T2>(this MonoBehaviour monoBehaviour, string path, T1 arg1, T2 arg2, Action<T, T1, T2> onLoaded, Action onLoading = null, ThreadPriority threadPriority = ThreadPriority.Low) where T : class
        {
            monoBehaviour.StartCoroutine(load(path, arg1, arg2, onLoaded, onLoading, threadPriority));
        }
        public static void Load<T, T1, T2, T3>(this MonoBehaviour monoBehaviour, string path, T1 arg1, T2 arg2, T3 arg3, Action<T, T1, T2, T3> onLoaded, Action onLoading = null, ThreadPriority threadPriority = ThreadPriority.Low) where T : class
        {
            monoBehaviour.StartCoroutine(load(path, arg1, arg2, arg3, onLoaded, onLoading, threadPriority));
        }
        private static IEnumerator load<T>(string path, Action<T> onLoaded, Action onLoading, ThreadPriority threadPriority) where T : class
        {
            ResourceRequest resourceRequest = Resources.LoadAsync(path);
            resourceRequest.priority = (int)threadPriority;
            if (onLoading != null)
                onLoading.Invoke();
            while (!resourceRequest.isDone)
            {
                yield return new WaitForEndOfFrame();
            }
            if (resourceRequest.asset == null)
            {

#if GAMO_DEBUG
                Debug.LogErrorFormat("can not load asset at path:  {0}", path);
#endif

                yield break;
            }
            if (onLoaded != null)
                onLoaded.Invoke(((GameObject)resourceRequest.asset).GetComponent<T>());
        }
        private static IEnumerator load<T, T1>(string path, T1 arg1, Action<T, T1> onLoaded, Action onLoading, ThreadPriority threadPriority) where T : class
        {
            ResourceRequest resourceRequest = Resources.LoadAsync(path);
            resourceRequest.priority = (int)threadPriority;
            if (onLoading != null)
                onLoading.Invoke();
            while (!resourceRequest.isDone)
            {
                yield return new WaitForEndOfFrame();
            }
            if (resourceRequest.asset == null)
            {

#if GAMO_DEBUG
                Debug.LogErrorFormat("can not load asset at path: {0}", path);
#endif

                yield break;
            }
            if (onLoaded != null)
                onLoaded.Invoke(((GameObject)resourceRequest.asset).GetComponent<T>(), arg1);
        }
        private static IEnumerator load<T, T1, T2>(string path, T1 arg1, T2 arg2, Action<T, T1, T2> onLoaded, Action onLoading, ThreadPriority threadPriority) where T : class
        {
            ResourceRequest resourceRequest = Resources.LoadAsync(path);
            resourceRequest.priority = (int)threadPriority;
            if (onLoading != null)
                onLoading.Invoke();
            while (!resourceRequest.isDone)
            {
                yield return new WaitForEndOfFrame();
            }
            if (resourceRequest.asset == null)
            {

#if GAMO_DEBUG
                Debug.LogErrorFormat("can not load asset at path: {0}", path);
#endif

                yield break;
            }
            if (onLoaded != null)
                onLoaded.Invoke(((GameObject)resourceRequest.asset).GetComponent<T>(), arg1, arg2);
        }
        private static IEnumerator load<T, T1, T2, T3>(string path, T1 arg1, T2 arg2, T3 arg3, Action<T, T1, T2, T3> onLoaded, Action onLoading, ThreadPriority threadPriority) where T : class
        {
            ResourceRequest resourceRequest = Resources.LoadAsync(path);
            resourceRequest.priority = (int)threadPriority;
            if (onLoading != null)
                onLoading.Invoke();
            while (!resourceRequest.isDone)
            {
                yield return new WaitForEndOfFrame();
            }
            if (resourceRequest.asset == null)
            {

#if GAMO_DEBUG
                Debug.LogErrorFormat("can not load asset at path: {0}", path);
#endif

                yield break;
            }
            if (onLoaded != null)
                onLoaded.Invoke(((GameObject)resourceRequest.asset).GetComponent<T>(), arg1, arg2, arg3);
        }
        public static void UnLoadAsset(this MonoBehaviour mono, Action callBack = null)
        {            
            Resources.UnloadUnusedAssets();
            if (callBack != null) callBack.Invoke();
        }
    }
}
