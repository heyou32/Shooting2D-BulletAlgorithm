using UnityEngine.Events;
public class Initializer : EngineBehaviour
{
    public UnityEvent events;

    public override void OnStart()
    {
        events.Invoke();
    }
}
