//By: Tommy Dugger
//Created: 1/18/2014
//Purpose: To handle dates in the BCE era that are not
//covered in .Nets DateTime class.

using System;
using System.Globalization;
using System.Linq;

namespace HistoricalDate
{
    public class HistoricalAge
    {
        public int Years { get; set; }
    }

    public class HistoricalDate
    {
        public int Year { get; set; }

        public int Month { get; set; }

        public int Day { get; set; }

        public Era Era { get; set; }

        /// <summary>
        /// Returns an HistoricalDate with the day, month, year,
        /// and era set to current date
        /// </summary>
        /// <returns>HistoricalDate</returns>
        public static HistoricalDate Now()
        {
            return new HistoricalDate
            {
                Day = DateTime.Now.Day,
                Month = DateTime.Now.Month,
                Year = DateTime.Now.Year,
                Era = Era.AD
            };
        }

        /// <summary>
        /// Converts string to HistoricalDateObject.  String must be in the
        /// following format. "day/month/year era" the slash in the string
        /// can be substituted with the follow characters, "-", ".", ",", "\"
        /// </summary>
        /// <param name="date">string</param>
        /// <returns>HistoricalDate</returns>
        public HistoricalDate ParseString(string date)
        {
            const int january = 31;
            const int february = 28;
            const int march = 31;
            const int april = 30;
            const int may = 31;
            const int june = 30;
            const int july = 31;
            const int august = 31;
            const int september = 30;
            const int october = 31;
            const int november = 30;
            const int december = 31;

            string[] separators = { ",", ".", "-", "/", "\\", " " };

            var seperatedString = date.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            var day = 0;
            var month = 0;
            var year = 0;
            var era = Era.AD;

            if (seperatedString.Count() <= 1 || seperatedString.Count() >= 5)
            {
                return Now();
            }

            if (seperatedString.Count() == 2)
            {
                day = 0;
                month = 0;
                year = int.Parse(seperatedString.ElementAt(0));

                if (seperatedString.ElementAt(1).ToLower() == "ad")
                {
                    era = Era.AD;
                }

                if (seperatedString.ElementAt(1).ToLower() == "bc")
                {
                    era = Era.BC;
                }

            }

            if (seperatedString.Count() == 3)
            {
                day = 0;
                month = int.Parse(seperatedString.ElementAt(0));
                year = int.Parse(seperatedString.ElementAt(1));

                if (seperatedString.ElementAt(2).ToLower() == "ad")
                {
                    era = Era.AD;
                }

                if (seperatedString.ElementAt(2).ToLower() == "bc")
                {
                    era = Era.BC;
                }

                if (month > 12)
                {
                    throw new Exception("Month is out of range!  Month can not be greater than 12.");
                }
            }

            if (seperatedString.Count() == 4)
            {
                day = int.Parse(seperatedString.ElementAt(0));
                month = int.Parse(seperatedString.ElementAt(1));
                year = int.Parse(seperatedString.ElementAt(2));

                if (seperatedString.ElementAt(3).ToLower() == "ad")
                {
                    era = Era.AD;
                }

                if (seperatedString.ElementAt(3).ToLower() == "bc")
                {
                    era = Era.BC;
                }

                if (month > 12)
                {
                    throw new Exception("Month is out of range!  Month can not be greater than 12.");
                }

                if (month == 1)
                {
                    if (day > january)
                    {
                        throw new Exception(string.Format("January has only {0} days.", january));
                    }
                }

                if (month == 2)
                {
                    if (day > february)
                    {
                        throw new Exception(string.Format("February has only {0} days.", february));
                    }
                }

                if (month == 3)
                {
                    if (day > march)
                    {
                        throw new Exception(string.Format("March has only {0} days.", march));
                    }
                }

                if (month == 4)
                {
                    if (day > april)
                    {
                        throw new Exception(string.Format("April has only {0} days.", april));
                    }
                }

                if (month == 5)
                {
                    if (day > may)
                    {
                        throw new Exception(string.Format("May has only {0} days.", may));
                    }
                }

                if (month == 6)
                {
                    if (day > june)
                    {
                        throw new Exception(string.Format("June has only {0} days.", june));
                    }
                }

                if (month == 7)
                {
                    if (day > july)
                    {
                        throw new Exception(string.Format("July has only {0} days.", july));
                    }
                }

                if (month == 8)
                {
                    if (day > august)
                    {
                        throw new Exception(string.Format("August has only {0} days.", august));
                    }
                }

                if (month == 9)
                {
                    if (day > september)
                    {
                        throw new Exception(string.Format("September has only {0} days.", september));
                    }
                }

                if (month == 10)
                {
                    if (day > october)
                    {
                        throw new Exception(string.Format("October has only {0} days.", october));
                    }
                }

                if (month == 11)
                {
                    if (day > november)
                    {
                        throw new Exception(string.Format("November has only {0} days.", november));
                    }
                }

                if (month == 12)
                {
                    if (day > december)
                    {
                        throw new Exception(string.Format("December has only {0} days.", december));
                    }
                }
            }

            var parsedDate = new HistoricalDate
            {
                Day = day,
                Month = month,
                Year = year,
                Era = era
            };

            return parsedDate;
        }

        /// <summary>
        /// Convert Historical Date to string.  Example: "January 1, 2014 AD"
        /// </summary>
        /// <param name="date">HistoricalDate</param>
        /// <returns>Date as string</returns>
        public static string DateToString(HistoricalDate date)
        {
            var day = string.Empty;
            var month = string.Empty;
            var year = date.Year.ToString(CultureInfo.InvariantCulture);
            var era = date.Era.ToString();

            var returnString = string.Empty;

            switch (date.Month)
            {
                case 1:
                    month = "January";
                    break;
                case 2:
                    month = "February";
                    break;
                case 3:
                    month = "March";
                    break;
                case 4:
                    month = "April";
                    break;
                case 5:
                    month = "May";
                    break;
                case 6:
                    month = "June";
                    break;
                case 7:
                    month = "July";
                    break;
                case 8:
                    month = "August";
                    break;
                case 9:
                    month = "September";
                    break;
                case 10:
                    month = "October";
                    break;
                case 11:
                    month = "November";
                    break;
                case 12:
                    month = "December";
                    break;
            }

            if (date.Day > 0)
            {
                day = date.Day.ToString(CultureInfo.InvariantCulture);
            }

            if (!string.IsNullOrEmpty(day) && !string.IsNullOrEmpty(month))
            {
                returnString = string.Format("{0} {1}, {2} {3}", month, day, year, era);
            }

            if (string.IsNullOrEmpty(day) && !string.IsNullOrEmpty(month))
            {
                returnString = string.Format("{0} {1} {2}", month, year, era);
            }

            if (string.IsNullOrEmpty(day) && string.IsNullOrEmpty(month))
            {
                returnString = string.Format("{0} {1}", year, era);
            }

            return returnString;
        }

        /// <summary>
        /// Determine time span between two historical dates
        /// </summary>
        /// <param name="date1">HistoricalDate1</param>
        /// <param name="date2">HistoricalDate2</param>
        /// <returns>HistoricalAge</returns>
        public static HistoricalAge YearsBetweenHistoricalDates(HistoricalDate date1, HistoricalDate date2)
        {
            const int oneMonth = 31;
            const int twoMonth = 59;
            const int threeMonth = 90;
            const int fourMonth = 120;
            const int fiveMonth = 151;
            const int sixMonth = 181;
            const int sevenMonth = 212;
            const int eightMonth = 243;
            const int nineMonth = 273;
            const int tenMonth = 304;
            const int elevenMonth = 334;
            const int twelveMonth = 365;

            var age = new HistoricalAge();

            if (date1.Era == date2.Era)
            {
                var years = date1.Year - date2.Year;

                var numberOfDays = years * 365.2425;

                //Date1 Months/Days
                if (date1.Month == 1)
                {
                    numberOfDays = (numberOfDays + oneMonth) - date1.Day;
                }

                if (date1.Month == 2)
                {
                    numberOfDays = (numberOfDays + twoMonth) - date1.Day;
                }

                if (date1.Month == 3)
                {
                    numberOfDays = (numberOfDays + threeMonth) - date1.Day;
                }

                if (date1.Month == 4)
                {
                    numberOfDays = (numberOfDays + fourMonth) - date1.Day;
                }

                if (date1.Month == 5)
                {
                    numberOfDays = (numberOfDays + fiveMonth) - date1.Day;
                }

                if (date1.Month == 6)
                {
                    numberOfDays = (numberOfDays + sixMonth) - date1.Day;
                }

                if (date1.Month == 7)
                {
                    numberOfDays = (numberOfDays + sevenMonth) - date1.Day;
                }

                if (date1.Month == 8)
                {
                    numberOfDays = (numberOfDays + eightMonth) - date1.Day;
                }

                if (date1.Month == 9)
                {
                    numberOfDays = (numberOfDays + nineMonth) - date1.Day;
                }

                if (date1.Month == 10)
                {
                    numberOfDays = (numberOfDays + tenMonth) - date1.Day;
                }

                if (date1.Month == 11)
                {
                    numberOfDays = (numberOfDays + elevenMonth) - date1.Day;
                }

                if (date1.Month == 12)
                {
                    numberOfDays = (numberOfDays + twelveMonth) - date1.Day;
                }

                //Date2 Months/Days
                if (date2.Month == 1)
                {
                    numberOfDays = (numberOfDays + oneMonth) - date2.Day;
                }

                if (date2.Month == 2)
                {
                    numberOfDays = (numberOfDays + twoMonth) - date2.Day;
                }

                if (date2.Month == 3)
                {
                    numberOfDays = (numberOfDays + threeMonth) - date2.Day;
                }

                if (date2.Month == 4)
                {
                    numberOfDays = (numberOfDays + fourMonth) - date2.Day;
                }

                if (date2.Month == 5)
                {
                    numberOfDays = (numberOfDays + fiveMonth) - date2.Day;
                }

                if (date2.Month == 6)
                {
                    numberOfDays = (numberOfDays + sixMonth) - date2.Day;
                }

                if (date2.Month == 7)
                {
                    numberOfDays = (numberOfDays + sevenMonth) - date2.Day;
                }

                if (date2.Month == 8)
                {
                    numberOfDays = (numberOfDays + eightMonth) - date2.Day;
                }

                if (date2.Month == 9)
                {
                    numberOfDays = (numberOfDays + nineMonth) - date2.Day;
                }

                if (date2.Month == 10)
                {
                    numberOfDays = (numberOfDays + tenMonth) - date2.Day;
                }

                if (date2.Month == 11)
                {
                    numberOfDays = (numberOfDays + elevenMonth) - date2.Day;
                }

                if (date2.Month == 12)
                {
                    numberOfDays = (numberOfDays + twelveMonth) - date2.Day;
                }

                var totalYears = numberOfDays / 365.2425;

                totalYears = Math.Floor(totalYears);

                age.Years = (int)totalYears;
            }
            else
            {
                var date1NumberOfDays = date1.Year * 365.2425;

                //Date1 Months/Days
                if (date1.Month == 1)
                {
                    date1NumberOfDays = (date1NumberOfDays + oneMonth) - date1.Day;
                }

                if (date1.Month == 2)
                {
                    date1NumberOfDays = (date1NumberOfDays + twoMonth) - date1.Day;
                }

                if (date1.Month == 3)
                {
                    date1NumberOfDays = (date1NumberOfDays + threeMonth) - date1.Day;
                }

                if (date1.Month == 4)
                {
                    date1NumberOfDays = (date1NumberOfDays + fourMonth) - date1.Day;
                }

                if (date1.Month == 5)
                {
                    date1NumberOfDays = (date1NumberOfDays + fiveMonth) - date1.Day;
                }

                if (date1.Month == 6)
                {
                    date1NumberOfDays = (date1NumberOfDays + sixMonth) - date1.Day;
                }

                if (date1.Month == 7)
                {
                    date1NumberOfDays = (date1NumberOfDays + sevenMonth) - date1.Day;
                }

                if (date1.Month == 8)
                {
                    date1NumberOfDays = (date1NumberOfDays + eightMonth) - date1.Day;
                }

                if (date1.Month == 9)
                {
                    date1NumberOfDays = (date1NumberOfDays + nineMonth) - date1.Day;
                }

                if (date1.Month == 10)
                {
                    date1NumberOfDays = (date1NumberOfDays + tenMonth) - date1.Day;
                }

                if (date1.Month == 11)
                {
                    date1NumberOfDays = (date1NumberOfDays + elevenMonth) - date1.Day;
                }

                if (date1.Month == 12)
                {
                    date1NumberOfDays = (date1NumberOfDays + twelveMonth) - date1.Day;
                }

                var date2NumberOfDays = date2.Year * 365.2425;

                //Date2 Months/Days
                if (date2.Month == 1)
                {
                    date2NumberOfDays = (date2NumberOfDays + oneMonth) - date2.Day;
                }

                if (date2.Month == 2)
                {
                    date2NumberOfDays = (date2NumberOfDays + twoMonth) - date2.Day;
                }

                if (date2.Month == 3)
                {
                    date2NumberOfDays = (date2NumberOfDays + threeMonth) - date2.Day;
                }

                if (date2.Month == 4)
                {
                    date2NumberOfDays = (date2NumberOfDays + fourMonth) - date2.Day;
                }

                if (date2.Month == 5)
                {
                    date2NumberOfDays = (date2NumberOfDays + fiveMonth) - date2.Day;
                }

                if (date2.Month == 6)
                {
                    date2NumberOfDays = (date2NumberOfDays + sixMonth) - date2.Day;
                }

                if (date2.Month == 7)
                {
                    date2NumberOfDays = (date2NumberOfDays + sevenMonth) - date2.Day;
                }

                if (date2.Month == 8)
                {
                    date2NumberOfDays = (date2NumberOfDays + eightMonth) - date2.Day;
                }

                if (date2.Month == 9)
                {
                    date2NumberOfDays = (date2NumberOfDays + nineMonth) - date2.Day;
                }

                if (date2.Month == 10)
                {
                    date2NumberOfDays = (date2NumberOfDays + tenMonth) - date2.Day;
                }

                if (date2.Month == 11)
                {
                    date2NumberOfDays = (date2NumberOfDays + elevenMonth) - date2.Day;
                }

                if (date2.Month == 12)
                {
                    date2NumberOfDays = (date2NumberOfDays + twelveMonth) - date2.Day;
                }

                var totalDaysBetweenDates = date1NumberOfDays + date2NumberOfDays;

                var totalYears = totalDaysBetweenDates / 365.2425;

                totalYears = Math.Floor(totalYears);

                age.Years = (int)totalYears;
            }

            return age;
        }
    }

    /// <summary>
    /// Enum of era types
    /// </summary>
    public enum Era
    {
        BC,
        AD
    }
}
