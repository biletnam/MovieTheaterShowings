using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedResources.Interfaces
{
    public interface IMovieMapper
    {
        //Database fields from Movies table:
        int Id { get; set; }
        string Title { get; set; }
        int RunTime { get; set; }
        string Image { get; set; }

        //A list of ShowTimes for this movie:
        List<IShowTimesMapper> ShowTimes { get; set; }
    }
}