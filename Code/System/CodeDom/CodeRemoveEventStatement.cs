using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a statement that removes an event handler.</summary>
	// Token: 0x0200005C RID: 92
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeRemoveEventStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeRemoveEventStatement" /> class.</summary>
		// Token: 0x06000306 RID: 774 RVA: 0x0000CA94 File Offset: 0x0000AC94
		public CodeRemoveEventStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeRemoveEventStatement" /> class with the specified event and event handler.</summary>
		/// <param name="eventRef">A <see cref="T:System.CodeDom.CodeEventReferenceExpression" /> that indicates the event to detach the event handler from. </param>
		/// <param name="listener">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the event handler to remove. </param>
		// Token: 0x06000307 RID: 775 RVA: 0x0000CA9C File Offset: 0x0000AC9C
		public CodeRemoveEventStatement(CodeEventReferenceExpression eventRef, CodeExpression listener)
		{
			this.eventRef = eventRef;
			this.listener = listener;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeRemoveEventStatement" /> class using the specified target object, event name, and event handler.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object that contains the event. </param>
		/// <param name="eventName">The name of the event. </param>
		/// <param name="listener">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the event handler to remove. </param>
		// Token: 0x06000308 RID: 776 RVA: 0x0000CAB4 File Offset: 0x0000ACB4
		public CodeRemoveEventStatement(CodeExpression targetObject, string eventName, CodeExpression listener)
		{
			this.eventRef = new CodeEventReferenceExpression(targetObject, eventName);
			this.listener = listener;
		}

		/// <summary>Gets or sets the event to remove a listener from.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeEventReferenceExpression" /> that indicates the event to remove a listener from.</returns>
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000309 RID: 777 RVA: 0x0000CAD0 File Offset: 0x0000ACD0
		// (set) Token: 0x0600030A RID: 778 RVA: 0x0000CAF0 File Offset: 0x0000ACF0
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

		/// <summary>Gets or sets the event handler to remove.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the event handler to remove.</returns>
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600030B RID: 779 RVA: 0x0000CAFC File Offset: 0x0000ACFC
		// (set) Token: 0x0600030C RID: 780 RVA: 0x0000CB04 File Offset: 0x0000AD04
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

		// Token: 0x0600030D RID: 781 RVA: 0x0000CB10 File Offset: 0x0000AD10
		internal override void Accept(ICodeDomVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x040000EC RID: 236
		private CodeEventReferenceExpression eventRef;

		// Token: 0x040000ED RID: 237
		private CodeExpression listener;
	}
}
