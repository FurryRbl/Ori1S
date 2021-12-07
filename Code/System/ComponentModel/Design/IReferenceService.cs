﻿using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides an interface for obtaining references to objects within a project by name or type, obtaining the name of a specified object, and for locating the parent of a specified object within a designer project.</summary>
	// Token: 0x0200011B RID: 283
	public interface IReferenceService
	{
		/// <summary>Gets the component that contains the specified component.</summary>
		/// <returns>The base <see cref="T:System.ComponentModel.IComponent" /> that contains the specified object, or null if no parent component exists.</returns>
		/// <param name="reference">The object to retrieve the parent component for. </param>
		// Token: 0x06000B07 RID: 2823
		IComponent GetComponent(object reference);

		/// <summary>Gets the name of the specified component.</summary>
		/// <returns>The name of the object referenced, or null if the object reference is not valid.</returns>
		/// <param name="reference">The object to return the name of. </param>
		// Token: 0x06000B08 RID: 2824
		string GetName(object reference);

		/// <summary>Gets a reference to the component whose name matches the specified name.</summary>
		/// <returns>An object the specified name refers to, or null if no reference is found.</returns>
		/// <param name="name">The name of the component to return a reference to. </param>
		// Token: 0x06000B09 RID: 2825
		object GetReference(string name);

		/// <summary>Gets all available references to project components.</summary>
		/// <returns>An array of all objects with references available to the <see cref="T:System.ComponentModel.Design.IReferenceService" />.</returns>
		// Token: 0x06000B0A RID: 2826
		object[] GetReferences();

		/// <summary>Gets all available references to components of the specified type.</summary>
		/// <returns>An array of all available objects of the specified type.</returns>
		/// <param name="baseType">The type of object to return references to instances of. </param>
		// Token: 0x06000B0B RID: 2827
		object[] GetReferences(Type baseType);
	}
}
