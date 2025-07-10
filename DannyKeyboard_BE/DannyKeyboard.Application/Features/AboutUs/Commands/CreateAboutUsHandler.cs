using DannyKeyboard.Application.DTOs;
using DannyKeyboard.Application.Mappers;
using DannyKeyboard.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Features.AboutUs.Commands
{
    public class CreateAboutUsHandler : IRequestHandler<CreateAboutUsCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateAboutUsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(CreateAboutUsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var newItem = AboutUsMapper.ToAboutUs(request.Dto);
                await _unitOfWork.AboutUsRepo.Insert(newItem);
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
