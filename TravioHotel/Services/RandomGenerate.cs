using System.Text;

namespace TravioHotel.Services
{
    public class RandomGenerate
    {
        // This Will generate Numeric Random Numbers 
        public string NumberGenerate(int length)
        {
            Random Random = new Random();
            StringBuilder code = new StringBuilder();

            for(int i = 0; i <= length; i++)
            {
                code.Append(Random.Next(10)); // this is not the length its the range of random number to be generated between for exampl 0 to 9

            }
            return code.ToString();
        }
        // This will generate AlphaNumericValues
        public string CharacterGenerate(int length)
        {
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"; // Generate betweeen these Alpha Numeric Characters
            Random Random = new Random();
            StringBuilder code = new StringBuilder();   
            for(int i = 0; i <= length; i++)
            {
                code.Append(Random.Next(characters.Length));
            }
            return code.ToString();

        }
    }
}
