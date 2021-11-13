using System;
using ExtracaoLambda.Data.Enums;

namespace ExtracaoLambda.Data.Entities
{
    public class Noticia
    {
        public string url { get; set; }
        
        public string titulo { get; set; }
        
        public string corpo { get; set; }

        public DateTime date { get; set; }
        
        public SentimentalEnum analise { get; set; }
        
        public int empresa_id { get; set; }

    }
}