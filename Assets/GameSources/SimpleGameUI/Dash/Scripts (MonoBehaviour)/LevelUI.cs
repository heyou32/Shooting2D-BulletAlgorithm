using UnityEngine;
using UnityEngine.UI;
 

namespace FrameWork.UI.SimpleGameUI
{
    public class LevelUI : MonoBehaviour
    {
        public int format;
        public string prefix;
        public string suffix;
        public Text text;

        void Awake()
        {
            if (text == null) text = GetComponent<Text>();
        }

        void Update()
        {
            if (SimpleGameUI.instance == null) return;

            var count = SimpleGameUI.instance.GetCurrentLevel().ToString();

            if (format > 0) count = StringHelpers.Format(count, format);

            if (prefix != "") count = prefix + count;
            if (suffix != "") count += suffix;

            text.text = count;
        }
    }
}