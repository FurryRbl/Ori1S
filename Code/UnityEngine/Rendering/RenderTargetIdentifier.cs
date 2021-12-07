using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020002BE RID: 702
	public struct RenderTargetIdentifier
	{
		// Token: 0x060025C4 RID: 9668 RVA: 0x00034688 File Offset: 0x00032888
		public RenderTargetIdentifier(BuiltinRenderTextureType type)
		{
			this.m_Type = type;
			this.m_NameID = -1;
			this.m_InstanceID = 0;
		}

		// Token: 0x060025C5 RID: 9669 RVA: 0x000346A0 File Offset: 0x000328A0
		public RenderTargetIdentifier(string name)
		{
			this.m_Type = BuiltinRenderTextureType.None;
			this.m_NameID = Shader.PropertyToID(name);
			this.m_InstanceID = 0;
		}

		// Token: 0x060025C6 RID: 9670 RVA: 0x000346BC File Offset: 0x000328BC
		public RenderTargetIdentifier(int nameID)
		{
			this.m_Type = BuiltinRenderTextureType.None;
			this.m_NameID = nameID;
			this.m_InstanceID = 0;
		}

		// Token: 0x060025C7 RID: 9671 RVA: 0x000346D4 File Offset: 0x000328D4
		public RenderTargetIdentifier(RenderTexture rt)
		{
			this.m_Type = BuiltinRenderTextureType.None;
			this.m_NameID = -1;
			this.m_InstanceID = ((!rt) ? 0 : rt.GetInstanceID());
		}

		// Token: 0x060025C8 RID: 9672 RVA: 0x00034704 File Offset: 0x00032904
		public static implicit operator RenderTargetIdentifier(BuiltinRenderTextureType type)
		{
			return new RenderTargetIdentifier(type);
		}

		// Token: 0x060025C9 RID: 9673 RVA: 0x0003470C File Offset: 0x0003290C
		public static implicit operator RenderTargetIdentifier(string name)
		{
			return new RenderTargetIdentifier(name);
		}

		// Token: 0x060025CA RID: 9674 RVA: 0x00034714 File Offset: 0x00032914
		public static implicit operator RenderTargetIdentifier(int nameID)
		{
			return new RenderTargetIdentifier(nameID);
		}

		// Token: 0x060025CB RID: 9675 RVA: 0x0003471C File Offset: 0x0003291C
		public static implicit operator RenderTargetIdentifier(RenderTexture rt)
		{
			return new RenderTargetIdentifier(rt);
		}

		// Token: 0x04000B66 RID: 2918
		private BuiltinRenderTextureType m_Type;

		// Token: 0x04000B67 RID: 2919
		private int m_NameID;

		// Token: 0x04000B68 RID: 2920
		private int m_InstanceID;
	}
}
