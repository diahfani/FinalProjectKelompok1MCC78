﻿namespace Client.ViewModels
{
    public class ResponseViewModel<Entity>
    {
        public int Code { get; set; }
        public string StatusCode { get; set; }
        public string Message { get; set; }
        public Entity? Data { get; set; }
        /*        public int Code { get; set; }
                public string Message { get; set; }
                public Entity? Data { get; set; }
        */
    }
}
