using System.Globalization;

namespace FitrahWeb.Helper;

public static class Convertion
{
    public static string ConvertToRupiah(decimal money)
    {
        string rupiah = money.ToString("C0", CultureInfo.CreateSpecificCulture("id-ID"));
        return rupiah;
    }
    public static string ConvertToRupiahTwoZeros(decimal money)
    {
        string rupiah = money.ToString("C2", CultureInfo.CreateSpecificCulture("id-ID"));
        return rupiah;
    }
    public static string ConvertToRupiahTwoZeros(decimal? money)
    {
        string rupiah = money?.ToString("C2", CultureInfo.CreateSpecificCulture("id-ID"))!;
        return rupiah;
    }
    public static string ConvertToIndonesianDate(DateTime date)
    {
        string idDate = date.ToString("dd MMMM yyyy",CultureInfo.CreateSpecificCulture("id-ID"));
        return idDate;
    }
    private static (int,string) GetValues()
    {
        return(10,"Hello");
    }
}
