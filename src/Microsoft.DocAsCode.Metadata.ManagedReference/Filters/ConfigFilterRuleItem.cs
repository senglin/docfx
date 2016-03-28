﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.DocAsCode.Metadata.ManagedReference
{
    using System;
    using System.Text.RegularExpressions;

    using Microsoft.CodeAnalysis;
    using Microsoft.DocAsCode.DataContracts.Common;
    using YamlDotNet.Serialization;

    public abstract class ConfigFilterRuleItem
    {
        private Regex _uidRegex;

        [YamlMember(Alias = Constants.PropertyName.UidRegex)]
        public string UidRegex
        {
            get
            {
                return _uidRegex?.ToString();
            }
            set
            {
                _uidRegex = new Regex(value);
            }
        }

        [YamlMember(Alias = Constants.PropertyName.Type)]
        public ExtendedSymbolKind? Kind { get; set; }

        [YamlIgnore]
        public abstract bool CanVisit { get; }

        public bool IsMatch(ISymbol symbol)
        {
            if (symbol == null)
            {
                throw new ArgumentNullException("symbol");
            }
            return (_uidRegex == null || _uidRegex.IsMatch(VisitorHelper.GetId(symbol))) && (Kind == null || Kind.Value.Contains(symbol));
        }
    }
}
