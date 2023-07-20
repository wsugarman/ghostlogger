// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using Mono.Cecil;

namespace Ghostlogger.Cil;

internal sealed class LogTypeBuilder
{
    public TypeDefinition TypeDefinition { get; }

    public LogTypeBuilder(TypeFullName type)
    {
        if (type is null)
            throw new ArgumentNullException(nameof(type));

        TypeDefinition = new TypeDefinition(type.Namespace, type.Name, TypeAttributes.NotPublic | TypeAttributes.Sealed);
    }

    public LogTypeBuilder Append()
    {
        return this;
    }
}
