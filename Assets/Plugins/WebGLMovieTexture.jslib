var LibraryWebGLMovieTexture = {
$videoInstances: [],

WebGLMovieTextureCreate: function(url)
{
	var str = Pointer_stringify(url);
	var video = document.createElement('video');
	video.style.display = 'none';
	video.src = str;
	return videoInstances.push(video) - 1;
},

WebGLMovieTextureUpdate: function(video, tex)
{
	if (videoInstances[video].paused)
		return;
	GLctx.bindTexture(GLctx.TEXTURE_2D, GL.textures[tex]);
	GLctx.texImage2D(GLctx.TEXTURE_2D, 0, GLctx.RGBA, GLctx.RGBA, GLctx.UNSIGNED_BYTE, videoInstances[video]);
},

WebGLMovieTexturePlay: function(video)
{
	videoInstances[video].play();
},

WebGLMovieTexturePause: function(video)
{
	videoInstances[video].pause();
},

WebGLMovieTextureSeek: function(video, time)
{
	videoInstances[video].fastSeek(time);
},

WebGLMovieTextureLoop: function(video, loop)
{
	videoInstances[video].loop = loop;
},

WebGLMovieTextureHeight: function(video)
{
	return videoInstances[video].videoHeight;
},

WebGLMovieTextureWidth: function(video)
{
	return videoInstances[video].videoWidth;
},

WebGLMovieTextureTime: function(video)
{
	return videoInstances[video].currentTime;
},

WebGLMovieTextureDuration: function(video)
{
	return videoInstances[video].duration;
},

WebGLMovieTextureIsReady: function(video)
{
	return videoInstances[video].readyState >= videoInstances[video].HAVE_CURRENT_DATA;
}

};
autoAddDeps(LibraryWebGLMovieTexture, '$videoInstances');
mergeInto(LibraryManager.library, LibraryWebGLMovieTexture);
