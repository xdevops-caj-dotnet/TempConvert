using System.Threading.Tasks;

namespace TempConvert.Interfaces
{
    public interface ITempConvertRepository
    {

        Task<string> FahrenheitToCelsiusAsync(string fahrenheit);

        Task<string> CelsiusToFahrenheitAsync(string celsius);

    }
}