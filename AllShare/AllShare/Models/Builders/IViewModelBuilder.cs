using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllShare.Models.Builders
{
    public interface IViewModelBuilder<T>
    {
        Task<T> Build();
    }
}
