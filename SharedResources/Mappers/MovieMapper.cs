using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharedResources.Interfaces;

namespace SharedResources.Mappers
{
    public class MovieMapper : IMovieMapper
    {
        //Database fields from Movies table:
        public int Id { get; set; }
        public string Title { get; set; }
        public int RunTime { get; set; }
        public string Image { get; set; }

        //A list of ShowTimes for this movie:
        public List<IShowTimesMapper> ShowTimes { get; set; }
    }
}
