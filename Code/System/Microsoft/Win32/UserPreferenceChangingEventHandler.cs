﻿using System;

namespace Microsoft.Win32
{
	/// <summary>Represents the method that will handle the <see cref="E:Microsoft.Win32.SystemEvents.UserPreferenceChanging" /> event.</summary>
	/// <param name="sender">The source of the event. When this event is raised by the <see cref="T:Microsoft.Win32.SystemEvents" /> class, this object is always null. </param>
	/// <param name="e">A <see cref="T:Microsoft.Win32.UserPreferenceChangedEventArgs" /> that contains the event data. </param>
	// Token: 0x020004F4 RID: 1268
	// (Invoke) Token: 0x06002C7C RID: 11388
	public delegate void UserPreferenceChangingEventHandler(object sender, UserPreferenceChangingEventArgs e);
}
