// -----------------------------------------------------------------------
// <copyright file="CommentsPropertyDescriptor.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Client.Loader.Features.Configs
{
    using System;
    using System.ComponentModel;
    using YamlDotNet.Core;
    using YamlDotNet.Serialization;

    /// <summary>
    /// Source: https://dotnetfiddle.net/8M6iIE.
    /// </summary>
    public sealed class CommentsPropertyDescriptor : IPropertyDescriptor
    {
        private readonly IPropertyDescriptor _baseDescriptor;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentsPropertyDescriptor"/> class.
        /// </summary>
        /// <param name="baseDescriptor">The base descriptor instance.</param>
        public CommentsPropertyDescriptor(IPropertyDescriptor baseDescriptor)
        {
            this._baseDescriptor = baseDescriptor;
            Name = baseDescriptor.Name;
        }

        /// <inheritdoc/>
        public string Name { get; set; }

        /// <inheritdoc/>
        public Type Type => _baseDescriptor.Type;

        /// <inheritdoc/>
        public Type TypeOverride
        {
            get => _baseDescriptor.TypeOverride;
            set => _baseDescriptor.TypeOverride = value;
        }

        /// <inheritdoc/>
        public int Order { get; set; }

        /// <inheritdoc/>
        public ScalarStyle ScalarStyle
        {
            get => _baseDescriptor.ScalarStyle;
            set => _baseDescriptor.ScalarStyle = value;
        }

        /// <inheritdoc/>
        public bool CanWrite => _baseDescriptor.CanWrite;

        /// <inheritdoc/>
        public void Write(object target, object value)
        {
            _baseDescriptor.Write(target, value);
        }

        /// <inheritdoc/>
        public T GetCustomAttribute<T>()
            where T : Attribute => _baseDescriptor.GetCustomAttribute<T>();

        /// <inheritdoc/>
        public IObjectDescriptor Read(object target) => _baseDescriptor.GetCustomAttribute<DescriptionAttribute>() is DescriptionAttribute attribute &&
            attribute != null ? new CommentsObjectDescriptor(_baseDescriptor.Read(target), attribute.Description) : _baseDescriptor.Read(target);
    }
}