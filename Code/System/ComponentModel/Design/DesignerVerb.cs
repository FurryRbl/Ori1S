using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel.Design
{
	/// <summary>Represents a verb that can be invoked from a designer.</summary>
	// Token: 0x02000102 RID: 258
	[ComVisible(true)]
	public class DesignerVerb : MenuCommand
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerVerb" /> class.</summary>
		/// <param name="text">The text of the menu command that is shown to the user. </param>
		/// <param name="handler">The event handler that performs the actions of the verb. </param>
		// Token: 0x06000A77 RID: 2679 RVA: 0x0001D4D0 File Offset: 0x0001B6D0
		public DesignerVerb(string text, EventHandler handler) : this(text, handler, StandardCommands.VerbFirst)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerVerb" /> class.</summary>
		/// <param name="text">The text of the menu command that is shown to the user. </param>
		/// <param name="handler">The event handler that performs the actions of the verb. </param>
		/// <param name="startCommandID">The starting command ID for this verb. By default, the designer architecture sets aside a range of command IDs for verbs. You can override this by providing a custom command ID. </param>
		// Token: 0x06000A78 RID: 2680 RVA: 0x0001D4E0 File Offset: 0x0001B6E0
		public DesignerVerb(string text, EventHandler handler, CommandID startCommandID) : base(handler, startCommandID)
		{
			this.text = text;
		}

		/// <summary>Gets the text description for the verb command on the menu.</summary>
		/// <returns>A description for the verb command.</returns>
		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000A79 RID: 2681 RVA: 0x0001D4F4 File Offset: 0x0001B6F4
		public string Text
		{
			get
			{
				return this.text;
			}
		}

		/// <summary>Gets or sets the description of the menu item for the verb.</summary>
		/// <returns>A string describing the menu item. </returns>
		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000A7A RID: 2682 RVA: 0x0001D4FC File Offset: 0x0001B6FC
		// (set) Token: 0x06000A7B RID: 2683 RVA: 0x0001D504 File Offset: 0x0001B704
		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		/// <summary>Overrides <see cref="M:System.Object.ToString" />.</summary>
		/// <returns>The verb's text, or an empty string ("") if the text field is empty.</returns>
		// Token: 0x06000A7C RID: 2684 RVA: 0x0001D510 File Offset: 0x0001B710
		public override string ToString()
		{
			return this.text + " : " + base.ToString();
		}

		// Token: 0x040002C6 RID: 710
		private string text;

		// Token: 0x040002C7 RID: 711
		private string description;
	}
}
