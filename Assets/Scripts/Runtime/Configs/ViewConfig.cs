using UnityEngine;

namespace VBM {
    [System.Serializable]
    public class ViewConfig {
        [PropertyToEnumDrawerAttribute]
        public string name;
        public string assetPath;
        public GameObject prefab;
        [PropertyToEnumDrawerAttribute]
        public int layer;
        public ViewShowRule showRule;
        public ViewHideRule hideRule;

        public void LoadAsset(System.Action<GameObject> completed) {
            if (prefab != null)
                completed(Object.Instantiate(prefab));
        }
    }
}