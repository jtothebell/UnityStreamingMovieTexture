Simple MovieTextures for Unity WebGL
=================================

Description
===========

This package implements basic support for video playback on textures in Unity WebGL. 

Unity's built-in MovieTexture class is not currently available for the WebGL platform. However, it is simple and efficient to use the browser's built-in html5 video element to render video to a texture instead. This package provides a basic implementation of a MovieTexture class on WebGL using html5 video, with support for basic playback controls (play/pause/seek).

See the contained VideoTest.cs script for a simple sample of playing back video on a texture with a basic UI allowing playback control.

Disclaimer
==========

This is an unsupported package provided by Unity Technologies for demonstration purposes.

Manual
======

WebGLMovieTexture.WebGLMovieTexture(string url)

Creates a WebGLMovieTexture instance to play back the video file located at url.



void WebGLMovieTexture.Update()

Update the texture contents with the current video feed.



void WebGLMovieTexture.Play()

Starts playing the video. Check WebGLMovieTexture.isReady first.



void WebGLMovieTexture.Pause()

Pauses playback.


void WebGLMovieTexture.Seek(float t)

Jump to position t in the video, where t is in seconds from the start of the video.



bool WebGLMovieTexture.loop

Should the movie loop (writable)?


bool WebGLMovieTexture.isReady

Did we download enough data to start playing back video?



public float time

Current position in the video in seconds.



public float duration

Total duration of the video.

Notes
=====

Sample video file Chrome_ImF.mp4 taken from www.html5rocks.com, licensed under Apache license 2.0.