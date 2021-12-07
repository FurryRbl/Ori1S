using System;
using System.ComponentModel;

namespace System.IO
{
	/// <summary>Sets the description visual designers can display when referencing an event, extender, or property.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200028B RID: 651
	[AttributeUsage(AttributeTargets.All)]
	public class IODescriptionAttribute : System.ComponentModel.DescriptionAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.IODescriptionAttribute" /> class.</summary>
		/// <param name="description">The description to use. </param>
		// Token: 0x060016D1 RID: 5841 RVA: 0x0003EA78 File Offset: 0x0003CC78
		public IODescriptionAttribute(string description) : base(description)
		{
		}

		/// <summary>Gets the description.</summary>
		/// <returns>The description for the event, extender, or property.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x060016D2 RID: 5842 RVA: 0x0003EA84 File Offset: 0x0003CC84
		public override string Description
		{
			get
			{
				return base.DescriptionValue;
			}
		}
	}
}
