using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class NoteManager : MonoSingleton<NoteManager>
{
    [SerializeField] private TextAsset _text = null;
    [SerializeField] private AudioSource _audioSource = null;

    private List<Note> _notes = new List<Note>();
    private int _currentNoteIndex = 0;

    private void Start()
    {
        LoadNote();
    }

    private void Update()
    {
        var events = _notes[_currentNoteIndex].events;
        float remainingTime = _notes[_currentNoteIndex].time - _audioSource.time;

        while (events.Count > 0 && remainingTime <= events.First.Value.preDelay)
        {
            NoteEvent noteEvent = events.First.Value;
            Assert.IsNotNull(noteEvent.OnNoteEvent);

            noteEvent.OnNoteEvent?.Invoke(_currentNoteIndex, remainingTime);

            events.RemoveFirst();

            if (events.Count == 0)
            {
                _currentNoteIndex++;
            }
        }
    }

    public void AddEvent(int noteIndex, NoteEvent noteEvent)
    {
        _notes[noteIndex].AddEvent(noteEvent);
    }

    public void RemoveEvent(int noteIndex, NoteEvent noteEvent)
    {
        _notes[noteIndex].RemoveEvent(noteEvent);
    }

    void LoadNote()
    {
        string noteTimeString = _text.ToString();
        var noteTimes = noteTimeString.Split('\n');
        foreach (var time in noteTimes)
        {
            _notes.Add(new Note(float.Parse(time)));
        }
    }
}
