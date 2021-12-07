using System;

namespace System.Xml
{
	/// <summary>Enables a class to return an <see cref="T:System.Xml.XmlNode" /> from the current context or position.</summary>
	// Token: 0x020000D9 RID: 217
	public interface IHasXmlNode
	{
		/// <summary>Returns the <see cref="T:System.Xml.XmlNode" /> for the current position.</summary>
		/// <returns>The XmlNode for the current position.</returns>
		// Token: 0x060007E4 RID: 2020
		XmlNode GetNode();
	}
}
