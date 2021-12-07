using System;
using System.Collections;

namespace System.Diagnostics
{
	/// <summary>Correlates traces that are part of a logical transaction.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200020F RID: 527
	public class CorrelationManager
	{
		// Token: 0x06001197 RID: 4503 RVA: 0x0002EC60 File Offset: 0x0002CE60
		internal CorrelationManager()
		{
		}

		/// <summary>Gets or sets the identity for a global activity.</summary>
		/// <returns>A <see cref="T:System.Guid" /> structure that identifies the global activity.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06001198 RID: 4504 RVA: 0x0002EC74 File Offset: 0x0002CE74
		// (set) Token: 0x06001199 RID: 4505 RVA: 0x0002EC7C File Offset: 0x0002CE7C
		public Guid ActivityId
		{
			get
			{
				return this.activity;
			}
			set
			{
				this.activity = value;
			}
		}

		/// <summary>Gets the logical operation stack from the call context.</summary>
		/// <returns>A <see cref="T:System.Collections.Stack" /> object that represents the logical operation stack for the call context.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x0600119A RID: 4506 RVA: 0x0002EC88 File Offset: 0x0002CE88
		public Stack LogicalOperationStack
		{
			get
			{
				return this.op_stack;
			}
		}

		/// <summary>Starts a logical operation on a thread.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600119B RID: 4507 RVA: 0x0002EC90 File Offset: 0x0002CE90
		public void StartLogicalOperation()
		{
			this.StartLogicalOperation(Guid.NewGuid());
		}

		/// <summary>Starts a logical operation with the specified identity on a thread.</summary>
		/// <param name="operationId">An object identifying the operation.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="operationId" /> parameter is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600119C RID: 4508 RVA: 0x0002ECA4 File Offset: 0x0002CEA4
		public void StartLogicalOperation(object operationId)
		{
			this.op_stack.Push(operationId);
		}

		/// <summary>Stops the current logical operation.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Diagnostics.CorrelationManager.LogicalOperationStack" /> property is an empty stack.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600119D RID: 4509 RVA: 0x0002ECB4 File Offset: 0x0002CEB4
		public void StopLogicalOperation()
		{
			this.op_stack.Pop();
		}

		// Token: 0x04000509 RID: 1289
		private Guid activity;

		// Token: 0x0400050A RID: 1290
		private Stack op_stack = new Stack();
	}
}
