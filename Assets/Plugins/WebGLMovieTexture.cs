using UnityEngine;
using System;
using System.Runtime.InteropServices;

public class WebGLMovieTexture 
{
#if UNITY_WEBGL && !UNITY_EDITOR
	[DllImport("__Internal")]
	private static extern int WebGLMovieTextureCreate (string url);

	[DllImport("__Internal")]
	private static extern void WebGLMovieTextureUpdate (int video, int texture);

	[DllImport("__Internal")]
	private static extern void WebGLMovieTexturePlay (int video);

	[DllImport("__Internal")]
	private static extern void WebGLMovieTexturePause (int video);

	[DllImport("__Internal")]
	private static extern void WebGLMovieTextureSeek (int video, float time);

	[DllImport("__Internal")]
	private static extern void WebGLMovieTextureLoop (int video, bool loop);

	[DllImport("__Internal")]
	private static extern int WebGLMovieTextureWidth (int video);

	[DllImport("__Internal")]
	private static extern int WebGLMovieTextureHeight (int video);

	[DllImport("__Internal")]
	private static extern bool WebGLMovieTextureIsReady (int video);

	[DllImport("__Internal")]
	private static extern float WebGLMovieTextureTime (int video);

	[DllImport("__Internal")]
	private static extern float WebGLMovieTextureDuration (int video);
#else
	private static int WebGLMovieTextureCreate (string url)
	{
		throw new PlatformNotSupportedException("WebGLMovieTexture is only supported on WebGL.");
	}
	private static void WebGLMovieTextureUpdate (int video, int texture)
	{
		throw new PlatformNotSupportedException("WebGLMovieTexture is only supported on WebGL.");
	}
	private static void WebGLMovieTexturePlay (int video)
	{
		throw new PlatformNotSupportedException("WebGLMovieTexture is only supported on WebGL.");
	}
	private static void WebGLMovieTexturePause (int video)
	{
		throw new PlatformNotSupportedException("WebGLMovieTexture is only supported on WebGL.");
	}
	private static void WebGLMovieTextureSeek (int video, float time)
	{
		throw new PlatformNotSupportedException("WebGLMovieTexture is only supported on WebGL.");
	}
	private static void WebGLMovieTextureLoop (int video, bool loop)
	{
		throw new PlatformNotSupportedException("WebGLMovieTexture is only supported on WebGL.");
	}
	private static int WebGLMovieTextureWidth (int video)
	{
		throw new PlatformNotSupportedException("WebGLMovieTexture is only supported on WebGL.");
	}
	private static int WebGLMovieTextureHeight (int video)
	{
		throw new PlatformNotSupportedException("WebGLMovieTexture is only supported on WebGL.");
	}
	private static bool WebGLMovieTextureIsReady (int video)
	{
		throw new PlatformNotSupportedException("WebGLMovieTexture is only supported on WebGL.");
	}
	private static float WebGLMovieTextureTime (int video)
	{
		throw new PlatformNotSupportedException("WebGLMovieTexture is only supported on WebGL.");
	}
	private static float WebGLMovieTextureDuration (int video)
	{
		throw new PlatformNotSupportedException("WebGLMovieTexture is only supported on WebGL.");
	}
#endif
	Texture2D m_Texture;
	int m_Instance; 
	bool m_Loop;

	public WebGLMovieTexture (string url)
	{
		m_Instance = WebGLMovieTextureCreate(url);
		m_Texture = new Texture2D(0, 0, TextureFormat.ARGB32, false);
		m_Texture.wrapMode = TextureWrapMode.Clamp;		
	}

	public void Update()
	{
		var width = WebGLMovieTextureWidth(m_Instance);
		var height = WebGLMovieTextureHeight(m_Instance);
		if (width != m_Texture.width || height != m_Texture.height)
		{
			m_Texture.Resize(width, height, TextureFormat.ARGB32, false);
			m_Texture.Apply();
		}
		WebGLMovieTextureUpdate(m_Instance, m_Texture.GetNativeTextureID());
	}

	public void Play()
	{
		WebGLMovieTexturePlay(m_Instance);
	}

	public void Pause()
	{
		WebGLMovieTexturePause(m_Instance);
	}

	public void Seek(float t)
	{
		WebGLMovieTextureSeek(m_Instance, t);
	}

	public bool loop
	{
		get 
		{
			return m_Loop;
		}
		set
		{
			if (value != m_Loop)
			{
				m_Loop = value;
				WebGLMovieTextureLoop(m_Instance, m_Loop);
			}
		}
	}

	public bool isReady
	{
		get 
		{
			return WebGLMovieTextureIsReady(m_Instance);
		}
	}

	public float time
	{
		get
		{
			return WebGLMovieTextureTime(m_Instance);
		}
	}

	public float duration
	{
		get
		{
			return WebGLMovieTextureDuration(m_Instance);
		}
	}    

	static public implicit operator Texture2D(WebGLMovieTexture tex)
    {
        return tex.m_Texture;
    }	
}
