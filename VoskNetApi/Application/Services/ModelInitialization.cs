using System;
using System.IO;
using Vosk;

namespace VoskNetApi.Application.Services
{
    public static class ModelInitialization
    {
        
         private static Model _textModel = new Model(Path.Join(AppDomain.CurrentDomain.BaseDirectory, "TextModel"));
         private static SpkModel _speakerModel = new SpkModel(Path.Join(AppDomain.CurrentDomain.BaseDirectory, "SpeakerModel"));
         public static Model TextModel => _textModel;
         public static SpkModel SpeakerModel => _speakerModel;
    }
}
