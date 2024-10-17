using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace Phonebook
{
    internal class ValidationHandler
    {
        //
        // Summary:
        //     Checks If All The Input Fields
        //     Contain A Value And Are Not Empty
        //
        // Parameters:
        //   fields:
        //     Array Of Strings To Be Validated
        public bool AllFieldsCompleted(params String[] fields)
        {
            for (int i = 0; i < fields.Length; i++)
            {
                if (fields[i] == "") { return false; }
            }
            return true;
        }

        //
        // Summary:
        //     Checks That The Entered Data Is
        //     In The Correct Data Type
        //
        // Parameters:
        //   inputs:
        //     A Dictionary Where Each Key's Value Pair
        //     Specifies The Data Type The Key Should Be
        //     e.g. ("int", "str")
        public bool ValidateInputTypes(Dictionary<String, String> inputs)
        {
            for (int i = 0; i < inputs.Count; i++)
            {
                String key = inputs.ElementAt(i).Key;
                String value = inputs.ElementAt(i).Value;

                switch (value)
                {
                    case "str":
                        if (!Regex.IsMatch(key.Trim(), @"^[a-zA-Z]+$")) { return false; };
                        break;
                    case "int":
                        if (!Regex.IsMatch(key.Trim(), @"^\d+$")) { return false; };
                        break;
                    default:
                        return false;
                }
            }

            return true;
        }
    }
}
