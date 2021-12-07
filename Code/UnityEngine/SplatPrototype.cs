using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001CF RID: 463
	[UsedByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class SplatPrototype
	{
		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06001BFF RID: 7167 RVA: 0x0001A858 File Offset: 0x00018A58
		// (set) Token: 0x06001C00 RID: 7168 RVA: 0x0001A860 File Offset: 0x00018A60
		public Texture2D texture
		{
			get
			{
				return this.m_Texture;
			}
			set
			{
				this.m_Texture = value;
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06001C01 RID: 7169 RVA: 0x0001A86C File Offset: 0x00018A6C
		// (set) Token: 0x06001C02 RID: 7170 RVA: 0x0001A874 File Offset: 0x00018A74
		public Texture2D normalMap
		{
			get
			{
				return this.m_NormalMap;
			}
			set
			{
				this.m_NormalMap = value;
			}
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06001C03 RID: 7171 RVA: 0x0001A880 File Offset: 0x00018A80
		// (set) Token: 0x06001C04 RID: 7172 RVA: 0x0001A888 File Offset: 0x00018A88
		public Vector2 tileSize
		{
			get
			{
				return this.m_TileSize;
			}
			set
			{
				this.m_TileSize = value;
			}
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06001C05 RID: 7173 RVA: 0x0001A894 File Offset: 0x00018A94
		// (set) Token: 0x06001C06 RID: 7174 RVA: 0x0001A89C File Offset: 0x00018A9C
		public Vector2 tileOffset
		{
			get
			{
				return this.m_TileOffset;
			}
			set
			{
				this.m_TileOffset = value;
			}
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x06001C07 RID: 7175 RVA: 0x0001A8A8 File Offset: 0x00018AA8
		// (set) Token: 0x06001C08 RID: 7176 RVA: 0x0001A8DC File Offset: 0x00018ADC
		public Color specular
		{
			get
			{
				return new Color(this.m_SpecularMetallic.x, this.m_SpecularMetallic.y, this.m_SpecularMetallic.z);
			}
			set
			{
				this.m_SpecularMetallic.x = value.r;
				this.m_SpecularMetallic.y = value.g;
				this.m_SpecularMetallic.z = value.b;
			}
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x06001C09 RID: 7177 RVA: 0x0001A920 File Offset: 0x00018B20
		// (set) Token: 0x06001C0A RID: 7178 RVA: 0x0001A930 File Offset: 0x00018B30
		public float metallic
		{
			get
			{
				return this.m_SpecularMetallic.w;
			}
			set
			{
				this.m_SpecularMetallic.w = value;
			}
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x06001C0B RID: 7179 RVA: 0x0001A940 File Offset: 0x00018B40
		// (set) Token: 0x06001C0C RID: 7180 RVA: 0x0001A948 File Offset: 0x00018B48
		public float smoothness
		{
			get
			{
				return this.m_Smoothness;
			}
			set
			{
				this.m_Smoothness = value;
			}
		}

		// Token: 0x0400059D RID: 1437
		private Texture2D m_Texture;

		// Token: 0x0400059E RID: 1438
		private Texture2D m_NormalMap;

		// Token: 0x0400059F RID: 1439
		private Vector2 m_TileSize = new Vector2(15f, 15f);

		// Token: 0x040005A0 RID: 1440
		private Vector2 m_TileOffset = new Vector2(0f, 0f);

		// Token: 0x040005A1 RID: 1441
		private Vector4 m_SpecularMetallic = new Vector4(0f, 0f, 0f, 0f);

		// Token: 0x040005A2 RID: 1442
		private float m_Smoothness;
	}
}
