// -----------------------------------------------------------------------
// <copyright file="CommentsObjectDescriptor.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Client.Loader.Features.Configs
{
    using System;
    using YamlDotNet.Core;
    using YamlDotNet.Serialization;

    /// <summary>
    /// Source: https://dotnetfiddle.net/8M6iIE.
    /// </summary>
    public sealed class CommentsObjectDescriptor : IObjectDescriptor
    {
        private readonly IObjectDescriptor _innerDescriptor;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentsObjectDescriptor"/> class.
        /// </summary>
        /// <param name="innerDescriptor">The inner descriptor instance.</param>
        /// <param name="comment">The comment to be written.</param>
        public CommentsObjectDescriptor(IObjectDescriptor innerDescriptor, string comment)
        {
            _innerDescriptor = innerDescriptor;
            Comment = comment;
        }

        /// <summary>
        /// Gets the comment to be written.
        /// </summary>
        public string Comment { get; private set; }

        /// <inheritdoc/>
        public object Value => _innerDescriptor.Value;

        /// <inheritdoc/>
        public Type Type => _innerDescriptor.Type;

        /// <inheritdoc/>
        public Type StaticType => _innerDescriptor.StaticType;

        /// <inheritdoc/>
        public ScalarStyle ScalarStyle => _innerDescriptor.ScalarStyle;
    }
}