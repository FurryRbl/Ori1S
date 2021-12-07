using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000E1 RID: 225
public class TextureAnimation : ScriptableObject
{
	// Token: 0x170001F3 RID: 499
	// (get) Token: 0x06000910 RID: 2320 RVA: 0x0002704B File Offset: 0x0002524B
	public float Duration
	{
		get
		{
			return this.FrameToTime((float)this.FrameGuids.Count) * (float)((!this.PingPong) ? 1 : 2);
		}
	}

	// Token: 0x06000911 RID: 2321 RVA: 0x00027073 File Offset: 0x00025273
	public float TimeToFrame(float time)
	{
		return time * 30f;
	}

	// Token: 0x06000912 RID: 2322 RVA: 0x0002707C File Offset: 0x0002527C
	public float FrameToTime(float frame)
	{
		return frame / 30f;
	}

	// Token: 0x06000913 RID: 2323 RVA: 0x00027085 File Offset: 0x00025285
	public AtlasSpriteTexture GetTextureAtTime(float time, out Atlas atlas)
	{
		return this.GetTextureAtIndex(this.TimeToFrame(time), out atlas);
	}

	// Token: 0x06000914 RID: 2324 RVA: 0x00027098 File Offset: 0x00025298
	public AtlasSpriteTexture GetTextureAtIndex(float index, out Atlas foundAtlas)
	{
		if (this.FrameGuids.Count == 0)
		{
			foundAtlas = null;
			return null;
		}
		int num;
		if (this.Loop)
		{
			num = Mathf.FloorToInt(Mathf.Repeat(index, (float)this.FrameGuids.Count));
			num = Mathf.Clamp(num, 0, this.FrameGuids.Count - 1);
		}
		else if (this.PingPong)
		{
			num = Mathf.FloorToInt(Mathf.PingPong(index, (float)this.FrameGuids.Count));
			num = Mathf.Clamp(num, 0, this.FrameGuids.Count - 1);
		}
		else
		{
			num = Mathf.FloorToInt(Mathf.Clamp(index, 0f, (float)(this.FrameGuids.Count - 1)));
		}
		MoonGuid guid = this.FrameGuids[num];
		return this.GetFrameForId(guid, out foundAtlas);
	}

	// Token: 0x06000915 RID: 2325 RVA: 0x0002716C File Offset: 0x0002536C
	public AtlasSpriteTexture GetFrameForId(MoonGuid guid, out Atlas foundAtlas)
	{
		TextureAnimation.AnimationTextureInfo animationTextureInfo;
		if (this.m_guidToTex.TryGetValue(guid, out animationTextureInfo))
		{
			foundAtlas = animationTextureInfo.Atlas;
			return animationTextureInfo.SpriteTexture;
		}
		for (int i = 0; i < this.Atlases.Count; i++)
		{
			Atlas atlas = this.Atlases[i];
			if (atlas != null)
			{
				AtlasSpriteTexture atlasSpriteTexture = atlas.FindAtlasSprite(guid);
				if (atlasSpriteTexture != null)
				{
					foundAtlas = atlas;
					this.m_guidToTex.Add(guid, new TextureAnimation.AnimationTextureInfo
					{
						Atlas = atlas,
						SpriteTexture = atlasSpriteTexture
					});
					return atlasSpriteTexture;
				}
			}
		}
		foundAtlas = null;
		return null;
	}

	// Token: 0x06000916 RID: 2326 RVA: 0x00027210 File Offset: 0x00025410
	public void SortAnimationFrames()
	{
		Atlas at;
		this.FrameGuids.Sort((MoonGuid g1, MoonGuid g2) => string.Compare(this.GetFrameForId(g1, out at).Name, this.GetFrameForId(g2, out at).Name, StringComparison.Ordinal));
	}

	// Token: 0x06000917 RID: 2327 RVA: 0x00027244 File Offset: 0x00025444
	public void AddFrameForPlatform(AtlasSpriteTexture frame, Atlas atlas, UberAtlassingPlatform platform)
	{
		if (platform == UberAtlassingPlatform.HD)
		{
			this.FrameGuids.Add(frame.Guid);
			if (!this.Atlases.Contains(atlas))
			{
				this.Atlases.Add(atlas);
			}
		}
	}

	// Token: 0x04000747 RID: 1863
	public float Speed = 30f;

	// Token: 0x04000748 RID: 1864
	public bool Loop = true;

	// Token: 0x04000749 RID: 1865
	public bool PingPong;

	// Token: 0x0400074A RID: 1866
	public bool IgnoreTimeScale;

	// Token: 0x0400074B RID: 1867
	public AnimationMetaData AnimationMetaData;

	// Token: 0x0400074C RID: 1868
	public List<MoonGuid> FrameGuids = new List<MoonGuid>();

	// Token: 0x0400074D RID: 1869
	public List<Atlas> Atlases = new List<Atlas>();

	// Token: 0x0400074E RID: 1870
	private Dictionary<MoonGuid, TextureAnimation.AnimationTextureInfo> m_guidToTex = new Dictionary<MoonGuid, TextureAnimation.AnimationTextureInfo>();

	// Token: 0x02000390 RID: 912
	private struct AnimationTextureInfo
	{
		// Token: 0x04001627 RID: 5671
		public Atlas Atlas;

		// Token: 0x04001628 RID: 5672
		public AtlasSpriteTexture SpriteTexture;
	}
}
