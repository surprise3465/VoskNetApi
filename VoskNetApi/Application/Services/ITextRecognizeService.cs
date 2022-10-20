using System.IO;
using System.Threading.Tasks;
using VoskNetApi.Application.Models;

namespace VoskNetApi.Application.Services
{
    public interface ITextRecognizeService
    {
        TextRecognized Recognize(Stream stream, string filename);
    }
}

