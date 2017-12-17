namespace UsingSimplifier
{
    using System.Collections.Generic;
    using System.Composition;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CodeActions;
    using Microsoft.CodeAnalysis.CodeRefactorings;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Microsoft.CodeAnalysis.Editing;

    [ExportCodeRefactoringProvider(
        LanguageNames.CSharp, 
        Name = nameof(UsingSimplifierCodeRefactoringProvider)),
        Shared]
    internal class UsingSimplifierCodeRefactoringProvider : CodeRefactoringProvider
    {
        public sealed override async Task ComputeRefactoringsAsync(CodeRefactoringContext context)
        {
            var root = await context
                .Document
                .GetSyntaxRootAsync(context.CancellationToken)
                .ConfigureAwait(false);

            // Find the using declarations nodes at the root childs level.
            var nodes = root.ChildNodes().OfType<UsingDirectiveSyntax>();

            // Only offer a refactoring if the selected node is a using directive node.
            if (!nodes.Any())
            {
                return;
            }

            if (!root.DescendantNodes().OfType<NamespaceDeclarationSyntax>().Any())
            {
                return;
            }

            // For any type using directive not inside a namespace, create a code action to move them into the document namespaces.
            var action = CodeAction.Create(
                "Simplify using directives", 
                c => this.SimplifyUsingDirectives(context.Document, nodes, c));

            // Register this code action.
            context.RegisterRefactoring(action);
        }

        private async Task<Document> SimplifyUsingDirectives(
            Document document,
            IEnumerable<UsingDirectiveSyntax> rootUsingDirectives,
            CancellationToken c)
        {
            var descendantNameSpaces = rootUsingDirectives
                .First()
                .Parent
                .DescendantNodes()
                .OfType<NamespaceDeclarationSyntax>();

            var editor = await DocumentEditor.CreateAsync(document, c);

            foreach (var usingDirective in rootUsingDirectives)
            {
                editor.RemoveNode(usingDirective);
            }

            foreach (var namespaceDeclaration in descendantNameSpaces)
            {
                this.AddUsingsToNamespace(namespaceDeclaration, rootUsingDirectives, editor);
            }

            return editor.GetChangedDocument();
        }

        private void AddUsingsToNamespace(
            NamespaceDeclarationSyntax namespaceDeclaration,
            IEnumerable<UsingDirectiveSyntax> siblingUsingDirectives,
            DocumentEditor editor)
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