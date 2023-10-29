using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace WildLifeApp;

public class Helpers
{
   


    public static mv[] csv2moves(string whereItIs)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = false
        };
        using var reader = new StreamReader(whereItIs);
        using var csv = new CsvReader(reader, config);
        return csv.GetRecords<mv>().ToArray();
    }

    public static tr[] csv2trait(string whereItIs)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = false
        };
        using var reader = new StreamReader(whereItIs);
        using var csv = new CsvReader(reader, config);
        return csv.GetRecords<tr>().ToArray();
    }

    public class mv
    {
        [Index(0)] public string move { get; set; }
        [Index(1)] public string how_often { get; set; }
        [Index(2)] public string effect { get; set; }
    }

    public class ab
    {
        [Index(0)] public string ability { get; set; }
        [Index(1)] public string effect { get; set; }
    }

    public class tr
    {
        [Index(0)] public string Name { get; set; }
        [Index(1)] public string effect { get; set; }
        [Index(2)] public string Requirement { get; set; }
    }


    public static int StatToInt(int stat, double? nerf = null)
    {
        var calc = stat;
        if (nerf.HasValue)
            calc = Convert.ToInt32(Math.Ceiling(nerf.Value * calc));
        return Convert.ToInt32(calc * ((double)18 / 200));
    }


    public static string? StrengthToDice(int? strength, double? nerf = null)
    {
        if (!strength.HasValue) return null;

        var calc = strength.Value / 10;
        if (nerf.HasValue)
            calc = Convert.ToInt32(Math.Ceiling(nerf.Value * calc));

        switch (calc)
        {
            case <= 3:
                return "1d4";
            case 4:
                return "1d6";
            case 5:
                return "1d8";
            case 6:
                return "1d10";
            case 7:
                return "2d6";
            case 8:
                return "3d4";
            case 9:
                return "2d8";
            case 10:
                return "4d4";
            case 11:
                return "2d10";
            case 12:
                return "2d10";
            case 13:
                return "2d12";
            case 14:
                return "4d6";
            case 15:
                return "4d6";
            case 16:
                return "4d6";
            case 17:
                return "4d6";
            case 18:
                return "4d8";
            case 19:
                return "4d8";
            case 20:
                return "4d8";
            case 21:
                return "2d20";
            case 22:
                return "4d10";
            case 23:
                return "4d10";
            case 24:
                return "4d10";
            case 25:
                return "4d10";
            default:
                return "1d2";
        }
    }

    public static string accuracy_string(int? accuracy)
    {
        switch (accuracy)
        {
            case 30:
                return "Roll an 9 or higher on a 2d6 for this move to succeed. ";
            case 50:
                return "Roll an 5 or lower on a d10 for this move to succeed. ";
            case 55:
                return "Roll an 7 or higher on a 2d6 for this move to succeed. ";
            case 60:
                return "Roll an 6 or lower on a d10 for this move to succeed. ";
            case 70:
                return "Roll an 6 or lower on a d10 for this move to succeed. ";
            case 75:
                return "Roll an 3 or lower on a d4 for this move to succeed. ";
            case 80:
                return "Roll an 8 or lower on a d10 for this move to succeed. ";
            case 85:
                return "Roll an 10 or lower on a 2d6 for this move to succeed. ";
            case 90:
                return "If you roll an 1 on a d10, this move fails. ";
            case 95:
                return "If you roll an 2 on a 2d6, this move fails. ";
            default:
                return "";
        }
    }
}