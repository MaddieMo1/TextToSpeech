using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TextToSpeech_ZH : MonoBehaviour
{
    [Header("音源")]
    public AudioSource _Audio;

    //全局变量
    public static TextToSpeech_ZH _World;

    //网页文字转语音
    private string _Url;




    private void Start()
    {
        _World = this;
        _World.StartCoroutine(GetAudioClip("世界"));
    }


    //获取 Web网页音源信息并播放
    private IEnumerator GetAudioClip(string AudioText)
    {
        _Url = "https://tsn.baidu.com/text2audio?tex=" + AudioText + "+&lan=zh&cuid=7919875968150074&ctp=1&aue=6&tok=25.3141e5ae3aa109abb6fc9a8179131181.315360000.1886566986.282335-17539441";
        using (UnityWebRequest _AudioWeb = UnityWebRequestMultimedia.GetAudioClip(_Url, AudioType.WAV))
        {

            yield return _AudioWeb.SendWebRequest();
            if (_AudioWeb.isNetworkError)
            {
                yield break;
            }
            AudioClip _Cli = DownloadHandlerAudioClip.GetContent(_AudioWeb);
            _Audio.clip = _Cli;
            _Audio.Play();
        }
    }
}
