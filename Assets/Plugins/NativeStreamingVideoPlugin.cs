using UnityEngine;

#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
namespace Assets.Plugins
{
    public class NativeStreamingVideoPlugin : IStreamingVideoPlugin
    {
        private GameObject _gameObject;
        private MovieTexture _movieTexture;
        private AudioClip _audioClip;
        private WWW _www;
        private AudioSource _audioSource;

        public NativeStreamingVideoPlugin(string url, GameObject gameObject)
        {
            DebugStatus = "Creating WWW for ogv";
            _gameObject = gameObject;
            _status = StreamingVideoStatus.Unknown;
            _url = url;
            var fullyQualifiedUrl = _url.StartsWith("http://") ? _url : Application.streamingAssetsPath + "/" + _url;
            Debug.Log("Creating www object with url: " + fullyQualifiedUrl);
            _www = new WWW(fullyQualifiedUrl);
            _audioSource = _gameObject.GetComponent<AudioSource>();
            if (_audioSource == null)
            {
                _audioSource = _gameObject.AddComponent<AudioSource>();
            }
        }

        public StreamingVideoStatus Status { get { return _status; } }
        private StreamingVideoStatus _status;

        private string _url;
        public string Url { get { return _url; } set { _url = value; _status = StreamingVideoStatus.Unknown; } }

        public bool IsReadyToPlay
        {
            get { return Status == StreamingVideoStatus.ReadyToPlay; }
        }

        public bool IsPlaying
        {
            get { return Status == StreamingVideoStatus.Playing; }
        }

        public void Play()
        {
            _gameObject.GetComponent<Renderer>().material.mainTexture = _movieTexture;
            _movieTexture.Play();
            if (_audioClip != null)
            {
                _audioSource.PlayOneShot(_audioClip);
            }
        }

        public void Pause()
        {
            _movieTexture.Pause();
        }

        public bool IsDone
        {
            get { return Status == StreamingVideoStatus.Done; }
        }

        public string DebugStatus
        {
            get;
            private set;
        }

        public float Duration { get { return _movieTexture.duration; } }

        public void Update()
        {
             var prevStatus = _status;
             switch (prevStatus)
             {
                 case StreamingVideoStatus.Unknown:
                 case StreamingVideoStatus.Loading:
                     if (_www != null)
                     {
                         if (_www.error != null)
                         {
                             DebugStatus = _www.error;
                             _status = StreamingVideoStatus.Error;
                         }
                         bool movieReady = false;
                         bool audioReady = true;
                         if (_www.movie != null && _www.movie.isReadyToPlay)
                         {
                             Debug.Log("Movie ready");
                             movieReady = true;
                         }
                         //if (_www.movie.audioClip != null)
                         //{
                         //    Debug.Log("Audio Clip not null");
                         //    audioReady = false;
                         //    if (_www.movie.audioClip.loadState == AudioDataLoadState.Loaded)
                         //    {
                         //        Debug.Log("Audio Clip Ready");
                         //        audioReady = true;
                         //    }
                         //}
                         if (movieReady && audioReady)
                         {
                             Debug.Log("Ready To Play");
                             _status = StreamingVideoStatus.ReadyToPlay;
                             _movieTexture = _www.movie;
                             _audioClip = _movieTexture.audioClip;
                         }
                         else
                         {
                             _status = StreamingVideoStatus.Loading;
                         }
                     }
                     break;
                 case StreamingVideoStatus.ReadyToPlay:
                     if (_movieTexture.isPlaying)
                     {
                         _status = StreamingVideoStatus.Playing;
                     }
                     break;
                 case StreamingVideoStatus.Playing:
                     if (!_movieTexture.isPlaying)
                     {
                         _status = StreamingVideoStatus.Done;
                     }
                     break;
             }
        }
    }
}
#endif
