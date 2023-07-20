// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using Mono.Cecil.Cil;

namespace Ghostlogger.Cil;

internal sealed class InstructionMatch
{
    public MethodBody Body { get; }

    public int Index { get; }

    public Instruction Instruction => Body.Instructions[Index];

    public InstructionMatch(MethodBody body, int index)
    {
        if (body is null)
            throw new ArgumentNullException(nameof(body));

        if (index < 0 || index >= body.Instructions.Count)
            throw new ArgumentOutOfRangeException(nameof(index));

        Body = body;
        Index = index;
    }
}
