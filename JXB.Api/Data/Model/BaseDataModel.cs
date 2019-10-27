using System;

namespace JXB.Api.Data.Model
{
    public class BaseDataModel
    {
        public string Id { get; set; }

        public BaseDataModel()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}