using System;

namespace System.Xml
{
	/// <summary>Provides data for the <see cref="E:System.Xml.XmlDocument.NodeChanged" />, <see cref="E:System.Xml.XmlDocument.NodeChanging" />, <see cref="E:System.Xml.XmlDocument.NodeInserted" />, <see cref="E:System.Xml.XmlDocument.NodeInserting" />, <see cref="E:System.Xml.XmlDocument.NodeRemoved" /> and <see cref="E:System.Xml.XmlDocument.NodeRemoving" /> events.</summary>
	// Token: 0x02000106 RID: 262
	public class XmlNodeChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlNodeChangedEventArgs" /> class.</summary>
		/// <param name="node">The <see cref="T:System.Xml.XmlNode" /> that generated the event.</param>
		/// <param name="oldParent">The old parent <see cref="T:System.Xml.XmlNode" /> of the <see cref="T:System.Xml.XmlNode" /> that generated the event.</param>
		/// <param name="newParent">The new parent <see cref="T:System.Xml.XmlNode" /> of the <see cref="T:System.Xml.XmlNode" /> that generated the event.</param>
		/// <param name="oldValue">The old value of the <see cref="T:System.Xml.XmlNode" /> that generated the event.</param>
		/// <param name="newValue">The new value of the <see cref="T:System.Xml.XmlNode" /> that generated the event.</param>
		/// <param name="action">The <see cref="T:System.Xml.XmlNodeChangedAction" />.</param>
		// Token: 0x06000A8A RID: 2698 RVA: 0x00037B74 File Offset: 0x00035D74
		public XmlNodeChangedEventArgs(XmlNode node, XmlNode oldParent, XmlNode newParent, string oldValue, string newValue, XmlNodeChangedAction action)
		{
			this._node = node;
			this._oldParent = oldParent;
			this._newParent = newParent;
			this._oldValue = oldValue;
			this._newValue = newValue;
			this._action = action;
		}

		/// <summary>Gets a value indicating what type of node change event is occurring.</summary>
		/// <returns>An XmlNodeChangedAction value describing the node change event.XmlNodeChangedAction Value Description Insert A node has been or will be inserted. Remove A node has been or will be removed. Change A node has been or will be changed. Note:The Action value does not differentiate between when the event occurred (before or after). You can create separate event handlers to handle both instances.</returns>
		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000A8B RID: 2699 RVA: 0x00037BAC File Offset: 0x00035DAC
		public XmlNodeChangedAction Action
		{
			get
			{
				return this._action;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlNode" /> that is being added, removed or changed.</summary>
		/// <returns>The XmlNode that is being added, removed or changed; this property never returns null.</returns>
		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000A8C RID: 2700 RVA: 0x00037BB4 File Offset: 0x00035DB4
		public XmlNode Node
		{
			get
			{
				return this._node;
			}
		}

		/// <summary>Gets the value of the <see cref="P:System.Xml.XmlNode.ParentNode" /> before the operation began.</summary>
		/// <returns>The value of the ParentNode before the operation began. This property returns null if the node did not have a parent.Note:For attribute nodes this property returns the <see cref="P:System.Xml.XmlAttribute.OwnerElement" />.</returns>
		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000A8D RID: 2701 RVA: 0x00037BBC File Offset: 0x00035DBC
		public XmlNode OldParent
		{
			get
			{
				return this._oldParent;
			}
		}

		/// <summary>Gets the value of the <see cref="P:System.Xml.XmlNode.ParentNode" /> after the operation completes.</summary>
		/// <returns>The value of the ParentNode after the operation completes. This property returns null if the node is being removed.Note:For attribute nodes this property returns the <see cref="P:System.Xml.XmlAttribute.OwnerElement" />.</returns>
		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000A8E RID: 2702 RVA: 0x00037BC4 File Offset: 0x00035DC4
		public XmlNode NewParent
		{
			get
			{
				return this._newParent;
			}
		}

		/// <summary>Gets the original value of the node.</summary>
		/// <returns>The original value of the node. This property returns null if the node is neither an attribute nor a text node, or if the node is being inserted.If called in a <see cref="E:System.Xml.XmlDocument.NodeChanging" /> event, OldValue returns the current value of the node that will be replaced if the change is successful. If called in a <see cref="E:System.Xml.XmlDocument.NodeChanged" /> event, OldValue returns the value of node prior to the change.</returns>
		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000A8F RID: 2703 RVA: 0x00037BCC File Offset: 0x00035DCC
		public string OldValue
		{
			get
			{
				return (this._oldValue == null) ? this._node.Value : this._oldValue;
			}
		}

		/// <summary>Gets the new value of the node.</summary>
		/// <returns>The new value of the node. This property returns null if the node is neither an attribute nor a text node, or if the node is being removed.If called in a <see cref="E:System.Xml.XmlDocument.NodeChanging" /> event, NewValue returns the value of the node if the change is successful. If called in a <see cref="E:System.Xml.XmlDocument.NodeChanged" /> event, NewValue returns the current value of the node.</returns>
		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000A90 RID: 2704 RVA: 0x00037BF0 File Offset: 0x00035DF0
		public string NewValue
		{
			get
			{
				return (this._newValue == null) ? this._node.Value : this._newValue;
			}
		}

		// Token: 0x04000532 RID: 1330
		private XmlNode _oldParent;

		// Token: 0x04000533 RID: 1331
		private XmlNode _newParent;

		// Token: 0x04000534 RID: 1332
		private XmlNodeChangedAction _action;

		// Token: 0x04000535 RID: 1333
		private XmlNode _node;

		// Token: 0x04000536 RID: 1334
		private string _oldValue;

		// Token: 0x04000537 RID: 1335
		private string _newValue;
	}
}
