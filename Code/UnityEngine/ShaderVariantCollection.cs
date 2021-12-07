using System;
using System.Runtime.CompilerServices;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x0200008E RID: 142
	public sealed class ShaderVariantCollection : Object
	{
		// Token: 0x0600089B RID: 2203 RVA: 0x0000BC20 File Offset: 0x00009E20
		public ShaderVariantCollection()
		{
			ShaderVariantCollection.Internal_Create(this);
		}

		// Token: 0x0600089C RID: 2204
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] ShaderVariantCollection mono);

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x0600089D RID: 2205
		public extern int shaderCount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x0600089E RID: 2206
		public extern int variantCount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x0600089F RID: 2207 RVA: 0x0000BC30 File Offset: 0x00009E30
		public bool Add(ShaderVariantCollection.ShaderVariant variant)
		{
			return this.AddInternal(variant.shader, variant.passType, variant.keywords);
		}

		// Token: 0x060008A0 RID: 2208
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool AddInternal(Shader shader, PassType passType, string[] keywords);

		// Token: 0x060008A1 RID: 2209 RVA: 0x0000BC50 File Offset: 0x00009E50
		public bool Remove(ShaderVariantCollection.ShaderVariant variant)
		{
			return this.RemoveInternal(variant.shader, variant.passType, variant.keywords);
		}

		// Token: 0x060008A2 RID: 2210
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool RemoveInternal(Shader shader, PassType passType, string[] keywords);

		// Token: 0x060008A3 RID: 2211 RVA: 0x0000BC70 File Offset: 0x00009E70
		public bool Contains(ShaderVariantCollection.ShaderVariant variant)
		{
			return this.ContainsInternal(variant.shader, variant.passType, variant.keywords);
		}

		// Token: 0x060008A4 RID: 2212
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool ContainsInternal(Shader shader, PassType passType, string[] keywords);

		// Token: 0x060008A5 RID: 2213
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Clear();

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060008A6 RID: 2214
		public extern bool isWarmedUp { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x060008A7 RID: 2215
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void WarmUp();

		// Token: 0x0200008F RID: 143
		public struct ShaderVariant
		{
			// Token: 0x060008A8 RID: 2216 RVA: 0x0000BC90 File Offset: 0x00009E90
			public ShaderVariant(Shader shader, PassType passType, params string[] keywords)
			{
				this.shader = shader;
				this.passType = passType;
				this.keywords = keywords;
				ShaderVariantCollection.ShaderVariant.Internal_CheckVariant(shader, passType, keywords);
			}

			// Token: 0x060008A9 RID: 2217
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void Internal_CheckVariant(Shader shader, PassType passType, string[] keywords);

			// Token: 0x04000188 RID: 392
			public Shader shader;

			// Token: 0x04000189 RID: 393
			public PassType passType;

			// Token: 0x0400018A RID: 394
			public string[] keywords;
		}
	}
}
