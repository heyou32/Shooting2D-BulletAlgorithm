using UnityEngine;
public class AlphaEffects : MonoBehaviour
{
    [System.Serializable]
    // Fade in datatype.
    public class FadeInSettings
    {
        // Fade speed.
        public float speed = 1;
    }

    [System.Serializable]
    // Fade out datatype.
    public class FadeOutSettings
    {
        // Fade speed.
        public float speed = 1;
    }

    [System.Serializable]
    // Ping pong datatype.
    public class PingPongSettings
    {
        // Fade speed.
        public float speed = 1;
        // Lowest possible value.
        public float min = .25f;
        // Highest possible value.
        public float max = 1;
        // Whether to start at random value or not.
        public bool startRandom;
    }

    public enum Mode { PingPong, FadeOut, FadeIn }

    // Mode can be Mode.PingPong, Mode.FadeOut or Mode.FadeIn
    public Mode mode = Mode.PingPong;
    // Have a look at the fade in datatype for settings.
    public FadeInSettings fadeInSettings;
    // Have a look at the fade out datatype for settings.
    public FadeOutSettings fadeOutSettings;
    // Have a look at the ping pong datatype for settings.
    public PingPongSettings pingPongSettings;
    // spriteRenderer AlphaEffects is using.
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();

        if (mode == Mode.PingPong)
        {
            if (pingPongSettings.startRandom)
            {
                var color = spriteRenderer.color;

                color.a = Random.Range(pingPongSettings.min, pingPongSettings.max);

                if (color.a < pingPongSettings.min)
                {
                    color.a = pingPongSettings.min;

                    pingPongSettings.speed = Mathf.Abs(pingPongSettings.speed);
                }

                if (color.a > pingPongSettings.max)
                {
                    color.a = pingPongSettings.max;

                    pingPongSettings.speed = -Mathf.Abs(pingPongSettings.speed);
                }

                spriteRenderer.color = color;
            }
        }
        else if (mode == Mode.FadeIn)
        {
            var color = spriteRenderer.color;
            color.a = 0;
            spriteRenderer.color = color;
        }

    }

    void Update()
    {
        if (mode == Mode.PingPong)
        {
            _UpdatePingPong();
        }
        else if (mode == Mode.FadeOut)
        {
            _UpdateFadeOut();
        }
        else if (mode == Mode.FadeIn && spriteRenderer.color.a != 1)
        {
            _UpdateFadeIn();
        }
    }

    void _UpdateFadeIn()
    {
        var color = spriteRenderer.color;

        color.a += fadeOutSettings.speed * Time.deltaTime;

        if (color.a > 1)
        {
            color.a = 1;

            spriteRenderer.color = color;
        }
        else
        {
            spriteRenderer.color = color;
        }
    }

    void _UpdateFadeOut()
    {
        var color = spriteRenderer.color;

        color.a -= fadeOutSettings.speed * Time.deltaTime;

        if (color.a < 0)
        {
            color.a = 0;

            spriteRenderer.color = color;

            Destroy(gameObject);
        }
        else
        {
            spriteRenderer.color = color;
        }
    }

    void _UpdatePingPong()
    {
        var color = spriteRenderer.color;

        color.a += pingPongSettings.speed * Time.deltaTime;

        if (color.a < pingPongSettings.min)
        {
            color.a = pingPongSettings.min;

            pingPongSettings.speed = Mathf.Abs(pingPongSettings.speed);
        }

        if (color.a > pingPongSettings.max)
        {
            color.a = pingPongSettings.max;

            pingPongSettings.speed = -Mathf.Abs(pingPongSettings.speed);
        }

        spriteRenderer.color = color;
    }
}