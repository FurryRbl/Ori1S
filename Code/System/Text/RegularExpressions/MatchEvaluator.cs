using System;

namespace System.Text.RegularExpressions
{
	/// <summary>Represents the method that is called each time a regular expression match is found during a <see cref="Overload:System.Text.RegularExpressions.Regex.Replace" /> method operation.</summary>
	/// <returns>A string returned by the method that is represented by the <see cref="T:System.Text.RegularExpressions.MatchEvaluator" /> delegate.</returns>
	/// <param name="match">The <see cref="T:System.Text.RegularExpressions.Match" /> object that represents a single regular expression match during a <see cref="Overload:System.Text.RegularExpressions.Regex.Replace" /> method operation. </param>
	// Token: 0x0200051D RID: 1309
	// (Invoke) Token: 0x06002D20 RID: 11552
	[Serializable]
	public delegate string MatchEvaluator(Match match);
}
