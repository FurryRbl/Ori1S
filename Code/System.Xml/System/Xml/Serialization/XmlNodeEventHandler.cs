using System;

namespace System.Xml.Serialization
{
	/// <summary>Represents the method that handles the <see cref="E:System.Xml.Serialization.XmlSerializer.UnknownNode" /> event of an <see cref="T:System.Xml.Serialization.XmlSerializer" />.</summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">An <see cref="T:System.Xml.Serialization.XmlNodeEventArgs" /> that contains the event data. </param>
	// Token: 0x020002DC RID: 732
	// (Invoke) Token: 0x06001EC5 RID: 7877
	public delegate void XmlNodeEventHandler(object sender, XmlNodeEventArgs e);
}
