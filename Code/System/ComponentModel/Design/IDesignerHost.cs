using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel.Design
{
	/// <summary>Provides an interface for managing designer transactions and components.</summary>
	// Token: 0x02000112 RID: 274
	[ComVisible(true)]
	public interface IDesignerHost : IServiceProvider, IServiceContainer
	{
		/// <summary>Occurs when this designer is activated.</summary>
		// Token: 0x14000028 RID: 40
		// (add) Token: 0x06000ACA RID: 2762
		// (remove) Token: 0x06000ACB RID: 2763
		event EventHandler Activated;

		/// <summary>Occurs when this designer is deactivated.</summary>
		// Token: 0x14000029 RID: 41
		// (add) Token: 0x06000ACC RID: 2764
		// (remove) Token: 0x06000ACD RID: 2765
		event EventHandler Deactivated;

		/// <summary>Occurs when this designer completes loading its document.</summary>
		// Token: 0x1400002A RID: 42
		// (add) Token: 0x06000ACE RID: 2766
		// (remove) Token: 0x06000ACF RID: 2767
		event EventHandler LoadComplete;

		/// <summary>Adds an event handler for the <see cref="E:System.ComponentModel.Design.IDesignerHost.TransactionClosed" /> event.</summary>
		// Token: 0x1400002B RID: 43
		// (add) Token: 0x06000AD0 RID: 2768
		// (remove) Token: 0x06000AD1 RID: 2769
		event DesignerTransactionCloseEventHandler TransactionClosed;

		/// <summary>Adds an event handler for the <see cref="E:System.ComponentModel.Design.IDesignerHost.TransactionClosing" /> event.</summary>
		// Token: 0x1400002C RID: 44
		// (add) Token: 0x06000AD2 RID: 2770
		// (remove) Token: 0x06000AD3 RID: 2771
		event DesignerTransactionCloseEventHandler TransactionClosing;

		/// <summary>Adds an event handler for the <see cref="E:System.ComponentModel.Design.IDesignerHost.TransactionOpened" /> event.</summary>
		// Token: 0x1400002D RID: 45
		// (add) Token: 0x06000AD4 RID: 2772
		// (remove) Token: 0x06000AD5 RID: 2773
		event EventHandler TransactionOpened;

		/// <summary>Adds an event handler for the <see cref="E:System.ComponentModel.Design.IDesignerHost.TransactionOpening" /> event.</summary>
		// Token: 0x1400002E RID: 46
		// (add) Token: 0x06000AD6 RID: 2774
		// (remove) Token: 0x06000AD7 RID: 2775
		event EventHandler TransactionOpening;

		/// <summary>Gets the container for this designer host.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.IContainer" /> for this host.</returns>
		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000AD8 RID: 2776
		IContainer Container { get; }

		/// <summary>Gets a value indicating whether the designer host is currently in a transaction.</summary>
		/// <returns>true if a transaction is in progress; otherwise, false.</returns>
		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000AD9 RID: 2777
		bool InTransaction { get; }

		/// <summary>Gets a value indicating whether the designer host is currently loading the document.</summary>
		/// <returns>true if the designer host is currently loading the document; otherwise, false.</returns>
		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000ADA RID: 2778
		bool Loading { get; }

		/// <summary>Gets the instance of the base class used as the root component for the current design.</summary>
		/// <returns>The instance of the root component class.</returns>
		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000ADB RID: 2779
		IComponent RootComponent { get; }

		/// <summary>Gets the fully qualified name of the class being designed.</summary>
		/// <returns>The fully qualified name of the base component class.</returns>
		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000ADC RID: 2780
		string RootComponentClassName { get; }

		/// <summary>Gets the description of the current transaction.</summary>
		/// <returns>A description of the current transaction.</returns>
		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000ADD RID: 2781
		string TransactionDescription { get; }

		/// <summary>Activates the designer that this host is hosting.</summary>
		// Token: 0x06000ADE RID: 2782
		void Activate();

		/// <summary>Creates a component of the specified type and adds it to the design document.</summary>
		/// <returns>The newly created component.</returns>
		/// <param name="componentClass">The type of the component to create. </param>
		// Token: 0x06000ADF RID: 2783
		IComponent CreateComponent(Type componentClass);

		/// <summary>Creates a component of the specified type and name, and adds it to the design document.</summary>
		/// <returns>The newly created component.</returns>
		/// <param name="componentClass">The type of the component to create. </param>
		/// <param name="name">The name for the component. </param>
		// Token: 0x06000AE0 RID: 2784
		IComponent CreateComponent(Type componentClass, string name);

		/// <summary>Creates a <see cref="T:System.ComponentModel.Design.DesignerTransaction" /> that can encapsulate event sequences to improve performance and enable undo and redo support functionality.</summary>
		/// <returns>A new instance of <see cref="T:System.ComponentModel.Design.DesignerTransaction" />. When you complete the steps in your transaction, you should call <see cref="M:System.ComponentModel.Design.DesignerTransaction.Commit" /> on this object.</returns>
		// Token: 0x06000AE1 RID: 2785
		DesignerTransaction CreateTransaction();

		/// <summary>Creates a <see cref="T:System.ComponentModel.Design.DesignerTransaction" /> that can encapsulate event sequences to improve performance and enable undo and redo support functionality, using the specified transaction description.</summary>
		/// <returns>A new <see cref="T:System.ComponentModel.Design.DesignerTransaction" />. When you have completed the steps in your transaction, you should call <see cref="M:System.ComponentModel.Design.DesignerTransaction.Commit" /> on this object.</returns>
		/// <param name="description">A title or description for the newly created transaction. </param>
		// Token: 0x06000AE2 RID: 2786
		DesignerTransaction CreateTransaction(string description);

		/// <summary>Destroys the specified component and removes it from the designer container.</summary>
		/// <param name="component">The component to destroy. </param>
		// Token: 0x06000AE3 RID: 2787
		void DestroyComponent(IComponent component);

		/// <summary>Gets the designer instance that contains the specified component.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.Design.IDesigner" />, or null if there is no designer for the specified component.</returns>
		/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to retrieve the designer for. </param>
		// Token: 0x06000AE4 RID: 2788
		IDesigner GetDesigner(IComponent component);

		/// <summary>Gets an instance of the specified, fully qualified type name.</summary>
		/// <returns>The type object for the specified type name, or null if the type cannot be found.</returns>
		/// <param name="typeName">The name of the type to load. </param>
		// Token: 0x06000AE5 RID: 2789
		Type GetType(string typeName);
	}
}
