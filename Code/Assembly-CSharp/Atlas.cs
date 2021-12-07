using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000045 RID: 69
public class Atlas : ScriptableObject
{
	// Token: 0x170000BA RID: 186
	// (get) Token: 0x060002ED RID: 749 RVA: 0x0000C0C3 File Offset: 0x0000A2C3
	// (set) Token: 0x060002EE RID: 750 RVA: 0x0000C0CB File Offset: 0x0000A2CB
	public string TexturePath { get; set; }

	// Token: 0x060002EF RID: 751 RVA: 0x0000C0D4 File Offset: 0x0000A2D4
	public AtlasSpriteTexture FindAtlasSprite(MoonGuid guid)
	{
		if (this.m_atlasCache.Count == 0)
		{
			this.InitCache();
		}
		int index;
		return (!this.m_atlasCache.TryGetValue(guid, out index)) ? null : this.SpriteTextures[index];
	}

	// Token: 0x060002F0 RID: 752 RVA: 0x0000C11C File Offset: 0x0000A31C
	private void InitCache()
	{
		for (int i = 0; i < this.SpriteTextures.Count; i++)
		{
			AtlasSpriteTexture atlasSpriteTexture = this.SpriteTextures[i];
			if (!this.m_atlasCache.ContainsKey(atlasSpriteTexture.Guid))
			{
				this.m_atlasCache.Add(atlasSpriteTexture.Guid, i);
			}
		}
	}

	// Token: 0x060002F1 RID: 753 RVA: 0x0000C17C File Offset: 0x0000A37C
	public void ClearData()
	{
		this.SpriteTextures.Clear();
		this.Texture = null;
		this.Width = 0f;
		this.Height = 0f;
	}

	// Token: 0x060002F2 RID: 754 RVA: 0x0000C1B4 File Offset: 0x0000A3B4
	public AtlasSpriteTexture AddFrameTexture(string frameName, Vector2 centerOffset, Vector2 originalSize, Rect normalizedRect, bool flipped, MoonGuid guid)
	{
		if (originalSize == Vector2.zero)
		{
		}
		AtlasSpriteTexture atlasSpriteTexture = new AtlasSpriteTexture();
		atlasSpriteTexture.Name = frameName;
		atlasSpriteTexture.Guid = guid;
		atlasSpriteTexture.CenterOffset = centerOffset;
		atlasSpriteTexture.Flipped = flipped;
		atlasSpriteTexture.OriginalSize = originalSize;
		atlasSpriteTexture.NormalizedRect = normalizedRect;
		this.SpriteTextures.Add(atlasSpriteTexture);
		return atlasSpriteTexture;
	}

	// Token: 0x060002F3 RID: 755 RVA: 0x0000C211 File Offset: 0x0000A411
	public void InvalidateCache()
	{
		this.m_atlasCache.Clear();
	}

	// Token: 0x04000217 RID: 535
	public float Width;

	// Token: 0x04000218 RID: 536
	public float Height;

	// Token: 0x04000219 RID: 537
	public Texture2D Texture;

	// Token: 0x0400021A RID: 538
	public UberScreenMode ScreenMode = UberScreenMode.Green;

	// Token: 0x0400021B RID: 539
	public float UberScreenTweak;

	// Token: 0x0400021C RID: 540
	public List<AtlasSpriteTexture> SpriteTextures = new List<AtlasSpriteTexture>();

	// Token: 0x0400021D RID: 541
	private readonly Dictionary<MoonGuid, int> m_atlasCache = new Dictionary<MoonGuid, int>();
}
