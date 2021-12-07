using System;

namespace System.ComponentModel.Design
{
	/// <summary>Provides methods for showing Help topics and adding and removing Help keywords at design time.</summary>
	// Token: 0x02000118 RID: 280
	public interface IHelpService
	{
		/// <summary>Adds a context attribute to the document.</summary>
		/// <param name="name">The name of the attribute to add. </param>
		/// <param name="value">The value of the attribute. </param>
		/// <param name="keywordType">The type of the keyword, from the enumeration <see cref="T:System.ComponentModel.Design.HelpKeywordType" />. </param>
		// Token: 0x06000AF6 RID: 2806
		void AddContextAttribute(string name, string value, HelpKeywordType keywordType);

		/// <summary>Removes all existing context attributes from the document.</summary>
		// Token: 0x06000AF7 RID: 2807
		void ClearContextAttributes();

		/// <summary>Creates a local <see cref="T:System.ComponentModel.Design.IHelpService" /> to manage subcontexts.</summary>
		/// <returns>The newly created <see cref="T:System.ComponentModel.Design.IHelpService" />.</returns>
		/// <param name="contextType">The priority type of the subcontext to add. </param>
		// Token: 0x06000AF8 RID: 2808
		IHelpService CreateLocalContext(HelpContextType contextType);

		/// <summary>Removes a previously added context attribute.</summary>
		/// <param name="name">The name of the attribute to remove. </param>
		/// <param name="value">The value of the attribute to remove. </param>
		// Token: 0x06000AF9 RID: 2809
		void RemoveContextAttribute(string name, string value);

		/// <summary>Removes a context created with <see cref="M:System.ComponentModel.Design.IHelpService.CreateLocalContext(System.ComponentModel.Design.HelpContextType)" />.</summary>
		/// <param name="localContext">The local context <see cref="T:System.ComponentModel.Design.IHelpService" /> to remove. </param>
		// Token: 0x06000AFA RID: 2810
		void RemoveLocalContext(IHelpService localContext);

		/// <summary>Shows the Help topic that corresponds to the specified keyword.</summary>
		/// <param name="helpKeyword">The keyword of the Help topic to display. </param>
		// Token: 0x06000AFB RID: 2811
		void ShowHelpFromKeyword(string helpKeyword);

		/// <summary>Shows the Help topic that corresponds to the specified URL.</summary>
		/// <param name="helpUrl">The URL of the Help topic to display. </param>
		// Token: 0x06000AFC RID: 2812
		void ShowHelpFromUrl(string helpUrl);
	}
}
