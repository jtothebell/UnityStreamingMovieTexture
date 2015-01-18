using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Plugins
{
    public interface IStreamingVideoPlugin
    {
        string Url { get; set; }
        bool IsReadyToPlay { get; }
        bool IsPlaying { get; }
        void Play();
        void Pause();
        bool IsDone { get; }
        StreamingVideoStatus Status { get; }
        void Update();
        float Duration { get; }
    }

    public enum StreamingVideoStatus
    {
        Unknown,
        Error,
        Loading,
        ReadyToPlay,
        Playing,
        Paused,
        Done,
        Stopped
    }
}
