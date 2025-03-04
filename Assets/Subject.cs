using System.Collections;
using UnityEngine;

public abstract class Subject : MonoBehaviour
{

    private ArrayList _observers = new ArrayList();

    // ������ ����
    public void Attach(Observer observer)
    {
        _observers.Add(observer);
    }

    // ������ Ż��
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
