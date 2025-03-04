using System.Collections;
using UnityEngine;

public abstract class Subject : MonoBehaviour
{

    private ArrayList _observers = new ArrayList();

    // 可历滚 何馒
    public void Attach(Observer observer)
    {
        _observers.Add(observer);
    }

    // 可历滚 呕馒
    public void Detach(Observer observer)
    {
        _observers.Remove(observer);
    }

    public void NotifyObservers()
    {
        foreach (Observer observer in _observers)
        {
            observer.Notify(this);
        }
    }

}
