namespace ConsoleApplication
{
    public class CustomServiceConfig
    {
        public string ApiUrl { get; set; }
        public string TokenUrl { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }

        public override string ToString()
        {
            return $"ApiUrl : {ApiUrl}, TokenUrl : {TokenUrl}, ClientId : {ClientId}, ClientSecret : {ClientSecret}";
        }
    }
}
