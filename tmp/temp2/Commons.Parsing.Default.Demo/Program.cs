﻿using System;
using System.Collections.Generic;
using System.Globalization;

namespace Commons.Parsing.Default.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Running demo...");

            RunNumberParserDemo();
            RunDateTimeParserDemo();
            RunBooleanParserDemo();
            RunEnumParserDemo();

            Console.WriteLine("Finished. Press any key to continue...");
            Console.ReadKey();
        }

        private static void RunNumberParserDemo()
        {
            // We can set up custom parser by specifying the thousands and decimal separators
            IFormatProvider format = new NumberFormatInfo { NumberGroupSeparator = ".", NumberDecimalSeparator = "," };
            NumberParser parser = new NumberParser(format);
            double result = parser.ParseDouble("5.300.199,20"); // converts to 5300199.20

            // Alternatively we can use predefined parsers which support the common European and American number formats
            double result2 = NumberParser.European.ParseDouble("5.300.199,20"); // converts to 5300199.20
            double result3 = NumberParser.American.ParseDouble("5,300,199.20"); // converts to 5300199.20
        }

        private static void RunDateTimeParserDemo()
        {
            // Create and use datetime parser using specified datetime formats 
            string format = "yyyy-MM-dd";
            DateTimeParser parser = new DateTimeParser(format);
            var result = parser.Parse("1996-04-01");
            var result2 = parser.Parse("2011-12-28");
        }

        private static void RunBooleanParserDemo()
        {
            // Use the default boolean parser
            BooleanParser parser = new BooleanParser();
            var result = parser.Parse("truE"); // converts to true
            var result2 = parser.Parse("FAlse"); // converts to false

            // Alternatively, we can set custom strings for conversion of boolean values
            Dictionary<string, bool> mapping = new Dictionary<string, bool>
            {
                { "Yes", true },
                { "No", false },
                { "Y", true },
                { "N", false }
            };

            var parser2 = new BooleanMapParser(mapping);
            var result3 = parser2.Parse("true"); // converts to true
            var result4 = parser2.Parse("Yes"); // converts to true
            var result5 = parser2.Parse("N"); // converts to false
        }

        enum Seasons { Spring, Summer, Autumn, Winter }

        private static void RunEnumParserDemo()
        {
            bool ignoreCase = true; // set whether case will be ignored when parsing enum
            var parser = new EnumParser<Seasons>(ignoreCase);

            var result1 = parser.Parse("winTER"); // Converts to Seasons.Winter
            var result2 = parser.Parse("summer"); // Converts to Seasons.Summer
        }

        private static void RunEnumStringParserDemo()
        {
            Dictionary<string, Seasons> map = new Dictionary<string, Seasons>
                {
                    { "sppringg", Seasons.Spring },
                    { "Sptring", Seasons.Spring },
                    { "Summer", Seasons.Summer },
                    { "SSS", Seasons.Summer },
                    { "suMMer", Seasons.Summer }
                };

            EnumStringParser<Seasons> parser = new EnumStringParser<Seasons>(map);
            Seasons result1 = parser.Parse("Sptring"); // Converts to Seasons.Spring
            Seasons result2 = parser.Parse("SSS"); // Converts to Seasons.Summer
        }
    }
}
