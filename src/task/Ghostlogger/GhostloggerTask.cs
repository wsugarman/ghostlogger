// Copyright © William Sugarman.
// Licensed under the MIT License.

using Ghostlogger.Cil;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Ghostlogger;

/// <summary>
/// Represents an MSBuild <see cref="Task"/> that is responsible for optimizing structured logging.
/// </summary>
public sealed class GhostloggerTask : Task, ICancelableTask
{
    private bool _canceled;

    /// <summary>
    /// Gets or sets the name of the class that will contain the logging delegates.
    /// </summary>
    /// <value>The name of the generated class.</value>
    public string? LogClassName { get; set; } = "Log";

    /// <summary>
    /// Gets or sets the namespace used for the class that will contain the logging delegates.
    /// </summary>
    /// <value>The namespace for the generated class.</value>
    public string? LogClassNamespace { get; set; } = nameof(Ghostlogger);

    /// <summary>
    /// Gets or sets the assembly output path.
    /// </summary>
    /// <remarks>
    /// The path is used as both the input and the output to the task.
    /// </remarks>
    /// <value>The path to the target assembly.</value>
    [Required]
    public string? OutputPath { get; set; }

    /// <summary>
    /// Gets or sets whether the original assembly should be preserved in the output directory.
    /// </summary>
    /// <remarks>
    /// The original assembly will use the extension <c>.bak</c>.
    /// </remarks>
    /// <value>
    /// <see langword="true"/> if the original assembly should be preserved; <see langword="false"/> otherwise.
    /// </value>
    public bool PreserveOriginal { get; set; }

    /// <inheritdoc/>
    public void Cancel()
        => _canceled = true;

    /// <summary>
    /// Optimizes the logging for the assembly located at <see cref="OutputPath"/> by leveraging
    /// <a href="https://learn.microsoft.com/en-us/dotnet/core/extensions/high-performance-logging">high-performance logging</a>.
    /// </summary>
    /// <returns><see langword="true"/> if successful; otherwise <see langword="false"/>.</returns>
    public override bool Execute()
    {
        LogPatternMatcher patternMatcher = new LogPatternMatcher();
        LogOptimizer logOptimizer = new LogOptimizer();
        WeavingOptions options = new WeavingOptions
        {
            LogTypeFullName = new TypeFullName(LogClassNamespace!, LogClassName!),
            PreserveOriginal = PreserveOriginal,
        };

        LogWeaver weaver = new LogWeaver(patternMatcher, logOptimizer, options);
        weaver.Update(OutputPath!);

        return _canceled;
    }
}
