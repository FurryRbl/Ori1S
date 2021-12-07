using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000267 RID: 615
	internal abstract class SerializationSource
	{
		// Token: 0x06001903 RID: 6403 RVA: 0x000843E4 File Offset: 0x000825E4
		public SerializationSource(string namspace, Type[] includedTypes)
		{
			this.namspace = namspace;
			this.includedTypes = includedTypes;
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x00084404 File Offset: 0x00082604
		protected bool BaseEquals(SerializationSource other)
		{
			if (this.namspace != other.namspace)
			{
				return false;
			}
			if (this.canBeGenerated != other.canBeGenerated)
			{
				return false;
			}
			if (this.includedTypes == null)
			{
				return other.includedTypes == null;
			}
			if (other.includedTypes == null || this.includedTypes.Length != other.includedTypes.Length)
			{
				return false;
			}
			for (int i = 0; i < this.includedTypes.Length; i++)
			{
				if (!this.includedTypes[i].Equals(other.includedTypes[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06001905 RID: 6405 RVA: 0x000844AC File Offset: 0x000826AC
		// (set) Token: 0x06001906 RID: 6406 RVA: 0x000844B4 File Offset: 0x000826B4
		public virtual bool CanBeGenerated
		{
			get
			{
				return this.canBeGenerated;
			}
			set
			{
				this.canBeGenerated = value;
			}
		}

		// Token: 0x04000A69 RID: 2665
		private Type[] includedTypes;

		// Token: 0x04000A6A RID: 2666
		private string namspace;

		// Token: 0x04000A6B RID: 2667
		private bool canBeGenerated = true;
	}
}
