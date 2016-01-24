using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaGad.Api
{
    public class DisplayItem: AbstractDisplayItem<int,string>
    {
        public DisplayItem(int id, string value) : base(id, value)
        {
        }
    }
}
