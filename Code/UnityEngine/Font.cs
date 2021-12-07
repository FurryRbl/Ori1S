using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001E0 RID: 480
	public sealed class Font : Object
	{
		// Token: 0x06001D2D RID: 7469 RVA: 0x0001B930 File Offset: 0x00019B30
		public Font()
		{
			Font.Internal_CreateFont(this, null);
		}

		// Token: 0x06001D2E RID: 7470 RVA: 0x0001B940 File Offset: 0x00019B40
		public Font(string name)
		{
			Font.Internal_CreateFont(this, name);
		}

		// Token: 0x06001D2F RID: 7471 RVA: 0x0001B950 File Offset: 0x00019B50
		private Font(string[] names, int size)
		{
			Font.Internal_CreateDynamicFont(this, names, size);
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06001D30 RID: 7472 RVA: 0x0001B960 File Offset: 0x00019B60
		// (remove) Token: 0x06001D31 RID: 7473 RVA: 0x0001B978 File Offset: 0x00019B78
		public static event Action<Font> textureRebuilt;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06001D32 RID: 7474 RVA: 0x0001B990 File Offset: 0x00019B90
		// (remove) Token: 0x06001D33 RID: 7475 RVA: 0x0001B9AC File Offset: 0x00019BAC
		private event Font.FontTextureRebuildCallback m_FontTextureRebuildCallback;

		// Token: 0x06001D34 RID: 7476
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string[] GetOSInstalledFontNames();

		// Token: 0x06001D35 RID: 7477
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateFont([Writable] Font _font, string name);

		// Token: 0x06001D36 RID: 7478
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateDynamicFont([Writable] Font _font, string[] _names, int size);

		// Token: 0x06001D37 RID: 7479 RVA: 0x0001B9C8 File Offset: 0x00019BC8
		public static Font CreateDynamicFontFromOSFont(string fontname, int size)
		{
			return new Font(new string[]
			{
				fontname
			}, size);
		}

		// Token: 0x06001D38 RID: 7480 RVA: 0x0001B9E8 File Offset: 0x00019BE8
		public static Font CreateDynamicFontFromOSFont(string[] fontnames, int size)
		{
			return new Font(fontnames, size);
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06001D39 RID: 7481
		// (set) Token: 0x06001D3A RID: 7482
		public extern Material material { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06001D3B RID: 7483
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasCharacter(char c);

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06001D3C RID: 7484
		// (set) Token: 0x06001D3D RID: 7485
		public extern string[] fontNames { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06001D3E RID: 7486
		// (set) Token: 0x06001D3F RID: 7487
		public extern CharacterInfo[] characterInfo { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06001D40 RID: 7488
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RequestCharactersInTexture(string characters, [UnityEngine.Internal.DefaultValue("0")] int size, [UnityEngine.Internal.DefaultValue("FontStyle.Normal")] FontStyle style);

		// Token: 0x06001D41 RID: 7489 RVA: 0x0001BA00 File Offset: 0x00019C00
		[ExcludeFromDocs]
		public void RequestCharactersInTexture(string characters, int size)
		{
			FontStyle style = FontStyle.Normal;
			this.RequestCharactersInTexture(characters, size, style);
		}

		// Token: 0x06001D42 RID: 7490 RVA: 0x0001BA18 File Offset: 0x00019C18
		[ExcludeFromDocs]
		public void RequestCharactersInTexture(string characters)
		{
			FontStyle style = FontStyle.Normal;
			int size = 0;
			this.RequestCharactersInTexture(characters, size, style);
		}

		// Token: 0x06001D43 RID: 7491 RVA: 0x0001BA34 File Offset: 0x00019C34
		[RequiredByNativeCode]
		private static void InvokeTextureRebuilt_Internal(Font font)
		{
			Action<Font> action = Font.textureRebuilt;
			if (action != null)
			{
				action(font);
			}
			if (font.m_FontTextureRebuildCallback != null)
			{
				font.m_FontTextureRebuildCallback();
			}
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06001D44 RID: 7492 RVA: 0x0001BA6C File Offset: 0x00019C6C
		// (set) Token: 0x06001D45 RID: 7493 RVA: 0x0001BA74 File Offset: 0x00019C74
		[Obsolete("Font.textureRebuildCallback has been deprecated. Use Font.textureRebuilt instead.")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public Font.FontTextureRebuildCallback textureRebuildCallback
		{
			get
			{
				return this.m_FontTextureRebuildCallback;
			}
			set
			{
				this.m_FontTextureRebuildCallback = value;
			}
		}

		// Token: 0x06001D46 RID: 7494 RVA: 0x0001BA80 File Offset: 0x00019C80
		public static int GetMaxVertsForString(string str)
		{
			return str.Length * 4 + 4;
		}

		// Token: 0x06001D47 RID: 7495
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool GetCharacterInfo(char ch, out CharacterInfo info, [UnityEngine.Internal.DefaultValue("0")] int size, [UnityEngine.Internal.DefaultValue("FontStyle.Normal")] FontStyle style);

		// Token: 0x06001D48 RID: 7496 RVA: 0x0001BA8C File Offset: 0x00019C8C
		[ExcludeFromDocs]
		public bool GetCharacterInfo(char ch, out CharacterInfo info, int size)
		{
			FontStyle style = FontStyle.Normal;
			return this.GetCharacterInfo(ch, out info, size, style);
		}

		// Token: 0x06001D49 RID: 7497 RVA: 0x0001BAA8 File Offset: 0x00019CA8
		[ExcludeFromDocs]
		public bool GetCharacterInfo(char ch, out CharacterInfo info)
		{
			FontStyle style = FontStyle.Normal;
			int size = 0;
			return this.GetCharacterInfo(ch, out info, size, style);
		}

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06001D4A RID: 7498
		public extern bool dynamic { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x06001D4B RID: 7499
		public extern int ascent { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x06001D4C RID: 7500
		public extern int lineHeight { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06001D4D RID: 7501
		public extern int fontSize { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x02000348 RID: 840
		// (Invoke) Token: 0x0600287A RID: 10362
		[EditorBrowsable(EditorBrowsableState.Never)]
		public delegate void FontTextureRebuildCallback();
	}
}
