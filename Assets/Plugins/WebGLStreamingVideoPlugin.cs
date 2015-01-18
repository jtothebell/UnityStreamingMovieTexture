using System;
using UnityEngine;

namespace Assets.Plugins
{
    public class WebGLStreamingVideoPlugin : IStreamingVideoPlugin
    {
        private WebGLMovieTexture _movieTexture;

        public WebGLStreamingVideoPlugin(string url)
        {
            Url = url;
            _movieTexture = new WebGLMovieTexture(url);
            _status = StreamingVideoStatus.Unknown;
        }

        public string Url
        {
            get;
            set;
        }

        public bool IsReadyToPlay
        {
            get { return _movieTexture.isReady; }
        }

        public bool IsPlaying
        {
            get { return _status == StreamingVideoStatus.Playing; }
        }

        public void Play()
        {
            if (IsReadyToPlay)
            {
                _movieTexture.Play();
                _status = StreamingVideoStatus.Playing;
            }
        }

        public void Pause()
        {
            _movieTexture.Pause();
            _status = StreamingVideoStatus.Paused;
        }

        public bool IsDone
        {
            get { return _status == StreamingVideoStatus.Done; }
        }

        private StreamingVideoStatus _status;
        public StreamingVideoStatus Status
        {
            get { return _status; }
        }

        public float Duration { get { return _movieTexture.duration; } }


        public void Update()
        {
            _movieTexture.Update();
            if (_status < StreamingVideoStatus.ReadyToPlay && IsReadyToPlay)
            {
                _status = StreamingVideoStatus.ReadyToPlay;
            }
            if (_status == StreamingVideoStatus.Playing && _movieTexture.time == _movieTexture.duration)
            {
                _status = StreamingVideoStatus.Done;
            }
        }
    }
}
