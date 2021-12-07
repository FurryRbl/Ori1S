using System;

namespace System.Xml.Serialization
{
	/// <summary>Contains fields that can be used to pass event delegates to a thread-safe <see cref="Overload:System.Xml.Serialization.XmlSerializer.Deserialize" /> method of the <see cref="T:System.Xml.Serialization.XmlSerializer" />.</summary>
	// Token: 0x0200028B RID: 651
	public struct XmlDeserializationEvents
	{
		/// <summary>Gets or sets an object that represents the method that handles the <see cref="E:System.Xml.Serialization.XmlSerializer.UnknownAttribute" /> event.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlAttributeEventHandler" /> that points to the event handler.</returns>
		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06001A73 RID: 6771 RVA: 0x0008A894 File Offset: 0x00088A94
		// (set) Token: 0x06001A74 RID: 6772 RVA: 0x0008A89C File Offset: 0x00088A9C
		public XmlAttributeEventHandler OnUnknownAttribute
		{
			get
			{
				return this.onUnknownAttribute;
			}
			set
			{
				this.onUnknownAttribute = value;
			}
		}

		/// <summary>Gets or sets an object that represents the method that handles the <see cref="E:System.Xml.Serialization.XmlSerializer.UnknownElement" /> event.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlElementEventHandler" /> that points to the event handler.</returns>
		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x06001A75 RID: 6773 RVA: 0x0008A8A8 File Offset: 0x00088AA8
		// (set) Token: 0x06001A76 RID: 6774 RVA: 0x0008A8B0 File Offset: 0x00088AB0
		public XmlElementEventHandler OnUnknownElement
		{
			get
			{
				return this.onUnknownElement;
			}
			set
			{
				this.onUnknownElement = value;
			}
		}

		/// <summary>Gets or sets an object that represents the method that handles the <see cref="E:System.Xml.Serialization.XmlSerializer.UnknownNode" /> event.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.XmlNodeEventHandler" /> that points to the event handler.</returns>
		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x06001A77 RID: 6775 RVA: 0x0008A8BC File Offset: 0x00088ABC
		// (set) Token: 0x06001A78 RID: 6776 RVA: 0x0008A8C4 File Offset: 0x00088AC4
		public XmlNodeEventHandler OnUnknownNode
		{
			get
			{
				return this.onUnknownNode;
			}
			set
			{
				this.onUnknownNode = value;
			}
		}

		/// <summary>Gets or sets an object that represents the method that handles the <see cref="E:System.Xml.Serialization.XmlSerializer.UnreferencedObject" /> event.</summary>
		/// <returns>An <see cref="T:System.Xml.Serialization.UnreferencedObjectEventHandler" /> that points to the event handler.</returns>
		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x06001A79 RID: 6777 RVA: 0x0008A8D0 File Offset: 0x00088AD0
		// (set) Token: 0x06001A7A RID: 6778 RVA: 0x0008A8D8 File Offset: 0x00088AD8
		public UnreferencedObjectEventHandler OnUnreferencedObject
		{
			get
			{
				return this.onUnreferencedObject;
			}
			set
			{
				this.onUnreferencedObject = value;
			}
		}

		// Token: 0x04000AD8 RID: 2776
		private XmlAttributeEventHandler onUnknownAttribute;

		// Token: 0x04000AD9 RID: 2777
		private XmlElementEventHandler onUnknownElement;

		// Token: 0x04000ADA RID: 2778
		private XmlNodeEventHandler onUnknownNode;

		// Token: 0x04000ADB RID: 2779
		private UnreferencedObjectEventHandler onUnreferencedObject;
	}
}
