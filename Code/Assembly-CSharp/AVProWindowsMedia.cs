using System;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;

// Token: 0x02000205 RID: 517
public class AVProWindowsMedia : IDisposable
{
	// Token: 0x17000332 RID: 818
	// (get) Token: 0x060011EA RID: 4586 RVA: 0x00052920 File Offset: 0x00050B20
	public int Handle
	{
		get
		{
			return this._movieHandle;
		}
	}

	// Token: 0x17000333 RID: 819
	// (get) Token: 0x060011EB RID: 4587 RVA: 0x00052928 File Offset: 0x00050B28
	// (set) Token: 0x060011EC RID: 4588 RVA: 0x00052930 File Offset: 0x00050B30
	public string Filename { get; private set; }

	// Token: 0x17000334 RID: 820
	// (get) Token: 0x060011ED RID: 4589 RVA: 0x00052939 File Offset: 0x00050B39
	// (set) Token: 0x060011EE RID: 4590 RVA: 0x00052941 File Offset: 0x00050B41
	public int Width { get; private set; }

	// Token: 0x17000335 RID: 821
	// (get) Token: 0x060011EF RID: 4591 RVA: 0x0005294A File Offset: 0x00050B4A
	// (set) Token: 0x060011F0 RID: 4592 RVA: 0x00052952 File Offset: 0x00050B52
	public int Height { get; private set; }

	// Token: 0x17000336 RID: 822
	// (get) Token: 0x060011F1 RID: 4593 RVA: 0x0005295B File Offset: 0x00050B5B
	public float AspectRatio
	{
		get
		{
			return (float)this.Width / (float)this.Height;
		}
	}

	// Token: 0x17000337 RID: 823
	// (get) Token: 0x060011F2 RID: 4594 RVA: 0x0005296C File Offset: 0x00050B6C
	// (set) Token: 0x060011F3 RID: 4595 RVA: 0x00052974 File Offset: 0x00050B74
	public float FrameRate { get; private set; }

	// Token: 0x17000338 RID: 824
	// (get) Token: 0x060011F4 RID: 4596 RVA: 0x0005297D File Offset: 0x00050B7D
	// (set) Token: 0x060011F5 RID: 4597 RVA: 0x00052985 File Offset: 0x00050B85
	public float DurationSeconds { get; private set; }

	// Token: 0x17000339 RID: 825
	// (get) Token: 0x060011F6 RID: 4598 RVA: 0x0005298E File Offset: 0x00050B8E
	// (set) Token: 0x060011F7 RID: 4599 RVA: 0x00052996 File Offset: 0x00050B96
	public uint DurationFrames { get; private set; }

	// Token: 0x1700033A RID: 826
	// (get) Token: 0x060011F8 RID: 4600 RVA: 0x0005299F File Offset: 0x00050B9F
	public uint LastFrame
	{
		get
		{
			return (uint)Mathf.Max(0, (int)(this.DurationFrames - 1U));
		}
	}

	// Token: 0x1700033B RID: 827
	// (get) Token: 0x060011F9 RID: 4601 RVA: 0x000529AF File Offset: 0x00050BAF
	// (set) Token: 0x060011FA RID: 4602 RVA: 0x000529B7 File Offset: 0x00050BB7
	public bool IsPlaying { get; private set; }

	// Token: 0x1700033C RID: 828
	// (get) Token: 0x060011FC RID: 4604 RVA: 0x000529D5 File Offset: 0x00050BD5
	// (set) Token: 0x060011FB RID: 4603 RVA: 0x000529C0 File Offset: 0x00050BC0
	public bool Loop
	{
		get
		{
			return this._isLooping;
		}
		set
		{
			this._isLooping = value;
			AVProWindowsMediaPlugin.SetLooping(this._movieHandle, value);
		}
	}

	// Token: 0x1700033D RID: 829
	// (get) Token: 0x060011FE RID: 4606 RVA: 0x000529F7 File Offset: 0x00050BF7
	// (set) Token: 0x060011FD RID: 4605 RVA: 0x000529DD File Offset: 0x00050BDD
	public int AudioDelay
	{
		get
		{
			return this._audioDelay;
		}
		set
		{
			this._audioDelay = value;
			AVProWindowsMediaPlugin.SetAudioDelay(this._movieHandle, this._audioDelay);
		}
	}

	// Token: 0x1700033E RID: 830
	// (get) Token: 0x06001200 RID: 4608 RVA: 0x00052A19 File Offset: 0x00050C19
	// (set) Token: 0x060011FF RID: 4607 RVA: 0x000529FF File Offset: 0x00050BFF
	public float Volume
	{
		get
		{
			return this._volume;
		}
		set
		{
			this._volume = value;
			AVProWindowsMediaPlugin.SetVolume(this._movieHandle, this._volume);
		}
	}

	// Token: 0x1700033F RID: 831
	// (get) Token: 0x06001202 RID: 4610 RVA: 0x00052A2F File Offset: 0x00050C2F
	// (set) Token: 0x06001201 RID: 4609 RVA: 0x00052A21 File Offset: 0x00050C21
	public float PlaybackRate
	{
		get
		{
			return AVProWindowsMediaPlugin.GetPlaybackRate(this._movieHandle);
		}
		set
		{
			AVProWindowsMediaPlugin.SetPlaybackRate(this._movieHandle, value);
		}
	}

	// Token: 0x17000340 RID: 832
	// (get) Token: 0x06001203 RID: 4611 RVA: 0x00052A3C File Offset: 0x00050C3C
	// (set) Token: 0x06001204 RID: 4612 RVA: 0x00052A49 File Offset: 0x00050C49
	public float PositionSeconds
	{
		get
		{
			return AVProWindowsMediaPlugin.GetCurrentPositionSeconds(this._movieHandle);
		}
		set
		{
			AVProWindowsMediaPlugin.SeekSeconds(this._movieHandle, value);
		}
	}

	// Token: 0x17000341 RID: 833
	// (get) Token: 0x06001205 RID: 4613 RVA: 0x00052A57 File Offset: 0x00050C57
	// (set) Token: 0x06001206 RID: 4614 RVA: 0x00052A5F File Offset: 0x00050C5F
	public uint PositionFrames
	{
		get
		{
			return (uint)this.DisplayFrame;
		}
		set
		{
			AVProWindowsMediaPlugin.SeekFrames(this._movieHandle, value);
		}
	}

	// Token: 0x17000342 RID: 834
	// (get) Token: 0x06001207 RID: 4615 RVA: 0x00052A6D File Offset: 0x00050C6D
	// (set) Token: 0x06001208 RID: 4616 RVA: 0x00052A7A File Offset: 0x00050C7A
	public float AudioBalance
	{
		get
		{
			return AVProWindowsMediaPlugin.GetAudioBalance(this._movieHandle);
		}
		set
		{
			AVProWindowsMediaPlugin.SetAudioBalance(this._movieHandle, value);
		}
	}

	// Token: 0x17000343 RID: 835
	// (get) Token: 0x06001209 RID: 4617 RVA: 0x00052A88 File Offset: 0x00050C88
	public bool IsFinishedPlaying
	{
		get
		{
			return AVProWindowsMediaPlugin.IsFinishedPlaying(this._movieHandle);
		}
	}

	// Token: 0x17000344 RID: 836
	// (get) Token: 0x0600120A RID: 4618 RVA: 0x00052A95 File Offset: 0x00050C95
	// (set) Token: 0x0600120B RID: 4619 RVA: 0x00052A9D File Offset: 0x00050C9D
	public bool RequiresFlipY { get; private set; }

	// Token: 0x17000345 RID: 837
	// (get) Token: 0x0600120C RID: 4620 RVA: 0x00052AA8 File Offset: 0x00050CA8
	public Texture OutputTexture
	{
		get
		{
			if (this._formatConverter != null && this._formatConverter.ValidPicture)
			{
				return this._formatConverter.OutputTexture;
			}
			return null;
		}
	}

	// Token: 0x17000346 RID: 838
	// (get) Token: 0x0600120D RID: 4621 RVA: 0x00052AE0 File Offset: 0x00050CE0
	public int DisplayFrame
	{
		get
		{
			if (this._formatConverter != null && this._formatConverter.ValidPicture)
			{
				return this._formatConverter.DisplayFrame;
			}
			return -1;
		}
	}

	// Token: 0x0600120E RID: 4622 RVA: 0x00052B18 File Offset: 0x00050D18
	public bool StartVideo(string filename, bool allowNativeFormat, bool useBT709, bool allowAudio, bool useAudioDelay, bool useAudioMixer, bool useDisplaySync, bool ignoreFlips, FilterMode textureFilterMode, TextureWrapMode textureWrapMode)
	{
		this.Filename = filename;
		if (!string.IsNullOrEmpty(this.Filename))
		{
			if (File.Exists(filename))
			{
				if (this._movieHandle < 0)
				{
					this._movieHandle = AVProWindowsMediaPlugin.GetInstanceHandle();
				}
				IntPtr intPtr = Marshal.StringToHGlobalUni(this.Filename);
				if (AVProWindowsMediaPlugin.LoadMovie(this._movieHandle, intPtr, false, allowNativeFormat, allowAudio, useAudioDelay, useAudioMixer, useDisplaySync))
				{
					this.CompleteVideoLoad(useBT709, ignoreFlips, textureFilterMode, textureWrapMode);
				}
				else
				{
					Debug.LogError("[AVProWindowsMedia] Movie failed to load");
					this.Close();
				}
				Marshal.FreeHGlobal(intPtr);
			}
			else
			{
				Debug.LogError("[AVProWindowsMedia] File not found " + filename);
				this.Close();
			}
		}
		else
		{
			Debug.LogError("[AVProWindowsMedia] No movie file specified");
			this.Close();
		}
		return this._movieHandle >= 0;
	}

	// Token: 0x0600120F RID: 4623 RVA: 0x00052BEC File Offset: 0x00050DEC
	public bool StartVideoFromMemory(string name, IntPtr moviePointer, long movieLength, bool allowNativeFormat, bool useBT709, bool allowAudio, bool useAudioDelay, bool useAudioMixer, bool useDisplaySync, bool ignoreFlips, FilterMode textureFilterMode, TextureWrapMode textureWrapMode)
	{
		this.Filename = name;
		if (moviePointer != IntPtr.Zero && movieLength > 0L)
		{
			if (this._movieHandle < 0)
			{
				this._movieHandle = AVProWindowsMediaPlugin.GetInstanceHandle();
			}
			if (AVProWindowsMediaPlugin.LoadMovieFromMemory(this._movieHandle, moviePointer, movieLength, allowNativeFormat, allowAudio, useAudioDelay, useAudioMixer, useDisplaySync))
			{
				this.CompleteVideoLoad(useBT709, ignoreFlips, textureFilterMode, textureWrapMode);
			}
			else
			{
				Debug.LogWarning("[AVProWindowsMedia] Movie failed to load");
				this.Close();
			}
		}
		else
		{
			Debug.LogWarning("[AVProWindowsMedia] No movie file specified");
			this.Close();
		}
		return this._movieHandle >= 0;
	}

	// Token: 0x06001210 RID: 4624 RVA: 0x00052C90 File Offset: 0x00050E90
	public Material GetConversionMaterial()
	{
		Material result = null;
		if (this._formatConverter != null)
		{
			result = this._formatConverter.GetConversionMaterial();
		}
		return result;
	}

	// Token: 0x06001211 RID: 4625 RVA: 0x00052CB8 File Offset: 0x00050EB8
	private void CompleteVideoLoad(bool useBT709, bool ignoreFlips, FilterMode textureFilterMode, TextureWrapMode textureWrapMode)
	{
		this.RequiresFlipY = false;
		this.Loop = false;
		this.Volume = this._volume;
		this.Width = AVProWindowsMediaPlugin.GetWidth(this._movieHandle);
		this.Height = AVProWindowsMediaPlugin.GetHeight(this._movieHandle);
		this.FrameRate = AVProWindowsMediaPlugin.GetFrameRate(this._movieHandle);
		this.DurationSeconds = AVProWindowsMediaPlugin.GetDurationSeconds(this._movieHandle);
		this.DurationFrames = AVProWindowsMediaPlugin.GetDurationFrames(this._movieHandle);
		AVProWindowsMediaPlugin.VideoFrameFormat format = (AVProWindowsMediaPlugin.VideoFrameFormat)AVProWindowsMediaPlugin.GetFormat(this._movieHandle);
		if (AVProWindowsMediaManager.Instance._logVideoLoads)
		{
			Debug.Log(string.Format("[AVProWindowsMedia] Loaded video '{0}' ({1}x{2} @ {3} fps) {4} frames, {5} seconds - format: {6}", new object[]
			{
				this.Filename,
				this.Width,
				this.Height,
				this.FrameRate.ToString("F2"),
				this.DurationFrames,
				this.DurationSeconds.ToString("F2"),
				format.ToString()
			}));
		}
		if ((this.Width <= 0 || this.Width > 8192) && (this.Height <= 0 || this.Height > 8192))
		{
			int num = 0;
			this.Height = num;
			this.Width = num;
			if (this._formatConverter != null)
			{
				this._formatConverter.Dispose();
				this._formatConverter = null;
			}
		}
		else
		{
			bool flag = AVProWindowsMediaPlugin.IsOrientedTopDown(this._movieHandle);
			if (this._formatConverter == null)
			{
				this._formatConverter = new AVProWindowsMediaFormatConverter();
			}
			bool flipX = false;
			bool flag2 = flag;
			if (ignoreFlips)
			{
				if (flag2)
				{
					this.RequiresFlipY = true;
				}
				flag2 = (flipX = false);
			}
			if (!this._formatConverter.Build(this._movieHandle, this.Width, this.Height, format, useBT709, flipX, flag2, textureFilterMode, textureWrapMode))
			{
				Debug.LogError("[AVProWindowsMedia] unable to convert video format");
				int num = 0;
				this.Height = num;
				this.Width = num;
				if (this._formatConverter != null)
				{
					this._formatConverter.Dispose();
					this._formatConverter = null;
					this.Close();
				}
			}
		}
		this.PreRoll();
	}

	// Token: 0x06001212 RID: 4626 RVA: 0x00052F00 File Offset: 0x00051100
	public bool StartAudio(string filename)
	{
		this.Filename = filename;
		int num = 0;
		this.Height = num;
		this.Width = num;
		if (!string.IsNullOrEmpty(this.Filename))
		{
			if (this._movieHandle < 0)
			{
				this._movieHandle = AVProWindowsMediaPlugin.GetInstanceHandle();
			}
			if (this._formatConverter != null)
			{
				this._formatConverter.Dispose();
				this._formatConverter = null;
			}
			IntPtr intPtr = Marshal.StringToHGlobalUni(this.Filename);
			if (AVProWindowsMediaPlugin.LoadMovie(this._movieHandle, intPtr, false, false, true, false, false, false))
			{
				this.Volume = this._volume;
				this.DurationSeconds = AVProWindowsMediaPlugin.GetDurationSeconds(this._movieHandle);
				Debug.Log(string.Concat(new string[]
				{
					"[AVProWindowsMedia] Loaded audio ",
					this.Filename,
					" ",
					this.DurationSeconds.ToString("F2"),
					" sec"
				}));
			}
			else
			{
				Debug.LogError("[AVProWindowsMedia] Movie failed to load");
				this.Close();
			}
			Marshal.FreeHGlobal(intPtr);
		}
		else
		{
			Debug.LogError("[AVProWindowsMedia] No movie file specified");
			this.Close();
		}
		return this._movieHandle >= 0;
	}

	// Token: 0x06001213 RID: 4627 RVA: 0x0005302C File Offset: 0x0005122C
	private void PreRoll()
	{
		if (this._movieHandle < 0)
		{
			return;
		}
	}

	// Token: 0x06001214 RID: 4628 RVA: 0x00053048 File Offset: 0x00051248
	public bool Update(bool force)
	{
		bool result = false;
		if (this._movieHandle >= 0)
		{
			AVProWindowsMediaPlugin.Update(this._movieHandle);
			if (this._formatConverter != null)
			{
				result = this._formatConverter.Update();
			}
		}
		return result;
	}

	// Token: 0x06001215 RID: 4629 RVA: 0x00053087 File Offset: 0x00051287
	public void Play()
	{
		if (this._movieHandle >= 0)
		{
			AVProWindowsMediaPlugin.Play(this._movieHandle);
			this.IsPlaying = true;
		}
	}

	// Token: 0x06001216 RID: 4630 RVA: 0x000530A7 File Offset: 0x000512A7
	public void Pause()
	{
		if (this._movieHandle >= 0)
		{
			AVProWindowsMediaPlugin.Pause(this._movieHandle);
			this.IsPlaying = false;
		}
	}

	// Token: 0x06001217 RID: 4631 RVA: 0x000530C7 File Offset: 0x000512C7
	public void Rewind()
	{
		if (this._movieHandle >= 0)
		{
			this.PositionSeconds = 0f;
		}
	}

	// Token: 0x06001218 RID: 4632 RVA: 0x000530E0 File Offset: 0x000512E0
	public void Dispose()
	{
		this.Close();
		if (this._formatConverter != null)
		{
			this._formatConverter.Dispose();
			this._formatConverter = null;
		}
	}

	// Token: 0x06001219 RID: 4633 RVA: 0x00053105 File Offset: 0x00051305
	public void SetFrameRange(int min, int max)
	{
		AVProWindowsMediaPlugin.SetDisplayFrameRange(this._movieHandle, min, max);
	}

	// Token: 0x0600121A RID: 4634 RVA: 0x00053114 File Offset: 0x00051314
	public void ClearFrameRange()
	{
		this.SetFrameRange(-1, -1);
	}

	// Token: 0x0600121B RID: 4635 RVA: 0x00053120 File Offset: 0x00051320
	private void Close()
	{
		int num = 0;
		this.Height = num;
		this.Width = num;
		if (this._movieHandle >= 0)
		{
			this.Pause();
			AVProWindowsMediaPlugin.Stop(this._movieHandle);
			AVProWindowsMediaPlugin.FreeInstanceHandle(this._movieHandle);
			this._movieHandle = -1;
		}
	}

	// Token: 0x04000F75 RID: 3957
	private int _movieHandle = -1;

	// Token: 0x04000F76 RID: 3958
	private AVProWindowsMediaFormatConverter _formatConverter;

	// Token: 0x04000F77 RID: 3959
	private bool _isLooping;

	// Token: 0x04000F78 RID: 3960
	private int _audioDelay;

	// Token: 0x04000F79 RID: 3961
	private float _volume = 1f;
}
