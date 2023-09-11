using Microsoft.VisualStudio.Core.Imaging;
using Microsoft.VisualStudio.Imaging;
using Microsoft.VisualStudio.Language.Intellisense.AsyncCompletion;
using Microsoft.VisualStudio.Language.Intellisense.AsyncCompletion.Data;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Adornments;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;

namespace CustomIntellisense
{
    internal class CommentCompletionSource : IAsyncCompletionSource
    {
        private readonly ImageElement _icon = new ImageElement(KnownMonikers.IntellisenseKeyword.ToImageId(), "icon");
        private readonly CompletionContext _context;

        public CommentCompletionSource()
        {
            var list = new List<CompletionItem>
            {
                new CompletionItem("Regional Director, C# Corner", this, _icon),
                new CompletionItem("A Dash of .NET", this, _icon),
                new CompletionItem("Cool Host", this, _icon)
            };

            _context = new CompletionContext(list.ToImmutableArray());
        }

        public Task<CompletionContext> GetCompletionContextAsync(IAsyncCompletionSession session, CompletionTrigger trigger, 
            SnapshotPoint triggerLocation, SnapshotSpan applicableToSpan, CancellationToken token)
        {
            var containingLine = triggerLocation.GetContainingLine();
            var text = containingLine.Extent.GetText();
            if (!string.IsNullOrWhiteSpace(text) && text.IndexOf("// Stephen", StringComparison.OrdinalIgnoreCase) > -1)
            {
                return Task.FromResult(_context);
            }
            return Task.FromResult(CompletionContext.Empty);
        }

        public async Task<object> GetDescriptionAsync(IAsyncCompletionSession session, CompletionItem item, CancellationToken token)
        {
            return await Task.FromResult(item.DisplayText);
        }

        public CompletionStartData InitializeCompletion(CompletionTrigger trigger, SnapshotPoint triggerLocation, CancellationToken token)
        {
            return CompletionStartData.ParticipatesInCompletionIfAny;
        }
    }
}
