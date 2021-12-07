using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001D8 RID: 472
	[UsedByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class TextGenerator : IDisposable
	{
		// Token: 0x06001CB2 RID: 7346 RVA: 0x0001AF9C File Offset: 0x0001919C
		public TextGenerator() : this(50)
		{
		}

		// Token: 0x06001CB3 RID: 7347 RVA: 0x0001AFA8 File Offset: 0x000191A8
		public TextGenerator(int initialCapacity)
		{
			this.m_Verts = new List<UIVertex>((initialCapacity + 1) * 4);
			this.m_Characters = new List<UICharInfo>(initialCapacity + 1);
			this.m_Lines = new List<UILineInfo>(20);
			this.Init();
		}

		// Token: 0x06001CB4 RID: 7348 RVA: 0x0001AFE4 File Offset: 0x000191E4
		void IDisposable.Dispose()
		{
			this.Dispose_cpp();
		}

		// Token: 0x06001CB5 RID: 7349 RVA: 0x0001AFEC File Offset: 0x000191EC
		~TextGenerator()
		{
			((IDisposable)this).Dispose();
		}

		// Token: 0x06001CB6 RID: 7350 RVA: 0x0001B028 File Offset: 0x00019228
		private TextGenerationSettings ValidatedSettings(TextGenerationSettings settings)
		{
			if (settings.font != null && settings.font.dynamic)
			{
				return settings;
			}
			if (settings.fontSize != 0 || settings.fontStyle != FontStyle.Normal)
			{
				Debug.LogWarning("Font size and style overrides are only supported for dynamic fonts.");
				settings.fontSize = 0;
				settings.fontStyle = FontStyle.Normal;
			}
			if (settings.resizeTextForBestFit)
			{
				Debug.LogWarning("BestFit is only supported for dynamic fonts.");
				settings.resizeTextForBestFit = false;
			}
			return settings;
		}

		// Token: 0x06001CB7 RID: 7351 RVA: 0x0001B0AC File Offset: 0x000192AC
		public void Invalidate()
		{
			this.m_HasGenerated = false;
		}

		// Token: 0x06001CB8 RID: 7352 RVA: 0x0001B0B8 File Offset: 0x000192B8
		public void GetCharacters(List<UICharInfo> characters)
		{
			this.GetCharactersInternal(characters);
		}

		// Token: 0x06001CB9 RID: 7353 RVA: 0x0001B0C4 File Offset: 0x000192C4
		public void GetLines(List<UILineInfo> lines)
		{
			this.GetLinesInternal(lines);
		}

		// Token: 0x06001CBA RID: 7354 RVA: 0x0001B0D0 File Offset: 0x000192D0
		public void GetVertices(List<UIVertex> vertices)
		{
			this.GetVerticesInternal(vertices);
		}

		// Token: 0x06001CBB RID: 7355 RVA: 0x0001B0DC File Offset: 0x000192DC
		public float GetPreferredWidth(string str, TextGenerationSettings settings)
		{
			settings.horizontalOverflow = HorizontalWrapMode.Overflow;
			settings.verticalOverflow = VerticalWrapMode.Overflow;
			settings.updateBounds = true;
			this.Populate(str, settings);
			return this.rectExtents.width;
		}

		// Token: 0x06001CBC RID: 7356 RVA: 0x0001B118 File Offset: 0x00019318
		public float GetPreferredHeight(string str, TextGenerationSettings settings)
		{
			settings.verticalOverflow = VerticalWrapMode.Overflow;
			settings.updateBounds = true;
			this.Populate(str, settings);
			return this.rectExtents.height;
		}

		// Token: 0x06001CBD RID: 7357 RVA: 0x0001B14C File Offset: 0x0001934C
		public bool Populate(string str, TextGenerationSettings settings)
		{
			if (this.m_HasGenerated && str == this.m_LastString && settings.Equals(this.m_LastSettings))
			{
				return this.m_LastValid;
			}
			return this.PopulateAlways(str, settings);
		}

		// Token: 0x06001CBE RID: 7358 RVA: 0x0001B18C File Offset: 0x0001938C
		private bool PopulateAlways(string str, TextGenerationSettings settings)
		{
			this.m_LastString = str;
			this.m_HasGenerated = true;
			this.m_CachedVerts = false;
			this.m_CachedCharacters = false;
			this.m_CachedLines = false;
			this.m_LastSettings = settings;
			TextGenerationSettings textGenerationSettings = this.ValidatedSettings(settings);
			this.m_LastValid = this.Populate_Internal(str, textGenerationSettings.font, textGenerationSettings.color, textGenerationSettings.fontSize, textGenerationSettings.scaleFactor, textGenerationSettings.lineSpacing, textGenerationSettings.fontStyle, textGenerationSettings.richText, textGenerationSettings.resizeTextForBestFit, textGenerationSettings.resizeTextMinSize, textGenerationSettings.resizeTextMaxSize, textGenerationSettings.verticalOverflow, textGenerationSettings.horizontalOverflow, textGenerationSettings.updateBounds, textGenerationSettings.textAnchor, textGenerationSettings.generationExtents, textGenerationSettings.pivot, textGenerationSettings.generateOutOfBounds, textGenerationSettings.alignByGeometry);
			return this.m_LastValid;
		}

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06001CBF RID: 7359 RVA: 0x0001B25C File Offset: 0x0001945C
		public IList<UIVertex> verts
		{
			get
			{
				if (!this.m_CachedVerts)
				{
					this.GetVertices(this.m_Verts);
					this.m_CachedVerts = true;
				}
				return this.m_Verts;
			}
		}

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06001CC0 RID: 7360 RVA: 0x0001B290 File Offset: 0x00019490
		public IList<UICharInfo> characters
		{
			get
			{
				if (!this.m_CachedCharacters)
				{
					this.GetCharacters(this.m_Characters);
					this.m_CachedCharacters = true;
				}
				return this.m_Characters;
			}
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06001CC1 RID: 7361 RVA: 0x0001B2C4 File Offset: 0x000194C4
		public IList<UILineInfo> lines
		{
			get
			{
				if (!this.m_CachedLines)
				{
					this.GetLines(this.m_Lines);
					this.m_CachedLines = true;
				}
				return this.m_Lines;
			}
		}

		// Token: 0x06001CC2 RID: 7362
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Init();

		// Token: 0x06001CC3 RID: 7363
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Dispose_cpp();

		// Token: 0x06001CC4 RID: 7364 RVA: 0x0001B2F8 File Offset: 0x000194F8
		internal bool Populate_Internal(string str, Font font, Color color, int fontSize, float scaleFactor, float lineSpacing, FontStyle style, bool richText, bool resizeTextForBestFit, int resizeTextMinSize, int resizeTextMaxSize, VerticalWrapMode verticalOverFlow, HorizontalWrapMode horizontalOverflow, bool updateBounds, TextAnchor anchor, Vector2 extents, Vector2 pivot, bool generateOutOfBounds, bool alignByGeometry)
		{
			return this.Populate_Internal_cpp(str, font, color, fontSize, scaleFactor, lineSpacing, style, richText, resizeTextForBestFit, resizeTextMinSize, resizeTextMaxSize, (int)verticalOverFlow, (int)horizontalOverflow, updateBounds, anchor, extents.x, extents.y, pivot.x, pivot.y, generateOutOfBounds, alignByGeometry);
		}

		// Token: 0x06001CC5 RID: 7365 RVA: 0x0001B348 File Offset: 0x00019548
		internal bool Populate_Internal_cpp(string str, Font font, Color color, int fontSize, float scaleFactor, float lineSpacing, FontStyle style, bool richText, bool resizeTextForBestFit, int resizeTextMinSize, int resizeTextMaxSize, int verticalOverFlow, int horizontalOverflow, bool updateBounds, TextAnchor anchor, float extentsX, float extentsY, float pivotX, float pivotY, bool generateOutOfBounds, bool alignByGeometry)
		{
			return TextGenerator.INTERNAL_CALL_Populate_Internal_cpp(this, str, font, ref color, fontSize, scaleFactor, lineSpacing, style, richText, resizeTextForBestFit, resizeTextMinSize, resizeTextMaxSize, verticalOverFlow, horizontalOverflow, updateBounds, anchor, extentsX, extentsY, pivotX, pivotY, generateOutOfBounds, alignByGeometry);
		}

		// Token: 0x06001CC6 RID: 7366
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_Populate_Internal_cpp(TextGenerator self, string str, Font font, ref Color color, int fontSize, float scaleFactor, float lineSpacing, FontStyle style, bool richText, bool resizeTextForBestFit, int resizeTextMinSize, int resizeTextMaxSize, int verticalOverFlow, int horizontalOverflow, bool updateBounds, TextAnchor anchor, float extentsX, float extentsY, float pivotX, float pivotY, bool generateOutOfBounds, bool alignByGeometry);

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x06001CC7 RID: 7367 RVA: 0x0001B384 File Offset: 0x00019584
		public Rect rectExtents
		{
			get
			{
				Rect result;
				this.INTERNAL_get_rectExtents(out result);
				return result;
			}
		}

		// Token: 0x06001CC8 RID: 7368
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_rectExtents(out Rect value);

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06001CC9 RID: 7369
		public extern int vertexCount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06001CCA RID: 7370
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetVerticesInternal(object vertices);

		// Token: 0x06001CCB RID: 7371
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern UIVertex[] GetVerticesArray();

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06001CCC RID: 7372
		public extern int characterCount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06001CCD RID: 7373 RVA: 0x0001B39C File Offset: 0x0001959C
		public int characterCountVisible
		{
			get
			{
				return (!string.IsNullOrEmpty(this.m_LastString)) ? Mathf.Min(this.m_LastString.Length, Mathf.Max(0, (this.vertexCount - 4) / 4)) : 0;
			}
		}

		// Token: 0x06001CCE RID: 7374
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetCharactersInternal(object characters);

		// Token: 0x06001CCF RID: 7375
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern UICharInfo[] GetCharactersArray();

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06001CD0 RID: 7376
		public extern int lineCount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06001CD1 RID: 7377
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetLinesInternal(object lines);

		// Token: 0x06001CD2 RID: 7378
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern UILineInfo[] GetLinesArray();

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06001CD3 RID: 7379
		public extern int fontSizeUsedForBestFit { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x040005CF RID: 1487
		internal IntPtr m_Ptr;

		// Token: 0x040005D0 RID: 1488
		private string m_LastString;

		// Token: 0x040005D1 RID: 1489
		private TextGenerationSettings m_LastSettings;

		// Token: 0x040005D2 RID: 1490
		private bool m_HasGenerated;

		// Token: 0x040005D3 RID: 1491
		private bool m_LastValid;

		// Token: 0x040005D4 RID: 1492
		private readonly List<UIVertex> m_Verts;

		// Token: 0x040005D5 RID: 1493
		private readonly List<UICharInfo> m_Characters;

		// Token: 0x040005D6 RID: 1494
		private readonly List<UILineInfo> m_Lines;

		// Token: 0x040005D7 RID: 1495
		private bool m_CachedVerts;

		// Token: 0x040005D8 RID: 1496
		private bool m_CachedCharacters;

		// Token: 0x040005D9 RID: 1497
		private bool m_CachedLines;
	}
}
