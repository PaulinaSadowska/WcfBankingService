﻿using System;
using System.Globalization;

namespace WcfBankingService.Accounts.Number.ControlSum
{
    /// <summary>
    /// Calculates control sum of the account number and checks if account number is correct accordint to NRB format
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class NRBControlSumCalculator : IControlSumCalculator
    {
        private const int AccountNumberLength = 26;
        private const int BankIdLength = 8;
        private const int InnerAccountNumberLength = 16;

        private const string PL = "2521";

        /// <summary>
        /// <see cref="IControlSumCalculator.Calculate"/>
        /// Throws exception when bankId or innerAccountNumber length is invalid
        /// </summary>
        public string Calculate(string bankId, string innerAccountNumber)
        {
            ValidateInput(bankId, innerAccountNumber);
            var controlSum = CalculateControlSum($"{bankId}{innerAccountNumber}{PL}00");
            return controlSum.ToString("00");
        }

        /// <summary>
        /// <see cref="IControlSumCalculator.IsValid"/>
        /// </summary>
        public bool IsValid(string accountNumber)
        {
            if (accountNumber == null || accountNumber.Length != AccountNumberLength)
            {
                return false;
            }
            var checksum = CalculateControlSum($"{accountNumber.Substring(2)}{PL}00");
            return checksum == int.Parse(accountNumber.Substring(0, 2));
        }

        private static int CalculateControlSum(string number)
        {
            if(number.Length<AccountNumberLength+4)
                throw new ArgumentException("CalculateControlSum - number to short");

            var firstPart = number.Substring(0, number.Length / 2);
            var secondPart = number.Substring(number.Length / 2, number.Length / 2);

            var parsedFirstPart = ulong.Parse(firstPart, NumberStyles.Integer);
            var firstModulo = parsedFirstPart % 97;
            var parsedSecondPartCombined = ulong.Parse(firstModulo + secondPart, NumberStyles.Integer);

            var checksum = 98 - (int)(parsedSecondPartCombined % 97);
            return checksum;
        }

        private static void ValidateInput(string bankId, string number)
        {
            if(bankId == null || number == null)
            {
                throw new NullReferenceException("BankId and inner account innerAccountNumber can't be null");
            }
            if (bankId.Length != BankIdLength || number.Length != InnerAccountNumberLength)
            {
                throw new ArgumentException("BankId or inner account number length invalid");
            }
        }

    }
}