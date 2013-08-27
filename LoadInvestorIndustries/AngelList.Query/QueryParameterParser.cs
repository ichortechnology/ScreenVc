using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AngelList.Query
{
    /// <summary>
    /// Parses user ids into ranges of integers to be passed to AngelList queries. 
    /// Restricts the range of ids to configured values of MaxRangeLength and MaxRanges.
    /// </summary>
    public class QueryParameterParser
    {
        int maxRangeLength;
        int maxRanges;

        public int MaxRangeLength { get { return maxRangeLength; } }

        public int MaxRanges { get { return maxRanges; } }

        public QueryParameterParser(int maxRangeLength, int maxRanges)
        {
            this.maxRangeLength = maxRangeLength;
            this.maxRanges = maxRanges;
        }

        /// <summary>
        /// Returns the maximum valid value of the end of a query range. Used to prevent restrict ranges 
        /// to lengths that won't cause "out of resource" exceptions.
        /// </summary>
        /// <param name="start">Start of the range.</param>
        /// <param name="end">Desired end of the range.</param>
        /// <returns>Maximum valid value of the end of a query range</returns>
        public int RestrictIntRange(int start, int end)
        {
            int maxRange = MaxRangeLength * MaxRanges;

            if (end - start >= maxRange)
            {
                return start + maxRange - 1;
            }
            else
            {
                return end;
            }
        }

        public List<List<int>> ParseIntRange(int start, int end)
        {
            // TODO,, Check start and end parameter for negative ints, etc. 

            int numberOfRanges = (end - start) / maxRangeLength + 1;

            if (numberOfRanges < 1)
            {
                throw new ArgumentOutOfRangeException(string.Format("Ranges requested invalid: ranges requested={0} start={1}, end={2}, maxRangeLength={3}, maxRanges={4}.", numberOfRanges, start, end, maxRangeLength, maxRanges));
            }

            if (numberOfRanges > maxRanges)
            {
                throw new ArgumentOutOfRangeException(string.Format("Max number of parameter ranges exceeded: ranges requested={0} start={1}, end={2}, maxRangeLength={3}, maxRanges={4}.", numberOfRanges, start, end, maxRangeLength, maxRanges));
            }

            List<List<int>> parameterGroups = new List<List<int>>();

            for (int pgStart = start; pgStart <= end; pgStart += maxRangeLength)
            {
                var parameterGroup = new List<int>();
                int pgEnd = (end > pgStart + maxRangeLength) ? maxRangeLength + pgStart -1  : end;

                for (int j = pgStart; j <= pgEnd; j++)
                {
                    parameterGroup.Add(j); 
                }
                parameterGroups.Add(parameterGroup);
            }
            return parameterGroups;
        }

        public List<List<int>> ParseIntList(List<int> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }

            int numberOfRanges = list.Count / maxRangeLength + 1;

            if (numberOfRanges > maxRanges)
            {
                throw new ArgumentOutOfRangeException(string.Format("Max number of parameter ranges exceeded: ranges requested={0}, List.Count={1}, maxRangeLength={2}, maxRanges={3}.", numberOfRanges, list.Count, maxRangeLength, maxRanges));
            }

            List<List<int>> parameterGroups = new List<List<int>>();

            for (int iStart = 0; iStart < list.Count; iStart += maxRangeLength)
            {
                var parameterGroup = new List<int>();
                int parameterGroupCount = (list.Count > iStart + maxRangeLength) ? maxRangeLength : list.Count - iStart;

                parameterGroup.AddRange(list.GetRange(iStart, parameterGroupCount));
                parameterGroups.Add(parameterGroup);
            }
            return parameterGroups;
        }
    }
}
