using MyMediaCollection.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace MyMediaCollection.Model
{
    public class Medium
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ItemType MediaType { get; set; }
    }

    public class MediaItem
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ItemType MediaType { get; set; }
        [Computed]
        public Medium MediumInfo { get; set; }
        public LocationType Location { get; set; }
        public int MediumId => MediumInfo.Id;
    }
}
