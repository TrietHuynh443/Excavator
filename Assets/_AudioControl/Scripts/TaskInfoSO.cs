using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Networking;

enum EAudioType
{
    EXCAVATOR = 0,
    ENVIROMENT = 1,
}
public enum ETaskDescription
{
    FORWARD = 0,
    BACKWARD = 1,
    TURN_LEFT = 2,
    TURN_RIGHT = 3,
    LOADING = 4,
    UNLOADING = 5,
    FINISH = 6,

}

[CreateAssetMenu(fileName = "TaskInfoSO", menuName = "ScriptableObjects/TaskInfoSO")]
public class TaskInfoSO : ScriptableObject
{
    [SerializeField] private TaskInfo[] _tasks;
    [SerializeField] private DefaultAudioSoundSO _soundSO;
    [SerializeField] private Sprite _goForwardSprite;
    [SerializeField] private Sprite _turnLeftSprite;
    [SerializeField] private Sprite _turnRightSprite;
    [SerializeField] private Sprite _turnBackSprite;
    [SerializeField] private Sprite _loadingSprite;
    [SerializeField] private Sprite _unLoadingSprite;

    private static TaskInfo _defaultTask = new TaskInfo();

    public async void SetSource(string name, AudioSource audioSource)
    {
        string lowerName = name.ToLower();
        foreach (var sound in _soundSO.DefaultSounds)
        {
            if (sound.Name.ToLower() == lowerName)
            {
                sound.Source = audioSource;

                string audioClipPath = PlayerPrefs.GetString($"Audio {sound.Name}");
                if (string.IsNullOrEmpty(audioClipPath))
                {
                    sound.Source.clip = sound.DefaultAudioClip;
                    if (sound.DefaultAudioClip != null && !sound.DefaultAudioClip.preloadAudioData)
                    {
                        sound.DefaultAudioClip.LoadAudioData();
                    }
                }
                else
                {
                    sound.Source.clip = await LoadAudio(audioClipPath);
                    sound.Source.clip.name = sound.Name;
                }
            }
        }
    }

    private async Task<AudioClip> LoadAudio(string path)
    {
        if (File.Exists(path))
        {
            using UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file://" + path, AudioType.MPEG);
            {

                var operation = www.SendWebRequest();

                while (!operation.isDone)
                {
                    await Task.Yield();
                }

                if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogError(www.error);
                }
                else
                {
                    var audio = DownloadHandlerAudioClip.GetContent(www);
                    if (!audio.preloadAudioData)
                    {
                        audio.LoadAudioData();
                    }
                    return audio;
                }
            }
        }
        return null;
    }

    public AudioSource GetSource(string name)
    {
        string lowerName = name.ToLower();
        foreach (var sound in _soundSO.DefaultSounds)
        {
            if (sound.Name.ToLower() == lowerName)
            {
                return sound.Source;
            }
        }
        return null;
    }

    public void SetClip(string name, AudioClip clip)
    {
        string lowerName = name.ToLower();
        foreach (var sound in _soundSO.DefaultSounds)
        {
            if (sound.Name.ToLower() == lowerName)
            {
                bool isPlaying = sound.Source.isPlaying;
                sound.Clip = clip;
                sound.Source.clip = clip;
                if (isPlaying) sound.Source.Play();
            }
        }
    }

    public TaskInfo GetTask(string name)
    {
        foreach (var task in _tasks)
        {
            if (task.Name == name)
                return task;
        }
        return _defaultTask;
    }

    public Queue<string> GetTaskNameQueue()
    {
        Queue<string> queue = new();
        foreach (var task in _tasks)
        {
            queue.Enqueue(task.Name);
        }
        return queue;
    }

    public void StopSound(string name)
    {
        GetSource(name)?.Stop();
    }

    public IEnumerator PlaySound(string name)
    {
        AudioSource audioSource = GetSource(name);
        if (audioSource == null || audioSource.isPlaying)
        {
            yield return null;
        }
        else if (audioSource.clip.loadState is not AudioDataLoadState.Loaded)
        {
            audioSource.clip.LoadAudioData();
            yield return null;
        }
        audioSource.Play();
    }

    public (string, Sprite) GetDescription(ETaskDescription description)
    {
        return description switch
        {
            ETaskDescription.LOADING => ("Lifting the rock", _loadingSprite),
            ETaskDescription.TURN_LEFT => ("Turning Left", _turnLeftSprite),
            ETaskDescription.TURN_RIGHT => ("Turning Right", _turnRightSprite),
            ETaskDescription.FORWARD => ("Go Forward", _goForwardSprite),
            ETaskDescription.BACKWARD => ("Go Backward", _turnBackSprite),
            ETaskDescription.UNLOADING => ("Unload the rocks", _unLoadingSprite),
            ETaskDescription.FINISH => ("You've finished", null),

            _ => ("Follow an arrow", null),
        };
    }
}

[Serializable]
public struct TaskInfo
{
    public int Id;
    public string Name;
    public bool IsEnviromentQuiet;
    public bool IsExcavatorQuiet;
    public ETaskDescription Description;
}


