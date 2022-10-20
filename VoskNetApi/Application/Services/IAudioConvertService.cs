using System.IO;

namespace VoskNetApi.Application.Services
{
    public interface IAudioConvertService
    {
        Stream ConvertToWavFormatForRecognize(Stream stream);
        Stream ConvertToWavStreamForRecognizeFfMpeg(Stream inputStream);

        Stream ConvertToWavStreamForRecognize(Stream stream, string filename);
    }
}