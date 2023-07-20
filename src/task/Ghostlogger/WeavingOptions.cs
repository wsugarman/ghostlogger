// Copyright © William Sugarman.
// Licensed under the MIT License.

using Ghostlogger.Cil;

namespace Ghostlogger;

internal sealed class WeavingOptions
{
    public TypeFullName? LogTypeFullName { get; set; }

    public bool PreserveOriginal { get; set; }
}
