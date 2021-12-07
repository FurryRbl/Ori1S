using System;

namespace System.ComponentModel
{
	/// <summary>Specifies which event is raised on initialization. This class cannot be inherited.</summary>
	// Token: 0x02000161 RID: 353
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class InitializationEventAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InitializationEventAttribute" /> class.</summary>
		/// <param name="eventName">The name of the initialization event.</param>
		// Token: 0x06000CA2 RID: 3234 RVA: 0x0002030C File Offset: 0x0001E50C
		public InitializationEventAttribute(string eventName)
		{
			this.eventName = eventName;
		}

		/// <summary>Gets the name of the initialization event.</summary>
		/// <returns>The name of the initialization event.</returns>
		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000CA3 RID: 3235 RVA: 0x0002031C File Offset: 0x0001E51C
		public string EventName
		{
			get
			{
				return this.eventName;
			}
		}

		// Token: 0x04000380 RID: 896
		private string eventName;
	}
}
