using UnityEngine;

namespace PupilLabs
{
    public class DontDestroy : MonoBehaviour
    {
        /*/ 最初につくったやつを維持する方法
        private static DontDestroy instance = null;
        public static DontDestroy Instance => instance ?? (instance = GameObject.FindWithTag("GameController").GetComponent<DontDestroy>());
        void Awake()
        {
            if (this != Instance)
            {
                Destroy(this.gameObject);
                return;
            }
            DontDestroyOnLoad(this.gameObject);　
        }

        private void OnDestroy()
        {
            if (this == Instance) instance = null;
        }//*/

        private static DontDestroy instance = null;
        public static DontDestroy Instance => instance ?? (instance = GameObject.FindWithTag("GameController").GetComponent<DontDestroy>());
        private void Awake()
        {
            if (this != Instance)
            {
                Destroy(Instance.gameObject);
            }
            DontDestroyOnLoad(this.gameObject); // ここ以外書き足し
        }
    }
}