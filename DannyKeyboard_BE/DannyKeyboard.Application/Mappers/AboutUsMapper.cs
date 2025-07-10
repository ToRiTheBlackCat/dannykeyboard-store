using DannyKeyboard.Application.DTOs.AboutUs;
using DannyKeyboard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DannyKeyboard.Application.Mappers
{
    public static class AboutUsMapper
    {
        public static List<ListAboutUsDto> ToListAboutUsDto(this List<AboutUs> list)
        {
            var listDto = new List<ListAboutUsDto>();

            foreach (var item in list)
            {
                var dto = new ListAboutUsDto
                {
                    AboutUsId = item.AboutUsId,
                    Detail = item.Detail
                };
                listDto.Add(dto);
            }
            return listDto;
        }

        public static AboutUs ToAboutUs(this CreateAboutUsDto dto)
        {
            return new AboutUs()
            {
                Detail = dto.Detail,
                IsActive = true
            };
        }
        public static AboutUs ToAboutUs(this UpdateAboutUsDto dto)
        {
            return new AboutUs()
            {
                AboutUsId = dto.AboutUsId,
                Detail = dto.Detail,
                IsActive = true
            };
        }
    }
}
