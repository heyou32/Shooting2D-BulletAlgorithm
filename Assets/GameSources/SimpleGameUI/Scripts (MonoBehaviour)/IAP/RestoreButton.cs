using UnityEngine;

namespace FrameWork.UI.SimpleGameUI
{
    public class RestoreButton : MonoBehaviour
    {
        void Awake()
        {
            if (Application.isEditor == false && Application.platform != RuntimePlatform.IPhonePlayer) gameObject.SetActive(false);
        }
    }
}