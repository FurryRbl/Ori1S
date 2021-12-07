using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x02000204 RID: 516
[ExecuteInEditMode]
[AddComponentMenu("AVPro Windows Media/Movie")]
public class AVProWindowsMediaMovie : MonoBehaviour
{
	// Token: 0x1700032F RID: 815
	// (get) Token: 0x060011D6 RID: 4566 RVA: 0x00052348 File Offset: 0x00050548
	public Texture OutputTexture
	{
		get
		{
			if (this._moviePlayer != null)
			{
				return this._moviePlayer.OutputTexture;
			}
			return null;
		}
	}

	// Token: 0x17000330 RID: 816
	// (get) Token: 0x060011D7 RID: 4567 RVA: 0x00052362 File Offset: 0x00050562
	public AVProWindowsMedia MovieInstance
	{
		get
		{
			return this._moviePlayer;
		}
	}

	// Token: 0x060011D8 RID: 4568 RVA: 0x0005236A File Offset: 0x0005056A
	public virtual void Start()
	{
		if (null == AVProWindowsMediaManager.Instance)
		{
			throw new Exception("You need to add AVProWindowsMediaManager component to your scene.");
		}
		if (this._loadOnStart)
		{
			this.LoadMovie(this._playOnStart);
		}
	}

	// Token: 0x060011D9 RID: 4569 RVA: 0x000523A0 File Offset: 0x000505A0
	public virtual bool LoadMovie(bool autoPlay)
	{
		bool result = true;
		if (this._moviePlayer == null)
		{
			this._moviePlayer = new AVProWindowsMedia();
		}
		this.LoadClips();
		bool allowNativeFormat = this._colourFormat != AVProWindowsMediaMovie.ColourFormat.RGBA32;
		string filePath = this.GetFilePath();
		if (this._moviePlayer.StartVideo(filePath, allowNativeFormat, this._colourFormat == AVProWindowsMediaMovie.ColourFormat.YCbCr_HD, this._allowAudio, this._useAudioDelay, this._useAudioMixer, this._useDisplaySync, this._ignoreFlips, this._textureFilterMode, this._textureWrapMode))
		{
			this._moviePlayer.Volume = this._volume;
			this._moviePlayer.Loop = this._loop;
			if (autoPlay)
			{
				this._moviePlayer.Play();
			}
		}
		else
		{
			Debug.LogWarning("[AVProWindowsMedia] Couldn't load movie " + this._filename);
			this.UnloadMovie();
			result = false;
		}
		return result;
	}

	// Token: 0x060011DA RID: 4570 RVA: 0x0005247C File Offset: 0x0005067C
	public bool LoadMovieFromMemory(bool autoPlay, string name, IntPtr moviePointer, long movieLength, FilterMode textureFilterMode, TextureWrapMode textureWrapMode)
	{
		bool result = true;
		if (this._moviePlayer == null)
		{
			this._moviePlayer = new AVProWindowsMedia();
		}
		bool allowNativeFormat = this._colourFormat != AVProWindowsMediaMovie.ColourFormat.RGBA32;
		if (this._moviePlayer.StartVideoFromMemory(name, moviePointer, movieLength, allowNativeFormat, this._colourFormat == AVProWindowsMediaMovie.ColourFormat.YCbCr_HD, this._allowAudio, this._useAudioDelay, this._useAudioMixer, this._useDisplaySync, this._ignoreFlips, textureFilterMode, textureWrapMode))
		{
			this._moviePlayer.Volume = this._volume;
			if (autoPlay)
			{
				this._moviePlayer.Play();
			}
		}
		else
		{
			Debug.LogWarning("[AVProWindowsMedia] Couldn't load movie " + this._filename);
			this.UnloadMovie();
			result = false;
		}
		return result;
	}

	// Token: 0x060011DB RID: 4571 RVA: 0x00052534 File Offset: 0x00050734
	public void Update()
	{
		if (this._moviePlayer != null)
		{
			this._volume = Mathf.Clamp01(this._volume);
			if (this._volume != this._moviePlayer.Volume)
			{
				this._moviePlayer.Volume = this._volume;
			}
			if (this._loop != this._moviePlayer.Loop)
			{
				this._moviePlayer.Loop = this._loop;
			}
			this._moviePlayer.Update(false);
			if (!this._moviePlayer.Loop && this._moviePlayer.IsPlaying && this._moviePlayer.IsFinishedPlaying)
			{
				this._moviePlayer.Pause();
				base.SendMessage("MovieFinished", this, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	// Token: 0x060011DC RID: 4572 RVA: 0x00052600 File Offset: 0x00050800
	public void Play()
	{
		if (this._moviePlayer != null)
		{
			this._moviePlayer.Play();
		}
	}

	// Token: 0x060011DD RID: 4573 RVA: 0x00052618 File Offset: 0x00050818
	public void Pause()
	{
		if (this._moviePlayer != null)
		{
			this._moviePlayer.Pause();
		}
	}

	// Token: 0x17000331 RID: 817
	// (get) Token: 0x060011DE RID: 4574 RVA: 0x00052630 File Offset: 0x00050830
	public int NumClips
	{
		get
		{
			if (this._clips != null)
			{
				return this._clips.Count;
			}
			return 0;
		}
	}

	// Token: 0x060011DF RID: 4575 RVA: 0x0005264C File Offset: 0x0005084C
	public string GetClipName(int index)
	{
		string result = string.Empty;
		if (this._clips != null && index >= 0 && index < this._clips.Count)
		{
			result = this._clips[index].name;
		}
		return result;
	}

	// Token: 0x060011E0 RID: 4576 RVA: 0x00052695 File Offset: 0x00050895
	public void ClearClips()
	{
		this._currentClip = null;
		this._clips.Clear();
		this._clipLookup.Clear();
	}

	// Token: 0x060011E1 RID: 4577 RVA: 0x000526B4 File Offset: 0x000508B4
	public void AddClip(string name, int inPoint, int outPoint)
	{
		AVProWindowsMediaMovieClip avproWindowsMediaMovieClip = new AVProWindowsMediaMovieClip(name, inPoint, outPoint);
		this._clips.Add(avproWindowsMediaMovieClip);
		this._clipLookup.Add(name, avproWindowsMediaMovieClip);
	}

	// Token: 0x060011E2 RID: 4578 RVA: 0x000526E4 File Offset: 0x000508E4
	public string GetCurrentClipName()
	{
		string result = string.Empty;
		if (this._currentClip != null)
		{
			result = this._currentClip.name;
		}
		return result;
	}

	// Token: 0x060011E3 RID: 4579 RVA: 0x00052710 File Offset: 0x00050910
	public void LoadClips()
	{
		this._clipLookup.Clear();
		if (this._clips != null && this._clips.Count > 0)
		{
			for (int i = 0; i < this._clips.Count; i++)
			{
				if (!string.IsNullOrEmpty(this._clips[i].name))
				{
					this._clipLookup.Add(this._clips[i].name, this._clips[i]);
				}
			}
		}
	}

	// Token: 0x060011E4 RID: 4580 RVA: 0x000527A3 File Offset: 0x000509A3
	public void ResetClip()
	{
		this._currentClip = null;
		this.MovieInstance.SetFrameRange(-1, -1);
	}

	// Token: 0x060011E5 RID: 4581 RVA: 0x000527BC File Offset: 0x000509BC
	public void PlayClip(string name, bool loop, bool startPaused)
	{
		if (this.MovieInstance == null)
		{
			throw new Exception("Movie instance is null");
		}
		if (!this._clipLookup.ContainsKey(name))
		{
			throw new Exception("Frame range key not found");
		}
		this.MovieInstance.Loop = loop;
		this._currentClip = this._clipLookup[name];
		this.MovieInstance.SetFrameRange(this._currentClip.inPoint, this._currentClip.outPoint);
		this.MovieInstance.PositionFrames = (uint)this._currentClip.inPoint;
		if (!startPaused)
		{
			this.MovieInstance.Play();
		}
		else
		{
			this.MovieInstance.Pause();
		}
	}

	// Token: 0x060011E6 RID: 4582 RVA: 0x00052871 File Offset: 0x00050A71
	public virtual void UnloadMovie()
	{
		if (this._moviePlayer != null)
		{
			this._moviePlayer.Dispose();
			this._moviePlayer = null;
		}
	}

	// Token: 0x060011E7 RID: 4583 RVA: 0x00052890 File Offset: 0x00050A90
	public void OnDestroy()
	{
		this.UnloadMovie();
	}

	// Token: 0x060011E8 RID: 4584 RVA: 0x00052898 File Offset: 0x00050A98
	public string GetFilePath()
	{
		string text = Path.Combine(this._folder, this._filename);
		if (this._useStreamingAssetsPath)
		{
			text = Path.Combine(Application.streamingAssetsPath, text);
		}
		else if (!Application.isEditor && !Path.IsPathRooted(text))
		{
			string fullPath = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
			text = Path.Combine(fullPath, text);
		}
		return text;
	}

	// Token: 0x04000F61 RID: 3937
	protected AVProWindowsMedia _moviePlayer;

	// Token: 0x04000F62 RID: 3938
	public string _folder = "./";

	// Token: 0x04000F63 RID: 3939
	public string _filename = "movie.avi";

	// Token: 0x04000F64 RID: 3940
	public bool _useStreamingAssetsPath;

	// Token: 0x04000F65 RID: 3941
	public bool _loop;

	// Token: 0x04000F66 RID: 3942
	public AVProWindowsMediaMovie.ColourFormat _colourFormat = AVProWindowsMediaMovie.ColourFormat.YCbCr_HD;

	// Token: 0x04000F67 RID: 3943
	public bool _allowAudio = true;

	// Token: 0x04000F68 RID: 3944
	public bool _useAudioDelay;

	// Token: 0x04000F69 RID: 3945
	public bool _useAudioMixer;

	// Token: 0x04000F6A RID: 3946
	public bool _useDisplaySync;

	// Token: 0x04000F6B RID: 3947
	public bool _loadOnStart = true;

	// Token: 0x04000F6C RID: 3948
	public bool _playOnStart = true;

	// Token: 0x04000F6D RID: 3949
	public bool _editorPreview;

	// Token: 0x04000F6E RID: 3950
	public bool _ignoreFlips = true;

	// Token: 0x04000F6F RID: 3951
	public float _volume = 1f;

	// Token: 0x04000F70 RID: 3952
	public FilterMode _textureFilterMode = FilterMode.Bilinear;

	// Token: 0x04000F71 RID: 3953
	public TextureWrapMode _textureWrapMode = TextureWrapMode.Clamp;

	// Token: 0x04000F72 RID: 3954
	[SerializeField]
	private List<AVProWindowsMediaMovieClip> _clips;

	// Token: 0x04000F73 RID: 3955
	private Dictionary<string, AVProWindowsMediaMovieClip> _clipLookup = new Dictionary<string, AVProWindowsMediaMovieClip>();

	// Token: 0x04000F74 RID: 3956
	private AVProWindowsMediaMovieClip _currentClip;

	// Token: 0x0200020E RID: 526
	public enum ColourFormat
	{
		// Token: 0x04000FB1 RID: 4017
		RGBA32,
		// Token: 0x04000FB2 RID: 4018
		YCbCr_SD,
		// Token: 0x04000FB3 RID: 4019
		YCbCr_HD
	}
}
