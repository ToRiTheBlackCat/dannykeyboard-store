using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.Shift.Commands
{
    public class DeleteShiftHandler : IRequestHandler<DeleteShiftCommand, (bool, string)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private static string SUCCESS = "Delete shift successfully";
        private static string ERROR = "Error when delete shift";
        private static string NOTFOUND = "Delete fail. Not found any shift with that id";
        public DeleteShiftHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<(bool, string)> Handle(DeleteShiftCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                //Find existed Shift
                var foundShift = await _unitOfWork.ShiftRepo.GetOne(request.ShiftId);
                if (foundShift == null)
                {
                    return (false, NOTFOUND);
                }

                _unitOfWork.ShiftRepo.Delete(foundShift);
                return (true, SUCCESS);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                await _unitOfWork.RollbackTransactionAsync();

                return (false, ERROR);
            }
        }
    }
}
