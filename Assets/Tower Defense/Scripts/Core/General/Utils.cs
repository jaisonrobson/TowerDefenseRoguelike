using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Reflection;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Core.General
{
    public static class Utils
    {
#if UNITY_EDITOR
        public static List<T> FindAssetsByType<T>() where T : UnityEngine.Object
        {
            List<T> assets = new List<T>();
            string[] guids = AssetDatabase.FindAssets(string.Format("t:{0}", typeof(T)));
            for (int i = 0; i < guids.Length; i++)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
                T asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
                if (asset != null)
                {
                    assets.Add(asset);
                }
            }
            return assets;
        }
#endif
        public static T[,] ResizeMatrix<T>(T[,] original, int rows, int cols)
        {
            T[,] newMatrix = new T[rows, cols];
            int minX = Mathf.Min(original.GetLength(0), newMatrix.GetLength(0));
            int minY = Mathf.Min(original.GetLength(1), newMatrix.GetLength(1));

            for (int i = 0; i < minX; ++i)
                Array.Copy(original, i * original.GetLength(1), newMatrix, i * newMatrix.GetLength(1), minY);

            return newMatrix;
        }

        public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> source,
        int chunkSize)
        {
            // Validate parameters.
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (chunkSize <= 0) throw new ArgumentOutOfRangeException(nameof(chunkSize),
                "The chunkSize parameter must be a positive value.");

            // Call the internal implementation.
            return source.ChunkInternal(chunkSize);
        }

        private static IEnumerable<IEnumerable<T>> ChunkInternal<T>(
        this IEnumerable<T> source, int chunkSize)
        {
            // Validate parameters.
            Debug.Assert(source != null);
            Debug.Assert(chunkSize > 0);

            // Get the enumerator.  Dispose of when done.
            using (IEnumerator<T> enumerator = source.GetEnumerator())
                do
                {
                    // Move to the next element.  If there's nothing left
                    // then get out.
                    if (!enumerator.MoveNext()) yield break;

                    // Return the chunked sequence.
                    yield return ChunkSequence(enumerator, chunkSize);
                } while (true);
        }

        private static IEnumerable<T> ChunkSequence<T>(IEnumerator<T> enumerator,
        int chunkSize)
        {
            // Validate parameters.
            Debug.Assert(enumerator != null);
            Debug.Assert(chunkSize > 0);

            // The count.
            int count = 0;

            // There is at least one item.  Yield and then continue.
            do
            {
                // Yield the item.
                yield return enumerator.Current;
            } while (++count < chunkSize && enumerator.MoveNext());
        }

        public static bool IsGameObjectInsideAnother(Transform pGameObject, Transform pAnother)
        {
            if (pGameObject.root == pAnother.root)
            {
                if (pGameObject != pAnother)
                {
                    if (pGameObject == pGameObject.root) //If encountered root its not inside the another
                        return false;

                    return IsGameObjectInsideAnother(pGameObject.parent, pAnother);
                }

                if (pGameObject == pAnother)
                    return true;
            }

            return false;    
        }

        public static IEnumerable<T> UpdateValueInList<T>(this IEnumerable<T> items, Action<T>
         updateMethod)
        {
            foreach (T item in items)
            {
                updateMethod(item);
            }
            return items;
        }

        public static IEnumerable<T> UpdateValueInStructList<T>(IEnumerable<T> items, Func<T, T>
         updateMethod)
        {
            T[] localItems = items.ToArray();

            for (int i = 0; i < localItems.Count(); i++)
            {
                T structCopy = localItems[i];

                structCopy = updateMethod(structCopy);

                localItems[i] = structCopy;
            }

            return localItems.ToList();
        }

        public static bool IsInLayerMask(int layer, LayerMask mask) => mask == (mask | (1 << layer));
        public static void SetGameObjectAndChildrenLayers(Transform root, int layer)
        {
            root.gameObject.layer = layer;

            var children = root.GetComponentsInChildren<Transform>(includeInactive: true);

            foreach (var child in children)
            {
                child.gameObject.layer = layer;
            }
        }
    }
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////