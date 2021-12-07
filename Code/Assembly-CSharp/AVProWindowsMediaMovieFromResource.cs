using System;
using System.Runtime.InteropServices;
using UnityEngine;

// Token: 0x0200020F RID: 527
[AddComponentMenu("AVPro Windows Media/Movie From Resource")]
public class AVProWindowsMediaMovieFromResource : AVProWindowsMediaMovie
{
	// Token: 0x06001273 RID: 4723 RVA: 0x000539CD File Offset: 0x00051BCD
	public override void Start()
	{
		if (null == AVProWindowsMediaManager.Instance)
		{
			throw new Exception("You need to add AVProWindowsMediaManager component to your scene.");
		}
		if (this._loadOnStart)
		{
			this.LoadMovieFromResource(this._playOnStart, this._filename);
		}
	}

	// Token: 0x06001274 RID: 4724 RVA: 0x00053A08 File Offset: 0x00051C08
	public override bool LoadMovie(bool autoPlay)
	{
		return this.LoadMovieFromResource(autoPlay, this._filename);
	}

	// Token: 0x06001275 RID: 4725 RVA: 0x00053A18 File Offset: 0x00051C18
	public bool LoadMovieFromResource(bool autoPlay, string path)
	{
		bool flag = false;
		this.UnloadMovie();
		this._textAsset = (Resources.Load(path, typeof(TextAsset)) as TextAsset);
		if (this._textAsset != null && this._textAsset.bytes != null && this._textAsset.bytes.Length > 0)
		{
			this._bytesHandle = GCHandle.Alloc(this._textAsset.bytes, GCHandleType.Pinned);
			flag = base.LoadMovieFromMemory(autoPlay, path, this._bytesHandle.AddrOfPinnedObject(), (long)((ulong)this._textAsset.bytes.Length), FilterMode.Bilinear, TextureWrapMode.Clamp);
		}
		if (!flag)
		{
			Debug.LogError("[AVProWindowsMedia] Unable to load resource " + path);
		}
		return flag;
	}

	// Token: 0x06001276 RID: 4726 RVA: 0x00053ACE File Offset: 0x00051CCE
	public override void UnloadMovie()
	{
		if (this._moviePlayer != null)
		{
			this._moviePlayer.Dispose();
			this._moviePlayer = null;
		}
		this.UnloadResource();
	}

	// Token: 0x06001277 RID: 4727 RVA: 0x00053AF3 File Offset: 0x00051CF3
	private void UnloadResource()
	{
		if (this._bytesHandle.IsAllocated)
		{
			this._bytesHandle.Free();
		}
		if (this._textAsset != null)
		{
			Resources.UnloadAsset(this._textAsset);
			this._textAsset = null;
		}
	}

	// Token: 0x04000FB4 RID: 4020
	private TextAsset _textAsset;

	// Token: 0x04000FB5 RID: 4021
	private GCHandle _bytesHandle;
}
