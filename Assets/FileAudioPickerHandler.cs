using SimpleFileBrowser;
using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class FileAudioPickerHandler : MonoBehaviour
{
    [SerializeField] TaskInfoSO _taskInfoSo;
    [SerializeField] string _audioName;
    private Button pickFileButton;
    [SerializeField] TextMeshProUGUI _fileExcavatorSoundName;
    [SerializeField] TextMeshProUGUI _fileEnviromentSoundName;

    private void Start()
    {
        string path = PlayerPrefs.GetString($"Audio {_audioName}");
        if (_audioName.ToLower() == "excavator")
        {
            _fileExcavatorSoundName.text = string.IsNullOrEmpty(path) ? "Excavator Default" : Path.GetFileName(path);
        }
        else if (_audioName.ToLower() == "enviroment")
        {
            _fileEnviromentSoundName.text = string.IsNullOrEmpty(path) ? "Enviroment Default" : Path.GetFileName(path);
        }
        InitFileBrowser();
    }

    private void InitFileBrowser()
    {
        FileBrowser.DisplayedEntriesFilter += (entry) =>
        {
            if (entry.IsDirectory)
                return true;

            return entry.Name.EndsWith(".mp3") ||
           entry.Name.EndsWith(".wav") ||
           entry.Name.EndsWith(".ogg") ||
           entry.Name.EndsWith(".aiff") ||
           entry.Name.EndsWith(".aac");
        };
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Audio Files", ".mp3"));
        FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe");
        FileBrowser.AddQuickLink("Users", Application.dataPath + "/Resources", null);
    }

    // Start is called before the first frame update
    private void OnEnable()
    {
        pickFileButton = GetComponent<Button>();
        pickFileButton.onClick.AddListener(ChooseAudioFile);
    }

    private void ChangeNoise(string[] paths)
    {
        Debug.Log(string.Join("", paths));
        StartCoroutine(LoadAndPlayAudio(paths[0]));

    }


    IEnumerator LoadAndPlayAudio(string path)
    {
        AudioClip audioClip = null;

        if (File.Exists(path))
        {
            using UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file://" + path, AudioType.MPEG);
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                audioClip = DownloadHandlerAudioClip.GetContent(www);
                if (audioClip.loadType != AudioClipLoadType.DecompressOnLoad)
                {
                    audioClip.LoadAudioData();
                }
                _taskInfoSo.SetClip(_audioName, audioClip);
                PlayerPrefs.SetString($"Audio {_audioName}", path);
                if (_audioName.ToLower() == "excavator")
                {
                    _fileExcavatorSoundName.text = string.IsNullOrEmpty(path) ? "Excavator Default" : Path.GetFileName(path);
                }
                else if (_audioName.ToLower() == "enviroment")
                {
                    _fileEnviromentSoundName.text = string.IsNullOrEmpty(path) ? "Enviroment Default" : Path.GetFileName(path);
                }
            }
        }
    }


    private void OnDisable()
    {
        pickFileButton.onClick.RemoveListener(ChooseAudioFile);
    }


    private void ChooseAudioFile()
    {
        StartCoroutine(ShowLoadDialogCoroutine());
    }

    IEnumerator ShowLoadDialogCoroutine()
    {

        yield return FileBrowser.ShowLoadDialog(
    onSuccess: ChangeNoise,
    onCancel: () => { Debug.Log("File selection was cancelled."); },
    pickMode: FileBrowser.PickMode.Files,
    allowMultiSelection: false,
    initialPath: null,
    title: "Select an Audio File",
    loadButtonText: "Select"
);

        //if (FileBrowser.Success)
        //    ChangeNoise(FileBrowser.Result); // FileBrowser.Result is null, if FileBrowser.Success is false
    }

}
