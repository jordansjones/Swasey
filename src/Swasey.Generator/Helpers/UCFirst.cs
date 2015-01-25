using System;
using System.IO;
using System.Linq;

namespace Swasey.Helpers
{
    // ReSharper disable once InconsistentNaming
    internal class UCFirst : IInlineHelper
    {


        public void Run(TextWriter output, dynamic context, object[] arguments)
        {
            if (arguments.Length == 0) return;

            arguments
                .Select(x => x.ToString())
                .ToList()
                .ForEach(
                    x =>
                    {
                        if (!string.IsNullOrWhiteSpace(x) && !Char.IsUpper(x[0]))
                        {
                            x = x.Length == 1
                                ? x.ToUpper()
                                : Char.ToUpper(x[0]) + x.Substring(1);
                        }

                        output.Write(x);
                    });
        }

    }
}
