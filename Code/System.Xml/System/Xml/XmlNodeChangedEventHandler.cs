using System;

namespace System.Xml
{
	/// <summary>Represents the method that handles <see cref="E:System.Xml.XmlDocument.NodeChanged" />, <see cref="E:System.Xml.XmlDocument.NodeChanging" />, <see cref="E:System.Xml.XmlDocument.NodeInserted" />, <see cref="E:System.Xml.XmlDocument.NodeInserting" />, <see cref="E:System.Xml.XmlDocument.NodeRemoved" /> and <see cref="E:System.Xml.XmlDocument.NodeRemoving" /> events.</summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">An <see cref="T:System.Xml.XmlNodeChangedEventArgs" /> containing the event data. </param>
	// Token: 0x020002D1 RID: 721
	// (Invoke) Token: 0x06001E99 RID: 7833
	public delegate void XmlNodeChangedEventHandler(object sender, XmlNodeChangedEventArgs e);
}
