using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace RefactoringEssentials.CSharp.Diagnostics
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    [NotPortedYet]
    public class UnusedParameterAnalyzer : DiagnosticAnalyzer
    {
        static readonly DiagnosticDescriptor descriptor = new DiagnosticDescriptor(
            CSharpDiagnosticIDs.UnusedParameterAnalyzerID,
            GettextCatalog.GetString("Parameter is never used"),
            GettextCatalog.GetString("Parameter is never used"),
            DiagnosticAnalyzerCategories.RedundanciesInDeclarations,
            DiagnosticSeverity.Warning,
            isEnabledByDefault: true,
            helpLinkUri: HelpLink.CreateFor(CSharpDiagnosticIDs.UnusedParameterAnalyzerID)
        );

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(descriptor);

        public override void Initialize(AnalysisContext context)
        {
            //context.RegisterSyntaxNodeAction(
            //	(nodeContext) => {
            //		Diagnostic diagnostic;
            //		if (TryGetDiagnostic (nodeContext, out diagnostic)) {
            //			nodeContext.ReportDiagnostic(diagnostic);
            //		}
            //	}, 
            //	new SyntaxKind[] { SyntaxKind.None }
            //);
        }

        static bool TryGetDiagnostic(SyntaxNodeAnalysisContext nodeContext, out Diagnostic diagnostic)
        {
            diagnostic = default(Diagnostic);
            if (nodeContext.IsFromGeneratedCode())
                return false;
            //var node = nodeContext.Node as ;
            //diagnostic = Diagnostic.Create (descriptor, node.GetLocation ());
            //return true;
            return false;
        }

        //		#region ICodeIssueProvider implementation
        //
        //		protected override IGatherVisitor CreateVisitor(BaseSemanticModel context)
        //		{
        //			var delegateVisitor = new GetDelgateUsagesVisitor(context);
        //			context.RootNode.AcceptVisitor(delegateVisitor);
        //
        //			return new GatherVisitor(context, delegateVisitor);
        //		}
        //
        //		#endregion

        // Collect all methods that are used as delegate
        //		class GetDelgateUsagesVisitor : DepthFirstAstVisitor
        //		{
        //			BaseSemanticModel ctx;
        //			public readonly List<IMethod> UsedMethods = new List<IMethod>();
        //
        //			public GetDelgateUsagesVisitor(BaseSemanticModel ctx)
        //			{
        //				this.ctx = ctx;
        //			}
        //
        //			public override void VisitIdentifierExpression(IdentifierExpression identifierExpression)
        //			{
        //				if (!IsTargetOfInvocation(identifierExpression)) {
        //					var mgr = ctx.Resolve(identifierExpression) as MethodGroupResolveResult;
        //					if (mgr != null)
        //						UsedMethods.AddRange(mgr.Methods);
        //				}
        //				base.VisitIdentifierExpression(identifierExpression);
        //			}
        //
        //			public override void VisitMemberReferenceExpression(MemberReferenceExpression memberReferenceExpression)
        //			{
        //				if (!IsTargetOfInvocation(memberReferenceExpression)) {
        //					var mgr = ctx.Resolve(memberReferenceExpression) as MethodGroupResolveResult;
        //					if (mgr != null)
        //						UsedMethods.AddRange(mgr.Methods);
        //				}
        //				base.VisitMemberReferenceExpression(memberReferenceExpression);
        //			}
        //
        //			static bool IsTargetOfInvocation(AstNode node)
        //			{
        //				return node.Role == Roles.TargetExpression && node.Parent is InvocationExpression;
        //			}
        //		}

        //		class GatherVisitor : GatherVisitorBase<UnusedParameterAnalyzer>
        //		{
        //			//			GetDelgateUsagesVisitor usedDelegates;
        //			//bool currentTypeIsPartial;

        //			public GatherVisitor(SemanticModel semanticModel, Action<Diagnostic> addDiagnostic, CancellationToken cancellationToken)
        //				: base (semanticModel, addDiagnostic, cancellationToken)
        //			{
        //			}

        ////			public override void VisitTypeDeclaration(TypeDeclaration typeDeclaration)
        ////			{
        ////				bool outerTypeIsPartial = currentTypeIsPartial;
        ////				currentTypeIsPartial = typeDeclaration.HasModifier(Modifiers.Partial);
        ////				base.VisitTypeDeclaration(typeDeclaration);
        ////				currentTypeIsPartial = outerTypeIsPartial;
        ////			}
        ////
        ////			public override void VisitMethodDeclaration(MethodDeclaration methodDeclaration)
        ////			{
        ////				// Only some methods are candidates for the warning
        ////
        ////				if (methodDeclaration.Body.IsNull)
        ////					return;
        ////				if (methodDeclaration.Modifiers.HasFlag(Modifiers.Virtual) ||
        ////				    methodDeclaration.Modifiers.HasFlag(Modifiers.New) ||
        ////				    methodDeclaration.Modifiers.HasFlag(Modifiers.Partial))
        ////					return;
        ////				var methodResolveResult = ctx.Resolve(methodDeclaration) as MemberResolveResult;
        ////				if (methodResolveResult == null)
        ////					return;
        ////				var member = methodResolveResult.Member;
        ////				if (member.IsOverride)
        ////					return;
        ////				if (member.ImplementedInterfaceMembers.Any())
        ////					return;
        ////				if (usedDelegates.UsedMethods.Any(m => m.MemberDefinition == member))
        ////					return;
        ////				if (currentTypeIsPartial && methodDeclaration.Parameters.Count == 2) {
        ////					if (methodDeclaration.Parameters.First().Name == "sender") {
        ////						// Looks like an event handler; the registration might be in the designer part
        ////						return;
        ////					}
        ////				}
        ////				foreach (var parameter in methodDeclaration.Parameters)
        ////					parameter.AcceptVisitor(this);
        ////			}
        ////
        ////			public override void VisitParameterDeclaration(ParameterDeclaration parameterDeclaration)
        ////			{
        ////				base.VisitParameterDeclaration(parameterDeclaration);
        ////
        ////				if (!(parameterDeclaration.Parent is MethodDeclaration || parameterDeclaration.Parent is ConstructorDeclaration))
        ////					return;
        ////
        ////				var resolveResult = ctx.Resolve(parameterDeclaration) as LocalResolveResult;
        ////				if (resolveResult == null)
        ////					return;
        ////				if (resolveResult.Type.Name == "StreamingContext" && resolveResult.Type.Namespace == "System.Runtime.Serialization") {
        ////					// commonly unused parameter in constructors associated with ISerializable
        ////					return;
        ////				}
        ////
        ////				if (ctx.FindReferences(parameterDeclaration.Parent, resolveResult.Variable).Any(r => r.Node != parameterDeclaration))
        ////					return;
        ////
        ////				AddDiagnosticAnalyzer(new CodeIssue (
        ////					parameterDeclaration.NameToken, 
        //			//					string.Format(ctx.TranslateString("Parameter '{0}' is never used"), parameterDeclaration.Name)) { IssueMarker = IssueMarker.GrayOut });
        ////			}
        //		}
    }
}
