using System;
using System.ComponentModel;

namespace System.Timers
{
	/// <summary>Sets the description that visual designers can display when referencing an event, extender, or property.</summary>
	// Token: 0x020004AF RID: 1199
	[AttributeUsage(AttributeTargets.All)]
	public class TimersDescriptionAttribute : System.ComponentModel.DescriptionAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Timers.TimersDescriptionAttribute" /> class.</summary>
		/// <param name="description">The description to use. </param>
		// Token: 0x06002B0E RID: 11022 RVA: 0x00093DF4 File Offset: 0x00091FF4
		public TimersDescriptionAttribute(string description) : base(description)
		{
		}

		/// <summary>Gets the description that visual designers can display when referencing an event, extender, or property.</summary>
		/// <returns>The description for the event, extender, or property.</returns>
		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x06002B0F RID: 11023 RVA: 0x00093E00 File Offset: 0x00092000
		public override string Description
		{
			get
			{
				return base.Description;
			}
		}
	}
}
