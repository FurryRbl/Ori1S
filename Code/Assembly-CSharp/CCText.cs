using System;
using UnityEngine;

// Token: 0x020001F2 RID: 498
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
[ExecuteInEditMode]
public sealed class CCText : MonoBehaviour
{
	// Token: 0x17000301 RID: 769
	public char this[int index]
	{
		get
		{
			return this.text[index];
		}
	}

	// Token: 0x17000302 RID: 770
	// (get) Token: 0x06001117 RID: 4375 RVA: 0x0004DF87 File Offset: 0x0004C187
	// (set) Token: 0x06001118 RID: 4376 RVA: 0x0004DF8F File Offset: 0x0004C18F
	public CCText.AlignmentMode Alignment
	{
		get
		{
			return this.alignment;
		}
		set
		{
			if (this.alignment != value)
			{
				this.alignment = value;
				this.UpdateText();
			}
		}
	}

	// Token: 0x17000303 RID: 771
	// (get) Token: 0x06001119 RID: 4377 RVA: 0x0004DFAA File Offset: 0x0004C1AA
	// (set) Token: 0x0600111A RID: 4378 RVA: 0x0004DFB2 File Offset: 0x0004C1B2
	public CCText.BoundingMode Bounding
	{
		get
		{
			return this.bounding;
		}
		set
		{
			if (this.bounding != value)
			{
				this.bounding = value;
				this.UpdateText();
			}
		}
	}

	// Token: 0x17000304 RID: 772
	// (get) Token: 0x0600111B RID: 4379 RVA: 0x0004DFCD File Offset: 0x0004C1CD
	// (set) Token: 0x0600111C RID: 4380 RVA: 0x0004DFD5 File Offset: 0x0004C1D5
	public CCText.HorizontalAnchorMode HorizontalAnchor
	{
		get
		{
			return this.horizontalAnchor;
		}
		set
		{
			if (this.horizontalAnchor != value)
			{
				this.horizontalAnchor = value;
				this.UpdateText();
			}
		}
	}

	// Token: 0x17000305 RID: 773
	// (get) Token: 0x0600111D RID: 4381 RVA: 0x0004DFF0 File Offset: 0x0004C1F0
	// (set) Token: 0x0600111E RID: 4382 RVA: 0x0004DFF8 File Offset: 0x0004C1F8
	public CCText.VerticalAnchorMode VerticalAnchor
	{
		get
		{
			return this.verticalAnchor;
		}
		set
		{
			if (this.verticalAnchor != value)
			{
				this.verticalAnchor = value;
				this.UpdateText();
			}
		}
	}

	// Token: 0x17000306 RID: 774
	// (get) Token: 0x0600111F RID: 4383 RVA: 0x0004E014 File Offset: 0x0004C214
	public Vector3 CaretMinBounds
	{
		get
		{
			if (this.font == null)
			{
				return this.minBounds;
			}
			Vector3 result = this.minBounds;
			result.x += this.font.leftMargin;
			result.y += this.font.bottomMargin;
			return result;
		}
	}

	// Token: 0x17000307 RID: 775
	// (get) Token: 0x06001120 RID: 4384 RVA: 0x0004E074 File Offset: 0x0004C274
	public Vector3 CaretMaxBounds
	{
		get
		{
			if (this.font == null)
			{
				return this.maxBounds;
			}
			Vector3 result = this.maxBounds;
			result.x -= this.font.rightMargin;
			result.y -= this.font.topMargin;
			return result;
		}
	}

	// Token: 0x17000308 RID: 776
	// (get) Token: 0x06001121 RID: 4385 RVA: 0x0004E0D3 File Offset: 0x0004C2D3
	// (set) Token: 0x06001122 RID: 4386 RVA: 0x0004E0DB File Offset: 0x0004C2DB
	public int ChunkSize
	{
		get
		{
			return this.chunkSize;
		}
		set
		{
			this.chunkSize = ((value >= 1) ? value : 1);
		}
	}

	// Token: 0x17000309 RID: 777
	// (get) Token: 0x06001123 RID: 4387 RVA: 0x0004E0F1 File Offset: 0x0004C2F1
	// (set) Token: 0x06001124 RID: 4388 RVA: 0x0004E0FC File Offset: 0x0004C2FC
	public Color Color
	{
		get
		{
			return this.color;
		}
		set
		{
			if (this.color != value)
			{
				this.color = value;
				this.ResetColors();
				this.UpdateText();
			}
		}
	}

	// Token: 0x1700030A RID: 778
	// (get) Token: 0x06001125 RID: 4389 RVA: 0x0004E12D File Offset: 0x0004C32D
	// (set) Token: 0x06001126 RID: 4390 RVA: 0x0004E135 File Offset: 0x0004C335
	public CCFont Font
	{
		get
		{
			return this.font;
		}
		set
		{
			if (this.font != value && (value == null || value.IsValid))
			{
				this.font = value;
				this.UpdateText();
			}
		}
	}

	// Token: 0x1700030B RID: 779
	// (get) Token: 0x06001127 RID: 4391 RVA: 0x0004E16C File Offset: 0x0004C36C
	public int Length
	{
		get
		{
			return this.text.Length;
		}
	}

	// Token: 0x1700030C RID: 780
	// (get) Token: 0x06001128 RID: 4392 RVA: 0x0004E179 File Offset: 0x0004C379
	public int LineCount
	{
		get
		{
			return this.lineCount;
		}
	}

	// Token: 0x1700030D RID: 781
	// (get) Token: 0x06001129 RID: 4393 RVA: 0x0004E181 File Offset: 0x0004C381
	// (set) Token: 0x0600112A RID: 4394 RVA: 0x0004E18C File Offset: 0x0004C38C
	public float LineHeight
	{
		get
		{
			return this.lineHeight;
		}
		set
		{
			if (this.lineHeight != value)
			{
				this.lineHeight = ((value >= 0f) ? value : 0f);
				this.UpdateText();
			}
		}
	}

	// Token: 0x1700030E RID: 782
	// (get) Token: 0x0600112B RID: 4395 RVA: 0x0004E1C7 File Offset: 0x0004C3C7
	public float LineWidth
	{
		get
		{
			return this.lineWidth;
		}
	}

	// Token: 0x1700030F RID: 783
	// (get) Token: 0x0600112C RID: 4396 RVA: 0x0004E1CF File Offset: 0x0004C3CF
	// (set) Token: 0x0600112D RID: 4397 RVA: 0x0004E1D8 File Offset: 0x0004C3D8
	public CCTextModifier Modifier
	{
		get
		{
			return this.modifier;
		}
		set
		{
			if (this.modifier != value)
			{
				this.modifier = value;
				this.ResetColors();
				this.UpdateText();
			}
		}
	}

	// Token: 0x17000310 RID: 784
	// (get) Token: 0x0600112E RID: 4398 RVA: 0x0004E209 File Offset: 0x0004C409
	// (set) Token: 0x0600112F RID: 4399 RVA: 0x0004E211 File Offset: 0x0004C411
	public Vector3 Offset
	{
		get
		{
			return this.offset;
		}
		set
		{
			if (this.offset != value)
			{
				this.offset = value;
				this.UpdateText();
			}
		}
	}

	// Token: 0x17000311 RID: 785
	// (get) Token: 0x06001130 RID: 4400 RVA: 0x0004E231 File Offset: 0x0004C431
	public int SpriteCount
	{
		get
		{
			return this.spriteCount;
		}
	}

	// Token: 0x17000312 RID: 786
	// (get) Token: 0x06001131 RID: 4401 RVA: 0x0004E239 File Offset: 0x0004C439
	public int UsedSpriteCount
	{
		get
		{
			return this.usedSpriteCount;
		}
	}

	// Token: 0x17000313 RID: 787
	// (get) Token: 0x06001132 RID: 4402 RVA: 0x0004E241 File Offset: 0x0004C441
	// (set) Token: 0x06001133 RID: 4403 RVA: 0x0004E249 File Offset: 0x0004C449
	public string Text
	{
		get
		{
			return this.text;
		}
		set
		{
			this.text = ((value != null) ? value : string.Empty);
			this.UpdateText();
		}
	}

	// Token: 0x17000314 RID: 788
	// (get) Token: 0x06001134 RID: 4404 RVA: 0x0004E268 File Offset: 0x0004C468
	// (set) Token: 0x06001135 RID: 4405 RVA: 0x0004E270 File Offset: 0x0004C470
	public float TabSize
	{
		get
		{
			return this.tabSize;
		}
		set
		{
			if (this.tabSize != value)
			{
				this.tabSize = ((value >= 0.001f) ? value : 0.001f);
				this.UpdateText();
			}
		}
	}

	// Token: 0x17000315 RID: 789
	// (get) Token: 0x06001136 RID: 4406 RVA: 0x0004E2AB File Offset: 0x0004C4AB
	// (set) Token: 0x06001137 RID: 4407 RVA: 0x0004E2B4 File Offset: 0x0004C4B4
	public float Width
	{
		get
		{
			return this.width;
		}
		set
		{
			if (this.width != value)
			{
				this.width = ((value >= 0f) ? value : 0f);
				this.UpdateText();
			}
		}
	}

	// Token: 0x06001138 RID: 4408 RVA: 0x0004E2F0 File Offset: 0x0004C4F0
	private void Awake()
	{
		base.GetComponent<MeshFilter>().mesh = (this.mesh = new Mesh());
		this.mesh.name = "CCText Mesh";
		this.mesh.hideFlags = HideFlags.HideAndDontSave;
		this.meshCollider = base.GetComponent<MeshCollider>();
	}

	// Token: 0x06001139 RID: 4409 RVA: 0x0004E33F File Offset: 0x0004C53F
	private void Start()
	{
		if (this.vertices == null)
		{
			this.UpdateText();
		}
	}

	// Token: 0x0600113A RID: 4410 RVA: 0x0004E352 File Offset: 0x0004C552
	private void OnDestroy()
	{
		if (this.mesh)
		{
			base.GetComponent<MeshFilter>().mesh = null;
			UnityEngine.Object.DestroyImmediate(this.mesh);
		}
	}

	// Token: 0x0600113B RID: 4411 RVA: 0x0004E37C File Offset: 0x0004C57C
	public int TriangleToCharacterIndex(int triangleIndex)
	{
		int i = 0;
		int num = 0;
		int length = this.Length;
		while (i < length)
		{
			if (this.Text[i] > ' ')
			{
				if (num == triangleIndex || num + 1 == triangleIndex)
				{
					return i;
				}
				num += 2;
			}
			i++;
		}
		return -1;
	}

	// Token: 0x0600113C RID: 4412 RVA: 0x0004E3D4 File Offset: 0x0004C5D4
	public int HitCharacterIndex(RaycastHit hit)
	{
		if (hit.collider == this.meshCollider)
		{
			return this.TriangleToCharacterIndex(hit.triangleIndex);
		}
		return -1;
	}

	// Token: 0x0600113D RID: 4413 RVA: 0x0004E408 File Offset: 0x0004C608
	public void ResetColors()
	{
		if (this.colors == null)
		{
			return;
		}
		int i = 0;
		int num = this.colors.Length;
		while (i < num)
		{
			this.colors[i] = this.color;
			this.colors[i + 1] = this.color;
			this.colors[i + 2] = this.color;
			this.colors[i + 3] = this.color;
			i += 4;
		}
		this.mesh.colors = this.colors;
	}

	// Token: 0x0600113E RID: 4414 RVA: 0x0004E4B0 File Offset: 0x0004C6B0
	public void UpdateText()
	{
		if (this.font == null)
		{
			if (this.vertices != null)
			{
				this.HideSprites(0);
				this.mesh.vertices = this.vertices;
			}
			return;
		}
		Vector3 vector = this.offset;
		if (this.bounding == CCText.BoundingMode.Caret)
		{
			this.lineWidth = this.width;
		}
		else
		{
			this.lineWidth = this.width - this.font.leftMargin - this.font.rightMargin;
		}
		this.UpdateFromString();
		if (this.vertices == null)
		{
			return;
		}
		if (this.alignment != CCText.AlignmentMode.Left)
		{
			if (this.alignment == CCText.AlignmentMode.Justify)
			{
				this.JustifyChars();
			}
			else
			{
				this.AlignCharsCenterOrRight();
			}
		}
		if (this.bounding == CCText.BoundingMode.Caret)
		{
			switch (this.horizontalAnchor)
			{
			case CCText.HorizontalAnchorMode.Center:
				vector.x -= this.width * 0.5f;
				break;
			case CCText.HorizontalAnchorMode.Right:
				vector.x -= this.width;
				break;
			}
			switch (this.verticalAnchor)
			{
			case CCText.VerticalAnchorMode.Middle:
				vector.y += (float)this.lineCount * this.lineHeight * 0.5f;
				break;
			case CCText.VerticalAnchorMode.Bottom:
				vector.y += (float)this.lineCount * this.lineHeight;
				break;
			case CCText.VerticalAnchorMode.Baseline:
				vector.y += this.font.baseline;
				break;
			}
			this.minBounds.x = vector.x - this.font.leftMargin;
			this.minBounds.y = vector.y - this.font.bottomMargin - this.lineHeight * (float)(this.lineCount - 1) - 1f;
			this.maxBounds.x = vector.x + this.font.rightMargin + this.width;
			this.maxBounds.y = vector.y + this.font.topMargin;
		}
		else
		{
			switch (this.horizontalAnchor)
			{
			case CCText.HorizontalAnchorMode.Left:
				vector.x += this.font.leftMargin;
				break;
			case CCText.HorizontalAnchorMode.Center:
				vector.x += this.font.leftMargin - this.width * 0.5f;
				break;
			case CCText.HorizontalAnchorMode.Right:
				vector.x += this.font.leftMargin - this.width;
				break;
			}
			switch (this.verticalAnchor)
			{
			case CCText.VerticalAnchorMode.Top:
				vector.y -= this.font.topMargin;
				break;
			case CCText.VerticalAnchorMode.Middle:
				vector.y += (this.font.bottomMargin - this.font.topMargin + (float)this.lineCount * this.lineHeight) * 0.5f;
				break;
			case CCText.VerticalAnchorMode.Bottom:
				vector.y += this.font.bottomMargin + (float)this.lineCount * this.lineHeight;
				break;
			case CCText.VerticalAnchorMode.Baseline:
				vector.y += this.font.baseline;
				break;
			}
			this.minBounds.x = vector.x - this.font.leftMargin;
			this.minBounds.y = vector.y - this.font.bottomMargin - this.lineHeight * (float)(this.lineCount - 1) - 1f;
			this.maxBounds.x = this.minBounds.x + this.width;
			this.maxBounds.y = vector.y + this.font.topMargin;
		}
		this.minBounds.z = vector.z;
		this.maxBounds.z = vector.z;
		int num = 0;
		int i = 0;
		int length = this.text.Length;
		while (i < length)
		{
			char c = this.text[i];
			if (c > ' ')
			{
				Vector3 position = this.vertices[num];
				position.x += vector.x;
				position.y += vector.y;
				position.z = vector.z;
				this.UpdateSprite(this.font[c], num, position);
				num += 4;
			}
			i++;
		}
		if (this.modifier != null && Application.isPlaying)
		{
			this.modifier.Modify(this);
		}
		this.mesh.vertices = this.vertices;
		this.mesh.uv = this.uv;
		this.mesh.bounds = new Bounds((this.minBounds + this.maxBounds) * 0.5f, this.maxBounds - this.minBounds);
		this.mesh.colors = this.colors;
		if (this.meshCollider != null)
		{
			this.meshCollider.sharedMesh = null;
			this.meshCollider.sharedMesh = this.mesh;
		}
	}

	// Token: 0x0600113F RID: 4415 RVA: 0x0004EA74 File Offset: 0x0004CC74
	private void UpdateFromString()
	{
		this.lineCount = 1;
		int num = 0;
		int length = this.text.Length;
		if (length == 0)
		{
			this.HideSprites(0);
			this.usedSpriteCount = 0;
			return;
		}
		Vector3 vector = this.caretStart;
		Vector3 vector2 = CCText.zeroVector3;
		int num2 = 0;
		int num3 = 0;
		char c = this.text[0];
		for (;;)
		{
			if (c <= ' ')
			{
				if (++num2 == length)
				{
					break;
				}
				if (c == ' ')
				{
					vector.x += this.font.spaceAdvance;
				}
				else if (c == '\n')
				{
					this.lineCount++;
					vector.x = 0f;
					vector.y -= this.lineHeight;
				}
				else if (c == '\t')
				{
					vector.x = (1f + Mathf.Floor(vector.x / this.tabSize)) * this.tabSize;
				}
				c = this.text[num2];
			}
			else
			{
				if (++num > this.spriteCount)
				{
					this.AddSpritesFromString(num2 + 1);
				}
				CCFont.Char @char = this.font[c];
				if (vector.x + @char.width > this.lineWidth && this.WordWrapFromString(num2, num3, ref vector))
				{
					this.lineCount++;
				}
				this.vertices[num3] = vector;
				vector2.x = @char.advance;
				if (++num2 == length)
				{
					goto Block_10;
				}
				c = this.text[num2];
				if (c == '\n')
				{
					vector.x += @char.xOffset + @char.width;
					vector2.y = 1f;
					vector2.z = 0f;
					this.vertices[num3 + 1].y = 1f;
				}
				else if (c <= ' ')
				{
					vector.x += @char.advance;
					vector2.y = 0f;
					vector2.z = 1f;
					this.vertices[num3 + 1].z = 1f;
				}
				else
				{
					vector2.y = 0f;
					vector2.z = 0f;
					vector.x += @char.AdvanceWithKerning(c);
				}
				this.vertices[num3 + 1] = vector2;
				num3 += 4;
			}
		}
		goto IL_2DD;
		Block_10:
		vector2.y = 0f;
		vector2.z = 0f;
		this.vertices[num3 + 1] = vector2;
		IL_2DD:
		if (num < this.usedSpriteCount)
		{
			this.HideSprites(num);
		}
		this.usedSpriteCount = num;
	}

	// Token: 0x06001140 RID: 4416 RVA: 0x0004ED78 File Offset: 0x0004CF78
	private void UpdateSprite(CCFont.Char c, int index, Vector3 position)
	{
		Vector2 vector;
		vector.x = c.uMin;
		vector.y = c.vMax;
		this.uv[index] = vector;
		vector.x = c.uMax;
		this.uv[index + 1] = vector;
		vector.y = c.vMin;
		this.uv[index + 2] = vector;
		vector.x = c.uMin;
		this.uv[index + 3] = vector;
		position.x += c.xOffset;
		position.y += c.yOffset;
		this.vertices[index] = position;
		position.x += c.width;
		this.vertices[index + 1] = position;
		position.y -= c.height;
		this.vertices[index + 2] = position;
		position.x -= c.width;
		this.vertices[index + 3] = position;
	}

	// Token: 0x06001141 RID: 4417 RVA: 0x0004EEC8 File Offset: 0x0004D0C8
	private void AlignCharsCenterOrRight()
	{
		int num = 0;
		int i = 4;
		int num2 = this.usedSpriteCount << 2;
		float num3;
		while (i < num2)
		{
			if (this.vertices[i].x == 0f)
			{
				num3 = this.lineWidth - this.vertices[i - 4].x - this.vertices[i - 3].x;
				if (num3 > 0f)
				{
					if (this.alignment == CCText.AlignmentMode.Center)
					{
						num3 *= 0.5f;
					}
					for (int j = num; j < i; j += 4)
					{
						Vector3[] array = this.vertices;
						int num4 = j;
						array[num4].x = array[num4].x + num3;
					}
				}
				num = i;
			}
			i += 4;
		}
		num3 = this.lineWidth - this.vertices[i - 4].x - this.vertices[i - 3].x;
		if (num3 > 0f)
		{
			if (this.alignment == CCText.AlignmentMode.Center)
			{
				num3 *= 0.5f;
			}
			for (int k = num; k < i; k += 4)
			{
				Vector3[] array2 = this.vertices;
				int num5 = k;
				array2[num5].x = array2[num5].x + num3;
			}
		}
	}

	// Token: 0x06001142 RID: 4418 RVA: 0x0004F00C File Offset: 0x0004D20C
	private void JustifyChars()
	{
		int first = 0;
		int i = 4;
		int num = this.usedSpriteCount << 2;
		while (i < num)
		{
			if (this.vertices[i].x == 0f)
			{
				if (this.vertices[i - 3].y == 0f)
				{
					this.JustifyLine(first, i - 4);
				}
				first = i;
			}
			i += 4;
		}
	}

	// Token: 0x06001143 RID: 4419 RVA: 0x0004F07C File Offset: 0x0004D27C
	private void JustifyLine(int first, int last)
	{
		float num = this.vertices[last].x + this.vertices[last + 1].x;
		int num2 = -1;
		for (int i = first; i <= last; i += 4)
		{
			if (this.vertices[i + 1].z == 1f)
			{
				num2++;
			}
		}
		if (num2 <= 0)
		{
			return;
		}
		float num3 = (this.lineWidth - num) / (float)num2;
		float num4 = 0f;
		for (int j = first; j <= last; j += 4)
		{
			Vector3[] array = this.vertices;
			int num5 = j;
			array[num5].x = array[num5].x + num4;
			if (this.vertices[j + 1].z == 1f)
			{
				num4 += num3;
			}
		}
	}

	// Token: 0x06001144 RID: 4420 RVA: 0x0004F154 File Offset: 0x0004D354
	private void HideSprites(int i)
	{
		i <<= 2;
		int num = this.usedSpriteCount << 2;
		while (i < num)
		{
			this.vertices[i] = CCText.zeroVector3;
			this.vertices[i + 1] = CCText.zeroVector3;
			this.vertices[i + 2] = CCText.zeroVector3;
			this.vertices[i + 3] = CCText.zeroVector3;
			i += 4;
		}
	}

	// Token: 0x06001145 RID: 4421 RVA: 0x0004F1E0 File Offset: 0x0004D3E0
	private void AddSpritesFromString(int i)
	{
		int num = 1;
		int length = this.text.Length;
		while (i < length)
		{
			if (this.text[i] > ' ')
			{
				num++;
			}
			i++;
		}
		this.AddSprites(num);
	}

	// Token: 0x06001146 RID: 4422 RVA: 0x0004F22C File Offset: 0x0004D42C
	private void AddSprites(int newSpriteCount)
	{
		newSpriteCount = this.spriteCount + ((newSpriteCount - 1) / this.chunkSize + 1) * this.chunkSize;
		this.mesh.Clear();
		int num = newSpriteCount << 2;
		Vector3[] array = this.vertices;
		this.vertices = new Vector3[num];
		this.colors = new Color[num];
		this.uv = new Vector2[num];
		this.triangles = new int[newSpriteCount * 6];
		int i = 0;
		int num2 = 0;
		int num3 = this.spriteCount << 2;
		while (i < num3)
		{
			this.vertices[i] = array[i];
			this.vertices[i + 1] = array[i + 1];
			this.colors[i] = this.color;
			this.colors[i + 1] = this.color;
			this.colors[i + 2] = this.color;
			this.colors[i + 3] = this.color;
			this.triangles[num2] = i;
			this.triangles[num2 + 1] = i + 1;
			this.triangles[num2 + 2] = i + 2;
			this.triangles[num2 + 3] = i;
			this.triangles[num2 + 4] = i + 2;
			this.triangles[num2 + 5] = i + 3;
			i += 4;
			num2 += 6;
		}
		while (i < num)
		{
			this.colors[i] = this.color;
			this.colors[i + 1] = this.color;
			this.colors[i + 2] = this.color;
			this.colors[i + 3] = this.color;
			this.triangles[num2] = i;
			this.triangles[num2 + 1] = i + 1;
			this.triangles[num2 + 2] = i + 2;
			this.triangles[num2 + 3] = i;
			this.triangles[num2 + 4] = i + 2;
			this.triangles[num2 + 5] = i + 3;
			i += 4;
			num2 += 6;
		}
		this.mesh.vertices = this.vertices;
		this.mesh.colors = this.colors;
		this.mesh.triangles = this.triangles;
		this.spriteCount = newSpriteCount;
	}

	// Token: 0x06001147 RID: 4423 RVA: 0x0004F4A0 File Offset: 0x0004D6A0
	private bool WordWrapFromString(int textIndex, int vertexIndex, ref Vector3 caret)
	{
		if (vertexIndex == 0)
		{
			return false;
		}
		if (this.text[textIndex - 1] <= ' ')
		{
			caret.x = 0f;
			caret.y -= this.lineHeight;
			return true;
		}
		int i = textIndex;
		while (this.text[i] > ' ')
		{
			if (--i < 0)
			{
				if (caret.x == 0f)
				{
					return false;
				}
				IL_7E:
				i = vertexIndex + (i - textIndex << 2);
				i += 4;
				Vector3 b = new Vector3(-this.vertices[i].x, -this.lineHeight);
				if (b.x == 0f)
				{
					return false;
				}
				while (i < vertexIndex)
				{
					this.vertices[i] += b;
					i++;
				}
				caret += b;
				return true;
			}
		}
		goto IL_7E;
	}

	// Token: 0x04000ECE RID: 3790
	private static Vector3 zeroVector3 = Vector3.zero;

	// Token: 0x04000ECF RID: 3791
	[SerializeField]
	private CCText.AlignmentMode alignment;

	// Token: 0x04000ED0 RID: 3792
	[SerializeField]
	private CCText.BoundingMode bounding;

	// Token: 0x04000ED1 RID: 3793
	[SerializeField]
	private CCText.HorizontalAnchorMode horizontalAnchor;

	// Token: 0x04000ED2 RID: 3794
	[SerializeField]
	private CCText.VerticalAnchorMode verticalAnchor;

	// Token: 0x04000ED3 RID: 3795
	[SerializeField]
	private int chunkSize = 1;

	// Token: 0x04000ED4 RID: 3796
	[SerializeField]
	private Color color = Color.black;

	// Token: 0x04000ED5 RID: 3797
	[SerializeField]
	private CCFont font;

	// Token: 0x04000ED6 RID: 3798
	private int lineCount;

	// Token: 0x04000ED7 RID: 3799
	[SerializeField]
	private float lineHeight = 1f;

	// Token: 0x04000ED8 RID: 3800
	private float lineWidth;

	// Token: 0x04000ED9 RID: 3801
	[SerializeField]
	private CCTextModifier modifier;

	// Token: 0x04000EDA RID: 3802
	[SerializeField]
	private Vector3 offset;

	// Token: 0x04000EDB RID: 3803
	private int spriteCount;

	// Token: 0x04000EDC RID: 3804
	private int usedSpriteCount;

	// Token: 0x04000EDD RID: 3805
	[SerializeField]
	private string text = string.Empty;

	// Token: 0x04000EDE RID: 3806
	[SerializeField]
	private float tabSize = 2f;

	// Token: 0x04000EDF RID: 3807
	[SerializeField]
	private float width = 10f;

	// Token: 0x04000EE0 RID: 3808
	[NonSerialized]
	public Vector3 minBounds;

	// Token: 0x04000EE1 RID: 3809
	[NonSerialized]
	public Vector3 maxBounds;

	// Token: 0x04000EE2 RID: 3810
	[NonSerialized]
	public Mesh mesh;

	// Token: 0x04000EE3 RID: 3811
	[NonSerialized]
	public Vector3[] vertices;

	// Token: 0x04000EE4 RID: 3812
	[NonSerialized]
	public Color[] colors;

	// Token: 0x04000EE5 RID: 3813
	[NonSerialized]
	public Vector2[] uv;

	// Token: 0x04000EE6 RID: 3814
	[NonSerialized]
	public int[] triangles;

	// Token: 0x04000EE7 RID: 3815
	private MeshCollider meshCollider;

	// Token: 0x04000EE8 RID: 3816
	[SerializeField]
	private Vector3 caretStart;

	// Token: 0x0200022D RID: 557
	public enum AlignmentMode
	{
		// Token: 0x04001050 RID: 4176
		Left,
		// Token: 0x04001051 RID: 4177
		Center,
		// Token: 0x04001052 RID: 4178
		Right,
		// Token: 0x04001053 RID: 4179
		Justify
	}

	// Token: 0x0200022E RID: 558
	public enum BoundingMode
	{
		// Token: 0x04001055 RID: 4181
		Caret,
		// Token: 0x04001056 RID: 4182
		Margin
	}

	// Token: 0x0200022F RID: 559
	public enum HorizontalAnchorMode
	{
		// Token: 0x04001058 RID: 4184
		Left,
		// Token: 0x04001059 RID: 4185
		Center,
		// Token: 0x0400105A RID: 4186
		Right
	}

	// Token: 0x02000230 RID: 560
	public enum VerticalAnchorMode
	{
		// Token: 0x0400105C RID: 4188
		Top,
		// Token: 0x0400105D RID: 4189
		Middle,
		// Token: 0x0400105E RID: 4190
		Bottom,
		// Token: 0x0400105F RID: 4191
		Baseline
	}
}
