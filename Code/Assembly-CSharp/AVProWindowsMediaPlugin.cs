using System;
using System.Runtime.InteropServices;

// Token: 0x02000209 RID: 521
public class AVProWindowsMediaPlugin
{
	// Token: 0x06001231 RID: 4657
	[DllImport("AVProWindowsMedia")]
	public static extern bool Init();

	// Token: 0x06001232 RID: 4658
	[DllImport("AVProWindowsMedia")]
	public static extern void Deinit();

	// Token: 0x06001233 RID: 4659
	[DllImport("AVProWindowsMedia")]
	public static extern void SetUnityFeatures(bool supportExternalTextures);

	// Token: 0x06001234 RID: 4660
	[DllImport("AVProWindowsMedia")]
	public static extern float GetPluginVersion();

	// Token: 0x06001235 RID: 4661
	[DllImport("AVProWindowsMedia")]
	public static extern int GetInstanceHandle();

	// Token: 0x06001236 RID: 4662
	[DllImport("AVProWindowsMedia")]
	public static extern void FreeInstanceHandle(int handle);

	// Token: 0x06001237 RID: 4663
	[DllImport("AVProWindowsMedia")]
	public static extern bool LoadMovie(int handle, IntPtr filename, bool playFromMemory, bool allowNativeFormat, bool allowAudio, bool useAudioDelay, bool useAudioMixer, bool useDisplaySync);

	// Token: 0x06001238 RID: 4664
	[DllImport("AVProWindowsMedia")]
	public static extern bool LoadMovieFromMemory(int handle, IntPtr moviePointer, long movieLength, bool allowNativeFormat, bool allowAudio, bool useAudioDelay, bool useAudioMixer, bool useDisplaySync);

	// Token: 0x06001239 RID: 4665
	[DllImport("AVProWindowsMedia")]
	public static extern int GetWidth(int handle);

	// Token: 0x0600123A RID: 4666
	[DllImport("AVProWindowsMedia")]
	public static extern int GetHeight(int handle);

	// Token: 0x0600123B RID: 4667
	[DllImport("AVProWindowsMedia")]
	public static extern float GetFrameRate(int handle);

	// Token: 0x0600123C RID: 4668
	[DllImport("AVProWindowsMedia")]
	public static extern long GetFrameDuration(int handle);

	// Token: 0x0600123D RID: 4669
	[DllImport("AVProWindowsMedia")]
	public static extern int GetFormat(int handle);

	// Token: 0x0600123E RID: 4670
	[DllImport("AVProWindowsMedia")]
	public static extern float GetDurationSeconds(int handle);

	// Token: 0x0600123F RID: 4671
	[DllImport("AVProWindowsMedia")]
	public static extern uint GetDurationFrames(int handle);

	// Token: 0x06001240 RID: 4672
	[DllImport("AVProWindowsMedia")]
	public static extern bool IsOrientedTopDown(int handle);

	// Token: 0x06001241 RID: 4673
	[DllImport("AVProWindowsMedia")]
	public static extern void Play(int handle);

	// Token: 0x06001242 RID: 4674
	[DllImport("AVProWindowsMedia")]
	public static extern void Pause(int handle);

	// Token: 0x06001243 RID: 4675
	[DllImport("AVProWindowsMedia")]
	public static extern void Stop(int handle);

	// Token: 0x06001244 RID: 4676
	[DllImport("AVProWindowsMedia")]
	public static extern void SeekUnit(int handle, float position);

	// Token: 0x06001245 RID: 4677
	[DllImport("AVProWindowsMedia")]
	public static extern void SeekSeconds(int handle, float position);

	// Token: 0x06001246 RID: 4678
	[DllImport("AVProWindowsMedia")]
	public static extern void SeekFrames(int handle, uint position);

	// Token: 0x06001247 RID: 4679
	[DllImport("AVProWindowsMedia")]
	public static extern float GetCurrentPositionSeconds(int handle);

	// Token: 0x06001248 RID: 4680
	[DllImport("AVProWindowsMedia")]
	public static extern uint GetCurrentPositionFrames(int handle);

	// Token: 0x06001249 RID: 4681
	[DllImport("AVProWindowsMedia")]
	public static extern bool IsLooping(int handle);

	// Token: 0x0600124A RID: 4682
	[DllImport("AVProWindowsMedia")]
	public static extern float GetPlaybackRate(int handle);

	// Token: 0x0600124B RID: 4683
	[DllImport("AVProWindowsMedia")]
	public static extern float GetAudioBalance(int handle);

	// Token: 0x0600124C RID: 4684
	[DllImport("AVProWindowsMedia")]
	public static extern bool IsFinishedPlaying(int handle);

	// Token: 0x0600124D RID: 4685
	[DllImport("AVProWindowsMedia")]
	public static extern void SetVolume(int handle, float volume);

	// Token: 0x0600124E RID: 4686
	[DllImport("AVProWindowsMedia")]
	public static extern void SetLooping(int handle, bool loop);

	// Token: 0x0600124F RID: 4687
	[DllImport("AVProWindowsMedia")]
	public static extern void SetDisplayFrameRange(int handle, int min, int max);

	// Token: 0x06001250 RID: 4688
	[DllImport("AVProWindowsMedia")]
	public static extern void SetPlaybackRate(int handle, float rate);

	// Token: 0x06001251 RID: 4689
	[DllImport("AVProWindowsMedia")]
	public static extern void SetAudioBalance(int handle, float balance);

	// Token: 0x06001252 RID: 4690
	[DllImport("AVProWindowsMedia")]
	public static extern void SetAudioChannelMatrix(int handle, float[] values, int numValues);

	// Token: 0x06001253 RID: 4691
	[DllImport("AVProWindowsMedia")]
	public static extern void SetAudioDelay(int handle, int ms);

	// Token: 0x06001254 RID: 4692
	[DllImport("AVProWindowsMedia")]
	public static extern bool Update(int handle);

	// Token: 0x06001255 RID: 4693
	[DllImport("AVProWindowsMedia")]
	public static extern bool IsNextFrameReadyForGrab(int handle);

	// Token: 0x06001256 RID: 4694
	[DllImport("AVProWindowsMedia")]
	public static extern int GetLastFrameUploaded(int handle);

	// Token: 0x06001257 RID: 4695
	[DllImport("AVProWindowsMedia")]
	public static extern bool UpdateTextureGL(int handle, int textureID, ref int frameNumber);

	// Token: 0x06001258 RID: 4696
	[DllImport("AVProWindowsMedia")]
	public static extern bool GetFramePixels(int handle, IntPtr data, int bufferWidth, int bufferHeight, ref int frameNumber);

	// Token: 0x06001259 RID: 4697
	[DllImport("AVProWindowsMedia")]
	public static extern bool SetTexturePointer(int handle, IntPtr data);

	// Token: 0x0600125A RID: 4698
	[DllImport("AVProWindowsMedia")]
	public static extern IntPtr GetTexturePointer(int handle);

	// Token: 0x0600125B RID: 4699
	[DllImport("AVProWindowsMedia")]
	public static extern float GetCaptureFrameRate(int handle);

	// Token: 0x0600125C RID: 4700
	[DllImport("AVProWindowsMedia")]
	public static extern void SetFrameBufferSize(int handle, int read, int write);

	// Token: 0x0600125D RID: 4701
	[DllImport("AVProWindowsMedia")]
	public static extern long GetLastFrameBufferedTime(int handle);

	// Token: 0x0600125E RID: 4702
	[DllImport("AVProWindowsMedia")]
	public static extern IntPtr GetLastFrameBuffered(int handle);

	// Token: 0x0600125F RID: 4703
	[DllImport("AVProWindowsMedia")]
	public static extern IntPtr GetFrameFromBufferAtTime(int handle, long time);

	// Token: 0x06001260 RID: 4704
	[DllImport("AVProWindowsMedia")]
	public static extern int GetNumFrameBuffers(int handle);

	// Token: 0x06001261 RID: 4705
	[DllImport("AVProWindowsMedia")]
	public static extern void GetFrameBufferTimes(int handle, IntPtr dest, int destSizeBytes);

	// Token: 0x06001262 RID: 4706
	[DllImport("AVProWindowsMedia")]
	public static extern void FlushFrameBuffers(int handle);

	// Token: 0x06001263 RID: 4707
	[DllImport("AVProWindowsMedia")]
	public static extern int GetLastBufferUploaded(int handle);

	// Token: 0x06001264 RID: 4708
	[DllImport("AVProWindowsMedia")]
	public static extern int GetReadWriteBufferDistance(int handle);

	// Token: 0x04000F9B RID: 3995
	public const int PluginID = 262209536;

	// Token: 0x0200020A RID: 522
	public enum VideoFrameFormat
	{
		// Token: 0x04000F9D RID: 3997
		RAW_BGRA32,
		// Token: 0x04000F9E RID: 3998
		YUV_422_YUY2,
		// Token: 0x04000F9F RID: 3999
		YUV_422_UYVY,
		// Token: 0x04000FA0 RID: 4000
		YUV_422_YVYU,
		// Token: 0x04000FA1 RID: 4001
		YUV_422_HDYC,
		// Token: 0x04000FA2 RID: 4002
		YUV_420_NV12 = 7,
		// Token: 0x04000FA3 RID: 4003
		Hap_RGB = 9,
		// Token: 0x04000FA4 RID: 4004
		Hap_RGBA,
		// Token: 0x04000FA5 RID: 4005
		Hap_RGB_HQ
	}

	// Token: 0x02000211 RID: 529
	public enum PluginEvent
	{
		// Token: 0x04000FCA RID: 4042
		UpdateAllTextures
	}
}
