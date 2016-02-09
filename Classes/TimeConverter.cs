using System;

namespace BerlinClock
{
    public class TimeConverter : ITimeConverter
    {
        public string convertTime(string aTime)
        {
            DateTime time;
            if (DateTime.TryParse(aTime, out time))
            {
                var lines = new string[5];

                /// On the top of the clock there is a yellow lamp that blinks on/off every two seconds
                lines[0] = (time.Second % 2) == 0 ? "Y" : "O";

                /// The top two rows of lamps are red. These indicate the hours of a day. 
                /// In the top row there are 4 red lamps. Every lamp represents 5 hours. 
                lines[1] = GetRowWithLamps(rowLenght: 4, color: 'R', numberOfSwitchedOn: time.Hour / 5);

                /// In the lower row of red lamps every lamp represents 1 hour.
                lines[2] = GetRowWithLamps(rowLenght: 4, color: 'R', numberOfSwitchedOn: time.Hour % 5);

                /// The two rows of lamps at the bottom count the minutes.
                /// The first of these rows has 11 lamps. Every lamp represents 5 minutes.
                /// The 3rd, 6th and 9th lamp are red and indicate quarters.
                lines[3] = GetRowWithLamps(rowLenght: 11, color: 'Y', numberOfSwitchedOn: time.Minute / 5, withQuaters: true);

                //// In the last row with 4 lamps every lamp represents 1 minute
                lines[4] = GetRowWithLamps(rowLenght: 4, color: 'Y', numberOfSwitchedOn: time.Minute % 5);

                var result = string.Join("\r\n", lines);
                return result;
            }

            /// when time string is not correct, then show time for 24:00
            return "Y\r\nRRRR\r\nRRRR\r\nOOOOOOOOOOO\r\nOOOO";
        }

        private string GetRowWithLamps(int rowLenght, int numberOfSwitchedOn, char color, bool withQuaters = false)
        {
            string result = string.Empty;
            if (withQuaters)
            {
                /// add full quaters
                /// the 3rd, 6th and 9th lamp are red and indicate quarters
                for (int i = 0; i < numberOfSwitchedOn/3; i++)
                {
                    result += new string(color, 2) + "R";
                }

                // add the rest switched on lamps
                result += new string(color, numberOfSwitchedOn % 3);
            }
            else
            {
                /// add switched on lamps
                result += new string(color, numberOfSwitchedOn);
            }

            // add switched off lamps
            result += new string('O', rowLenght - numberOfSwitchedOn);

            return result;
        }
    }
}
