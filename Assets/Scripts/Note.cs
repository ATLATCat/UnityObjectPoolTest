using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteEvent
{
    public delegate void NoteEventHandler(int noteIndex, float time);

    public NoteEvent(float preDelay, NoteEventHandler preActionEvent)
    {
        this.preDelay = preDelay;
        OnNoteEvent = preActionEvent;
    }

    public float preDelay;
    public NoteEventHandler OnNoteEvent;
}

public class Note
{
    public Note(float time)
    {
        this.time = time;
        _events = new LinkedList<NoteEvent>();
    }

    public float time { get; set; }
    public LinkedList<NoteEvent> events {  get { return _events; } }

    private LinkedList<NoteEvent> _events;

    public void AddEvent(float preDelay, NoteEvent.NoteEventHandler preActionEvent)
    {
        AddEvent(new NoteEvent(preDelay, preActionEvent));
    }

    public void AddEvent(NoteEvent noteEvent)
    {
        _events.AddLast(noteEvent);
    }

    public void RemoveEvent(NoteEvent noteEvent)
    {
        foreach(var ne in _events)
        {
            if (ne.OnNoteEvent == noteEvent.OnNoteEvent)
            {
                _events.Remove(ne);
                break;
            }
        }
    }
}
