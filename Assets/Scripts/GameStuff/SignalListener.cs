using UnityEngine;
using UnityEngine.Events;

public class SignalListener : MonoBehaviour
{
    public SignalSender signalSender;
    public UnityEvent signalEvent;
    
    public void OnSignalRaised()
    {
        signalEvent.Invoke();
    }

    void OnEnable()
    {
        signalSender.RegisterListener(this);
    }

    void OnDisable()
    {
        signalSender.DeRegisterListener(this);
    }
}
