namespace ExtracaoLambda.Data.Entities
{
    public class Payload
    {
        public string Sigla { get; set; }
        public string DataInicial { get; set; }
        public string DataFinal { get; set; }
        
        public string[] Tickers { get; set; }
        
        public bool NewsAnalysis { get; set; }
    }
}