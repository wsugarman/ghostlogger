// Copyright © William Sugarman.
// Licensed under the MIT License.

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
    /// Gets or sets the assembly output path.
    /// </summary>
    /// <remarks>
    /// The path is used as both the input and the output to the task.
    /// </remarks>
    /// <value>The path to the target assembly.</value>
    [Required]
    public string? OutputPath { get; set; }

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
        return !_canceled;
    }
}
