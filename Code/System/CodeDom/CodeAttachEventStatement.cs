using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a statement that attaches an event-handler delegate to an event.</summary>
	// Token: 0x02000025 RID: 37
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Serializable]
	public class CodeAttachEventStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttachEventStatement" /> class.</summary>
		// Token: 0x06000150 RID: 336 RVA: 0x0000A5B8 File Offset: 0x000087B8
		public CodeAttachEventStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttachEventStatement" /> class using the specified event and delegate.</summary>
		/// <param name="eventRef">A <see cref="T:System.CodeDom.CodeEventReferenceExpression" /> that indicates the event to attach an event handler to. </param>
		/// <param name="listener">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the new event handler. </param>
		// Token: 0x06000151 RID: 337 RVA: 0x0000A5C0 File Offset: 0x000087C0
		public CodeAttachEventStatement(CodeEventReferenceExpression eventRef, CodeExpression listener)
		{
			this.eventRef = eventRef;
			this.listener = listener;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttachEventStatement" /> class using the specified object containing the event, event name, and event-handler delegate.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object that contains the event. </param>
		/// <param name="eventName">The name of the event to attach an event handler to. </param>
		/// <param name="listener">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the new event handler. </param>
		// Token: 0x06000152 RID: 338 RVA: 0x0000A5D8 File Offset: 0x000087D8
		public CodeAttachEventStatement(CodeExpression targetObject, string eventName, CodeExpression listener)
		{
			this.eventRef = new CodeEventReferenceExpression(targetObject, eventName);
			this.listener = listener;
		}

		/// <summary>Gets or sets the event to attach an event-handler delegate to.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeEventReferenceExpression" /> that indicates the event to attach an event handler to.</returns>
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000153 RID: 339 RVA: 0x0000A5F4 File Offset: 0x000087F4
		// (set) Token: 0x06000154 RID: 340 RVA: 0x0000A614 File Offset: 0x00008814
		public CodeEventReferenceExpression Event
		{
			get
			{
				if (this.eventRef == null)
				{
					this.eventRef = new CodeEventReferenceExpression();
				}
				return this.eventRef;
			}
			set
			{
				this.eventRef = value;
			}
		}

		/// <summary>Gets or sets the new event-handler delegate to attach to the event.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the new event handler to attach.</returns>
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000155 RID: 341 RVA: 0x0000A620 File Offset: 0x00008820
		// (set) Token: 0x06000156 RID: 342 RVA: 0x0000A628 File Offset: 0x00008828
		public CodeExpression Listener
		{
			get
			{
				return this.listener;
			}
			set
			{
				this.listener = value;
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000A634 File Offset: 0x00008834
		internal override void Accept(ICodeDomVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x04000068 RID: 104
		private CodeEventReferenceExpression eventRef;

		// Token: 0x04000069 RID: 105
		private CodeExpression listener;
	}
}
