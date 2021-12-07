using System;
using UnityEngine;

// Token: 0x02000203 RID: 515
public class AVProWindowsMediaControlPlayOnEnable : MonoBehaviour
{
	// Token: 0x060011D2 RID: 4562 RVA: 0x00052114 File Offset: 0x00050314
	private void OnEnable()
	{
		if (!this._enableLoopWhenInRange)
		{
			if (this._movie.MovieInstance != null)
			{
				this._movie.MovieInstance.SetFrameRange(this._minFrame, this._maxFrame);
			}
		}
		else
		{
			this._movie._loop = true;
		}
		this._movie.Play();
	}

	// Token: 0x060011D3 RID: 4563 RVA: 0x00052174 File Offset: 0x00050374
	private void Update()
	{
		if (this._enableLoopWhenInRange && this._movie.MovieInstance != null && this._movie.MovieInstance.IsPlaying)
		{
			if (!this._loop)
			{
				if (this._movie.MovieInstance.DisplayFrame >= this._minFrame)
				{
					this._movie.MovieInstance.SetFrameRange(this._minFrame, this._maxFrame);
					this._loop = true;
				}
			}
			else if (this._movie.MovieInstance.DisplayFrame >= this._maxFrame)
			{
				this._movie.MovieInstance.PositionFrames = (uint)(this._minFrame + 1);
			}
		}
	}

	// Token: 0x060011D4 RID: 4564 RVA: 0x00052234 File Offset: 0x00050434
	private void OnDisable()
	{
		if (this._enableLoopWhenInRange)
		{
			this._loop = false;
			if (this._movie.MovieInstance != null)
			{
				this._movie.MovieInstance.SetFrameRange(-1, -1);
			}
		}
		if (this._rewindOnDisable && this._movie.MovieInstance != null)
		{
			if (this._movie.MovieInstance.IsPlaying)
			{
				this._movie.Pause();
			}
			if (this._movie.MovieInstance.DisplayFrame > 1)
			{
				this._movie.MovieInstance.Rewind();
			}
		}
	}

	// Token: 0x04000F5B RID: 3931
	public AVProWindowsMediaMovie _movie;

	// Token: 0x04000F5C RID: 3932
	public bool _rewindOnDisable;

	// Token: 0x04000F5D RID: 3933
	public int _minFrame = -1;

	// Token: 0x04000F5E RID: 3934
	public int _maxFrame = -1;

	// Token: 0x04000F5F RID: 3935
	public bool _loop;

	// Token: 0x04000F60 RID: 3936
	public bool _enableLoopWhenInRange;
}
