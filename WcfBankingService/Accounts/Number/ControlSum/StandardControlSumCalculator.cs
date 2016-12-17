using WcfBankingService.Accounts.Number.ControlSum;

namespace WcfBankingService.Accounts.Number.ControlSum
{
    public class StandardControlSumCalculator : IControlSumCalculator
    {
        /*
         * Rozpatrujemy konto o numerze IBAN PL83101010230000261395100000
         * 
Numer posiada 28 znaków. Dobry znak, jest szansa, że numer jest prawidłowy. 

Po przeniesieniu 4 pierwszych znaków na koniec numeru uzyskujemy następujący ciąg
znaków alfanumerycznych: 101010230000261395100000PL83

Zamieniamy litery na cyfry, uzyskujemy liczbę: 101010230000261395100000252183
Wyliczamy resztę z dzielenia tej liczby przez 97. Reszta wynosi 1.

Reszta z dzielenia wynosi 1, co oznacza, że suma kontrolna jest prawidłowa.
         * */

        private const int BankIdLength = 8;
        private const int InnerAccountNumberLength = 16;

        public string Calculate(string bankId, string innerAccountNumber)
        {
            ValidateInput(bankId, innerAccountNumber);
            
            return "00";
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