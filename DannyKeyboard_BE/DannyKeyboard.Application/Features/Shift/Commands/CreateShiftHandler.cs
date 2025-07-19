using DannyKeyboard.Application.Mappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.Shift.Commands
{
    public class CreateShiftHandler : IRequestHandler<CreateShiftCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateShiftHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(CreateShiftCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var newShift = ShiftMapper.ToShift(request.Dto);
                if(newShift == null)
                {
                    return false;
                }

                await _unitOfWork.ShiftRepo.Insert(newShift);
                await _unitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                await _unitOfWork.RollbackTransactionAsync();

                return false;
            }
        }
    }
}
