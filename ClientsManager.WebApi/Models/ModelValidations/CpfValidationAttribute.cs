using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClientsManager.WebApi.Models.ModelValidations
{
    public class CpfValidationAttribute : ValidationAttribute
    {
        public CpfValidationAttribute()
        {

        }

        public override bool IsValid(object value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return false;

            bool isValid = CpfValidation(value.ToString());

            return isValid;
        }

        private bool CpfValidation(string cpf)
        {
            return CheckCpfFormat(cpf) && FirstDigitValidation(cpf) && SecondDigitValidation(cpf);
        }

        private bool CheckCpfFormat(string cpf)
        {
            var rgx = new Regex(@"^(\d{3}).(\d{3}).(\d{3})-(\d{2})");

            return rgx.IsMatch(cpf);
        }

        private bool FirstDigitValidation(string cpf)
        {
            char[] splitedCpf = cpf.ToCharArray();
            List<int> numbers = new List<int>();

            foreach (char item in splitedCpf)
            {
                if (char.IsDigit(item))
                    numbers.Add(int.Parse(item.ToString()));
            }

            numbers.Reverse();
            
            int sumTotal = 0;
            for (int i = 10; i >= 2; i--)
            {
                int currentNumber = numbers[i];
                var result = currentNumber * i;
                sumTotal += result;
            }

            int reminder = (sumTotal * 10) % 11;

            if (reminder == numbers[1])
                return true;

            return false;
        }

        private bool SecondDigitValidation(string cpf)
        {
            char[] splitedCpf = cpf.ToCharArray();
            List<int> numbers = new List<int>();

            foreach (char item in splitedCpf)
            {
                if (char.IsDigit(item))
                    numbers.Add(int.Parse(item.ToString()));
            }

            numbers.Reverse();

            int sumTotal = 0;
            for (int i = 11; i >= 2; i--)
            {
                int currentNumber = numbers[i - 1];
                var result = currentNumber * i;
                sumTotal += result;
            }

            int reminder = (sumTotal * 10) % 11;

            if (reminder == numbers[0])
                return true;

            return false;
        }
    }
}
