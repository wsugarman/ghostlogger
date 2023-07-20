// Copyright © William Sugarman.
// Licensed under the MIT License.

using System.Collections.Generic;
using Mono.Cecil.Cil;

namespace Ghostlogger.Cil;

internal class LogPatternMatcher
{
    public virtual IReadOnlyCollection<InstructionMatch> Matches(MethodBody body)
    {
        throw new System.NotImplementedException();
    }
}
