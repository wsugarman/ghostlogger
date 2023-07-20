// Copyright © William Sugarman.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Ghostlogger.Cil;
using Ghostlogger.IO;
using Mono.Cecil;

namespace Ghostlogger;

internal class LogWeaver
{
    private readonly LogPatternMatcher _patternMatcher;
    private readonly LogOptimizer _optimizer;
    private readonly WeavingOptions _options;

    public LogWeaver(LogPatternMatcher patternMatcher, LogOptimizer optimizer, WeavingOptions options)
    {
        _patternMatcher = patternMatcher ?? throw new ArgumentNullException(nameof(patternMatcher));
        _optimizer = optimizer ?? throw new ArgumentNullException(nameof(optimizer));
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    public virtual void Update(string path)
    {
        using ModuleDefinition module = ModuleDefinition.ReadModule(path);

        if (_options.PreserveOriginal)
            File.Copy(path, path + Extensions.Bak);

        IEnumerable<InstructionMatch> matches = module
            .Types
            .SelectMany(x => x.Methods)
            .SelectMany(x => _patternMatcher.Matches(x.Body));

        LogTypeBuilder typeBuilder = new LogTypeBuilder(_options.LogTypeFullName!);
        foreach (InstructionMatch match in matches)
        {
            _optimizer.Optimize(typeBuilder, match);
        }

        if (typeBuilder.TypeDefinition.HasFields)
            module.Types.Add(typeBuilder.TypeDefinition);

        // TODO: Update parameters
        module.Write(
            path,
            new WriterParameters
            {
                StrongNameKeyPair = null,
                WriteSymbols = false,
            });
    }
}
