using System;
using System.Globalization;
using WcfBankingService.Accounts.Number.ControlSum;

namespace WcfBankingService.Accounts.Number.ControlSum
{
    public class StandardControlSumCalculator : IControlSumCalculator
    {
        /*
Zamieniamy litery na cyfry, uzyskujemy liczbę: 101010230000261395100000252183
Wyliczamy resztę z dzielenia tej liczby przez 97. Reszta wynosi 1.

Reszta z dzielenia wynosi 1, co oznacza, że suma kontrolna jest prawidłowa.
         * */

        private const int BankIdLength = 8;
        private const int InnerAccountNumberLength = 16;

        private const string PL = "2521";

        public string Calculate(string bankId, string innerAccountNumber)
        {
            ValidateInput(bankId, innerAccountNumber);
            int controlSum = CalculateControlSum($"{bankId}{innerAccountNumber}{PL}00");
            return controlSum.ToString("00");
        }

        private int CalculateControlSum(string number)
        {
            var firstPart = number.Substring(0, number.Length / 2);
            var secondPart = number.Substring(number.Length / 2, number.Length / 2);

            var parsedFirstPart = ulong.Parse(firstPart, NumberStyles.Integer);
            var firstModulo = parsedFirstPart % 97;
            var parsedSecondPartCombined = ulong.Parse(firstModulo + secondPart, NumberStyles.Integer);

            var checksum = 98 - (int)(parsedSecondPartCombined % 97);
            return checksum;
        }

        private void ValidateInput(string bankId, string number)
        {
            if(bankId == null || number == null)
            {
                throw new System.NullReferenceException("BankId and inner account innerAccountNumber can't be null");
            }
            if (bankId.Length != BankIdLength || number.Length != InnerAccountNumberLength)
            {
                throw new System.ArgumentException("BankId or inner account number length invalid");
            }
        }

    }
}