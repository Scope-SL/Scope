// -----------------------------------------------------------------------
// <copyright file="ConfigsValidator.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Client.Loader.Features.Configs
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using YamlDotNet.Core;
    using YamlDotNet.Serialization;

    /// <summary>
    /// Used to validate configs.
    /// </summary>
    public sealed class ConfigsValidator : INodeDeserializer
    {
        private readonly INodeDeserializer _nodeDeserializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigsValidator"/> class.
        /// </summary>
        /// <param name="nodeDeserializer">The node deserializer instance.</param>
        public ConfigsValidator(INodeDeserializer nodeDeserializer) => _nodeDeserializer = nodeDeserializer;

        /// <inheritdoc/>
        public bool Deserialize(IParser parser, Type expectedType, Func<IParser, Type, object> nestedObjectDeserializer, out object value)
        {
            if (!_nodeDeserializer.Deserialize(parser, expectedType, nestedObjectDeserializer, out value))
                return false;

            Validator.ValidateObject(value, new(value, null, null), true);
            return true;
        }
    }
}