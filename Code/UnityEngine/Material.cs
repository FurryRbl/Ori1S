using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200008D RID: 141
	public class Material : Object
	{
		// Token: 0x06000854 RID: 2132 RVA: 0x0000B8F4 File Offset: 0x00009AF4
		[Obsolete("Creating materials from shader source string will be removed in the future. Use Shader assets instead.")]
		public Material(string contents)
		{
			Material.Internal_CreateWithString(this, contents);
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x0000B904 File Offset: 0x00009B04
		public Material(Shader shader)
		{
			Material.Internal_CreateWithShader(this, shader);
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x0000B914 File Offset: 0x00009B14
		public Material(Material source)
		{
			Material.Internal_CreateWithMaterial(this, source);
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000857 RID: 2135
		// (set) Token: 0x06000858 RID: 2136
		public extern Shader shader { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000859 RID: 2137 RVA: 0x0000B924 File Offset: 0x00009B24
		// (set) Token: 0x0600085A RID: 2138 RVA: 0x0000B934 File Offset: 0x00009B34
		public Color color
		{
			get
			{
				return this.GetColor("_Color");
			}
			set
			{
				this.SetColor("_Color", value);
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x0600085B RID: 2139 RVA: 0x0000B944 File Offset: 0x00009B44
		// (set) Token: 0x0600085C RID: 2140 RVA: 0x0000B954 File Offset: 0x00009B54
		public Texture mainTexture
		{
			get
			{
				return this.GetTexture("_MainTex");
			}
			set
			{
				this.SetTexture("_MainTex", value);
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x0600085D RID: 2141 RVA: 0x0000B964 File Offset: 0x00009B64
		// (set) Token: 0x0600085E RID: 2142 RVA: 0x0000B974 File Offset: 0x00009B74
		public Vector2 mainTextureOffset
		{
			get
			{
				return this.GetTextureOffset("_MainTex");
			}
			set
			{
				this.SetTextureOffset("_MainTex", value);
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x0600085F RID: 2143 RVA: 0x0000B984 File Offset: 0x00009B84
		// (set) Token: 0x06000860 RID: 2144 RVA: 0x0000B994 File Offset: 0x00009B94
		public Vector2 mainTextureScale
		{
			get
			{
				return this.GetTextureScale("_MainTex");
			}
			set
			{
				this.SetTextureScale("_MainTex", value);
			}
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x0000B9A4 File Offset: 0x00009BA4
		public void SetColor(string propertyName, Color color)
		{
			this.SetColor(Shader.PropertyToID(propertyName), color);
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x0000B9B4 File Offset: 0x00009BB4
		public void SetColor(int nameID, Color color)
		{
			Material.INTERNAL_CALL_SetColor(this, nameID, ref color);
		}

		// Token: 0x06000863 RID: 2147
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetColor(Material self, int nameID, ref Color color);

		// Token: 0x06000864 RID: 2148 RVA: 0x0000B9C0 File Offset: 0x00009BC0
		public Color GetColor(string propertyName)
		{
			return this.GetColor(Shader.PropertyToID(propertyName));
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x0000B9D0 File Offset: 0x00009BD0
		public Color GetColor(int nameID)
		{
			Color result;
			Material.INTERNAL_CALL_GetColor(this, nameID, out result);
			return result;
		}

		// Token: 0x06000866 RID: 2150
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_GetColor(Material self, int nameID, out Color value);

		// Token: 0x06000867 RID: 2151 RVA: 0x0000B9E8 File Offset: 0x00009BE8
		public void SetVector(string propertyName, Vector4 vector)
		{
			this.SetColor(propertyName, new Color(vector.x, vector.y, vector.z, vector.w));
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0000BA20 File Offset: 0x00009C20
		public void SetVector(int nameID, Vector4 vector)
		{
			this.SetColor(nameID, new Color(vector.x, vector.y, vector.z, vector.w));
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x0000BA58 File Offset: 0x00009C58
		public Vector4 GetVector(string propertyName)
		{
			Color color = this.GetColor(propertyName);
			return new Vector4(color.r, color.g, color.b, color.a);
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x0000BA90 File Offset: 0x00009C90
		public Vector4 GetVector(int nameID)
		{
			Color color = this.GetColor(nameID);
			return new Vector4(color.r, color.g, color.b, color.a);
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0000BAC8 File Offset: 0x00009CC8
		public void SetTexture(string propertyName, Texture texture)
		{
			this.SetTexture(Shader.PropertyToID(propertyName), texture);
		}

		// Token: 0x0600086C RID: 2156
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetTexture(int nameID, Texture texture);

		// Token: 0x0600086D RID: 2157 RVA: 0x0000BAD8 File Offset: 0x00009CD8
		public Texture GetTexture(string propertyName)
		{
			return this.GetTexture(Shader.PropertyToID(propertyName));
		}

		// Token: 0x0600086E RID: 2158
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Texture GetTexture(int nameID);

		// Token: 0x0600086F RID: 2159
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_GetTextureScaleAndOffset(Material mat, string name, out Vector4 output);

		// Token: 0x06000870 RID: 2160 RVA: 0x0000BAE8 File Offset: 0x00009CE8
		public void SetTextureOffset(string propertyName, Vector2 offset)
		{
			Material.INTERNAL_CALL_SetTextureOffset(this, propertyName, ref offset);
		}

		// Token: 0x06000871 RID: 2161
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetTextureOffset(Material self, string propertyName, ref Vector2 offset);

		// Token: 0x06000872 RID: 2162 RVA: 0x0000BAF4 File Offset: 0x00009CF4
		public Vector2 GetTextureOffset(string propertyName)
		{
			Vector4 vector;
			Material.Internal_GetTextureScaleAndOffset(this, propertyName, out vector);
			return new Vector2(vector.z, vector.w);
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x0000BB20 File Offset: 0x00009D20
		public void SetTextureScale(string propertyName, Vector2 scale)
		{
			Material.INTERNAL_CALL_SetTextureScale(this, propertyName, ref scale);
		}

		// Token: 0x06000874 RID: 2164
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetTextureScale(Material self, string propertyName, ref Vector2 scale);

		// Token: 0x06000875 RID: 2165 RVA: 0x0000BB2C File Offset: 0x00009D2C
		public Vector2 GetTextureScale(string propertyName)
		{
			Vector4 vector;
			Material.Internal_GetTextureScaleAndOffset(this, propertyName, out vector);
			return new Vector2(vector.x, vector.y);
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x0000BB58 File Offset: 0x00009D58
		public void SetMatrix(string propertyName, Matrix4x4 matrix)
		{
			this.SetMatrix(Shader.PropertyToID(propertyName), matrix);
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x0000BB68 File Offset: 0x00009D68
		public void SetMatrix(int nameID, Matrix4x4 matrix)
		{
			Material.INTERNAL_CALL_SetMatrix(this, nameID, ref matrix);
		}

		// Token: 0x06000878 RID: 2168
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetMatrix(Material self, int nameID, ref Matrix4x4 matrix);

		// Token: 0x06000879 RID: 2169 RVA: 0x0000BB74 File Offset: 0x00009D74
		public Matrix4x4 GetMatrix(string propertyName)
		{
			return this.GetMatrix(Shader.PropertyToID(propertyName));
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x0000BB84 File Offset: 0x00009D84
		public Matrix4x4 GetMatrix(int nameID)
		{
			Matrix4x4 result;
			Material.INTERNAL_CALL_GetMatrix(this, nameID, out result);
			return result;
		}

		// Token: 0x0600087B RID: 2171
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_GetMatrix(Material self, int nameID, out Matrix4x4 value);

		// Token: 0x0600087C RID: 2172 RVA: 0x0000BB9C File Offset: 0x00009D9C
		public void SetFloat(string propertyName, float value)
		{
			this.SetFloat(Shader.PropertyToID(propertyName), value);
		}

		// Token: 0x0600087D RID: 2173
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetFloat(int nameID, float value);

		// Token: 0x0600087E RID: 2174 RVA: 0x0000BBAC File Offset: 0x00009DAC
		public float GetFloat(string propertyName)
		{
			return this.GetFloat(Shader.PropertyToID(propertyName));
		}

		// Token: 0x0600087F RID: 2175
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetFloat(int nameID);

		// Token: 0x06000880 RID: 2176 RVA: 0x0000BBBC File Offset: 0x00009DBC
		public void SetInt(string propertyName, int value)
		{
			this.SetFloat(propertyName, (float)value);
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x0000BBC8 File Offset: 0x00009DC8
		public void SetInt(int nameID, int value)
		{
			this.SetFloat(nameID, (float)value);
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x0000BBD4 File Offset: 0x00009DD4
		public int GetInt(string propertyName)
		{
			return (int)this.GetFloat(propertyName);
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x0000BBE0 File Offset: 0x00009DE0
		public int GetInt(int nameID)
		{
			return (int)this.GetFloat(nameID);
		}

		// Token: 0x06000884 RID: 2180
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetBuffer(string propertyName, ComputeBuffer buffer);

		// Token: 0x06000885 RID: 2181 RVA: 0x0000BBEC File Offset: 0x00009DEC
		public bool HasProperty(string propertyName)
		{
			return this.HasProperty(Shader.PropertyToID(propertyName));
		}

		// Token: 0x06000886 RID: 2182
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasProperty(int nameID);

		// Token: 0x06000887 RID: 2183
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern string GetTag(string tag, bool searchFallbacks, [DefaultValue("\"\"")] string defaultValue);

		// Token: 0x06000888 RID: 2184 RVA: 0x0000BBFC File Offset: 0x00009DFC
		[ExcludeFromDocs]
		public string GetTag(string tag, bool searchFallbacks)
		{
			string empty = string.Empty;
			return this.GetTag(tag, searchFallbacks, empty);
		}

		// Token: 0x06000889 RID: 2185
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetOverrideTag(string tag, string val);

		// Token: 0x0600088A RID: 2186
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Lerp(Material start, Material end, float t);

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x0600088B RID: 2187
		public extern int passCount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x0600088C RID: 2188
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool SetPass(int pass);

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x0600088D RID: 2189
		// (set) Token: 0x0600088E RID: 2190
		public extern int renderQueue { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x0600088F RID: 2191 RVA: 0x0000BC18 File Offset: 0x00009E18
		[Obsolete("Creating materials from shader source string will be removed in the future. Use Shader assets instead.")]
		public static Material Create(string scriptContents)
		{
			return new Material(scriptContents);
		}

		// Token: 0x06000890 RID: 2192
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateWithString([Writable] Material mono, string contents);

		// Token: 0x06000891 RID: 2193
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateWithShader([Writable] Material mono, Shader shader);

		// Token: 0x06000892 RID: 2194
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateWithMaterial([Writable] Material mono, Material source);

		// Token: 0x06000893 RID: 2195
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void CopyPropertiesFromMaterial(Material mat);

		// Token: 0x06000894 RID: 2196
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void EnableKeyword(string keyword);

		// Token: 0x06000895 RID: 2197
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void DisableKeyword(string keyword);

		// Token: 0x06000896 RID: 2198
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsKeywordEnabled(string keyword);

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000897 RID: 2199
		// (set) Token: 0x06000898 RID: 2200
		public extern string[] shaderKeywords { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000899 RID: 2201
		// (set) Token: 0x0600089A RID: 2202
		public extern MaterialGlobalIlluminationFlags globalIlluminationFlags { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }
	}
}
