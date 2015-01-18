using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Plugins
{
    public class StreamingMovieTexture : MonoBehaviour
    {
        public string OgvUrl;
        public string Mp4Url;

        private IStreamingVideoPlugin _videoPlugin;

        public bool PlayRequested;

        void Start()
        {
            if (!string.IsNullOrEmpty(OgvUrl))
            {
                UnityEngine.Debug.Log("Creating WWW for ogv");

#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
                _videoPlugin = new NativeStreamingVideoPlugin(OgvUrl, gameObject);
#elif UNITY_WEBGL
                _videoPlugin = new WebGLStreamingVideoPlugin(OgvUrl);
#endif
            }
        }


        void Update()
        {
            gameObject.transform.Rotate(Time.deltaTime * 10, Time.deltaTime * 30, 0);

            _videoPlugin.Update();
            if (_videoPlugin.IsReadyToPlay && PlayRequested)
            {
                _videoPlugin.Play();
            }
        }
    }
}
