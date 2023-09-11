using Microsoft.VisualStudio.Language.Intellisense.AsyncCompletion;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;
using System;
using System.ComponentModel.Composition;

namespace CustomIntellisense
{
    [Export(typeof(IAsyncCompletionSourceProvider))]
    [ContentType("CSharp")]
    [Name("Comment Completion Source")]
    internal class CommentCompletionSourceProvider : IAsyncCompletionSourceProvider
    {
        private readonly Lazy<CommentCompletionSource> _source = new Lazy<CommentCompletionSource>();

        public IAsyncCompletionSource GetOrCreate(ITextView textView) => _source.Value;
    }
}
