using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200002E RID: 46
	public sealed class MaterialPropertyBlock
	{
		// Token: 0x06000232 RID: 562 RVA: 0x00003094 File Offset: 0x00001294
		public MaterialPropertyBlock()
		{
			this.InitBlock();
		}

		// Token: 0x06000233 RID: 563
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void InitBlock();

		// Token: 0x06000234 RID: 564
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void DestroyBlock();

		// Token: 0x06000235 RID: 565 RVA: 0x000030A4 File Offset: 0x000012A4
		~MaterialPropertyBlock()
		{
			this.DestroyBlock();
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000236 RID: 566
		public extern bool isEmpty { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000237 RID: 567 RVA: 0x000030E0 File Offset: 0x000012E0
		public void SetFloat(string name, float value)
		{
			this.SetFloat(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000238 RID: 568
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetFloat(int nameID, float value);

		// Token: 0x06000239 RID: 569 RVA: 0x000030F0 File Offset: 0x000012F0
		public void SetVector(string name, Vector4 value)
		{
			this.SetVector(Shader.PropertyToID(name), value);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00003100 File Offset: 0x00001300
		public void SetVector(int nameID, Vector4 value)
		{
			MaterialPropertyBlock.INTERNAL_CALL_SetVector(this, nameID, ref value);
		}

		// Token: 0x0600023B RID: 571
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetVector(MaterialPropertyBlock self, int nameID, ref Vector4 value);

		// Token: 0x0600023C RID: 572 RVA: 0x0000310C File Offset: 0x0000130C
		public void SetColor(string name, Color value)
		{
			this.SetColor(Shader.PropertyToID(name), value);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000311C File Offset: 0x0000131C
		public void SetColor(int nameID, Color value)
		{
			MaterialPropertyBlock.INTERNAL_CALL_SetColor(this, nameID, ref value);
		}

		// Token: 0x0600023E RID: 574
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetColor(MaterialPropertyBlock self, int nameID, ref Color value);

		// Token: 0x0600023F RID: 575 RVA: 0x00003128 File Offset: 0x00001328
		public void SetMatrix(string name, Matrix4x4 value)
		{
			this.SetMatrix(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00003138 File Offset: 0x00001338
		public void SetMatrix(int nameID, Matrix4x4 value)
		{
			MaterialPropertyBlock.INTERNAL_CALL_SetMatrix(this, nameID, ref value);
		}

		// Token: 0x06000241 RID: 577
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetMatrix(MaterialPropertyBlock self, int nameID, ref Matrix4x4 value);

		// Token: 0x06000242 RID: 578 RVA: 0x00003144 File Offset: 0x00001344
		public void SetTexture(string name, Texture value)
		{
			this.SetTexture(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000243 RID: 579
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetTexture(int nameID, Texture value);

		// Token: 0x06000244 RID: 580 RVA: 0x00003154 File Offset: 0x00001354
		[Obsolete("AddFloat has been deprecated. Please use SetFloat instead.")]
		public void AddFloat(string name, float value)
		{
			this.AddFloat(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00003164 File Offset: 0x00001364
		[Obsolete("AddFloat has been deprecated. Please use SetFloat instead.")]
		public void AddFloat(int nameID, float value)
		{
			this.SetFloat(nameID, value);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00003170 File Offset: 0x00001370
		[Obsolete("AddVector has been deprecated. Please use SetVector instead.")]
		public void AddVector(string name, Vector4 value)
		{
			this.AddVector(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00003180 File Offset: 0x00001380
		[Obsolete("AddVector has been deprecated. Please use SetVector instead.")]
		public void AddVector(int nameID, Vector4 value)
		{
			this.SetVector(nameID, value);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000318C File Offset: 0x0000138C
		[Obsolete("AddColor has been deprecated. Please use SetColor instead.")]
		public void AddColor(string name, Color value)
		{
			this.AddColor(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000319C File Offset: 0x0000139C
		[Obsolete("AddColor has been deprecated. Please use SetColor instead.")]
		public void AddColor(int nameID, Color value)
		{
			this.SetColor(nameID, value);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x000031A8 File Offset: 0x000013A8
		[Obsolete("AddMatrix has been deprecated. Please use SetMatrix instead.")]
		public void AddMatrix(string name, Matrix4x4 value)
		{
			this.AddMatrix(Shader.PropertyToID(name), value);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x000031B8 File Offset: 0x000013B8
		[Obsolete("AddMatrix has been deprecated. Please use SetMatrix instead.")]
		public void AddMatrix(int nameID, Matrix4x4 value)
		{
			this.SetMatrix(nameID, value);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x000031C4 File Offset: 0x000013C4
		[Obsolete("AddTexture has been deprecated. Please use SetTexture instead.")]
		public void AddTexture(string name, Texture value)
		{
			this.AddTexture(Shader.PropertyToID(name), value);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x000031D4 File Offset: 0x000013D4
		[Obsolete("AddTexture has been deprecated. Please use SetTexture instead.")]
		public void AddTexture(int nameID, Texture value)
		{
			this.SetTexture(nameID, value);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x000031E0 File Offset: 0x000013E0
		public float GetFloat(string name)
		{
			return this.GetFloat(Shader.PropertyToID(name));
		}

		// Token: 0x0600024F RID: 591
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetFloat(int nameID);

		// Token: 0x06000250 RID: 592 RVA: 0x000031F0 File Offset: 0x000013F0
		public Vector4 GetVector(string name)
		{
			return this.GetVector(Shader.PropertyToID(name));
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00003200 File Offset: 0x00001400
		public Vector4 GetVector(int nameID)
		{
			Vector4 result;
			MaterialPropertyBlock.INTERNAL_CALL_GetVector(this, nameID, out result);
			return result;
		}

		// Token: 0x06000252 RID: 594
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_GetVector(MaterialPropertyBlock self, int nameID, out Vector4 value);

		// Token: 0x06000253 RID: 595 RVA: 0x00003218 File Offset: 0x00001418
		public Matrix4x4 GetMatrix(string name)
		{
			return this.GetMatrix(Shader.PropertyToID(name));
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00003228 File Offset: 0x00001428
		public Matrix4x4 GetMatrix(int nameID)
		{
			Matrix4x4 result;
			MaterialPropertyBlock.INTERNAL_CALL_GetMatrix(this, nameID, out result);
			return result;
		}

		// Token: 0x06000255 RID: 597
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_GetMatrix(MaterialPropertyBlock self, int nameID, out Matrix4x4 value);

		// Token: 0x06000256 RID: 598 RVA: 0x00003240 File Offset: 0x00001440
		public Texture GetTexture(string name)
		{
			return this.GetTexture(Shader.PropertyToID(name));
		}

		// Token: 0x06000257 RID: 599
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Texture GetTexture(int nameID);

		// Token: 0x06000258 RID: 600
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Clear();

		// Token: 0x0400008B RID: 139
		internal IntPtr m_Ptr;
	}
}
