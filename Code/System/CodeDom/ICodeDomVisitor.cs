using System;

namespace System.CodeDom
{
	// Token: 0x02000075 RID: 117
	internal interface ICodeDomVisitor
	{
		// Token: 0x060003DF RID: 991
		void Visit(CodeArgumentReferenceExpression o);

		// Token: 0x060003E0 RID: 992
		void Visit(CodeArrayCreateExpression o);

		// Token: 0x060003E1 RID: 993
		void Visit(CodeArrayIndexerExpression o);

		// Token: 0x060003E2 RID: 994
		void Visit(CodeBaseReferenceExpression o);

		// Token: 0x060003E3 RID: 995
		void Visit(CodeBinaryOperatorExpression o);

		// Token: 0x060003E4 RID: 996
		void Visit(CodeCastExpression o);

		// Token: 0x060003E5 RID: 997
		void Visit(CodeDefaultValueExpression o);

		// Token: 0x060003E6 RID: 998
		void Visit(CodeDelegateCreateExpression o);

		// Token: 0x060003E7 RID: 999
		void Visit(CodeDelegateInvokeExpression o);

		// Token: 0x060003E8 RID: 1000
		void Visit(CodeDirectionExpression o);

		// Token: 0x060003E9 RID: 1001
		void Visit(CodeEventReferenceExpression o);

		// Token: 0x060003EA RID: 1002
		void Visit(CodeFieldReferenceExpression o);

		// Token: 0x060003EB RID: 1003
		void Visit(CodeIndexerExpression o);

		// Token: 0x060003EC RID: 1004
		void Visit(CodeMethodInvokeExpression o);

		// Token: 0x060003ED RID: 1005
		void Visit(CodeMethodReferenceExpression o);

		// Token: 0x060003EE RID: 1006
		void Visit(CodeObjectCreateExpression o);

		// Token: 0x060003EF RID: 1007
		void Visit(CodeParameterDeclarationExpression o);

		// Token: 0x060003F0 RID: 1008
		void Visit(CodePrimitiveExpression o);

		// Token: 0x060003F1 RID: 1009
		void Visit(CodePropertyReferenceExpression o);

		// Token: 0x060003F2 RID: 1010
		void Visit(CodePropertySetValueReferenceExpression o);

		// Token: 0x060003F3 RID: 1011
		void Visit(CodeSnippetExpression o);

		// Token: 0x060003F4 RID: 1012
		void Visit(CodeThisReferenceExpression o);

		// Token: 0x060003F5 RID: 1013
		void Visit(CodeTypeOfExpression o);

		// Token: 0x060003F6 RID: 1014
		void Visit(CodeTypeReferenceExpression o);

		// Token: 0x060003F7 RID: 1015
		void Visit(CodeVariableReferenceExpression o);

		// Token: 0x060003F8 RID: 1016
		void Visit(CodeAssignStatement o);

		// Token: 0x060003F9 RID: 1017
		void Visit(CodeAttachEventStatement o);

		// Token: 0x060003FA RID: 1018
		void Visit(CodeCommentStatement o);

		// Token: 0x060003FB RID: 1019
		void Visit(CodeConditionStatement o);

		// Token: 0x060003FC RID: 1020
		void Visit(CodeExpressionStatement o);

		// Token: 0x060003FD RID: 1021
		void Visit(CodeGotoStatement o);

		// Token: 0x060003FE RID: 1022
		void Visit(CodeIterationStatement o);

		// Token: 0x060003FF RID: 1023
		void Visit(CodeLabeledStatement o);

		// Token: 0x06000400 RID: 1024
		void Visit(CodeMethodReturnStatement o);

		// Token: 0x06000401 RID: 1025
		void Visit(CodeRemoveEventStatement o);

		// Token: 0x06000402 RID: 1026
		void Visit(CodeThrowExceptionStatement o);

		// Token: 0x06000403 RID: 1027
		void Visit(CodeTryCatchFinallyStatement o);

		// Token: 0x06000404 RID: 1028
		void Visit(CodeVariableDeclarationStatement o);

		// Token: 0x06000405 RID: 1029
		void Visit(CodeConstructor o);

		// Token: 0x06000406 RID: 1030
		void Visit(CodeEntryPointMethod o);

		// Token: 0x06000407 RID: 1031
		void Visit(CodeMemberEvent o);

		// Token: 0x06000408 RID: 1032
		void Visit(CodeMemberField o);

		// Token: 0x06000409 RID: 1033
		void Visit(CodeMemberMethod o);

		// Token: 0x0600040A RID: 1034
		void Visit(CodeMemberProperty o);

		// Token: 0x0600040B RID: 1035
		void Visit(CodeSnippetTypeMember o);

		// Token: 0x0600040C RID: 1036
		void Visit(CodeTypeConstructor o);
	}
}
