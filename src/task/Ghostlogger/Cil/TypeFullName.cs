// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;

namespace Ghostlogger.Cil;

internal sealed class TypeFullName
{
    public string? Name { get; }

    public string? Namespace { get; }

    public TypeFullName(string @namespace, string name)
    {
        if (string.IsNullOrWhiteSpace(@namespace))
            throw new ArgumentNullException(nameof(@namespace));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name));

        Name = name;
        Namespace = @namespace;
    }

    public override string ToString()
        => Namespace + "." + Name;
}
