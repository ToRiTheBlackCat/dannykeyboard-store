using DannyKeyboard.Application.Mappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.Shift.Commands
{
    public class UpdateShiftHandler : IRequestHandler<UpdateShiftCommand, (bool, string)>
    {
        private readonly IUnitOfWork _unitOfWork;

        private static string SUCCESS = "Update shift successfully";
        private static string ERROR = "Error when update shift";
        private static string NOTFOUND = "Update fail. Not found any shift with that id";


        public UpdateShiftHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<(bool, string)> Handle(UpdateShiftCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                //Find existed shift
                var foundShift = await _unitOfWork.ShiftRepo.GetOne(request.Dto.ShiftId);
                if (foundShift == null)
                {
                    return (false, NOTFOUND);
                }

                var updateShift = ShiftMapper.ToShift(request.Dto);

                _unitOfWork.ShiftRepo.Update(updateShift);
                await _unitOfWork.CommitTransactionAsync();

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
