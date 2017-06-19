using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharedResources.Interfaces;

namespace SharedResources.Mappers
{
    public class ShowTimesMapper : IShowTimesMapper
    {
        public int Id { get; set; }
        public DateTime ShowingDateTime { get; set; }
        public int MovieId { get; set; }
    }
}
