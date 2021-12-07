using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Audio
{
	// Token: 0x02000190 RID: 400
	public class AudioMixer : Object
	{
		// Token: 0x06001903 RID: 6403 RVA: 0x00018860 File Offset: 0x00016A60
		internal AudioMixer()
		{
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06001904 RID: 6404
		// (set) Token: 0x06001905 RID: 6405
		public extern AudioMixerGroup outputAudioMixerGroup { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06001906 RID: 6406
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern AudioMixerGroup[] FindMatchingGroups(string subPath);

		// Token: 0x06001907 RID: 6407
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern AudioMixerSnapshot FindSnapshot(string name);

		// Token: 0x06001908 RID: 6408 RVA: 0x00018868 File Offset: 0x00016A68
		private void TransitionToSnapshot(AudioMixerSnapshot snapshot, float timeToReach)
		{
			if (snapshot == null)
			{
				throw new ArgumentException("null Snapshot passed to AudioMixer.TransitionToSnapshot of AudioMixer '" + base.name + "'");
			}
			if (snapshot.audioMixer != this)
			{
				throw new ArgumentException(string.Concat(new string[]
				{
					"Snapshot '",
					snapshot.name,
					"' passed to AudioMixer.TransitionToSnapshot is not a snapshot from AudioMixer '",
					base.name,
					"'"
				}));
			}
			snapshot.TransitionTo(timeToReach);
		}

		// Token: 0x06001909 RID: 6409
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void TransitionToSnapshots(AudioMixerSnapshot[] snapshots, float[] weights, float timeToReach);

		// Token: 0x0600190A RID: 6410
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool SetFloat(string name, float value);

		// Token: 0x0600190B RID: 6411
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool ClearFloat(string name);

		// Token: 0x0600190C RID: 6412
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool GetFloat(string name, out float value);
	}
}
