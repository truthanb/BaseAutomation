namespace BaseAutomation.framework
{
    public class Configuration
    {
        //This class is used to deserialize the configuration.json file into an object. Find the related configuration.json file in the resources/date folder. 
        //You can add key value pairs to the configuation.json file, but make sure to account for them here . 
        public string browser { get; set; }
        public int time_out_duration { get; set; }
        public string base_url { get; set; }
    }
}