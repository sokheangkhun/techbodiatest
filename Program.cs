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
        var ip = ValidateHeader();
        IPAddress.TryParse(ip, out var result);

        //Output as statements. Uncomment the line below.
        //Console.WriteLine(result is null ? "Ip is invalid." : "Your ip is valid and it's {0}", result);

        //Output a boolean
        Console.WriteLine(result is null);
    }

    /// <summary>
    /// Using System.Text.RegularExpressions.RegEx to match the valid ip with valid ip pattern.
    /// </summary>
    public static void ValidateIpUsingRegularExpression()
    {
        var ip = ValidateHeader();
        var regex = new Regex(@"^([0-9]|[1-9][0-9]|[1-2][0-5][0-5])\.([0-9]|[1-9][0-9]|[1-2][0-5][0-5])\.([0-9]|[1-9][0-9]|[1-2][0-5][0-5])\.([0-9]|[1-9][0-9]|[1-2][0-5][0-5])$");

        //Output as statements. Uncomment the line below.
        // Console.WriteLine(regex.IsMatch(ip) ? "Your IP is valid." : "Your IP is invalid");

        //Output a bolean
        Console.WriteLine(regex.IsMatch(ip));
    }

    /// <summary>
    /// Using String.Split() to validate ip option 1
    /// </summary>
    public static void ValidateIP1()
    {
        var ip = ValidateHeader();
        var ipParts = ip.Split(".");

        var valid = false;
        foreach (var part in ipParts)
        {
            if (string.IsNullOrEmpty(part.Trim()))
            {
                valid = false;
                break;
            }
            else
            {
                switch (part.Length)
                {
                    case 1:
                        valid = int.TryParse(part, out int n1) && n1 >= 0;
                        break;
                    case 2:
                        valid = part[0] != '0' && int.TryParse(part, out int n2) && n2 >= 0;
                        break;
                    case 3:
                        valid = part[0] != '0' && int.TryParse(part, out int n3) && n3 >= 0 && n3 <= 255;
                        break;
                    default:
                        valid = false;
                        break;
                }
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
        var ip = ValidateHeader();
        var ipParts = ip.Split('.');
        var valid = ipParts.Length == 4 && ipParts.All(p => p.Length <= 3 && p.Length > 0 && (int.TryParse(p, out int n) && n >= 0 && n <= 255));
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
        var ip = Console.ReadLine() ?? "";

        //if we want to remove any spaces if user accidentally typed in. Uncomment the line below.
        //ip = ip?.Trim()?.Replace(" ", "");

        return ip;
    }
    #endregion

    //First interview test
    public static void ExcericeOne()
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