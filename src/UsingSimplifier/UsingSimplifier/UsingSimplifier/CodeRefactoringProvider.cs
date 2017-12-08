namespace UsingSimplifier
{
    using System;
    using System.Collections.Generic;
    using System.Composition;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CodeActions;
    using Microsoft.CodeAnalysis.CodeRefactorings;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Microsoft.CodeAnalysis.Rename;
    using Microsoft.CodeAnalysis.Text;
    using Microsoft.CodeAnalysis.Editing;

    [ExportCodeRefactoringProvider(LanguageNames.CSharp, Name = nameof(UsingSimplifierCodeRefactoringProvider)), Shared]
    internal class UsingSimplifierCodeRefactoringProvider : CodeRefactoringProvider
    {
        public sealed override async Task ComputeRefactoringsAsync(CodeRefactoringContext context)
        {
            // TODO: Replace the following code with your own analysis, generating a CodeAction for each refactoring to offer

            var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);

            // Find the node at the selection.
            var node = root.FindNode(context.Span);

            // Only offer a refactoring if the selected node is a using directive node.
            var usingDirective = node.AncestorsAndSelf().OfType<UsingDirectiveSyntax>().FirstOrDefault();

            if (usingDirective == null)
            {
                return;
            }


            if (usingDirective.AncestorsAndSelf().OfType<NamespaceDeclarationSyntax>().Any())
            {
                return;
            }

            if (!usingDirective.Parent.DescendantNodes().OfType<NamespaceDeclarationSyntax>().Any())
            {
                return;
            }

            var siblingUsingDirectives = usingDirective.Parent.ChildNodes().OfType<UsingDirectiveSyntax>();

            // For any type using directive not inside a namespace, create a code action to move them into the document namespaces.
            var action = CodeAction.Create("Simplify using directives", c => this.SimplifyUsingDirectives(context.Document, siblingUsingDirectives, c));

            // Register this code action.
            context.RegisterRefactoring(action);
        }

        private async Task<Document> SimplifyUsingDirectives(Document document, IEnumerable<UsingDirectiveSyntax> siblingUsingDirectives, CancellationToken c)
        {
            var descendantNameSpaces = siblingUsingDirectives.First().Parent.DescendantNodes().OfType<NamespaceDeclarationSyntax>();
            var editor = await DocumentEditor.CreateAsync(document, c);

            foreach (var usingDirective in siblingUsingDirectives)
            {
                editor.RemoveNode(usingDirective);
            }

            foreach (var namespaceDeclaration in descendantNameSpaces)
            {
                this.AddUsingsToNamespace(namespaceDeclaration, siblingUsingDirectives, editor);
            }

            return editor.GetChangedDocument();
        }

        private void AddUsingsToNamespace(NamespaceDeclarationSyntax namespaceDeclaration, IEnumerable<UsingDirectiveSyntax> siblingUsingDirectives, DocumentEditor editor)
        {
            var newNs = namespaceDeclaration;
            foreach (var usingDirective in siblingUsingDirectives)
            {
                newNs = newNs.AddUsings(usingDirective);
            }

            editor.ReplaceNode(namespaceDeclaration, newNs);
        }
    }
}