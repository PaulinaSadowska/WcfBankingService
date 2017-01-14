﻿using System.ServiceModel;
using WcfBankingService.Service.DataContract.Request;
using WcfBankingService.Users;

namespace WcfBankingService.Service.Validation
{
    public class ServiceInputValidator : IServiceInputValidator
    {
        public const int MinLoginLength = 3;
        public const int MinPasswordLength = 3;
        private const int AccountNumberLength = 26;

        public void ValidateLogin(string login)
        {
            CheckNotNull(login, "login");
            CheckMinLength(login, MinLoginLength, "login");
        }

        public void ValidatePassword(string password)
        {
            CheckNotNull(password, "password");
            CheckMinLength(password, MinPasswordLength, "password");
        }

        public void ValidateAccessToken(string accessToken)
        {
            CheckNotNull(accessToken, "accessToken");
            CheckLength(accessToken, User.AccessTokenLength, "accessToken");
        }

        public void ValidateAccountNumber(string accountNumber)
        {
            CheckNotNull(accountNumber, "accountNumber");
            CheckLength(accountNumber, AccountNumberLength, "acountNumber");
        }

        public void Validate(WithdrawData paymentData)
        {
            CheckNotNull(paymentData, "paymentData");
            ValidateAccountNumber(paymentData.AccountNumber);
            ValidateAccessToken(paymentData.AccessToken);
            CheckNotNull(paymentData.OperationTitle, "operation title");
            ValidateAmount(paymentData.Amount);
        }

        public void Validate(DepositData paymentData)
        {
            CheckNotNull(paymentData, "paymentData");
            ValidateAccountNumber(paymentData.AccountNumber);
            CheckNotNull(paymentData.OperationTitle, "operation title");
            ValidateAmount(paymentData.Amount);
        }

        public void Validate(TransferData transferData)
        {
            CheckNotNull(transferData, "transferData");
            ValidateAccountNumber(transferData.AccountNumber);
            ValidateAccountNumber(transferData.SenderAccountNumber);
            CheckNotSame(transferData.AccountNumber, transferData.SenderAccountNumber, "Account Numbers");
            CheckNotNull(transferData.Title, "operation title");
            if (transferData.Amount < 0)
            {
                throw new FaultException("Amount must be greater or equal to 0");
            }
        }

        public void Validate(SoapTransferData transferData)
        {
            CheckNotNull(transferData, "transferData");
            ValidateAccountNumber(transferData.SenderAccountNumber);
            ValidateAccountNumber(transferData.ReceiverAccountNumber);
            CheckNotSame(transferData.ReceiverAccountNumber, transferData.SenderAccountNumber, "Account Numbers");
            CheckNotNull(transferData.Title, "operation title");
            ValidateAmount(transferData.Amount);
        }

        private static void CheckNotSame(string accountNumber, string senderAccountNumber, string tag)
        {
            if (senderAccountNumber == accountNumber)
            {
                throw new FaultException($"{tag} cannot be the same!");
            }
        }

        private static void CheckNotNull(object value, string tag)
        {
            if (value == null)
            {
                throw new FaultException($"{tag} can't be null");
            }
        }

        private static void CheckMinLength(string value, int minLenght, string tag)
        {
            if (value.Length < minLenght)
            {
                throw new FaultException($"{tag} must contains at least {minLenght} characters");
            }
        }

        private static void CheckLength(string value, int lenght, string tag)
        {
            if (value.Length != lenght)
            {
                throw new FaultException($"{tag} must contain {lenght} characters");
            }
        }

        private static void ValidateAmount(string amount)
        {
            decimal amountValue;
            if (!decimal.TryParse(amount, out amountValue))
            {
                throw new FaultException("Wrong amount format");
            }
            if (amountValue < 0)
            {
                throw new FaultException("Amount must be greater or equal to 0");
            }
        }

    }
}