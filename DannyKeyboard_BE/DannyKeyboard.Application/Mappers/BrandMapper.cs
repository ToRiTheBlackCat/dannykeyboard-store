using DannyKeyboard.Application.DTOs.Brand;
using DannyKeyboard.Application.DTOs.Shift;
using DannyKeyboard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Mappers
{
    public static class BrandMapper
    {
        public static Brand ToBrand(this CreateBrandDto dto)
        {
            return new Brand()
            {
                BrandName = dto.BrandName,
                IsActive = dto.IsActive,
            };
        }

        public static Brand ToBrand(this UpdateBrandDto dto, Brand foundBrand)
        {
            foundBrand.BrandId = dto.BrandId;
            foundBrand.BrandName = dto.BrandName;
            foundBrand.IsActive = dto.IsActive;

            return foundBrand;
        }
    }
}
