// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;

namespace Ghostlogger.Cil;

internal class LogOptimizer
{
    public virtual void Optimize(LogTypeBuilder builder, InstructionMatch match)
    {
        throw new NotImplementedException();
    }
}
