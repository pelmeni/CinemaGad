using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaGad.Api
{
    public abstract class AbstractDisplayItem<TU,TV>
    {
        protected AbstractDisplayItem(TU id, TV value)
        {
            Id = id;
            Text = value;
        }
        public TU Id { get; set; }
        public TV Text { get; set; }
    }
}
