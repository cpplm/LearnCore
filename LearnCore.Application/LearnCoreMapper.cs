using AutoMapper;
using LearnCore.Application.MenuApp.Dtos;
using LearnCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearnCore.Application
{
    public class LearnCoreMapper
    {
        public static void Initialize()
        {
            Mapper.Initialize(map =>
            {
                map.CreateMap<Menu, MenuDto>();
                map.CreateMap<MenuDto, Menu>();
            });
        }
    }
}
