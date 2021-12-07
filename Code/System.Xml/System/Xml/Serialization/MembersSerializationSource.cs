using System;
using System.Globalization;
using System.Text;

namespace System.Xml.Serialization
{
	// Token: 0x0200026A RID: 618
	internal class MembersSerializationSource : SerializationSource
	{
		// Token: 0x0600190D RID: 6413 RVA: 0x00084638 File Offset: 0x00082838
		public MembersSerializationSource(string elementName, bool hasWrapperElement, XmlReflectionMember[] members, bool writeAccessors, bool literalFormat, string namspace, Type[] includedTypes) : base(namspace, includedTypes)
		{
			this.elementName = elementName;
			this.hasWrapperElement = hasWrapperElement;
			this.writeAccessors = writeAccessors;
			this.literalFormat = literalFormat;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(members.Length.ToString(CultureInfo.InvariantCulture));
			foreach (XmlReflectionMember xmlReflectionMember in members)
			{
				xmlReflectionMember.AddKeyHash(stringBuilder);
			}
			this.membersHash = stringBuilder.ToString();
		}

		// Token: 0x0600190E RID: 6414 RVA: 0x000846B8 File Offset: 0x000828B8
		public override bool Equals(object o)
		{
			MembersSerializationSource membersSerializationSource = o as MembersSerializationSource;
			return membersSerializationSource != null && !(this.literalFormat = membersSerializationSource.literalFormat) && !(this.elementName != membersSerializationSource.elementName) && this.hasWrapperElement == membersSerializationSource.hasWrapperElement && !(this.membersHash != membersSerializationSource.membersHash) && base.BaseEquals(membersSerializationSource);
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x00084734 File Offset: 0x00082934
		public override int GetHashCode()
		{
			return this.membersHash.GetHashCode();
		}

		// Token: 0x04000A71 RID: 2673
		private string elementName;

		// Token: 0x04000A72 RID: 2674
		private bool hasWrapperElement;

		// Token: 0x04000A73 RID: 2675
		private string membersHash;

		// Token: 0x04000A74 RID: 2676
		private bool writeAccessors;

		// Token: 0x04000A75 RID: 2677
		private bool literalFormat;
	}
}
