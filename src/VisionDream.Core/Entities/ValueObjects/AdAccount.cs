﻿// Copyright (c) 2023 VisionDream Authors. Web: https://visiondreamict.wordpress.com/.
// Licensed under the MIT license. See LICENSE file for license information.

using VisionDream.Core.Common;
using VisionDream.Core.Exceptions;

namespace VisionDream.Domain.ValueObjects
{
    /// <summary>
    /// Active directory account derived type.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The derived <see cref="AdAccount"/> class inherits from the base
    /// <see cref="ValueObject"/> <see langword="abstract"/> class.
    /// </para>
    /// </remarks>
    public class AdAccount : ValueObject
    {
        private AdAccount()
        {
        }

        public string? Domain { get; private set; }

        public string? Name { get; private set; }

        public static AdAccount For(string accountString)
        {
            var account = new AdAccount();

            try
            {
                var index = accountString.IndexOf("\\", StringComparison.Ordinal);
                account.Domain = accountString.Substring(0, index);
                account.Name = accountString.Substring(index + 1);
            }
            catch (Exception ex)
            {
                throw new AdAccountInvalidException(accountString, ex);
            }

            return account;
        }

        public static implicit operator string(AdAccount account)
        {
            return account.ToString();
        }

        public static explicit operator AdAccount(string accountString)
        {
            return For(accountString);
        }

        public override string ToString()
        {
            return $"{Domain}\\{Name}";
        }

        protected override IEnumerable<object?> GetAtomicValues()
        {
            yield return Domain;
            yield return Name;
        }
    }
}
