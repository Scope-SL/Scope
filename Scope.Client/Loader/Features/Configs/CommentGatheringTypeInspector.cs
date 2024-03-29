﻿// -----------------------------------------------------------------------
// <copyright file="CommentGatheringTypeInspector.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Client.Loader.Features.Configs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using YamlDotNet.Serialization;
    using YamlDotNet.Serialization.TypeInspectors;

    /// <summary>
    /// Source: https://dotnetfiddle.net/8M6iIE.
    /// </summary>
    public sealed class CommentGatheringTypeInspector : TypeInspectorSkeleton
    {
        private readonly ITypeInspector _innerTypeDescriptor;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentGatheringTypeInspector"/> class.
        /// </summary>
        /// <param name="innerTypeDescriptor">The inner type description instance.</param>
        public CommentGatheringTypeInspector(ITypeInspector innerTypeDescriptor) =>
            _innerTypeDescriptor = innerTypeDescriptor ?? throw new ArgumentNullException("innerTypeDescriptor");

        /// <inheritdoc/>
        public override IEnumerable<IPropertyDescriptor> GetProperties(Type type, object container) =>
            _innerTypeDescriptor.GetProperties(type, container).Select(descriptor => new CommentsPropertyDescriptor(descriptor));
    }
}