using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

namespace Swasey.Helpers
{
    internal abstract class FirstCharacterCaseHelper : IInlineHelper
    {

        public void Run(TextWriter output, dynamic context, object[] arguments)
        {
            if (arguments.Length == 0) { return; }

            arguments
                .Where(x => x != null)
                .Select(x => x.ToString())
                .ToList()
                .ForEach(
                    x =>
                    {
                        var toWrite = x;
                        if (!string.IsNullOrWhiteSpace(x) && !CaseTest(x[0]))
                        {
                            var caseChar = CaseAdjustment(x[0]);
                            var remaining = string.Empty;
                            if (x.Length > 1)
                            {
                                remaining = x.Substring(1);
                            }

                            toWrite = string.Format("{0}{1}", caseChar, remaining);
                        }

                        output.Write(toWrite);
                    });
        }

        protected abstract char CaseAdjustment(char c);

        protected abstract bool CaseTest(char c);

    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal class UCFirst : FirstCharacterCaseHelper
    {

        protected override char CaseAdjustment(char c)
        {
            return char.ToUpperInvariant(c);
        }

        protected override bool CaseTest(char c)
        {
            return char.IsUpper(c);
        }

    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal class LCFirst : FirstCharacterCaseHelper
    {

        protected override char CaseAdjustment(char c)
        {
            return char.ToLowerInvariant(c);
        }

        protected override bool CaseTest(char c)
        {
            return char.IsLower(c);
        }

    }
}
