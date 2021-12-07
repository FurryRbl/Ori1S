using System;
using System.Text;

namespace System.Xml.Serialization
{
	/// <summary>Specifies that the member can be further detected by using an enumeration.</summary>
	// Token: 0x02000289 RID: 649
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
	public class XmlChoiceIdentifierAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlChoiceIdentifierAttribute" /> class.</summary>
		// Token: 0x06001A51 RID: 6737 RVA: 0x00089838 File Offset: 0x00087A38
		public XmlChoiceIdentifierAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlChoiceIdentifierAttribute" /> class.</summary>
		/// <param name="name">The member name that returns the enumeration used to detect a choice. </param>
		// Token: 0x06001A52 RID: 6738 RVA: 0x00089840 File Offset: 0x00087A40
		public XmlChoiceIdentifierAttribute(string name)
		{
			this.memberName = name;
		}

		/// <summary>Gets or sets the name of the field that returns the enumeration to use when detecting types.</summary>
		/// <returns>The name of a field that returns an enumeration.</returns>
		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06001A53 RID: 6739 RVA: 0x00089850 File Offset: 0x00087A50
		// (set) Token: 0x06001A54 RID: 6740 RVA: 0x0008986C File Offset: 0x00087A6C
		public string MemberName
		{
			get
			{
				if (this.memberName == null)
				{
					return string.Empty;
				}
				return this.memberName;
			}
			set
			{
				this.memberName = value;
			}
		}

		// Token: 0x06001A55 RID: 6741 RVA: 0x00089878 File Offset: 0x00087A78
		internal void AddKeyHash(StringBuilder sb)
		{
			sb.Append("XCA ");
			KeyHelper.AddField(sb, 1, this.memberName);
			sb.Append('|');
		}

		// Token: 0x04000AD3 RID: 2771
		private string memberName;
	}
}
