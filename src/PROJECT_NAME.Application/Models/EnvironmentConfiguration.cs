namespace PROJECT_NAME.Application.Models
{
    /*
    This file should contain all the keys in the environment variables
    */
    public class EnvironmentConfiguration
    {
        public string ENV { get; set; }
        public string LOG_LEVEL { get; set; }
        public string SERVICE_URL { get; set; }
        public string SQL_CONNECTION_STRING { get; set; }
    }
}
