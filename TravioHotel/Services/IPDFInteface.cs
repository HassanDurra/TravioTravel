namespace TravioHotel.Services
{
    public interface IPDFInteface
    {
        byte[] GeneratePDF(string htmlContent);
    }
}
