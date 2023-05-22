using UnityEngine;

public class IntroEffect : MonoBehaviour
{
    [System.Serializable]
    public class FlickerSettings
    {
        public int count = 15;
        public float sustain = 1f;
        public SpriteRenderer[] spriteRenderer;
        public CollisionBase2D collisionBase;

        float _counter;
        float _timer;

        public void Start(IntroEffect instance)
        {
            if (CheckRenderers()) spriteRenderer = instance.GetComponents<SpriteRenderer>();

            if (instance.flickerSettings.collisionBase == null) instance.flickerSettings.collisionBase = instance.GetComponent<CollisionBase2D>();

            instance.flickerSettings.collisionBase.isSuspended = true;

            bool CheckRenderers()
            {
                if (spriteRenderer.Length == 0) return true;

                for (int i = 0; i < spriteRenderer.Length; i++)
                {
                    if (spriteRenderer[i] == null) return true;
                }
                return false;
            }
        }
        public void Update(IntroEffect instance)
        {
            if (collisionBase.isSuspended == false) return;

            if (_counter == count) collisionBase.isSuspended = false;

            _timer += Time.deltaTime;

            if (_timer > (sustain / 10))
            {
                for (int i = 0; i < spriteRenderer.Length; i++)
                {
                    spriteRenderer[i].enabled = !spriteRenderer[i].enabled;
                }

                _counter += .5f;
                _timer = 0;
            }
        }
    }

    [System.Serializable]
    public class ScaleSettings
    {
        public float speed = 5;

        public void Start(IntroEffect instance)
        {
            _scale = instance.transform.localScale;

            instance.transform.localScale = Vector3.zero;
        }

        public void Update(IntroEffect instance)
        {
            instance.transform.localScale += speed * Vector3.one * Time.deltaTime;

            if (instance.transform.localScale.x >= _scale.x)
            {
                instance.transform.localScale = _scale;

                instance.enabled = false;
            }
        }

        Vector3 _scale;
    }

    public enum Mode { Flicker, Scale };

    public Mode mode;
    public FlickerSettings flickerSettings;
    public ScaleSettings scaleSettings;

    void Start()
    {
        if (mode == Mode.Flicker)
        {
            flickerSettings.Start(this);
        }
        else if (mode == Mode.Scale)
        {
            scaleSettings.Start(this);
        }
    }
    void Update()
    {
        if (mode == Mode.Flicker)
        {
            flickerSettings.Update(this);
        }
        else if (mode == Mode.Scale)
        {
            scaleSettings.Update(this);
        }
    }
}