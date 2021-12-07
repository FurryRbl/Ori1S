using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x0200020A RID: 522
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class GUIStyleState
	{
		// Token: 0x06002020 RID: 8224 RVA: 0x000256B8 File Offset: 0x000238B8
		public GUIStyleState()
		{
			this.Init();
		}

		// Token: 0x06002021 RID: 8225 RVA: 0x000256C8 File Offset: 0x000238C8
		private GUIStyleState(GUIStyle sourceStyle, IntPtr source)
		{
			this.m_SourceStyle = sourceStyle;
			this.m_Ptr = source;
		}

		// Token: 0x06002022 RID: 8226 RVA: 0x000256E0 File Offset: 0x000238E0
		internal static GUIStyleState ProduceGUIStyleStateFromDeserialization(GUIStyle sourceStyle, IntPtr source)
		{
			GUIStyleState guistyleState = new GUIStyleState(sourceStyle, source);
			guistyleState.m_Background = guistyleState.GetBackgroundInternalFromDeserialization();
			return guistyleState;
		}

		// Token: 0x06002023 RID: 8227 RVA: 0x00025704 File Offset: 0x00023904
		internal static GUIStyleState GetGUIStyleState(GUIStyle sourceStyle, IntPtr source)
		{
			GUIStyleState guistyleState = new GUIStyleState(sourceStyle, source);
			guistyleState.m_Background = guistyleState.GetBackgroundInternal();
			return guistyleState;
		}

		// Token: 0x06002024 RID: 8228 RVA: 0x00025728 File Offset: 0x00023928
		~GUIStyleState()
		{
			if (this.m_SourceStyle == null)
			{
				this.Cleanup();
			}
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x06002025 RID: 8229 RVA: 0x00025770 File Offset: 0x00023970
		// (set) Token: 0x06002026 RID: 8230 RVA: 0x00025778 File Offset: 0x00023978
		public Texture2D background
		{
			get
			{
				return this.GetBackgroundInternal();
			}
			set
			{
				this.SetBackgroundInternal(value);
				this.m_Background = value;
			}
		}

		// Token: 0x06002027 RID: 8231
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Init();

		// Token: 0x06002028 RID: 8232
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Cleanup();

		// Token: 0x06002029 RID: 8233
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetBackgroundInternal(Texture2D value);

		// Token: 0x0600202A RID: 8234
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Texture2D GetBackgroundInternalFromDeserialization();

		// Token: 0x0600202B RID: 8235
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Texture2D GetBackgroundInternal();

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x0600202C RID: 8236 RVA: 0x00025788 File Offset: 0x00023988
		// (set) Token: 0x0600202D RID: 8237 RVA: 0x000257A0 File Offset: 0x000239A0
		public Color textColor
		{
			get
			{
				Color result;
				this.INTERNAL_get_textColor(out result);
				return result;
			}
			set
			{
				this.INTERNAL_set_textColor(ref value);
			}
		}

		// Token: 0x0600202E RID: 8238
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_textColor(out Color value);

		// Token: 0x0600202F RID: 8239
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_textColor(ref Color value);

		// Token: 0x04000810 RID: 2064
		[NonSerialized]
		internal IntPtr m_Ptr;

		// Token: 0x04000811 RID: 2065
		private readonly GUIStyle m_SourceStyle;

		// Token: 0x04000812 RID: 2066
		[NonSerialized]
		private Texture2D m_Background;
	}
}
