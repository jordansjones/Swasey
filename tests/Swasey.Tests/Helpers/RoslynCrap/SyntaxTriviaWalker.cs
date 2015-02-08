using System;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Swasey.Tests.Helpers
{
    public class SyntaxTriviaWalker : CSharpSyntaxWalker
    {

        public Predicate<SyntaxTrivia> TriviaPredicate { get; set; }
        public Action<SyntaxTrivia> TriviaAction { get; set; }

        public SyntaxTriviaWalker() : base(SyntaxWalkerDepth.StructuredTrivia) {}

        public override void VisitTrivia(SyntaxTrivia trivia)
        {
            if (TriviaPredicate != null && TriviaAction != null && TriviaPredicate(trivia))
            {
                TriviaAction(trivia);
            }
        }

    }
}