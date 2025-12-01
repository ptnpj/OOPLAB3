using System;

namespace SimpleClassLibrary
{
    public class Worker
    {
        public string FullName { get; set; }
        public string HomeCity { get; set; }
        public DateTime StartDate { get; set; }
        public Company WorkPlace { get; set; }

        public decimal BonusUAH { get; private set; }

        private const decimal UsdRate = 41.5m;
        private const decimal EurRate = 44.0m;
   

        public Worker()
        {
            FullName = "Невідомо";
            HomeCity = "Невідомо";
            StartDate = DateTime.Now;
            WorkPlace = new Company();
            BonusUAH = 0;
        }

        public Worker(string fullName, string homeCity, DateTime startDate, Company workPlace)
        {
            FullName = fullName;
            HomeCity = homeCity;
            StartDate = startDate;
            WorkPlace = workPlace;
            BonusUAH = 0;
        }

        public Worker(Worker other)
        {
            FullName = other.FullName;
            HomeCity = other.HomeCity;
            StartDate = other.StartDate;
            WorkPlace = new Company(other.WorkPlace);
            BonusUAH = other.BonusUAH;
        }

        public void SetBonus(decimal amount, int currencyType)
        {
            switch (currencyType)
            {
                case 1:
                    BonusUAH = amount;
                    break;
                case 2: 
                    BonusUAH = amount * UsdRate;
                    break;
                case 3: 
                    BonusUAH = amount * EurRate;
                    break;
                default:
                    BonusUAH = amount; 
                    break;
            }
        }

        public int GetWorkExperience()
        {
            DateTime now = DateTime.Now;
            return ((now.Year - StartDate.Year) * 12) + now.Month - StartDate.Month;
        }

        public bool LivesNotFarFromTheMainOffice()
        {
            if (WorkPlace == null) return false;
            return string.Equals(HomeCity, WorkPlace.MainOfficeCity, StringComparison.OrdinalIgnoreCase);
        }

        public string GetBonusInfo()
        {
            decimal bonusUSD = BonusUAH / UsdRate;
            decimal bonusEUR = BonusUAH / EurRate;

            return $"Премія: {BonusUAH:F2} UAH / {bonusUSD:F2} USD / {bonusEUR:F2} EUR";
        }

        public override string ToString()
        {
            return $"ПІБ: {FullName}\n" +
                   $"Місто проживання: {HomeCity}\n" +
                   $"Дата початку: {StartDate.ToShortDateString()} (Стаж міс.: {GetWorkExperience()})\n" +
                   $"Живе поруч з офісом: {(LivesNotFarFromTheMainOffice() ? "Так" : "Ні")}\n" +
                   $"{GetBonusInfo()}\n" +
                   $"[Інфо про роботу] -> {WorkPlace}";
        }
    }
}