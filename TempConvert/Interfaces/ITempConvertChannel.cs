using System.ServiceModel;

namespace TempConvert.Interfaces
{
    public interface ITempConvertChannel : ITempConvert, IClientChannel
    {
    }
}