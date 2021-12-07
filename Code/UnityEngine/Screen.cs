using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000039 RID: 57
	public sealed class Screen
	{
		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060002D0 RID: 720
		public static extern Resolution[] resolutions { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x00003BE4 File Offset: 0x00001DE4
		// (set) Token: 0x060002D2 RID: 722 RVA: 0x00003BF0 File Offset: 0x00001DF0
		[Obsolete("Property lockCursor has been deprecated. Use Cursor.lockState and Cursor.visible instead.")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static bool lockCursor
		{
			get
			{
				return CursorLockMode.Locked == Cursor.lockState;
			}
			set
			{
				if (value)
				{
					Cursor.visible = false;
					Cursor.lockState = CursorLockMode.Locked;
				}
				else
				{
					Cursor.lockState = CursorLockMode.None;
					Cursor.visible = true;
				}
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060002D3 RID: 723
		public static extern Resolution currentResolution { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x060002D4 RID: 724
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetResolution(int width, int height, bool fullscreen, [UnityEngine.Internal.DefaultValue("0")] int preferredRefreshRate);

		// Token: 0x060002D5 RID: 725 RVA: 0x00003C18 File Offset: 0x00001E18
		[ExcludeFromDocs]
		public static void SetResolution(int width, int height, bool fullscreen)
		{
			int preferredRefreshRate = 0;
			Screen.SetResolution(width, height, fullscreen, preferredRefreshRate);
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060002D6 RID: 726
		public static extern int width { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060002D7 RID: 727
		public static extern int height { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060002D8 RID: 728
		public static extern float dpi { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060002D9 RID: 729
		// (set) Token: 0x060002DA RID: 730
		public static extern bool fullScreen { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060002DB RID: 731
		// (set) Token: 0x060002DC RID: 732
		public static extern bool autorotateToPortrait { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060002DD RID: 733
		// (set) Token: 0x060002DE RID: 734
		public static extern bool autorotateToPortraitUpsideDown { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060002DF RID: 735
		// (set) Token: 0x060002E0 RID: 736
		public static extern bool autorotateToLandscapeLeft { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060002E1 RID: 737
		// (set) Token: 0x060002E2 RID: 738
		public static extern bool autorotateToLandscapeRight { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060002E3 RID: 739
		// (set) Token: 0x060002E4 RID: 740
		public static extern ScreenOrientation orientation { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060002E5 RID: 741
		// (set) Token: 0x060002E6 RID: 742
		public static extern int sleepTimeout { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
