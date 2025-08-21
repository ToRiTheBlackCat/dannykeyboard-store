using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.Brand.Commands
{
    public class DeleteBrandCommand : IRequest<(bool, string)>
    {
        public int BrandId { get; set; }
        public DeleteBrandCommand(int brandId)
        {
            BrandId = brandId;
        }
    }
}
