//By: Tommy Dugger & Jared Chavez
//Created: 1/18/2014
//Purpose: To handle dates in the BCE era that are not
//covered in .Nets DateTime class.

using System;
using System.Globalization;

namespace HistoricalDate
{
    /// <summary>
    /// Enum of era types
    /// </summary>
    public enum Era : byte
    {
        BC,
        AD
    }

    public class HistoricalAge
    {
        public int Years { get; set; }
    }

    public struct HistoricalDate
    {
        // -------------------------------------------------------------------------------
        //
        // Const
        //
        // -------------------------------------------------------------------------------
        private const float DaysPerYear = 365.2425f;
        private const int DefaultDay = 1;
        private const int DefaultMonth = 1;
        private const int DefaultYear = 1;
        private const int LeapYear = 2004;
        private const int NonLeapYear = 2005;

        /// <summary>
        /// Max valid value for a HistoricalDate
        /// </summary>
        public static readonly HistoricalDate MaxValue = new HistoricalDate
        {
            Day = 31,
            Month = 12,
            Year = 99999,
            Era = Era.AD
        };

        /// <summary>
        /// Min valid value for a HistoricalDate
        /// </summary>
        public static readonly HistoricalDate MinValue = new HistoricalDate
        {
            Day = 1,
            Month = 1,
            Year = 99999,
            Era = Era.BC
        };

        /// <summary>
        /// Valid date separators
        /// </summary>
        private static readonly string[] DateSeparators = { ",", ".", "-", "/", "\\", " " };        

        /// <summary>
        /// Static mapping of months
        /// </summary>
        private static readonly string[] Months = {
            "January",
            "February",
            "March",
            "April",
            "May",
            "June",
            "July",
            "August",
            "September",
            "October",
            "November",
            "December"
        };

        // -------------------------------------------------------------------------------
        //
        // Fields
        //
        // -------------------------------------------------------------------------------
        public int Year;
        public int Month;
        public int Day;
        public Era Era;

        public static HistoricalDate Now
        {
            get
            {
                return new HistoricalDate
                {
                    Day = DateTime.Now.Day,
                    Month = DateTime.Now.Month,
                    Year = DateTime.Now.Year,
                    Era = Era.AD
                };
            }
        }


        // -------------------------------------------------------------------------------
        //
        // Overrides
        //
        // -------------------------------------------------------------------------------

        /// <summary>
        /// ToString override
        /// </summary>
        /// <returns>A string representation of the HistoricalDate object.</returns>
        public override string ToString()
        {
            var day = string.Empty;
            var month = string.Empty;
            var year = Year.ToString(CultureInfo.InvariantCulture);
            var era = Era.ToString();

            var returnString = string.Empty;

            if (this.Month <= Months.Length)
                month = Months[this.Month - 1];

            if (this.Day > 0)
                day = this.Day.ToString(CultureInfo.InvariantCulture);

            if (!string.IsNullOrEmpty(day) && !string.IsNullOrEmpty(month))
                returnString = string.Format("{0} {1}, {2} {3}", month, day, year, era);


            if (string.IsNullOrEmpty(day) && !string.IsNullOrEmpty(month))
                returnString = string.Format("{0} {1} {2}", month, year, era);


            if (string.IsNullOrEmpty(day) && string.IsNullOrEmpty(month))
                returnString = string.Format("{0} {1}", year, era);

            return returnString;
        }


        // -------------------------------------------------------------------------------
        //
        // Public methods
        //
        // -------------------------------------------------------------------------------

        /// <summary>
        /// Determines if the specified year is a leap year.
        /// </summary>
        /// <returns></returns>
        /// <param name="year">Year to test.</param>
        public static bool IsLeapYear(int year)
        {
            // not divisible by 4? not a leap year
            if (year % 4 != 0)
                return false;

            // divisible by 4 and not divisible by 100? always a leap year
            if (year % 100 != 0)
                return true;

            // divisible by 4 and 100? Only a leap year if also divisible by 400
            if (year % 400 == 0)
                return true;

            // not a leap year, then
            return false;
        }

        /// <summary>
        /// Converts string to HistoricalDateObject.  String must be in the
        /// following format. "day/month/year era" the slash in the string
        /// can be substituted with the follow characters, "-", ".", ",", "\"
        /// </summary>
        /// <param name="date">string</param>
        /// <returns>HistoricalDate</returns>
        public static HistoricalDate Parse(string date)
        {
            // string -> object
            var newObj = ParseInternal(date);

            // validate the object
            string errData;
            if (!TryValidate(newObj, out errData))
                throw new Exception(errData);

            // return the object
            return newObj;
        }

        /// <summary>
        /// Attempts to parse the given date text into a usable HistoricalDate object. If
        /// the date string is parsed successfully, newDate contains the new HistoricalDate object.
        /// If the date string cannot be parsed successfully, newDate contains 
        /// HistoricalDate.MIN_VALUE.
        /// </summary>
        /// <returns><c>true</c>, if parsed successfully, <c>false</c> otherwise.</returns>
        /// <param name="date">Date in strong format.</param>
        /// <param name="newDate">Parsed HistoricalDate object.</param>
        public static bool TryParse(string date, out HistoricalDate newDate)
        {
            // string -> object
            newDate = ParseInternal(date);

            // validate the object
            string errData;
            if (!TryValidate(newDate, out errData))
            {
                // return false and set default value
                newDate = MinValue;
                return false;
            }

            // return true and the parsed object
            return true;
        }

        /// <summary>
        /// Determine time span between two historical dates
        /// </summary>
        /// <param name="date1">HistoricalDate1</param>
        /// <param name="date2">HistoricalDate2</param>
        /// <returns>HistoricalAge</returns>
        public static HistoricalAge YearsBetweenHistoricalDates(HistoricalDate date1, HistoricalDate date2)
        {
            // negate the year if we're BC
            var startYear = (date1.Era == Era.BC) ? -date1.Year : date1.Year;
            var endYear = (date2.Era == Era.BC) ? -date2.Year : date2.Year;

            // Figure out the difference in raw years
            var baseDiff = endYear - startYear;

            // Figure out the difference in the remaining months and days
            int startDays = -date1.Day;
            for (int i = 1; i <= date1.Month; ++i)
                startDays += GetDaysInMonth(i, date1.Year);


            int endDays = -date2.Day;
            for (int i = 12; i >= date2.Month; --i)
                endDays += GetDaysInMonth(i, date2.Year);

            // Increment year if it adds up
            if (startDays + endDays > DaysPerYear)
                baseDiff++;

            // return results
            return new HistoricalAge
            {
                Years = baseDiff
            };
        }


        // -------------------------------------------------------------------------------
        //
        // Private methods
        //
        // -------------------------------------------------------------------------------

        /// <summary>
        /// Gets the days in a particular month, accounting for leap years and eras.
        /// </summary>
        /// <returns>The days in month.</returns>
        /// <param name="month">Month.</param>
        /// <param name="year">Year.</param>
        private static int GetDaysInMonth(int month, int year)
        {
            return DateTime.DaysInMonth(
                IsLeapYear(year) ? LeapYear : NonLeapYear,
                month
            );
        }

        /// <summary>
        /// Internal parsing of string to a HistoricalDate struct. Validation is
        /// left to the various public implementations.
        /// </summary>
        /// <returns>An un-validated HistoricalDate.</returns>
        /// <param name="date">Date in string format.</param>
        private static HistoricalDate ParseInternal(string date)
        {
            var seperatedString = date.Split(
                DateSeparators,
                StringSplitOptions.RemoveEmptyEntries
            );

            var result = new HistoricalDate
            {
                Day = DefaultDay,
                Month = DefaultMonth,
                Year = DefaultYear
            };

            // once
            for (; ; )
            {
                if (seperatedString.Length < 2)
                {
                    return Now;
                }

                if (seperatedString.Length == 2)
                {
                    result.Year = SafeParse(seperatedString[0], DefaultYear);
                    Enum.TryParse(seperatedString[1], true, out result.Era);
                    break;
                }

                if (seperatedString.Length == 3)
                {
                    result.Month = SafeParse(seperatedString[0], DefaultMonth);
                    result.Year = SafeParse(seperatedString[1], DefaultYear);
                    Enum.TryParse(seperatedString[2], true, out result.Era);
                    break;
                }

                if (seperatedString.Length > 3)
                {
                    result.Day = SafeParse(seperatedString[0], DefaultDay);
                    result.Month = SafeParse(seperatedString[1], DefaultMonth);
                    result.Year = SafeParse(seperatedString[2], DefaultYear);
                    Enum.TryParse(seperatedString[3], true, out result.Era);
                    break;
                }

                throw new Exception("Infinite for(;;) loop. Should never get here!");
            }

            return result;
        }

        /// <summary>
        /// Helper function to safely parse data into ints while providing
        /// fallback default values.
        /// </summary>
        /// <returns>The parsed int, or the provided default if the parse was
        /// unsuccessful.</returns>
        /// <param name="data">Data in string format.</param>
        /// <param name="defaultVal">Default value to use if parsing fails.</param>
        private static int SafeParse(string data, int defaultVal)
        {
            int tempVal;

            if (int.TryParse(data, out tempVal))
                return tempVal;

            return defaultVal;
        }

        /// <summary>
        /// Tries to validate the values in a HistoricalDate object and returns true if all
        /// values are valid. Returns false and sets the errData output with the relevant
        /// error text if not.
        /// </summary>
        /// <returns><c>true</c>, if validated, otherwise <c>false</c></returns>
        /// <param name="date">Test Date.</param>
        /// <param name="errData">Error data, if invalid Date.</param>
        private static bool TryValidate(HistoricalDate date, out string errData)
        {
            if (date.Month < 1 || date.Month > 12)
            {
                errData = string.Format(
                    "Month is out of range! {0} < 1 || > 12",
                    date.Month
                );

                return false;
            }

            if (date.Day < 1 || date.Day > GetDaysInMonth(date.Month, date.Year))
            {
                errData = string.Format(
                    "Day is out of range! Month {0} can not be greater than {1}",
                    date.Month,
                    GetDaysInMonth(date.Month, date.Year)
                );

                return false;
            }

            errData = string.Empty;
            return true;
        }
    }
}
