using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a reference to an event.</summary>
	// Token: 0x0200003E RID: 62
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Serializable]
	public class CodeEventReferenceExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeEventReferenceExpression" /> class.</summary>
		// Token: 0x0600020D RID: 525 RVA: 0x0000B498 File Offset: 0x00009698
		public CodeEventReferenceExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeEventReferenceExpression" /> class using the specified target object and event name.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object that contains the event. </param>
		/// <param name="eventName">The name of the event to reference. </param>
		// Token: 0x0600020E RID: 526 RVA: 0x0000B4A0 File Offset: 0x000096A0
		public CodeEventReferenceExpression(CodeExpression targetObject, string eventName)
		{
			this.targetObject = targetObject;
			this.eventName = eventName;
		}

		/// <summary>Gets or sets the name of the event.</summary>
		/// <returns>The name of the event.</returns>
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000B4B8 File Offset: 0x000096B8
		// (set) Token: 0x06000210 RID: 528 RVA: 0x0000B4D4 File Offset: 0x000096D4
		public string EventName
		{
			get
			{
				if (this.eventName == null)
				{
					return string.Empty;
				}
				return this.eventName;
			}
			set
			{
				this.eventName = value;
			}
		}

		/// <summary>Gets or sets the object that contains the event.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object that contains the event.</returns>
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000211 RID: 529 RVA: 0x0000B4E0 File Offset: 0x000096E0
		// (set) Token: 0x06000212 RID: 530 RVA: 0x0000B4E8 File Offset: 0x000096E8
		public CodeExpression TargetObject
		{
			get
			{
				return this.targetObject;
			}
			set
			{
				this.targetObject = value;
			}
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000B4F4 File Offset: 0x000096F4
		internal override void Accept(ICodeDomVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x040000A1 RID: 161
		private string eventName;

		// Token: 0x040000A2 RID: 162
		private CodeExpression targetObject;
	}
}
