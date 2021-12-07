using System;

namespace System.Xml
{
	/// <summary>Specifies the state of the <see cref="T:System.Xml.XmlWriter" />.</summary>
	// Token: 0x020000E5 RID: 229
	public enum WriteState
	{
		/// <summary>Indicates that a Write method has not yet been called.</summary>
		// Token: 0x04000497 RID: 1175
		Start,
		/// <summary>Indicates that the prolog is being written.</summary>
		// Token: 0x04000498 RID: 1176
		Prolog,
		/// <summary>Indicates that an element start tag is being written.</summary>
		// Token: 0x04000499 RID: 1177
		Element,
		/// <summary>Indicates that an attribute value is being written.</summary>
		// Token: 0x0400049A RID: 1178
		Attribute,
		/// <summary>Indicates that element content is being written.</summary>
		// Token: 0x0400049B RID: 1179
		Content,
		/// <summary>Indicates that the <see cref="M:System.Xml.XmlWriter.Close" /> method has been called.</summary>
		// Token: 0x0400049C RID: 1180
		Closed,
		/// <summary>An exception has been thrown, which has left the <see cref="T:System.Xml.XmlWriter" /> in an invalid state. You can call the <see cref="M:System.Xml.XmlWriter.Close" /> method to put the <see cref="T:System.Xml.XmlWriter" /> in the <see cref="F:System.Xml.WriteState.Closed" /> state. Any other <see cref="T:System.Xml.XmlWriter" /> method calls results in an <see cref="T:System.InvalidOperationException" />.</summary>
		// Token: 0x0400049D RID: 1181
		Error
	}
}
