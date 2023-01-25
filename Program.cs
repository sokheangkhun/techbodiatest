using System.Text.RegularExpressions;
using System.Net;

public class Program
{
    public static void Main()
    {
        // TestInFirstInterview();

        // ValidateIpUsingSystemNet();
        // ValidateIpUsingRegularExpression();
        // ValidateIP1();
        ValidateIP2();
    }

    #region IP validation implementation

    /// <summary>
    /// using System.Net.IPAddress to try parse the ip.
    /// </summary>
    public static void ValidateIpUsingSystemNet()
    {
        //get ip string from user input
        var ip = ValidateHeader();
        //try to parse the ip input string
        IPAddress.TryParse(ip, out var result);

        //Output as statements. Uncomment the line below.
        //Console.WriteLine(result is null ? "Ip is invalid." : "Your ip is valid and it's {0}", result);

        //Output a boolean which indicate if it's valid when result isn't null
        Console.WriteLine(result is null);
    }

    /// <summary>
    /// Using System.Text.RegularExpressions.RegEx to match the valid ip with valid ip pattern.
    /// </summary>
    public static void ValidateIpUsingRegularExpression()
    {
        //get ip string from user input
        var ip = ValidateHeader();
        //define regular expression for matching ip pattern
        var regex = new Regex(@"^([0-9]|[1-9][0-9]|[1-2][0-5][0-5])\.([0-9]|[1-9][0-9]|[1-2][0-5][0-5])\.([0-9]|[1-9][0-9]|[1-2][0-5][0-5])\.([0-9]|[1-9][0-9]|[1-2][0-5][0-5])$");

        //Output as statements. Uncomment the line below.
        // Console.WriteLine(regex.IsMatch(ip) ? "Your IP is valid." : "Your IP is invalid");

        //Output a bolean which indicate the ip is valid when regex matched.
        Console.WriteLine(regex.IsMatch(ip));
    }

    /// <summary>
    /// Using String.Split() to validate ip option 1
    /// </summary>
    public static void ValidateIP1()
    {
        //get ip string from user input
        var ip = ValidateHeader();
        //split the ip string into array
        var ipParts = ip.Split(".");
        //define the variable which indicate the ip is valid and use later in the flow control below
        var valid = false;

        foreach (var part in ipParts)
        {
            //check  the array item if it's null or empty. If it is null or empty, the ip string is invalid
            if (string.IsNullOrEmpty(part.Trim()))
            {
                //ip string is invalid here so we assign the valid variable to false
                valid = false;
                //after assiged the valid variable we break the loop because there's no point to loop the next array item because the ip string is invalid
                break;
            }
            //when array item is not null or empty
            else
            {
                //check the text length of the array item 
                switch (part.Length)
                {
                    //if the length is 1 the valid value of the ip is between 0-9
                    case 1:
                        valid = int.TryParse(part, out int n1) && n1 >= 0;
                        break;
                    //if the length is 2 the valid value of the ip is between 0-99 and it cannot start with 0.
                    case 2:
                        valid = part[0] != '0' && int.TryParse(part, out int n2) && n2 >= 0;
                        break;
                    //if the length is 3 the valid value of the ip is between 0-255 and it cannot start with 0 either.
                    case 3:
                        valid = part[0] != '0' && int.TryParse(part, out int n3) && n3 >= 0 && n3 <= 255;
                        break;
                    //other lenght is considered the ip string invalid
                    default:
                        valid = false;
                        break;
                }
                //if during swith there is the possibility which the valid is false whcih indicates that the ip string is invalid, we break the loop.
                if (!valid)
                    break;
            }

            //Output as statements. Uncomment the line below.
            // Console.WriteLine(isValid ? "Your IP is valid." : "Your IP is invalid");

            //Output a boolean.
            Console.WriteLine(valid);
        }
    }

    /// <summary>
    /// Using String.Split() and take avantage of array filter function.
    /// </summary>
    public static void ValidateIP2()
    {
        //get ip string from user input
        var ip = ValidateHeader();
        //split ip string into array
        var ipParts = ip.Split('.');
        //define the ip string validated by using array filter method
        var valid = ipParts.Length == 4 && ipParts.All(p => p.Length <= 3 && p.Length > 0 && (int.TryParse(p, out int n) && n >= 0 && n <= 255));
        //if one of the array item has the text length > 1 and the first character of the array item text is '0' which indicate that the ip string is invalid.
        if (ipParts.Any(p => p.Length > 1 && p[0] == '0'))
        {
            valid = false;
        }

        //Output as statements. Uncomment the line below.
        // Console.WriteLine(valid ? "Your IP is valid." : "Your IP is invalid");

        //Output a bolean
        Console.WriteLine(valid);
    }

    /// <summary>
    /// All validate ip method header
    /// </summary>
    /// <returns></returns>
    public static string ValidateHeader()
    {
        Console.Write("Input IP:");
        //get ip string from user input
        var ip = Console.ReadLine() ?? "";

        //if we want to remove any spaces if user accidentally typed in. Uncomment the line below.
        //ip = ip?.Trim()?.Replace(" ", "");

        return ip;
    }
    #endregion

    //First interview test
    public static void TestInFirstInterview()
    {
        /*
            You are given a string of numbers between 0-9. 
            Find the average of these numbers and return it as a floored whole number (ie: no decimal places) written out as a string.

            Eg: 
            Input -> "four nine five two"
            Output -> "five"
            The calculation will be : (4+9+5+2) divided by how many number in string, in this case is 4 number in string, so divided by 4
            And the result will be (4+9+5+2)/4 = 5, convert into string is five, so the output will be 5
            And please be noted that if the string is empty or includes a number greater than 9, return "n/a"

            Example 1
            Input -> "five four"
            Output -> "four"

            Example 2
            Input -> "zero zero zero zero zero zero zero zero"
            Output -> "zero"

            Example 3
            Input -> "twenty"
            Output -> "n/a"

            Example 4
            Input -> "one one eight one"
            Output -> "two"

            Example 5
            Input -> ""
            Output -> "n/a"	

            Example 6
            Input -> "zero nine five two six"
            Output -> "four"
            */
        var stringOfNumbers = new List<string> { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
        var numbers = new List<double> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        Console.Write("Input string of numbers:");
        var input = Console.ReadLine();
        if (!string.IsNullOrEmpty(input?.Trim()))
        {
            var stringArray = input.Split(" ");
            var sum = 0.0;
            var incorrect = false;
            foreach (var n in stringArray)
            {
                if (!stringOfNumbers.Contains(n))
                {
                    Console.WriteLine("n/a");
                    incorrect = true;
                    break;
                }
                else
                {
                    sum += numbers[stringOfNumbers.IndexOf(n)];
                }
            }
            if (!incorrect)
            {
                var average = sum / Convert.ToDouble(stringArray.Length);
                var floorAverage = Math.Floor(average);
                Console.WriteLine(stringOfNumbers[numbers.IndexOf(floorAverage)]);
            }

        }
        else
        {
            Console.WriteLine("n/a");
        }
    }

}