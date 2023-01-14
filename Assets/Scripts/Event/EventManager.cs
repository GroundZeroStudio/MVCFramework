using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EventManager : TSingleton<EventManager>
{
    private Dictionary<EventType, List<Action<List<object>>>> Events = new Dictionary<EventType, List<Action<List<object>>>>(); 
    public void Register(EventType rEventType,Action<List<object>> rAction)
    {
        if (this.Events == null) return;

        List<Action<List<object>>> rEvent = null;
        if (this.Events.TryGetValue(rEventType, out rEvent))
        {
            if (rEvent == null)
            {
                rEvent = new List<Action<List<object>>>();
            }
            else
            {
                if (!rEvent.Contains(rAction))
                {
                    rEvent.Add(rAction);
                }
            }
        }
        else
        {
            rEvent = new List<Action<List<object>>>() { rAction };
            this.Events.Add(rEventType, rEvent);
        }
    }

    public void UnRegister(EventType rEventType,Action<List<object>> rAction)
    {
        if (this.Events == null) return;

        List<Action<List<object>>> rEvent = null;
        if (this.Events.TryGetValue(rEventType, out rEvent))
        {
            if (rEvent != null)
                rEvent.Remove(rAction);
        }
    }

    public void ExecuteEvent(EventType rEventType, params object[] rObj)
    {
        if (this.Events == null) return;
        List<object> rList = new List<object>();
        rList.AddRange(rObj);
        if(this.Events.TryGetValue(rEventType,out var rEventCallbacks))
        {
            for (int i = 0; i < rEventCallbacks.Count; i++)
            {
                rEventCallbacks[i].Invoke(rList);
            }
        }
    }
}