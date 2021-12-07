using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.ComponentModel.Design
{
	/// <summary>Represents a Windows menu or toolbar command item.</summary>
	// Token: 0x02000124 RID: 292
	[ComVisible(true)]
	public class MenuCommand
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.MenuCommand" /> class.</summary>
		/// <param name="handler">The event to raise when the user selects the menu item or toolbar button. </param>
		/// <param name="command">The unique command ID that links this menu command to the environment's menu. </param>
		// Token: 0x06000B2D RID: 2861 RVA: 0x0001D9B0 File Offset: 0x0001BBB0
		public MenuCommand(EventHandler handler, CommandID command)
		{
			this.handler = handler;
			this.command = command;
		}

		/// <summary>Occurs when the menu command changes.</summary>
		// Token: 0x14000031 RID: 49
		// (add) Token: 0x06000B2E RID: 2862 RVA: 0x0001D9DC File Offset: 0x0001BBDC
		// (remove) Token: 0x06000B2F RID: 2863 RVA: 0x0001D9F8 File Offset: 0x0001BBF8
		public event EventHandler CommandChanged;

		/// <summary>Gets or sets a value indicating whether this menu item is checked.</summary>
		/// <returns>true if the item is checked; otherwise, false.</returns>
		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000B30 RID: 2864 RVA: 0x0001DA14 File Offset: 0x0001BC14
		// (set) Token: 0x06000B31 RID: 2865 RVA: 0x0001DA1C File Offset: 0x0001BC1C
		public virtual bool Checked
		{
			get
			{
				return this.ischecked;
			}
			set
			{
				if (this.ischecked != value)
				{
					this.ischecked = value;
					this.OnCommandChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets the <see cref="T:System.ComponentModel.Design.CommandID" /> associated with this menu command.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.Design.CommandID" /> associated with the menu command.</returns>
		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000B32 RID: 2866 RVA: 0x0001DA3C File Offset: 0x0001BC3C
		public virtual CommandID CommandID
		{
			get
			{
				return this.command;
			}
		}

		/// <summary>Gets a value indicating whether this menu item is available.</summary>
		/// <returns>true if the item is enabled; otherwise, false.</returns>
		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000B33 RID: 2867 RVA: 0x0001DA44 File Offset: 0x0001BC44
		// (set) Token: 0x06000B34 RID: 2868 RVA: 0x0001DA4C File Offset: 0x0001BC4C
		public virtual bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				if (this.enabled != value)
				{
					this.enabled = value;
					this.OnCommandChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets the OLE command status code for this menu item.</summary>
		/// <returns>An integer containing a mixture of status flags that reflect the state of this menu item.</returns>
		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000B35 RID: 2869 RVA: 0x0001DA6C File Offset: 0x0001BC6C
		[MonoTODO]
		public virtual int OleStatus
		{
			get
			{
				return 3;
			}
		}

		/// <summary>Gets the public properties associated with the <see cref="T:System.ComponentModel.Design.MenuCommand" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> containing the public properties of the <see cref="T:System.ComponentModel.Design.MenuCommand" />. </returns>
		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000B36 RID: 2870 RVA: 0x0001DA70 File Offset: 0x0001BC70
		public virtual IDictionary Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new Hashtable();
				}
				return this.properties;
			}
		}

		/// <summary>Gets or sets a value indicating whether this menu item is supported.</summary>
		/// <returns>true if the item is supported, which is the default; otherwise, false.</returns>
		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000B37 RID: 2871 RVA: 0x0001DA90 File Offset: 0x0001BC90
		// (set) Token: 0x06000B38 RID: 2872 RVA: 0x0001DA98 File Offset: 0x0001BC98
		public virtual bool Supported
		{
			get
			{
				return this.issupported;
			}
			set
			{
				this.issupported = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether this menu item is visible.</summary>
		/// <returns>true if the item is visible; otherwise, false.</returns>
		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000B39 RID: 2873 RVA: 0x0001DAA4 File Offset: 0x0001BCA4
		// (set) Token: 0x06000B3A RID: 2874 RVA: 0x0001DAAC File Offset: 0x0001BCAC
		public virtual bool Visible
		{
			get
			{
				return this.visible;
			}
			set
			{
				this.visible = value;
			}
		}

		/// <summary>Invokes the command.</summary>
		// Token: 0x06000B3B RID: 2875 RVA: 0x0001DAB8 File Offset: 0x0001BCB8
		public virtual void Invoke()
		{
			if (this.handler != null)
			{
				this.handler(this, EventArgs.Empty);
			}
		}

		/// <summary>Invokes the command with the given parameter.</summary>
		/// <param name="arg">An optional argument for use by the command.</param>
		// Token: 0x06000B3C RID: 2876 RVA: 0x0001DAD8 File Offset: 0x0001BCD8
		public virtual void Invoke(object arg)
		{
			this.Invoke();
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.Design.MenuCommand.CommandChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000B3D RID: 2877 RVA: 0x0001DAE0 File Offset: 0x0001BCE0
		protected virtual void OnCommandChanged(EventArgs e)
		{
			if (this.CommandChanged != null)
			{
				this.CommandChanged(this, e);
			}
		}

		/// <summary>Returns a string representation of this menu command.</summary>
		/// <returns>A string containing the value of the <see cref="P:System.ComponentModel.Design.MenuCommand.CommandID" /> property appended with the names of any flags that are set, separated by pipe bars (|). These flag properties include <see cref="P:System.ComponentModel.Design.MenuCommand.Checked" />, <see cref="P:System.ComponentModel.Design.MenuCommand.Enabled" />, <see cref="P:System.ComponentModel.Design.MenuCommand.Supported" />, and <see cref="P:System.ComponentModel.Design.MenuCommand.Visible" />.</returns>
		// Token: 0x06000B3E RID: 2878 RVA: 0x0001DAFC File Offset: 0x0001BCFC
		public override string ToString()
		{
			string text = string.Empty;
			if (this.command != null)
			{
				text = this.command.ToString();
			}
			text += " : ";
			if (this.Supported)
			{
				text += "Supported";
			}
			if (this.Enabled)
			{
				text += "|Enabled";
			}
			if (this.Visible)
			{
				text += "|Visible";
			}
			if (this.Checked)
			{
				text += "|Checked";
			}
			return text;
		}

		// Token: 0x040002E4 RID: 740
		private EventHandler handler;

		// Token: 0x040002E5 RID: 741
		private CommandID command;

		// Token: 0x040002E6 RID: 742
		private bool ischecked;

		// Token: 0x040002E7 RID: 743
		private bool enabled = true;

		// Token: 0x040002E8 RID: 744
		private bool issupported = true;

		// Token: 0x040002E9 RID: 745
		private bool visible = true;

		// Token: 0x040002EA RID: 746
		private Hashtable properties;
	}
}
