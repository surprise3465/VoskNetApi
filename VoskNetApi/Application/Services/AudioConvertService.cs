using System;
using System.IO;
using FFMpegCore;
using FFMpegCore.Pipes;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using VoskNetApi.Application.Helpers;

namespace VoskNetApi.Application.Services
{
    public class AudioConvertService : IAudioConvertService
    {
        public AudioConvertService()
        {
            var os = OSUtils.GetOperatingSystem().ToString();
            var ffmpegDirectory = os switch
            {
                "LINUX" => "/usr/bin",
                "WINDOWS" => Path.Combine(Directory.GetCurrentDirectory(),
                    "FFmpeg"),
                _ => "/usr/bin"
            };

            GlobalFFOptions.Configure(options => options.BinaryFolder = ffmpegDirectory);
        }

        public Stream ConvertToWavFormatForRecognize(Stream stream)
        {
            stream.Position = 0;
            var outStream = new MemoryStream();
            var waveSource = new WaveFileReader(stream);
            var resampler = new WdlResamplingSampleProvider(waveSource.ToSampleProvider(), 16000);
            var monoSource = resampler.ToMono(1f, 1f).ToWaveProvider16();
            WaveFileWriter.WriteWavFileToStream(outStream, monoSource);

            return outStream;
        }

        public Stream ConvertToWavStreamForRecognizeFfMpeg(Stream inputStream)
        {
            inputStream.Flush();

            inputStream.Seek(0, SeekOrigin.Begin);
            //https://github.com/rosenbjerg/FFMpegCore/issues/112
            var outputStream = new MemoryStream();
            FFMpegArguments
                .FromPipeInput(new StreamPipeSource(inputStream))
                .OutputToPipe(new StreamPipeSink(outputStream), options => options
                    .ForceFormat("s16le -ac 1 -ar 16000 -f wav")
                )
                .ProcessSynchronously();

            var bytesCount = BitConverter.GetBytes((int)outputStream.Length);

            outputStream.Position = 4;
            outputStream.WriteByte(bytesCount[0]);
            outputStream.Position = 5;
            outputStream.WriteByte(bytesCount[1]);
            outputStream.Position = 6;
            outputStream.WriteByte(bytesCount[2]);
            outputStream.Position = 7;
            outputStream.WriteByte(bytesCount[3]);

            outputStream.Position = 74;
            outputStream.WriteByte(bytesCount[0]);
            outputStream.Position = 75;
            outputStream.WriteByte(bytesCount[1]);
            outputStream.Position = 76;
            outputStream.WriteByte(bytesCount[2]);
            outputStream.Position = 77;
            outputStream.WriteByte(bytesCount[3]);

            return outputStream;
        }

        public Stream ConvertToWavStreamForRecognize(Stream stream, string filename)
        {
            var extension = Path.GetExtension(filename);
            return extension.ToLower() switch
            {
                ".mp3" => ConvertToWavStreamForRecognizeFfMpeg(stream),
                ".wav" => ConvertToWavFormatForRecognize(stream),
                _ => ConvertToWavStreamForRecognizeFfMpeg(stream)
            };
        }
    }
}
